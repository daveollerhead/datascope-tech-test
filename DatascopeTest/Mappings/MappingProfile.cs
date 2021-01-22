using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatascopeTest.Commands;
using DatascopeTest.Controllers;
using DatascopeTest.DTOs;
using DatascopeTest.Models;

namespace DatascopeTest.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateGameCommand, Game>();
            CreateMap<CreateGameCommand, Game>();
            CreateMap<Game, Game>();
            CreateMap<Game, GetGameDto>();
        }
    }
}
