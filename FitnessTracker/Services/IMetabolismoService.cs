namespace FitnessTracker.Services;

public interface IMetabolismoService
{
    /// <summary>
    /// Calcula a Taxa Metabólica Basal (TMB) pela fórmula Mifflin-St Jeor.
    /// </summary>
    /// <param name="peso">Peso em kg</param>
    /// <param name="altura">Altura em metros</param>
    /// <param name="idade">Idade em anos</param>
    /// <param name="sexo">M ou F</param>
    /// <returns>TMB em kcal/dia</returns>
    double CalcularTMB(decimal peso, decimal altura, int idade, string sexo);
}
