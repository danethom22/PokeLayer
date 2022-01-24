using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PokeLayer.Models.SharedModels.PokeApi;

namespace PokeLayer.Application.ApiClients.PokeApi
{
  public interface IPokeApiClient
  {
    Task<PokemonSpecies> GetPokemonSpeciesFromApi(string pokemonname);
  }
}
