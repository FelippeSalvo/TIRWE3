namespace FitnessTracker.DTOs;

public class AdvancedAnalysisDto
{
    public int Score { get; set; }
    public string Classificacao { get; set; } = "Regular"; // Excelente | Bom | Regular | Ruim
    public List<string> PontosFortes { get; set; } = new();
    public List<string> PontosFracos { get; set; } = new();
    public List<string> Sugestoes { get; set; } = new();
}
