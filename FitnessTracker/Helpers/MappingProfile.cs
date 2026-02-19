using AutoMapper;
using FitnessTracker.DTOs;
using FitnessTracker.Models;

namespace FitnessTracker.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<Workout, WorkoutDto>();
        CreateMap<CreateWorkoutDto, Workout>();
        CreateMap<UpdateWorkoutDto, Workout>();
    }
}
