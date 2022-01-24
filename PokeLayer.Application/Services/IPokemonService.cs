using System;
using System.Collections.Generic;
using System.Text;
using PokeLayer.Models.Models;
using System.Threading.Tasks;

namespace PokeLayer.Application.Services
{
  public interface IPokemonService
  {
    Task<Pokemon> GetPokemonAsync(string pokemonName);

    Task<Pokemon> GetTranslatedPokemonAsync(string pokemonName);
  }
}
