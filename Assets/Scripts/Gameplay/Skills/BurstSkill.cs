﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BurstSkill", menuName = "Skills/BurstSkill")]
public class BurstSkill : SkillAction
{
    public override void ExecuteSkill(CharacterModel skillUser, CharacterModel target){
        if(target.DealSkillDamage(skillUser.GetFinalDmgMod() * SkillDmgMultiplier)){
            InflictEffectsToTarget(target);
        }
    }
}
