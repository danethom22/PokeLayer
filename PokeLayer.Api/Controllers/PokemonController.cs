using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeLayer.Application.Services;

namespace PokeLayer.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class PokemonController : ControllerBase
  {

    private readonly ILogger<PokemonController> _logger;
    private readonly IPokemonService _pokemonService;

    public PokemonController(ILogger<PokemonController> logger, IPokemonService pokemonService)
    {
      _logger = logger;
      _pokemonService = pokemonService;
    }

    /// <summary>
    /// Controller to asynchronously fetch pokemon information
    /// </summary>
    /// <param name="pokemonName">Parameter containing the name of the pokemon who's information should be fetched</param>
    /// <returns>Task of type IAction result containing the result of the fetch method</returns>
    [HttpGet("{pokemonName}")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 300)]
    public async Task<IActionResult> GetPokemonAsync(string pokemonName)
    {
      var pokemon = await _pokemonService.GetPokemonAsync(pokemonName);

      if (pokemon == null)
      {
        return NotFound();
      }
 
      return Ok(pokemon);
    }

    /// <summary>
    /// Controller to asynchronously fetch pokemon information and translate the pokemons description
    /// </summary>
    /// <param name="pokemonName">Parameter containing the name of the pokemon who's information should be fetched</param>
    /// <returns>Task of type IAction result containing the result of the fetch method</returns>
    [HttpGet("translated/{pokemonName}")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 300)]
    public async Task<IActionResult> GetTranslatedPokemonAsync(string pokemonName)
    {
      var pokemon = await _pokemonService.GetTranslatedPokemonAsync(pokemonName);

      if (pokemon == null)
      {
        return NotFound();
      }

      return Ok(pokemon);
    }
  }
}
