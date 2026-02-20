using FitnessTracker.Services;
using Xunit;

namespace FitnessTracker.Tests;

public class SplitTemplateServiceTests
{
    [Fact]
    public async Task GetTemplatesAsync_ReturnsFourTemplates()
    {
        var service = new SplitTemplateService();
        var templates = await service.GetTemplatesAsync();
        var list = templates.ToList();
        Assert.Equal(4, list.Count);
    }

    [Fact]
    public async Task GetTemplatesAsync_ContainsPushPullLegs()
    {
        var service = new SplitTemplateService();
        var templates = await service.GetTemplatesAsync();
        var ppl = templates.FirstOrDefault(t => t.DivisaoTreino == "PushPullLegs");
        Assert.NotNull(ppl);
        Assert.Equal(3, ppl.Dias.Count);
        Assert.Equal(6, ppl.FrequenciaSemanalSugerida);
        var dia1 = ppl.Dias[0];
        Assert.Contains("Peito", dia1.GruposMusculares);
        Assert.Contains("Ombro", dia1.GruposMusculares);
        Assert.Contains("Tríceps", dia1.GruposMusculares);
    }

    [Fact]
    public async Task GetTemplatesAsync_ContainsUpperLower()
    {
        var service = new SplitTemplateService();
        var templates = await service.GetTemplatesAsync();
        var ul = templates.FirstOrDefault(t => t.DivisaoTreino == "UpperLower");
        Assert.NotNull(ul);
        Assert.Equal(2, ul.Dias.Count);
        Assert.Equal(4, ul.FrequenciaSemanalSugerida);
    }

    [Fact]
    public async Task GetTemplatesAsync_ContainsBroSplit()
    {
        var service = new SplitTemplateService();
        var templates = await service.GetTemplatesAsync();
        var bro = templates.FirstOrDefault(t => t.DivisaoTreino == "BroSplit");
        Assert.NotNull(bro);
        Assert.Equal(5, bro.Dias.Count);
        Assert.Equal(5, bro.FrequenciaSemanalSugerida);
    }

    [Fact]
    public async Task GetTemplatesAsync_ContainsPersonalizado()
    {
        var service = new SplitTemplateService();
        var templates = await service.GetTemplatesAsync();
        var pers = templates.FirstOrDefault(t => t.DivisaoTreino == "Personalizado");
        Assert.NotNull(pers);
        Assert.Single(pers.Dias);
    }
}
