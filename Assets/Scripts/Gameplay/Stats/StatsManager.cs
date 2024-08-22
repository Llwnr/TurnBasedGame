using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This Class will get the final stats of the character, calculating buffs debuffs from status effects
*/
public static class StatsManager
{
    /// <summary>
    /// Calculates the final character stats, optionally including linked stat buffs.
    /// </summary>
    /// <param name="characterModel">The character model to calculate stats for.</param>
    /// <param name="applyLinkedBuffs">Whether to include linked stat buffs in the calculation. Defaults to false.</param>
    /// <returns>The final calculated character stats.</returns>
    public static CharacterStat GetFinalData(CharacterModel characterModel, bool applyLinkedBuffs = true){
        // Get the character's base stats.
        CharacterStat baseStats = characterModel.GetBaseStatsData();
        // Initialize the final stats with the base stats.
        CharacterStat finalStats = baseStats;

        // Add stat modifier effects (buffs/debuffs from status effects).
        finalStats += GetStatModifierEffects(characterModel);

        // If applyLinkedBuffs is true, add linked stat buffs.
        if (applyLinkedBuffs && characterModel.GetType() == typeof(PlayerModel)){
            finalStats += GetLinkedStatBuff(characterModel);
        }

        // Return the final calculated stats.
        return finalStats;
    }

    /// <summary>
    /// Retrieves the stat buffs provided by linked characters.
    /// </summary>
    /// <param name="characterModel">The character model to retrieve linked stat buffs for.</param>
    /// <returns>The character stats representing the linked stat buffs.</returns>
    static CharacterStat GetLinkedStatBuff(CharacterModel characterModel){
        if(characterModel.GetType() != typeof(PlayerModel))return characterModel.GetBaseStatsData();
        // Delegate the retrieval of linked stat buffs to the StatLinkerModel.
        return ((PlayerModel)characterModel).GetStatLinkerModel().GetLinkedStatBuff();
    }

    /// <summary>
    /// Calculates the stat modifications from status effects.
    /// </summary>
    /// <param name="characterModel">The character model to calculate stat modifications for.</param>
    /// <returns>The character stats representing the stat modifications from status effects.</returns>
    static CharacterStat GetStatModifierEffects(CharacterModel characterModel){
        // Get the StatusEffectManager from the character model.
        StatusEffectManager statusEffectManager = characterModel.GetStatusEffectManager();

        // Get the list of stat modifiers from the StatusEffectManager.
        List<StatusEffectData> data = statusEffectManager.GetStatModifiers();

        // Initialize variables to track attack and defense modifications.
        float dmgMod = 0;
        float defMod = 0;

        // Iterate through the stat modifiers and accumulate the attack and defense modifications.
        foreach (StatusEffectData statModifier in data){
            // Cast the StatusEffect to StatsModificationEffect.
            var statMod = statModifier.StatusEffect as StatsModificationEffect;

            // Add the attack and defense modifications from the StatsModificationEffect.
            dmgMod += statMod.GetStatModificationData(StatModifierType.Attack);
            defMod += statMod.GetStatModificationData(StatModifierType.Defense);
        }

        // Create a new CharacterStat object with the calculated attack and defense modifications.
        CharacterStat characterStat = new CharacterStat(){
            Attack = dmgMod,
            Defense = defMod
        };

        // Return the character stats representing the stat modifications.
        return characterStat;
    }
}
