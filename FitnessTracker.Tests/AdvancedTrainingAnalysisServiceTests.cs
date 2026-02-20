using FitnessTracker.DTOs;
using FitnessTracker.Models;
using FitnessTracker.Repositories;
using FitnessTracker.Services;
using Moq;
using Xunit;

namespace FitnessTracker.Tests;

public class AdvancedTrainingAnalysisServiceTests
{
    private static Exercise CreateExercise(string id, string grupo, int fadiga = 3, string nivel = "Iniciante")
    {
        return new Exercise
        {
            Id = id,
            Nome = $"Ex {id}",
            GrupoMuscularPrincipal = grupo,
            GruposMuscularesSecundarios = new List<string>(),
            TipoMovimento = "Empurrar",
            PlanoMovimento = "Sagital",
            Multiarticulado = true,
            NivelDificuldade = nivel,
            Equipamento = "Barra",
            PadraoMovimento = $"Padrao-{id}",
            FatorFadiga = fadiga,
            SimilaridadeGrupo = $"sim-{id}"
        };
    }

    [Fact]
    public async Task AnalyzeAsync_TreinoNotFound_ReturnsNull()
    {
        var treinoRepo = new Mock<ITreinoRepository>();
        treinoRepo.Setup(r => r.GetByIdAsync(It.IsAny<string>())).ReturnsAsync((Treino?)null);
        var exerciseRepo = new Mock<IExerciseRepository>();
        var userRepo = new Mock<IUsuarioRepository>();
        var service = new AdvancedTrainingAnalysisService(treinoRepo.Object, exerciseRepo.Object, userRepo.Object);

        var result = await service.AnalyzeAsync("id", "userId");
        Assert.Null(result);
    }

    [Fact]
    public async Task AnalyzeAsync_TreinoEmpty_ReturnsZeroScore()
    {
        var treino = new Treino
        {
            Id = "t1",
            UsuarioId = "u1",
            Nome = "T",
            GrupoMuscularFoco = "Peito",
            DivisaoTreino = "PushPullLegs",
            ListaExercicios = new List<TreinoExercicioItem>(),
            DataCriacao = DateTime.UtcNow
        };
        var treinoRepo = new Mock<ITreinoRepository>();
        treinoRepo.Setup(r => r.GetByIdAsync("t1")).ReturnsAsync(treino);
        var exerciseRepo = new Mock<IExerciseRepository>();
        var userRepo = new Mock<IUsuarioRepository>();
        var service = new AdvancedTrainingAnalysisService(treinoRepo.Object, exerciseRepo.Object, userRepo.Object);

        var result = await service.AnalyzeAsync("t1", "u1");
        Assert.NotNull(result);
        Assert.Equal(0, result.Score);
        Assert.Equal("Ruim", result.Classificacao);
    }

    [Fact]
    public async Task AnalyzeAsync_ValidTreino_ScoreNeverExceeds100()
    {
        var ex = CreateExercise("e1", "Peito", 1, "Iniciante");
        var treino = new Treino
        {
            Id = "t1",
            UsuarioId = "u1",
            Nome = "T",
            GrupoMuscularFoco = "Peito",
            DivisaoTreino = "PushPullLegs",
            ListaExercicios = new List<TreinoExercicioItem> { new() { ExerciseId = "e1", Series = 4 } },
            DataCriacao = DateTime.UtcNow
        };
        var user = new Usuario { Id = "u1", Nivel = "Iniciante" };
        var treinoRepo = new Mock<ITreinoRepository>();
        treinoRepo.Setup(r => r.GetByIdAsync("t1")).ReturnsAsync(treino);
        var exerciseRepo = new Mock<IExerciseRepository>();
        exerciseRepo.Setup(r => r.GetByIdsAsync(It.IsAny<IEnumerable<string>>())).ReturnsAsync(new[] { ex });
        var userRepo = new Mock<IUsuarioRepository>();
        userRepo.Setup(r => r.GetByIdAsync("u1")).ReturnsAsync(user);
        var service = new AdvancedTrainingAnalysisService(treinoRepo.Object, exerciseRepo.Object, userRepo.Object);

        var result = await service.AnalyzeAsync("t1", "u1");
        Assert.NotNull(result);
        Assert.True(result.Score <= 100, "Score must not exceed 100");
    }

    [Fact]
    public async Task AnalyzeAsync_ValidTreino_ScoreNeverNegative()
    {
        var ex = CreateExercise("e1", "Peito", 5, "Avançado");
        var treino = new Treino
        {
            Id = "t1",
            UsuarioId = "u1",
            Nome = "T",
            GrupoMuscularFoco = "Peito",
            DivisaoTreino = "BroSplit",
            ListaExercicios = new List<TreinoExercicioItem>
            {
                new() { ExerciseId = "e1", Series = 5 },
                new() { ExerciseId = "e1", Series = 5 }
            },
            DataCriacao = DateTime.UtcNow
        };
        var user = new Usuario { Id = "u1", Nivel = "Iniciante" };
        var treinoRepo = new Mock<ITreinoRepository>();
        treinoRepo.Setup(r => r.GetByIdAsync("t1")).ReturnsAsync(treino);
        var exerciseRepo = new Mock<IExerciseRepository>();
        exerciseRepo.Setup(r => r.GetByIdsAsync(It.IsAny<IEnumerable<string>>())).ReturnsAsync(new[] { ex });
        var userRepo = new Mock<IUsuarioRepository>();
        userRepo.Setup(r => r.GetByIdAsync("u1")).ReturnsAsync(user);
        var service = new AdvancedTrainingAnalysisService(treinoRepo.Object, exerciseRepo.Object, userRepo.Object);

        var result = await service.AnalyzeAsync("t1", "u1");
        Assert.NotNull(result);
        Assert.True(result.Score >= 0, "Score must not be negative");
    }

    [Fact]
    public async Task AnalyzeAsync_ReturnsClassification()
    {
        var ex = CreateExercise("e1", "Peito", 2, "Iniciante");
        var treino = new Treino
        {
            Id = "t1",
            UsuarioId = "u1",
            Nome = "T",
            GrupoMuscularFoco = "Peito",
            DivisaoTreino = "PushPullLegs",
            ListaExercicios = new List<TreinoExercicioItem> { new() { ExerciseId = "e1", Series = 4 } },
            DataCriacao = DateTime.UtcNow
        };
        var user = new Usuario { Id = "u1", Nivel = "Iniciante" };
        var treinoRepo = new Mock<ITreinoRepository>();
        treinoRepo.Setup(r => r.GetByIdAsync("t1")).ReturnsAsync(treino);
        var exerciseRepo = new Mock<IExerciseRepository>();
        exerciseRepo.Setup(r => r.GetByIdsAsync(It.IsAny<IEnumerable<string>>())).ReturnsAsync(new[] { ex });
        var userRepo = new Mock<IUsuarioRepository>();
        userRepo.Setup(r => r.GetByIdAsync("u1")).ReturnsAsync(user);
        var service = new AdvancedTrainingAnalysisService(treinoRepo.Object, exerciseRepo.Object, userRepo.Object);

        var result = await service.AnalyzeAsync("t1", "u1");
        Assert.NotNull(result);
        Assert.Contains(result.Classificacao, new[] { "Excelente", "Bom", "Regular", "Ruim" });
    }
}
