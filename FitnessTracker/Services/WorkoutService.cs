using AutoMapper;
using FitnessTracker.DTOs;
using FitnessTracker.Models;
using FitnessTracker.Repositories;

namespace FitnessTracker.Services;

public class WorkoutService : IWorkoutService
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IMapper _mapper;

    public WorkoutService(IWorkoutRepository workoutRepository, IMapper mapper)
    {
        _workoutRepository = workoutRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WorkoutDto>> GetByUserIdAsync(string userId)
    {
        var workouts = await _workoutRepository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<WorkoutDto>>(workouts);
    }

    public async Task<WorkoutDto?> GetByIdAsync(string id)
    {
        var workout = await _workoutRepository.GetByIdAsync(id);
        return workout == null ? null : _mapper.Map<WorkoutDto>(workout);
    }

    public async Task<WorkoutDto> CreateAsync(string userId, CreateWorkoutDto createWorkoutDto)
    {
        var workout = _mapper.Map<Workout>(createWorkoutDto);
        workout.UserId = userId;
        workout.WorkoutDate = createWorkoutDto.WorkoutDate ?? DateTime.UtcNow;
        workout.CreatedAt = DateTime.UtcNow;
        workout.UpdatedAt = DateTime.UtcNow;

        await _workoutRepository.CreateAsync(workout);
        return _mapper.Map<WorkoutDto>(workout);
    }

    public async Task<WorkoutDto?> UpdateAsync(string id, string userId, UpdateWorkoutDto updateWorkoutDto)
    {
        var workout = await _workoutRepository.GetByIdAsync(id);

        if (workout == null || workout.UserId != userId)
        {
            return null;
        }

        workout.Name = updateWorkoutDto.Name;
        workout.Description = updateWorkoutDto.Description;
        workout.Duration = updateWorkoutDto.Duration;
        workout.CaloriesBurned = updateWorkoutDto.CaloriesBurned;
        workout.WorkoutDate = updateWorkoutDto.WorkoutDate ?? workout.WorkoutDate;
        workout.UpdatedAt = DateTime.UtcNow;

        await _workoutRepository.UpdateAsync(workout);
        return _mapper.Map<WorkoutDto>(workout);
    }

    public async Task<bool> DeleteAsync(string id, string userId)
    {
        var workout = await _workoutRepository.GetByIdAsync(id);

        if (workout == null || workout.UserId != userId)
        {
            return false;
        }

        return await _workoutRepository.DeleteAsync(id);
    }
}
