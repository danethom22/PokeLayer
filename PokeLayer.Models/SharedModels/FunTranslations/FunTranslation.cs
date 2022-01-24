using System;
using System.Collections.Generic;
using System.Text;

namespace PokeLayer.Models.SharedModels.FunTranslations
{
  /// <summary>
  /// Class to hold FunTranslation Api response
  /// </summary>
  public class FunTranslation
  {
    /// <summary>
    /// Indicates success of the request via the <see cref="Success"/> class
    /// </summary>
    public Success success { get; set; }

    /// <summary>
    /// Displays the contents of the request in the <see cref="Contents"/> class
    /// </summary>
    public Contents contents { get; set; }
  }

  /// <summary>
  /// Class indicating the success of the FunTranslation Api request
  /// </summary>
  public class Success
  {
    /// <summary>
    /// Integer to show total number of successful translations
    /// </summary>
    public int total { get; set; }
  }

  /// <summary>
  /// Class to hold the contents of the FunTranslation Api response
  /// </summary>
  public class Contents
  {
    /// <summary>
    /// String containing the translated text
    /// </summary>
    public string translated { get; set; }

    /// <summary>
    /// String containing the original text
    /// </summary>
    public string text { get; set; }

    /// <summary>
    /// String containing the type of translation used
    /// </summary>
    public string translation { get; set; }
  }
}
