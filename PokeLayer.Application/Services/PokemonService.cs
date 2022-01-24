using System;
using System.Collections.Generic;
using System.Text;
using PokeLayer.Application.ApiClients.FunTranslationsApi;
using PokeLayer.Application.ApiClients.PokeApi;
using PokeLayer.Models.Models;
using PokeLayer.Models.SharedModels.PokeApi;
using PokeLayer.Models.SharedModels.FunTranslations;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;

namespace PokeLayer.Application.Services
{
  public class PokemonService : IPokemonService
  {
    private readonly IPokeApiClient _pokeApiClient;
    private readonly IFunTranslationsApiClient _funTranslationsApiClient;

    public PokemonService(IPokeApiClient pokeApiClient, IFunTranslationsApiClient funTranslationsApiClient)
    {
      this._pokeApiClient = pokeApiClient;
      this._funTranslationsApiClient = funTranslationsApiClient;
    }

    public async Task<Pokemon> GetTranslatedPokemonAsync(string pokemonName)
    {
      var pokemon = await GetPokemonAsync(pokemonName);
      if (pokemon != null)
      {
        if (isHabitatCaveOrPokemonLegendary(pokemon))
        {
          pokemon.description = GetYodaTranslationFromFunTranslationApi(pokemon.description).Result;
        }
        else
        {
          pokemon.description = GetShakespeareTranslationFromFunTranslationApi(pokemon.description).Result;
        }
      }

      return pokemon;
    }

    public async Task<Pokemon> GetPokemonAsync(string pokemonName)
    {
      var pokemonFromApi = await _pokeApiClient.GetPokemonSpeciesFromApi(pokemonName);
      if (pokemonFromApi is null)
      {
        return null;
      } 
      else
      {
        return MapPokemonFromPokemonSpecies(pokemonFromApi);
      }
    }

    private Pokemon MapPokemonFromPokemonSpecies(PokemonSpecies pokemonSpecies)
    {
      Pokemon pokemon = new Pokemon()
      {
        name = pokemonSpecies.name,
        description = SanitizeString(GetFirstEnglishFlavorText(pokemonSpecies.flavor_text_entries)),
        habitat = pokemonSpecies.habitat.name,
        isLegendary = pokemonSpecies.is_legendary
      };

      return pokemon;
    }

    private string GetFirstEnglishFlavorText(IList<FlavorTextEntry> flavorTextEntries)
    {
      return  flavorTextEntries.FirstOrDefault(x => x.language.name == "en").flavor_text;
    }

    private bool isHabitatCaveOrPokemonLegendary(Pokemon pokemon)
    {
      return (pokemon.habitat == "cave" || pokemon.isLegendary) ? true : false;
    }

    private async Task<string> GetYodaTranslationFromFunTranslationApi(string textToTranslate)
    {
      var translation = await _funTranslationsApiClient.GetYodaFunTranslationFromApi(textToTranslate);
      if (translation == null || translation.success.total == 0)
      {
        return textToTranslate;
      }
      else
      {
        return translation.contents.translated;
      }
    }

    private async Task<string> GetShakespeareTranslationFromFunTranslationApi(string textToTranslate)
    {
      var translation = await _funTranslationsApiClient.GetShakespeareFunTranslationFromApi(textToTranslate);
      if (translation == null || translation.success.total == 0)
      {
        return textToTranslate;
      }
      else
      {
        return translation.contents.translated;
      }
    }

    private string SanitizeString(string stringToSanitize)
    {
      return Regex.Replace(stringToSanitize, @"\r|\n|\f", " ");
    }
  }
}
