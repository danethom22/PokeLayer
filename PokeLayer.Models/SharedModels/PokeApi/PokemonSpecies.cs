using System;
using System.Collections.Generic;
using System.Text;

namespace PokeLayer.Models.SharedModels.PokeApi
{
  /// <summary>
  /// Class containing the pokemon species returned from an ApiClient
  /// </summary>
  public class PokemonSpecies
  {
    /// <summary>
    /// String representing the pokemon species' name
    /// </summary>
    public string name { get; set; }

    /// <summary>
    /// List containing the flavor text entries for the pokemon
    /// </summary>
    public IList<FlavorTextEntry> flavor_text_entries { get; set; }

    /// <summary>
    /// Habitat class representing information about the pokemons habitat
    /// </summary>
    public Habitat habitat { get; set; }

    /// <summary>
    /// Boolean indicating if the pokemon is legendary
    /// </summary>
    public bool is_legendary { get; set; }
  }

  /// <summary>
  /// Class containing habitat information
  /// </summary>
  public class Habitat
  {
    /// <summary>
    /// String representing the name of the habitat
    /// </summary>
    public string name { get; set; }
  }

  /// <summary>
  /// Class containing flavor text information
  /// </summary>
  public class FlavorTextEntry
  {
    /// <summary>
    /// String representing the flavor text
    /// </summary>
    public string flavor_text { get; set; }

    /// <summary>
    /// Language class representing information about the language
    /// </summary>
    public Language language { get; set; }
  }

  /// <summary>
  /// Class containing language information
  /// </summary>
  public class Language
  {
    /// <summary>
    /// String representing the name of the language
    /// </summary>
    public string name { get; set; }
  }
}
