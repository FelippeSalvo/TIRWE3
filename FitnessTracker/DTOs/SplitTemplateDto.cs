namespace FitnessTracker.DTOs;

public class SplitTemplateDto
{
    public string Id { get; set; } = string.Empty; // PushPullLegs, UpperLower, BroSplit, Personalizado
    public string Nome { get; set; } = string.Empty;
    public string DivisaoTreino { get; set; } = string.Empty;
    public int FrequenciaSemanalSugerida { get; set; }
    public List<SplitDayDto> Dias { get; set; } = new();
}

public class SplitDayDto
{
    public int DiaNumero { get; set; }
    public string NomeDia { get; set; } = string.Empty;
    public List<string> GruposMusculares { get; set; } = new();
}
