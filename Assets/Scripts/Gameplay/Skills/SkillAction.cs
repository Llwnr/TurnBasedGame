using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillAction : ScriptableObject
{
    public List<StatusEffectData> SelfStatusEffects, EnemyStatusEffects;
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
        foreach(StatusEffectData statusEffectData in SelfStatusEffects){

        }
        foreach(StatusEffectData statusEffectData in EnemyStatusEffects){
            target.InflictStatusEffect(statusEffectData.StatusEffect, statusEffectData.StacksToApply);
        }
    }
}

[Serializable]
public class StatusEffectData{
    public StatusEffect StatusEffect;
    public int StacksToApply = 1;
}
