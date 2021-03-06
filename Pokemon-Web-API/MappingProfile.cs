using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using Entities.GETAllFromPokeApi;
using Entities.Models;
using Microsoft.AspNetCore.Http.Connections;
using RestSharp;

namespace Pokemon_Web_API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RootObject, NewRootObject>().ForMember(c => c.Count, x
                    => x.MapFrom(y => 
                        807));
            CreateMap<Result, NResult>().ForMember(c => c.Url, x
                    => x.MapFrom(y =>
                        //Previous map - https://localhost:5001/api/pokemons/poke-api
                        string.Concat("https://pokemon-web-api.azurewebsites.net/api/pokemons/poke-api", y.Url.Remove(0, 33))))
                .ForMember(c => c.Name,
                    opt
                        => opt.MapFrom(x => x.Name));
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<Type, TypeDto>();
            CreateMap<PokemonForCreationDto, Pokemon>().ReverseMap();
            CreateMap<TypeForCreationDto, Type>().ReverseMap();
            CreateMap<Entities.GetPokemonsFromPokeApi.Pokemon, PokemonForCreationDto>();
            CreateMap<PokemonForUpdateDto, Pokemon>().ReverseMap();
            CreateMap<UserForRegistrationDto, User>();
        }
    }
}