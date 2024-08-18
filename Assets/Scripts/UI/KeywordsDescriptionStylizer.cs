using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public static class KeywordsDescriptionStylizer
{
    public static string Goodtimes_font = "good times rg SDF";
    public static Dictionary<string,string> StatusEffectsDescription = new Dictionary<string,string>();
    public static Dictionary<string, string> StylizedReplacements = new Dictionary<string, string>(){
        {"Poison", $"<color=green><font=\"{Goodtimes_font}\">Poison</font></color>"},
        {"Burn", $"<color=orange><font=\"{Goodtimes_font}\">Burn</font></color>"},
        {"Rupture", $"<color=green><font=\"{Goodtimes_font}\">Rupture</font></color>"}
    };

    static KeywordsDescriptionStylizer(){
        SetKeywordsDescription();
    }
    //Populate status effects description dictionary
    [InitializeOnEnterPlayMode]
    public static void SetKeywordsDescription(){
        StatusEffectsDescription.Clear();
        StatusEffect[] allStatusEffects = Resources.LoadAll<StatusEffect>("ScriptableObjects/StatusEffects");
        Debug.Log("Mapping status effects name to description");
        foreach(StatusEffect statusEffect in allStatusEffects){
            StatusEffectsDescription.Add(statusEffect.Name, statusEffect.Description);
        }
    }

    public static string GetStylizedString(string data){
        foreach(var kvp in StylizedReplacements){
            data = Regex.Replace(data, kvp.Key, kvp.Value);
        }
        return data;
    }
    public static string GetStatusEffectsDescription(StatusEffect statusEffect){
        return statusEffect.Description;
    }
}
