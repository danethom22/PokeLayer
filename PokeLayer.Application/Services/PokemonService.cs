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

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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

    /// <summary>
    /// Private method to map <see cref="PokemonSpecies"/> shared model to <see cref="Pokemon"/> model
    /// </summary>
    /// <param name="pokemonSpecies">Pokemon species returned from the API Client</param>
    /// <returns><see cref="Pokemon"/>Mapped Pokemon model</returns>
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

    /// <summary>
    /// Private method to extract the default english flavor text from a list of flavor text entries
    /// </summary>
    /// <param name="flavorTextEntries">List of flavor text entries</param>
    /// <returns>String containing the default flavor text</returns>
    private string GetFirstEnglishFlavorText(IList<FlavorTextEntry> flavorTextEntries)
    {
      return  flavorTextEntries.FirstOrDefault(x => x.language.name == "en").flavor_text;
    }

    /// <summary>
    /// Private method to determine if the pokemon's habitat is cave or if the pokemon is legendary
    /// </summary>
    /// <param name="pokemon">Pokemon who's habitat and legendary status will be determined</param>
    /// <returns>Boolean indicating if the pokemon's habitat is cave or if the pokemon is legendary</returns>
    private bool isHabitatCaveOrPokemonLegendary(Pokemon pokemon)
    {
      return (pokemon.habitat == "cave" || pokemon.isLegendary) ? true : false;
    }

    /// <summary>
    /// Private method to fetch translated text from the Yoda API
    /// </summary>
    /// <param name="textToTranslate">Text to be translated by the API</param>
    /// <returns>Task of type string containing the result of the translation</returns>
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

    /// <summary>
    /// Private method to fetch translated text from the Shakespeare API
    /// </summary>
    /// <param name="textToTranslate">Text to be translated by the API</param>
    /// <returns>Task of type string containing the result of the translation</returns>
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

    /// <summary>
    /// Private method to clean string of line feed, carriage return and page break placeholders
    /// </summary>
    /// <param name="stringToSanitize">String to sanitize</param>
    /// <returns>Sanitized string</returns>
    private string SanitizeString(string stringToSanitize)
    {
      return Regex.Replace(stringToSanitize, @"\r|\n|\f", " ");
    }
  }
}
