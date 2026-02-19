using AutoMapper;
using FitnessTracker.DTOs;
using FitnessTracker.Models;

namespace FitnessTracker.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Usuario, UsuarioDto>();
        CreateMap<Workout, WorkoutDto>();
        CreateMap<CreateWorkoutDto, Workout>();
        CreateMap<UpdateWorkoutDto, Workout>();
    }
}
