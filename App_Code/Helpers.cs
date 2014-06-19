using System.Configuration;
using System.Text.RegularExpressions;

/// <summary>
/// Samling af små hjælpe funktioner
/// </summary>
public class Helpers
{
    /// <summary>
    /// Her sættes den "globale" connection string
    /// </summary>
    public static string ConnectionString = ConfigurationManager.ConnectionStrings["cmk_asp_nyhedssite"].ToString();
    /// <summary>
    /// Trim en string ned til en bestemt maksimum længde
    /// </summary>
    /// <param name="FullText">Den fulde tekst der skal klippes af</param>
    /// <param name="MaxLength">Antallet af tegn der skal trimmes ned til</param>
    /// <returns></returns>
    public static string EvalTrimmed(string FullText, int MaxLength)
    {
        // Teksten kan indeholde HTML tags, som ikke bør blive klippet over,
        // derfor fjernes alt der ligger imellem to <>
        FullText = Regex.Replace(FullText, "<.*?>", string.Empty);
        // hvis teksten stadig er længere end MaxLength
        if (FullText.Length > MaxLength)
        {
            // så returneres det ønskede antal tegn, plus tre ...
            return FullText.Substring(0, MaxLength - 3) + "..."; ;
        }
        // hvis teksten ikke er længere end MaxLength, returneres den den HTML frie tekst
        return FullText;
    }
}