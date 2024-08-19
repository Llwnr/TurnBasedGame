using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicSkill", menuName = "Skills/BasicSkill")]
public class BasicSkill : SkillAction
{
    public override void ExecuteSkill(CharacterModel skillUser, CharacterModel target){
        if(target.DealDamage(skillUser.GetFinalDmgMod() * SkillDmgMultiplier)){
            //To inflict status effect when damage is taken
            InflictEffectsToTarget(target);
        }
    }
}
