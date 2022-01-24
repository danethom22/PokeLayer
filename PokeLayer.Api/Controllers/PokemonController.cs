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

    [HttpGet("{pokemonName}")]
    public async Task<IActionResult> GetPokemonAsync(string pokemonName)
    {
      var pokemon = await _pokemonService.GetPokemonAsync(pokemonName);
      return Ok(pokemon);
    }

    [HttpGet("translated/{pokemonName}")]
    public async Task<IActionResult> GetTranslatedPokemonAsync(string pokemonName)
    {
      var pokemon = await _pokemonService.GetTranslatedPokemonAsync(pokemonName);
      return Ok(pokemon);
    }
  }
}
