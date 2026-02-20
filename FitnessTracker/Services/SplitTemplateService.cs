using FitnessTracker.DTOs;
using FitnessTracker.Enums;

namespace FitnessTracker.Services;

/// <summary>
/// Gera sugestão automática de dias de treino, grupos musculares por dia e frequência semanal ideal
/// para cada tipo de divisão (PushPullLegs, UpperLower, BroSplit, Personalizado).
/// </summary>
public class SplitTemplateService : ISplitTemplateService
{
    public Task<IEnumerable<SplitTemplateDto>> GetTemplatesAsync()
    {
        var templates = new List<SplitTemplateDto>
        {
            BuildPushPullLegs(),
            BuildUpperLower(),
            BuildBroSplit(),
            BuildPersonalizado()
        };
        return Task.FromResult<IEnumerable<SplitTemplateDto>>(templates);
    }

    /// <summary>
    /// Push/Pull/Legs: 3 dias padrão, 3-6x/semana (ciclo de 3 dias repetido).
    /// Dia 1 – Peito, Ombro, Tríceps (Push)
    /// Dia 2 – Costas, Bíceps (Pull)
    /// Dia 3 – Pernas
    /// </summary>
    private static SplitTemplateDto BuildPushPullLegs()
    {
        return new SplitTemplateDto
        {
            Id = nameof(TrainingSplitType.PushPullLegs),
            Nome = "Push / Pull / Legs",
            DivisaoTreino = "PushPullLegs",
            FrequenciaSemanalSugerida = 6, // 2x cada em uma semana
            Dias = new List<SplitDayDto>
            {
                new() { DiaNumero = 1, NomeDia = "Dia 1 - Push", GruposMusculares = new List<string> { "Peito", "Ombro", "Tríceps" } },
                new() { DiaNumero = 2, NomeDia = "Dia 2 - Pull", GruposMusculares = new List<string> { "Costas", "Bíceps" } },
                new() { DiaNumero = 3, NomeDia = "Dia 3 - Legs", GruposMusculares = new List<string> { "Quadríceps", "Posterior", "Glúteos", "Panturrilha" } }
            }
        };
    }

    /// <summary>
    /// Upper/Lower: 2 dias padrão, 4x/semana (2x superior, 2x inferior).
    /// Dia 1 – Superior (peito, costas, ombro, braços)
    /// Dia 2 – Inferior (pernas)
    /// </summary>
    private static SplitTemplateDto BuildUpperLower()
    {
        return new SplitTemplateDto
        {
            Id = nameof(TrainingSplitType.UpperLower),
            Nome = "Superior / Inferior",
            DivisaoTreino = "UpperLower",
            FrequenciaSemanalSugerida = 4,
            Dias = new List<SplitDayDto>
            {
                new() { DiaNumero = 1, NomeDia = "Dia 1 - Superior", GruposMusculares = new List<string> { "Peito", "Costas", "Ombro", "Tríceps", "Bíceps" } },
                new() { DiaNumero = 2, NomeDia = "Dia 2 - Inferior", GruposMusculares = new List<string> { "Quadríceps", "Posterior", "Glúteos", "Panturrilha" } }
            }
        };
    }

    /// <summary>
    /// Bro Split: 5 dias, 5x/semana (um grupo por dia).
    /// Dia 1 – Peito | Dia 2 – Costas | Dia 3 – Pernas | Dia 4 – Ombro | Dia 5 – Braços
    /// </summary>
    private static SplitTemplateDto BuildBroSplit()
    {
        return new SplitTemplateDto
        {
            Id = nameof(TrainingSplitType.BroSplit),
            Nome = "Bro Split (5 dias)",
            DivisaoTreino = "BroSplit",
            FrequenciaSemanalSugerida = 5,
            Dias = new List<SplitDayDto>
            {
                new() { DiaNumero = 1, NomeDia = "Dia 1 - Peito", GruposMusculares = new List<string> { "Peito" } },
                new() { DiaNumero = 2, NomeDia = "Dia 2 - Costas", GruposMusculares = new List<string> { "Costas" } },
                new() { DiaNumero = 3, NomeDia = "Dia 3 - Pernas", GruposMusculares = new List<string> { "Quadríceps", "Posterior", "Glúteos", "Panturrilha" } },
                new() { DiaNumero = 4, NomeDia = "Dia 4 - Ombro", GruposMusculares = new List<string> { "Ombro" } },
                new() { DiaNumero = 5, NomeDia = "Dia 5 - Braços", GruposMusculares = new List<string> { "Tríceps", "Bíceps" } }
            }
        };
    }

    /// <summary>
    /// Personalizado: usuário define dias e grupos; sugestão 3-5x/semana.
    /// </summary>
    private static SplitTemplateDto BuildPersonalizado()
    {
        return new SplitTemplateDto
        {
            Id = nameof(TrainingSplitType.Personalizado),
            Nome = "Personalizado",
            DivisaoTreino = "Personalizado",
            FrequenciaSemanalSugerida = 4,
            Dias = new List<SplitDayDto>
            {
                new() { DiaNumero = 1, NomeDia = "Dia 1", GruposMusculares = new List<string> { "Defina os grupos" } }
            }
        };
    }
}
