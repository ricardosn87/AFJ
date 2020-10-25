using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelefoniaApi.Domain.DTOs;
using TelefoniaApi.Domain.Entities;
using TelefoniaApi.Domain.ViewsModels;

namespace TelefoniaApi.Data.AutoMappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Plano, PlanoDTO>();

            CreateMap<Operadora, OperadoraDTO>();
            CreateMap<OperadoraViewModel, OperadoraDTO>();

            CreateMap<DDD, DDDDTO>();
        }
    }
}
