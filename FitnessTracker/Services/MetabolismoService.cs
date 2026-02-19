namespace FitnessTracker.Services;

/// <summary>
/// Serviço de cálculo metabólico usando a fórmula Mifflin-St Jeor.
/// TMB (Homem) = (10 × peso kg) + (6,25 × altura cm) - (5 × idade) + 5
/// TMB (Mulher) = (10 × peso kg) + (6,25 × altura cm) - (5 × idade) - 161
/// </summary>
public class MetabolismoService : IMetabolismoService
{
    public double CalcularTMB(decimal peso, decimal altura, int idade, string sexo)
    {
        var pesoKg = (double)peso;
        var alturaCm = (double)altura * 100; // converte metros para cm

        var tmb = (10 * pesoKg) + (6.25 * alturaCm) - (5 * idade);

        return sexo?.ToUpperInvariant() == "M"
            ? tmb + 5
            : tmb - 161;
    }
}
