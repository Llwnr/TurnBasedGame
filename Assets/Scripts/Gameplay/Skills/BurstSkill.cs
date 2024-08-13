using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BurstSkill", menuName = "Skills/BurstSkill")]
public class BurstSkill : SkillAction
{
    public float skillDmgMultiplier;

    public override void ExecuteSkill(CharacterModel skillUser, CharacterModel target)
    {
        if(target.DealDamage(skillUser.GetBaseDmg() * skillDmgMultiplier)){
            InflictEffectsToTarget(target);
        }
    }
}
