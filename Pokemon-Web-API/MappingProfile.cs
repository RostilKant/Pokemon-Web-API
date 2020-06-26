using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using Entities.JsonModels;
using Entities.Models;
using Microsoft.AspNetCore.Http.Connections;
using RestSharp;

namespace Pokemon_Web_API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RootObject, NewRootObject>();
            CreateMap<Result,NResult>().ForMember(c => c.Url,x 
                => x.MapFrom(y=>
                string.Concat("https://localhost:5001/api/pokemons",y.Url.Remove(0,33))))
                .ForMember(c=> c.Name,
                    opt 
                    => opt.MapFrom(x => x.Name));
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<Type, TypeDto>();
        }
    }
}