namespace FitnessTracker.DTOs;

public class TreinoAnaliseDto
{
    /// <summary>Score geral de qualidade do treino (0 a 100).</summary>
    public int Score { get; set; }

    /// <summary>Lista de sugestões de melhoria.</summary>
    public List<string> Sugestoes { get; set; } = new();

    /// <summary>Resumo dos critérios avaliados (opcional, para transparência).</summary>
    public TreinoAnaliseDetalheDto? Detalhes { get; set; }

    /// <summary>Explicação resumida da lógica usada para calcular o score.</summary>
    public string LogicaScore { get; set; } = string.Empty;
}

public class TreinoAnaliseDetalheDto
{
    public int PontosVolume { get; set; }
    public int PontosFrequenciaGrupos { get; set; }
    public int PontosDistribuicao { get; set; }
    public int PontosSeries { get; set; }
    public decimal VolumeTotal { get; set; }
    public int TotalSeries { get; set; }
    public int QuantidadeGruposMusculares { get; set; }
}
