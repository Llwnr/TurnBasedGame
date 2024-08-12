using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillAction : ScriptableObject
{
    public List<EffectInflictData> SelfStatusEffects, EnemyStatusEffects;
    [TextArea(3,10)]
    public string Description;
    public void Execute(CharacterModel skillUser, Func<CharacterModel> targetFinder){
        //HANDLE WHETHER SKILL IS EXECUTABLE OR NOT FIRST, THEN EXECUTE SKILL
        if(!skillUser.gameObject.activeSelf) return;
        else{
            CharacterModel target = targetFinder.Invoke();
            ExecuteSkill(skillUser, target);
            InflictEffects(skillUser, target);
        }
    }
    public abstract void ExecuteSkill(CharacterModel skillUser, CharacterModel target);

    void InflictEffects(CharacterModel skillUser, CharacterModel target){
        foreach(EffectInflictData statusEffectData in SelfStatusEffects){
            skillUser.InflictStatusEffect(statusEffectData.StatusEffect, statusEffectData.StacksToApply);
        }
        foreach(EffectInflictData statusEffectData in EnemyStatusEffects){
            target.InflictStatusEffect(statusEffectData.StatusEffect, statusEffectData.StacksToApply);
        }
    }
}

[Serializable]
//This class will define which status effect to inflict, and how much stacks to inflict. Its use is finished once the skill inflicts the status effect
public class EffectInflictData{
    public StatusEffect StatusEffect;
    public int StacksToApply = 1;
}
