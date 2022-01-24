using System;
using System.Collections.Generic;
using System.Text;

namespace PokeLayer.Models.SharedModels.PokeApi
{
  public class PokemonSpecies
  {
    public string name { get; set; }

    public IList<FlavorTextEntry> flavor_text_entries { get; set; }

    public Habitat habitat { get; set; }

    public bool is_legendary { get; set; }
  }
  public class Habitat
  {
    public string name { get; set; }
  }

  public class FlavorTextEntry
  {
    public string flavor_text { get; set; }

    public Language language { get; set; }
  }

  public class Language
  {
    public string name { get; set; }
  }
}
