namespace TornOps.Helpers;

using System.Text.RegularExpressions;

public static class Icons
{
    // ---------- Flags (destinations) ----------
    private static readonly Dictionary<string, string> FlagByDestination =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["Argentina"] = "fl_argentina.png",
            ["Canada"] = "fl_canada.png",
            ["Cayman Islands"] = "fl_cayman_islands.png",
            ["China"] = "fl_china.png",
            ["Hawaii"] = "fl_hawaii.png",        // verify asset exists
            ["Japan"] = "fl_japan.png",
            ["Mexico"] = "fl_mexico.png",
            ["South Africa"] = "fl_south_africa.png",
            ["Switzerland"] = "fl_switzerland.png",
            ["Torn"] = "fl_torn.png",
            ["UAE"] = "fl_uae.png",
            ["UK"] = "fl_uk.png",
        };

    private const string FlagFallback = "fl_torn.png";

    private static readonly Dictionary<string, string> StatusByState =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["Hospital"] = "hospital.png",
            ["Jail"] = "jail.png",
            ["Traveling"] = "traveling.png",
            ["Abroad"] = "traveling.png", // same icon as Traveling
            ["Okay"] = "online.png"
        };

    /// <summary>Return a flag filename for a destination; falls back to a neutral flag.</summary>
    public static string FlagFor(string? destination)
    {
        if (string.IsNullOrWhiteSpace(destination)) return FlagFallback;

        var key = Normalize(destination);
        return FlagByDestination.TryGetValue(key, out var file) ? file : FlagFallback;
    }

    /// <summary>Return a status icon filename for a state; returns null when no icon is needed (e.g., Okay).</summary>
    public static string? StatusFor(string? state)
    {
        if (string.IsNullOrWhiteSpace(state)) return null;

        var key = Normalize(state);
        return StatusByState.TryGetValue(key, out var file) ? file : null;
    }

    // Collapse multiple spaces and drop periods so "U.A.E." == "UAE", "  United   Kingdom " == "United Kingdom"
    private static string Normalize(string s)
    {
        s = s.Trim();
        s = s.Replace(".", "");
        s = Regex.Replace(s, @"\s+", " ");
        return s;
    }

    // drug: 0–10m, 10m–1h, 1–3h, 3–6h, 6h+
    public static string DrugIcon(int seconds)
    {
        double minutes = seconds / 60.0;
        if (minutes < 1) return "drug_0.png";
        if (minutes <= 10) return "drug_1.png";
        if (minutes <= 60) return "drug_2.png";
        if (minutes <= 180) return "drug_3.png";
        if (minutes <= 360) return "drug_4.png";
        return "drug_5.png"; // > 6h
    }

    // medical: same as drug
    public static string MedicalIcon(int seconds)
    {
        double minutes = seconds / 60.0;
        if (minutes < 1) return "medical_0.png";
        if (minutes <= 10) return "medical_1.png";
        if (minutes <= 60) return "medical_2.png";
        if (minutes <= 180) return "medical_3.png";
        if (minutes <= 360) return "medical_4.png";
        return "medical_5.png"; // > 6h
    }

    // booster: 0–6h, 6–12h, 12–18h, 18–24h, 24h+
    public static string BoosterIcon(int seconds)
    {
        double hours = seconds / 3600.0;
        if (hours < 1) return "booster_0.png";
        if (hours <= 6) return "booster_1.png";
        if (hours <= 12) return "booster_2.png";
        if (hours <= 18) return "booster_3.png";
        if (hours <= 24) return "booster_4.png";
        return "booster_5.png"; // > 24h
    }
}
