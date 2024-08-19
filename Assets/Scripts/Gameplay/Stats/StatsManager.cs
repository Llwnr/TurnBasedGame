using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This Class will get the final stats of the character, calculating buffs debuffs from status effects
*/
public static class StatsManager
{
    public static CharacterStatsData GetFinalData(CharacterStatsData baseData, StatusEffectManager statusEffectManager){
        CharacterStatsData finalCalculatedData = CharacterStatsData.Instantiate(baseData);
        List<StatusEffectData> data = statusEffectManager.GetStatModifiers();

        float dmgMod = 0;
        float defMod = 0;
        foreach (StatusEffectData statModifier in data){
            var statMod = statModifier.StatusEffect as StatsModificationEffect;
            dmgMod += statMod.GetStatModificationData(StatModifierType.Attack);
            defMod += statMod.GetStatModificationData(StatModifierType.Defense);
        }
        finalCalculatedData.AttackMultiplier += dmgMod;

        return finalCalculatedData;
    }
}
