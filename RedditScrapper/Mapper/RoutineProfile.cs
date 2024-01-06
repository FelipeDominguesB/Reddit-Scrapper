using AutoMapper;
using RedditScrapper.Domain.Entities;
using RedditScrapper.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Mapper
{
    public class RoutineProfile : Profile
    {
        public RoutineProfile() 
        { 
            CreateMap<Routine, RoutineDTO>().ReverseMap();
            CreateMap<RoutineExecution, RoutineExecutionDTO>().ReverseMap();
            CreateMap<RoutineExecutionFile, RoutineExecutionFileDTO>().ReverseMap();
        }
    }
}
