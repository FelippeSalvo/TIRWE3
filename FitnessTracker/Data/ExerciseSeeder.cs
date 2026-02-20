using FitnessTracker.Models;

namespace FitnessTracker.Data;

/// <summary>
/// Popula a collection Exercises com exercícios pré-definidos se estiver vazia.
/// </summary>
public static class ExerciseSeeder
{
    public const string CollectionName = "Exercises";

    public static List<Exercise> GetSeedExercises()
    {
        return new List<Exercise>
        {
            // ---- PEITO ----
            new Exercise
            {
                Nome = "Supino reto com barra",
                GrupoMuscularPrincipal = "Peito",
                GruposMuscularesSecundarios = new List<string> { "Tríceps", "Ombro" },
                TipoMovimento = "Empurrar",
                PlanoMovimento = "Transversal",
                Multiarticulado = true,
                NivelDificuldade = "Intermediário",
                Equipamento = "Barra",
                PadraoMovimento = "Supino horizontal",
                FatorFadiga = 4,
                SimilaridadeGrupo = "supino-horizontal"
            },
            new Exercise
            {
                Nome = "Supino inclinado com halter",
                GrupoMuscularPrincipal = "Peito",
                GruposMuscularesSecundarios = new List<string> { "Tríceps", "Ombro" },
                TipoMovimento = "Empurrar",
                PlanoMovimento = "Transversal",
                Multiarticulado = true,
                NivelDificuldade = "Intermediário",
                Equipamento = "Halter",
                PadraoMovimento = "Supino inclinado",
                FatorFadiga = 4,
                SimilaridadeGrupo = "supino-inclinado"
            },
            new Exercise
            {
                Nome = "Supino declinado com barra",
                GrupoMuscularPrincipal = "Peito",
                GruposMuscularesSecundarios = new List<string> { "Tríceps" },
                TipoMovimento = "Empurrar",
                PlanoMovimento = "Transversal",
                Multiarticulado = true,
                NivelDificuldade = "Intermediário",
                Equipamento = "Barra",
                PadraoMovimento = "Supino declinado",
                FatorFadiga = 4,
                SimilaridadeGrupo = "supino-declinado"
            },
            new Exercise
            {
                Nome = "Crucifixo com halter",
                GrupoMuscularPrincipal = "Peito",
                GruposMuscularesSecundarios = new List<string>(),
                TipoMovimento = "Isolado",
                PlanoMovimento = "Transversal",
                Multiarticulado = false,
                NivelDificuldade = "Iniciante",
                Equipamento = "Halter",
                PadraoMovimento = "Crucifixo",
                FatorFadiga = 2,
                SimilaridadeGrupo = "crucifixo"
            },
            new Exercise
            {
                Nome = "Flexão de braço",
                GrupoMuscularPrincipal = "Peito",
                GruposMuscularesSecundarios = new List<string> { "Tríceps", "Ombro" },
                TipoMovimento = "Empurrar",
                PlanoMovimento = "Sagital",
                Multiarticulado = true,
                NivelDificuldade = "Iniciante",
                Equipamento = "Peso corporal",
                PadraoMovimento = "Supino horizontal",
                FatorFadiga = 3,
                SimilaridadeGrupo = "supino-horizontal"
            },
            new Exercise
            {
                Nome = "Cross over (cabo)",
                GrupoMuscularPrincipal = "Peito",
                GruposMuscularesSecundarios = new List<string>(),
                TipoMovimento = "Isolado",
                PlanoMovimento = "Transversal",
                Multiarticulado = false,
                NivelDificuldade = "Intermediário",
                Equipamento = "Máquina",
                PadraoMovimento = "Crucifixo",
                FatorFadiga = 2,
                SimilaridadeGrupo = "crucifixo"
            },
            // ---- COSTAS ----
            new Exercise
            {
                Nome = "Remada curvada com barra",
                GrupoMuscularPrincipal = "Costas",
                GruposMuscularesSecundarios = new List<string> { "Bíceps", "Antebraço" },
                TipoMovimento = "Puxar",
                PlanoMovimento = "Sagital",
                Multiarticulado = true,
                NivelDificuldade = "Intermediário",
                Equipamento = "Barra",
                PadraoMovimento = "Remada horizontal",
                FatorFadiga = 4,
                SimilaridadeGrupo = "remada-horizontal"
            },
            new Exercise
            {
                Nome = "Remada unilateral com halter",
                GrupoMuscularPrincipal = "Costas",
                GruposMuscularesSecundarios = new List<string> { "Bíceps" },
                TipoMovimento = "Puxar",
                PlanoMovimento = "Sagital",
                Multiarticulado = true,
                NivelDificuldade = "Iniciante",
                Equipamento = "Halter",
                PadraoMovimento = "Remada horizontal",
                FatorFadiga = 3,
                SimilaridadeGrupo = "remada-horizontal"
            },
            new Exercise
            {
                Nome = "Puxada frontal",
                GrupoMuscularPrincipal = "Costas",
                GruposMuscularesSecundarios = new List<string> { "Bíceps" },
                TipoMovimento = "Puxar",
                PlanoMovimento = "Sagital",
                Multiarticulado = true,
                NivelDificuldade = "Iniciante",
                Equipamento = "Máquina",
                PadraoMovimento = "Puxada vertical",
                FatorFadiga = 3,
                SimilaridadeGrupo = "puxada-vertical"
            },
            new Exercise
            {
                Nome = "Barra fixa",
                GrupoMuscularPrincipal = "Costas",
                GruposMuscularesSecundarios = new List<string> { "Bíceps" },
                TipoMovimento = "Puxar",
                PlanoMovimento = "Sagital",
                Multiarticulado = true,
                NivelDificuldade = "Intermediário",
                Equipamento = "Peso corporal",
                PadraoMovimento = "Puxada vertical",
                FatorFadiga = 4,
                SimilaridadeGrupo = "puxada-vertical"
            },
            new Exercise
            {
                Nome = "Remada no cabo",
                GrupoMuscularPrincipal = "Costas",
                GruposMuscularesSecundarios = new List<string> { "Bíceps" },
                TipoMovimento = "Puxar",
                PlanoMovimento = "Sagital",
                Multiarticulado = true,
                NivelDificuldade = "Iniciante",
                Equipamento = "Máquina",
                PadraoMovimento = "Remada horizontal",
                FatorFadiga = 3,
                SimilaridadeGrupo = "remada-horizontal"
            },
            new Exercise
            {
                Nome = "Puxada aberta costas",
                GrupoMuscularPrincipal = "Costas",
                GruposMuscularesSecundarios = new List<string> { "Bíceps" },
                TipoMovimento = "Puxar",
                PlanoMovimento = "Sagital",
                Multiarticulado = true,
                NivelDificuldade = "Iniciante",
                Equipamento = "Máquina",
                PadraoMovimento = "Puxada vertical",
                FatorFadiga = 3,
                SimilaridadeGrupo = "puxada-vertical"
            },
            // ---- OMBRO ----
            new Exercise
            {
                Nome = "Desenvolvimento com barra",
                GrupoMuscularPrincipal = "Ombro",
                GruposMuscularesSecundarios = new List<string> { "Tríceps" },
                TipoMovimento = "Empurrar",
                PlanoMovimento = "Frontal",
                Multiarticulado = true,
                NivelDificuldade = "Intermediário",
                Equipamento = "Barra",
                PadraoMovimento = "Desenvolvimento militar",
                FatorFadiga = 4,
                SimilaridadeGrupo = "desenvolvimento"
            },
            new Exercise
            {
                Nome = "Elevação lateral com halter",
                GrupoMuscularPrincipal = "Ombro",
                GruposMuscularesSecundarios = new List<string>(),
                TipoMovimento = "Isolado",
                PlanoMovimento = "Frontal",
                Multiarticulado = false,
                NivelDificuldade = "Iniciante",
                Equipamento = "Halter",
                PadraoMovimento = "Elevação lateral",
                FatorFadiga = 2,
                SimilaridadeGrupo = "elevacao-lateral"
            },
            new Exercise
            {
                Nome = "Elevação frontal com barra",
                GrupoMuscularPrincipal = "Ombro",
                GruposMuscularesSecundarios = new List<string>(),
                TipoMovimento = "Isolado",
                PlanoMovimento = "Sagital",
                Multiarticulado = false,
                NivelDificuldade = "Iniciante",
                Equipamento = "Barra",
                PadraoMovimento = "Elevação frontal",
                FatorFadiga = 2,
                SimilaridadeGrupo = "elevacao-frontal"
            },
            new Exercise
            {
                Nome = "Remada alta (desenvolvimento trapézio)",
                GrupoMuscularPrincipal = "Ombro",
                GruposMuscularesSecundarios = new List<string> { "Trapézio" },
                TipoMovimento = "Puxar",
                PlanoMovimento = "Frontal",
                Multiarticulado = true,
                NivelDificuldade = "Intermediário",
                Equipamento = "Barra",
                PadraoMovimento = "Remada alta",
                FatorFadiga = 3,
                SimilaridadeGrupo = "remada-alta"
            },
            new Exercise
            {
                Nome = "Desenvolvimento com halter sentado",
                GrupoMuscularPrincipal = "Ombro",
                GruposMuscularesSecundarios = new List<string> { "Tríceps" },
                TipoMovimento = "Empurrar",
                PlanoMovimento = "Frontal",
                Multiarticulado = true,
                NivelDificuldade = "Iniciante",
                Equipamento = "Halter",
                PadraoMovimento = "Desenvolvimento militar",
                FatorFadiga = 4,
                SimilaridadeGrupo = "desenvolvimento"
            },
            // ---- TRÍCEPS ----
            new Exercise
            {
                Nome = "Tríceps testa com barra",
                GrupoMuscularPrincipal = "Tríceps",
                GruposMuscularesSecundarios = new List<string>(),
                TipoMovimento = "Isolado",
                PlanoMovimento = "Sagital",
                Multiarticulado = false,
                NivelDificuldade = "Iniciante",
                Equipamento = "Barra",
                PadraoMovimento = "Extensão de cotovelo",
                FatorFadiga = 3,
                SimilaridadeGrupo = "triceps-extensao"
            },
            new Exercise
            {
                Nome = "Tríceps corda (pulley)",
                GrupoMuscularPrincipal = "Tríceps",
                GruposMuscularesSecundarios = new List<string>(),
                TipoMovimento = "Isolado",
                PlanoMovimento = "Sagital",
                Multiarticulado = false,
                NivelDificuldade = "Iniciante",
                Equipamento = "Máquina",
                PadraoMovimento = "Extensão de cotovelo",
                FatorFadiga = 2,
                SimilaridadeGrupo = "triceps-extensao"
            },
            new Exercise
            {
                Nome = "Mergulho (tríceps)",
                GrupoMuscularPrincipal = "Tríceps",
                GruposMuscularesSecundarios = new List<string> { "Peito", "Ombro" },
                TipoMovimento = "Empurrar",
                PlanoMovimento = "Sagital",
                Multiarticulado = true,
                NivelDificuldade = "Intermediário",
                Equipamento = "Peso corporal",
                PadraoMovimento = "Extensão de cotovelo",
                FatorFadiga = 4,
                SimilaridadeGrupo = "triceps-extensao"
            },
            // ---- BÍCEPS ----
            new Exercise
            {
                Nome = "Rosca direta com barra",
                GrupoMuscularPrincipal = "Bíceps",
                GruposMuscularesSecundarios = new List<string> { "Antebraço" },
                TipoMovimento = "Isolado",
                PlanoMovimento = "Sagital",
                Multiarticulado = false,
                NivelDificuldade = "Iniciante",
                Equipamento = "Barra",
                PadraoMovimento = "Flexão de cotovelo",
                FatorFadiga = 3,
                SimilaridadeGrupo = "rosca-biceps"
            },
            new Exercise
            {
                Nome = "Rosca martelo",
                GrupoMuscularPrincipal = "Bíceps",
                GruposMuscularesSecundarios = new List<string> { "Antebraço" },
                TipoMovimento = "Isolado",
                PlanoMovimento = "Sagital",
                Multiarticulado = false,
                NivelDificuldade = "Iniciante",
                Equipamento = "Halter",
                PadraoMovimento = "Flexão de cotovelo",
                FatorFadiga = 3,
                SimilaridadeGrupo = "rosca-biceps"
            },
            new Exercise
            {
                Nome = "Rosca Scott",
                GrupoMuscularPrincipal = "Bíceps",
                GruposMuscularesSecundarios = new List<string>(),
                TipoMovimento = "Isolado",
                PlanoMovimento = "Sagital",
                Multiarticulado = false,
                NivelDificuldade = "Iniciante",
                Equipamento = "Barra",
                PadraoMovimento = "Flexão de cotovelo",
                FatorFadiga = 2,
                SimilaridadeGrupo = "rosca-biceps"
            },
            // ---- PERNAS (quadríceps, posterior, glúteos) ----
            new Exercise
            {
                Nome = "Agachamento livre",
                GrupoMuscularPrincipal = "Quadríceps",
                GruposMuscularesSecundarios = new List<string> { "Glúteos", "Posterior" },
                TipoMovimento = "Agachar",
                PlanoMovimento = "Sagital",
                Multiarticulado = true,
                NivelDificuldade = "Intermediário",
                Equipamento = "Barra",
                PadraoMovimento = "Agachamento",
                FatorFadiga = 5,
                SimilaridadeGrupo = "agachamento"
            },
            new Exercise
            {
                Nome = "Agachamento búlgaro",
                GrupoMuscularPrincipal = "Quadríceps",
                GruposMuscularesSecundarios = new List<string> { "Glúteos" },
                TipoMovimento = "Agachar",
                PlanoMovimento = "Sagital",
                Multiarticulado = true,
                NivelDificuldade = "Intermediário",
                Equipamento = "Halter",
                PadraoMovimento = "Agachamento unilateral",
                FatorFadiga = 4,
                SimilaridadeGrupo = "agachamento"
            },
            new Exercise
            {
                Nome = "Leg press 45°",
                GrupoMuscularPrincipal = "Quadríceps",
                GruposMuscularesSecundarios = new List<string> { "Glúteos", "Posterior" },
                TipoMovimento = "Agachar",
                PlanoMovimento = "Sagital",
                Multiarticulado = true,
                NivelDificuldade = "Iniciante",
                Equipamento = "Máquina",
                PadraoMovimento = "Leg press",
                FatorFadiga = 4,
                SimilaridadeGrupo = "leg-press"
            },
            new Exercise
            {
                Nome = "Cadeira extensora",
                GrupoMuscularPrincipal = "Quadríceps",
                GruposMuscularesSecundarios = new List<string>(),
                TipoMovimento = "Isolado",
                PlanoMovimento = "Sagital",
                Multiarticulado = false,
                NivelDificuldade = "Iniciante",
                Equipamento = "Máquina",
                PadraoMovimento = "Extensão de joelho",
                FatorFadiga = 2,
                SimilaridadeGrupo = "extensao-joelho"
            },
            new Exercise
            {
                Nome = "Stiff (terra romeno)",
                GrupoMuscularPrincipal = "Posterior",
                GruposMuscularesSecundarios = new List<string> { "Glúteos" },
                TipoMovimento = "Levantar",
                PlanoMovimento = "Sagital",
                Multiarticulado = true,
                NivelDificuldade = "Intermediário",
                Equipamento = "Barra",
                PadraoMovimento = "Hinge quadril",
                FatorFadiga = 4,
                SimilaridadeGrupo = "hinge-quadril"
            },
            new Exercise
            {
                Nome = "Mesa flexora",
                GrupoMuscularPrincipal = "Posterior",
                GruposMuscularesSecundarios = new List<string>(),
                TipoMovimento = "Isolado",
                PlanoMovimento = "Sagital",
                Multiarticulado = false,
                NivelDificuldade = "Iniciante",
                Equipamento = "Máquina",
                PadraoMovimento = "Flexão de joelho",
                FatorFadiga = 2,
                SimilaridadeGrupo = "flexao-joelho"
            },
            new Exercise
            {
                Nome = "Cadeira flexora",
                GrupoMuscularPrincipal = "Posterior",
                GruposMuscularesSecundarios = new List<string>(),
                TipoMovimento = "Isolado",
                PlanoMovimento = "Sagital",
                Multiarticulado = false,
                NivelDificuldade = "Iniciante",
                Equipamento = "Máquina",
                PadraoMovimento = "Flexão de joelho",
                FatorFadiga = 2,
                SimilaridadeGrupo = "flexao-joelho"
            },
            new Exercise
            {
                Nome = "Afundo (passada)",
                GrupoMuscularPrincipal = "Quadríceps",
                GruposMuscularesSecundarios = new List<string> { "Glúteos", "Posterior" },
                TipoMovimento = "Agachar",
                PlanoMovimento = "Sagital",
                Multiarticulado = true,
                NivelDificuldade = "Iniciante",
                Equipamento = "Peso corporal",
                PadraoMovimento = "Agachamento unilateral",
                FatorFadiga = 3,
                SimilaridadeGrupo = "agachamento"
            },
            new Exercise
            {
                Nome = "Panturrilha em pé",
                GrupoMuscularPrincipal = "Panturrilha",
                GruposMuscularesSecundarios = new List<string>(),
                TipoMovimento = "Levantar",
                PlanoMovimento = "Sagital",
                Multiarticulado = false,
                NivelDificuldade = "Iniciante",
                Equipamento = "Máquina",
                PadraoMovimento = "Extensão de tornozelo",
                FatorFadiga = 1,
                SimilaridadeGrupo = "panturrilha"
            },
            new Exercise
            {
                Nome = "Panturrilha sentado",
                GrupoMuscularPrincipal = "Panturrilha",
                GruposMuscularesSecundarios = new List<string>(),
                TipoMovimento = "Levantar",
                PlanoMovimento = "Sagital",
                Multiarticulado = false,
                NivelDificuldade = "Iniciante",
                Equipamento = "Máquina",
                PadraoMovimento = "Extensão de tornozelo",
                FatorFadiga = 1,
                SimilaridadeGrupo = "panturrilha"
            },
            new Exercise
            {
                Nome = "Supino máquina",
                GrupoMuscularPrincipal = "Peito",
                GruposMuscularesSecundarios = new List<string> { "Tríceps", "Ombro" },
                TipoMovimento = "Empurrar",
                PlanoMovimento = "Transversal",
                Multiarticulado = true,
                NivelDificuldade = "Iniciante",
                Equipamento = "Máquina",
                PadraoMovimento = "Supino horizontal",
                FatorFadiga = 3,
                SimilaridadeGrupo = "supino-horizontal"
            },
            new Exercise
            {
                Nome = "Agachamento hack",
                GrupoMuscularPrincipal = "Quadríceps",
                GruposMuscularesSecundarios = new List<string> { "Glúteos" },
                TipoMovimento = "Agachar",
                PlanoMovimento = "Sagital",
                Multiarticulado = true,
                NivelDificuldade = "Intermediário",
                Equipamento = "Máquina",
                PadraoMovimento = "Agachamento",
                FatorFadiga = 4,
                SimilaridadeGrupo = "agachamento"
            },
            new Exercise
            {
                Nome = "Levantamento terra convencional",
                GrupoMuscularPrincipal = "Costas",
                GruposMuscularesSecundarios = new List<string> { "Posterior", "Glúteos", "Antebraço" },
                TipoMovimento = "Levantar",
                PlanoMovimento = "Sagital",
                Multiarticulado = true,
                NivelDificuldade = "Avançado",
                Equipamento = "Barra",
                PadraoMovimento = "Hinge quadril",
                FatorFadiga = 5,
                SimilaridadeGrupo = "hinge-quadril"
            },
            new Exercise
            {
                Nome = "Desenvolvimento Arnold",
                GrupoMuscularPrincipal = "Ombro",
                GruposMuscularesSecundarios = new List<string> { "Tríceps" },
                TipoMovimento = "Empurrar",
                PlanoMovimento = "Transversal",
                Multiarticulado = true,
                NivelDificuldade = "Intermediário",
                Equipamento = "Halter",
                PadraoMovimento = "Desenvolvimento militar",
                FatorFadiga = 4,
                SimilaridadeGrupo = "desenvolvimento"
            }
        };
    }
}
