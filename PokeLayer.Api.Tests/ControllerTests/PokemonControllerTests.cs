using System;
using System.Collections.Generic;
using System.Text;
using PokeLayer.Api.Controllers;
using PokeLayer.Application.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using PokeLayer.Models.Models;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft;

namespace PokeLayer.Api.Tests.ControllerTests
{
  public class PokemonControllerTests
  {
    private Mock<IPokemonService> mockPokemonService;
    private Mock<ILogger<PokemonController>> mockLogger;
    private PokemonController pokemonController;

    public PokemonControllerTests()
    {
      mockPokemonService = new Mock<IPokemonService>();
      mockLogger = new Mock<ILogger<PokemonController>>();
      pokemonController = new PokemonController(mockLogger.Object, mockPokemonService.Object);
    }

    [Fact]
    private async void GivenAValidPokemonName_WhenIReceiveThatPokemon_ThenIExpectOkResultWithMatchingPokemon()
    {
      // Arrange
      mockPokemonService.Setup(x => x.GetPokemonAsync("pikachu"))
        .ReturnsAsync(GetValidTestPokemon());

      // Act
      var result = await pokemonController.GetPokemonAsync("pikachu");

      // Assert
      var typeresult = Assert.IsType<OkObjectResult>(result);
      Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(GetValidTestPokemon()), Newtonsoft.Json.JsonConvert.SerializeObject(typeresult.Value));
    }

    [Fact]
    private async void GivenAValidPokemonName_WhenIReceiveAnInvalidPokemon_ThenIExpectANotFoundResult()
    {
      // Arrange
      mockPokemonService.Setup(x => x.GetPokemonAsync("pikachu"))
        .ReturnsAsync(GetInavlidTestPokemon());

      // Act
      var result = await pokemonController.GetPokemonAsync("pikachu");

      // Assert
      var typeresult = Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    private async void GivenAPokemonName_WhenIReceiveANull_ThenIExpectANotFoundResult()
    {
      // Arrange
      mockPokemonService.Setup(x => x.GetPokemonAsync("pikachu"))
        .ReturnsAsync((Pokemon)null);

      // Act
      var result = await pokemonController.GetPokemonAsync("pikachu");

      // Assert
      var typeresult = Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    private async void GivenAValidTranslatePokemonName_WhenIReceiveThatPokemon_ThenIExpectOkResultWithMatchingTranslatedPokemon()
    {
      // Arrange
      mockPokemonService.Setup(x => x.GetTranslatedPokemonAsync("pikachu"))
        .ReturnsAsync(GetValidTestPokemon());

      // Act
      var result = await pokemonController.GetTranslatedPokemonAsync("pikachu");

      // Assert
      var typeresult = Assert.IsType<OkObjectResult>(result);
      Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(GetValidTestPokemon()), Newtonsoft.Json.JsonConvert.SerializeObject(typeresult.Value));
    }

    [Fact]
    private async void GivenAValidTranslatePokemonName_WhenIReceiveAnInvalidPokemon_ThenIExpectANotFoundResult()
    {
      // Arrange
      mockPokemonService.Setup(x => x.GetTranslatedPokemonAsync("pikachu"))
        .ReturnsAsync(GetInavlidTestPokemon());

      // Act
      var result = await pokemonController.GetTranslatedPokemonAsync("pikachu");

      // Assert
      var typeresult = Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    private async void GivenATranslatePokemonName_WhenIReceiveANull_ThenIExpectANotFoundResult()
    {
      // Arrange
      mockPokemonService.Setup(x => x.GetTranslatedPokemonAsync("pikachu"))
        .ReturnsAsync((Pokemon)null);

      // Act
      var result = await pokemonController.GetTranslatedPokemonAsync("pikachu");

      // Assert
      var typeresult = Assert.IsType<NotFoundResult>(result);
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

    private Pokemon GetInavlidTestPokemon()
    {
      var invalidTestPokemon = new Pokemon()
      {
        name = "IAmNotAPokemon",
        description = "IAmNotAPokemon simply does not exist. This makes it difficult to describe",
        habitat = "Aether",
        isLegendary = true
      };

      return invalidTestPokemon;
    }
  }
}
