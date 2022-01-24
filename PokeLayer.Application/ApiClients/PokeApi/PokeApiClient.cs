using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using PokeLayer.Models.SharedModels.PokeApi;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace PokeLayer.Application.ApiClients.PokeApi
{
  public class PokeApiClient : IPokeApiClient
  {
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public PokeApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
      this._httpClientFactory = httpClientFactory;
      this._configuration = configuration;
    }

    public async Task<PokemonSpecies> GetPokemonSpeciesFromApi(string pokemonName)
    {
      var httpClient = _httpClientFactory.CreateClient();
      var httpResponse = await httpClient.GetAsync(GetPokemonSpeciesRequestUrl(pokemonName));

      if (httpResponse.StatusCode  == System.Net.HttpStatusCode.NotFound)
      {
        return null;
      }

      var json = await httpResponse.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<PokemonSpecies>(json);
    }

    private string GetPokemonSpeciesRequestUrl(string pokemonName)
    {
      var baseUrl = _configuration["ApiClientUrls:PokeApiUrl"];
      var endpoint = "pokemon-species";

      return $"{baseUrl}/{endpoint}/{pokemonName}";
    }

  }
}
