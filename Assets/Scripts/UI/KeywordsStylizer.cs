using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class KeywordsStylizer
{
    public static string slugfest_font = "good times rg SDF";
    public static Dictionary<string, string> stylizedReplacements = new Dictionary<string, string>(){
        {"Poison", $"<color=green><font=\"{slugfest_font}\">Poison</font></color>"},
        {"Burn", $"<color=orange><font=\"{slugfest_font}\">Burn</font></color>"},
        {"Rupture", $"<color=green><font=\"{slugfest_font}\">Rupture</font></color>"}
    };

    public static string GetStylizedString(string data){
        foreach(var kvp in stylizedReplacements){
            data = Regex.Replace(data, kvp.Key, kvp.Value);
        }
        return data;
    }

}
