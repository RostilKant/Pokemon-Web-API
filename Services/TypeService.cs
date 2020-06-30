using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.Extensions.Logging;

namespace Services
{
    public class TypeService : ITypeService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public TypeService(IRepositoryManager repositoryManager, ILogger<TypeService> logger, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }
        public IEnumerable<TypeDto> GetAllTypesOfPokemon(int pokemonId)
        {
            var pokemon = _repositoryManager.Pokemon.GetPokemon(pokemonId, false);
            
            if (pokemon != null)
            {
                var types = _repositoryManager.Type.GetAllTypes(pokemonId, false);
                var typesDto = _mapper.Map<IEnumerable<TypeDto>>(types);
                return typesDto;
            }

            _logger.LogInformation($"Pokemon with Id {pokemonId} doesn't exists in DB.");
            return null;
        }

        /*public TypeDto PostType(int pokemonId, TypeForCreationDto typeForCreation)
        {
            if (typeForCreation == null)
            {
                _logger.LogError("TypeForCreationDto object sent from client is null.");
                return null;
            }
            var pokemon = _repositoryManager.Pokemon.GetPokemon(pokemonId, false);

            if (pokemon == null)
            {
                _logger.LogInformation($"Company with id: {pokemonId} doesn't exist in the database.");
                typeForCreation.Name = "null";
                return _mapper.Map<TypeDto>(_mapper.Map<Type>(typeForCreation));
            }

            var typeEntity = _mapper.Map<Type>(typeForCreation);
            _repositoryManager.Type.CreateType(pokemonId,typeEntity);
            _repositoryManager.Save();
            return _mapper.Map<TypeDto>(typeEntity);
        }*/
    }
}