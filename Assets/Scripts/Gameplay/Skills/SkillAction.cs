﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
//This class will define which status effect to inflict, and how much stacks to inflict. Its use is finished once the skill inflicts the status effect
public class EffectInflictData{
    public StatusEffect StatusEffect;
    public int StacksToApply = 1;
}

public abstract class SkillAction : ScriptableObject
{
    public List<EffectInflictData> SelfStatusEffects, EnemyStatusEffects;
    public bool isAoe = false;
    [TextArea(3,10)]
    public string Description;
    public void Execute(CharacterModel skillUser, Func<CharacterModel> targetFinder){
        //DONT EXECUTE SKILL IF THE SKILL USER IS DEAD, cuz dead ppl cant attack
        if(!skillUser.gameObject.activeSelf) return;
        CharacterModel target = targetFinder.Invoke();
        if(!isAoe){
            ExecuteSkill(skillUser, target);
            InflictSelfEffects(skillUser);
        }else{
            //THIS SKILL IS AOE SO:
            if(target is PlayerModel){
                foreach(CharacterModel characterModel in TargetManager.GetAllPlayerModels()){
                    ExecuteSkill(skillUser, characterModel);
                }
            }else{
                foreach(CharacterModel characterModel in TargetManager.GetAllEnemyModels()){
                    ExecuteSkill(skillUser, characterModel);
                }
            }
        }

    }
    public abstract void ExecuteSkill(CharacterModel skillUser, CharacterModel target);

    //This will apply to the user
    void InflictSelfEffects(CharacterModel skillUser){
        foreach(EffectInflictData statusEffectData in SelfStatusEffects){
            skillUser.InflictStatusEffect(statusEffectData.StatusEffect, statusEffectData.StacksToApply);
        }
    }
    //This us usually used to apply status effects to the target.. mostly when it is hit by this skill
    protected void InflictEffectsToTarget(CharacterModel target){
        foreach(EffectInflictData statusEffectData in EnemyStatusEffects){
            target.InflictStatusEffect(statusEffectData.StatusEffect, statusEffectData.StacksToApply);
        }
    }
}
