using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using PokeLayer.Models.SharedModels.FunTranslations;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace PokeLayer.Application.ApiClients.FunTranslationsApi
{
  public class FunTranslationsApiClient : IFunTranslationsApiClient
  {
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<FunTranslationsApiClient> _logger;

    public FunTranslationsApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<FunTranslationsApiClient> logger)
    {
      this._httpClientFactory = httpClientFactory;
      this._configuration = configuration;
      this._logger = logger;
    }

    public async Task<FunTranslation> GetYodaFunTranslationFromApi(string textToTranslate)
    {
      var httpClient = _httpClientFactory.CreateClient();
      var httpResponse = await httpClient.GetAsync(GetFunTranslationUrl(textToTranslate, "yoda"));

      if (httpResponse.StatusCode != System.Net.HttpStatusCode.OK)
      {
        _logger.LogError($"Error while fetching translation. Status code: {httpResponse.StatusCode}.");
        return null;
      } 
      else
      {
        var json = await httpResponse.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<FunTranslation>(json);
      }
    }

    public async Task<FunTranslation> GetShakespeareFunTranslationFromApi(string textToTranslate)
    {
      var httpClient = _httpClientFactory.CreateClient();
      var requestUrl = GetFunTranslationUrl(textToTranslate, "shakespeare");
      var httpResponse = await httpClient.GetAsync(requestUrl);

      if (httpResponse.StatusCode != System.Net.HttpStatusCode.OK)
      {
        _logger.LogError($"Error while fetching translation. Status code: {httpResponse.StatusCode}.");
        return null;
      }
      else
      {
        var json = await httpResponse.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<FunTranslation>(json);
      }
    }

    private string GetFunTranslationUrl(string textToTranslate, string translationType)
    {
      var baseUrl = _configuration["ApiClientUrls:FunTranslationsApiUrl"];
      var endpoint = $"translate/{translationType}.json";
      var textQueryString = $"text={textToTranslate}";

      return $"{baseUrl}/{endpoint}?{textQueryString}";
    }
  }
}
