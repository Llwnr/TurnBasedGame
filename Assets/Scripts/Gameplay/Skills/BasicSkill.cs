﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicSkill", menuName = "Skills/BasicSkill")]
public class BasicSkill : SkillAction
{
    public float skillDmgMultiplier;

    public override void ExecuteSkill(CharacterModel skillUser, CharacterModel target){
        if(target.DealDamage(skillUser.GetBaseDmg() * skillDmgMultiplier)){
            //To inflict status effect when damage is taken
            InflictEffectsToTarget(target);
        }
    }
}
