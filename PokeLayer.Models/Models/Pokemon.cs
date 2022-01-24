using System;
using System.Collections.Generic;
using System.Text;

namespace PokeLayer.Models.Models
{
  /// <summary>
  /// Class containing instance of a pokemon
  /// </summary>
  public class Pokemon
  {
    /// <summary>
    /// String representing the pokemons name
    /// </summary>
    public string name { get; set; }

    /// <summary>
    /// String representing the pokemons description
    /// </summary>
    public string description { get; set; }

    /// <summary>
    /// String representing the pokemons habitat
    /// </summary>
    public string habitat { get; set; }

    /// <summary>
    /// Boolean indicated if the pokemone is legendary
    /// </summary>
    public bool isLegendary { get; set; }
  }
}
