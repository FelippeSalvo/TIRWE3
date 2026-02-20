namespace FitnessTracker.DTOs;

public class TreinoUpdateResult
{
    public TreinoDto? Treino { get; set; }
    /// <summary>Null = sucesso; "NotFound" = treino não encontrado; "InvalidExerciseIds" = um ou mais ExerciseIds não existem.</summary>
    public string? Error { get; set; }
}
