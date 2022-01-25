using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using PokeLayer.Application.ApiClients.FunTranslationsApi;
using PokeLayer.Application.ApiClients.PokeApi;
using PokeLayer.Application.Services;
using Xunit;
using PokeLayer.Models.SharedModels.PokeApi;
using PokeLayer.Models.Models;

namespace PokeLayer.Application.Tests.Services
{
  public class PokemonServiceTests
  {
    private Mock<IPokeApiClient> mockPokeApiClient;
    private Mock<IFunTranslationsApiClient> mockFunTranslationsApiClient;
    private PokemonService pokemonService;

    public PokemonServiceTests()
    {
      mockPokeApiClient = new Mock<IPokeApiClient>();
      mockFunTranslationsApiClient = new Mock<IFunTranslationsApiClient>();
      pokemonService = new PokemonService(mockPokeApiClient.Object, mockFunTranslationsApiClient.Object);
    }

    [Fact]
    private async void GivenAValidPokemonName_WhenIFetchThePokemonFromTheApi_ThenIExpectToReceiveThatPokemon()
    {
      // Arrange
      mockPokeApiClient.Setup(x => x.GetPokemonSpeciesFromApi("pikachu"))
        .ReturnsAsync(GetValidTestPokemonSpecies());

      // Act
      var result = await pokemonService.GetPokemonAsync("pikachu");

      // Assert
      var typeresult = Assert.IsType<Pokemon>(result);
      Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(GetValidTestPokemon()), Newtonsoft.Json.JsonConvert.SerializeObject(typeresult));
    }

    private PokemonSpecies GetValidTestPokemonSpecies()
    {
      var validPokemonSpecies = new PokemonSpecies()
      {
        name = "pikachu",
        flavor_text_entries = new List<FlavorTextEntry>() {
          new FlavorTextEntry() {
            flavor_text = "When several of these POKéMON gather, their electricity could build and cause lightning storms.",
            language = new Language() {
              name = "en"
            }
          }
        },
        habitat = new Habitat() { name = "forest" },
        is_legendary = false
      };

      return validPokemonSpecies;
    }

    private Pokemon GetValidTestPokemon()
    {
      var testPokemon = new Pokemon()
      {
        name = "pikachu",
        description = "When several of these POKéMON gather, their electricity could build and cause lightning storms.",
        habitat = "forest",
        isLegendary = false
      };

      return testPokemon;
    }
  }
}
