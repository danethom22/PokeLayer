using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PokeLayer.Models.SharedModels.FunTranslations;

namespace PokeLayer.Application.ApiClients.FunTranslationsApi
{
  public interface IFunTranslationsApiClient
  {
    Task<FunTranslation> GetYodaFunTranslationFromApi(string textToTranslate);

    Task<FunTranslation> GetShakespeareFunTranslationFromApi(string textToTranslate);
  }
}
