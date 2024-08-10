using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillAction : ScriptableObject
{
    public List<StatusEffect> SelfStatusEffects, EnemyStatusEffects;
    public void Execute(CharacterModel skillUser, Func<CharacterModel> target){
        //HANDLE WHETHER SKILL IS EXECUTABLE OR NOT FIRST, THEN EXECUTE SKILL
        if(!skillUser.gameObject.activeSelf) return;
        else{
            ExecuteSkill(skillUser, target);
        }
    }
    public abstract void ExecuteSkill(CharacterModel skillUser, Func<CharacterModel> target);
}
