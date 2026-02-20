using FitnessTracker.Models;
using FitnessTracker.Repositories;

namespace FitnessTracker.Data;

/// <summary>
/// Executa o seed de exercícios na inicialização da aplicação se a collection Exercises estiver vazia.
/// </summary>
public class ExerciseSeedHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public ExerciseSeedHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IExerciseRepository>();
        if (await repo.AnyAsync())
            return;
        var exercises = ExerciseSeeder.GetSeedExercises();
        await repo.InsertManyAsync(exercises);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
