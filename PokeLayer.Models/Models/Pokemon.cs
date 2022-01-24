using System;
using System.Collections.Generic;
using System.Text;

namespace PokeLayer.Models.Models
{
  public class Pokemon
  {
    public string name { get; set; }

    public string description { get; set; }

    public string habitat { get; set; }

    public bool isLegendary { get; set; }
  }
}
