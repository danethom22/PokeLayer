using System;
using System.Collections.Generic;
using System.Text;
using PokeLayer.Models.Models;
using System.Threading.Tasks;

namespace PokeLayer.Application.Services
{
  public interface IPokemonService
  {
    /// <summary>
    /// Method to fetch information about a pokemon
    /// </summary>
    /// <param name="pokemonName">Name of the pokemon to fetch</param>
    /// <returns><see cref="Task"/> of type <see cref="Pokemon"/> containing the result of the asynchronous method</returns>
    Task<Pokemon> GetPokemonAsync(string pokemonName);

    /// <summary>
    /// Method to fetch information about a pokemon with a translated description
    /// </summary>
    /// <param name="pokemonName">Name of the pokemon to fetch and translate</param>
    /// <returns><see cref="Task"/> of type <see cref="Pokemon"/> containing the result of the asynchronous method</returns>
    Task<Pokemon> GetTranslatedPokemonAsync(string pokemonName);
  }
}
