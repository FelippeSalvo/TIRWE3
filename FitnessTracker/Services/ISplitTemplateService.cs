using FitnessTracker.DTOs;

namespace FitnessTracker.Services;

public interface ISplitTemplateService
{
    /// <summary>
    /// Retorna todos os templates de divisão com dias, grupos por dia e frequência semanal sugerida.
    /// </summary>
    Task<IEnumerable<SplitTemplateDto>> GetTemplatesAsync();
}
