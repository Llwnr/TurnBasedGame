using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicSkill", menuName = "Skills/BasicSkill")]
public class BasicSkill : SkillAction
{
    public float dmgMultiplier;

    public override void ExecuteSkill(CharacterModel skillUser, Func<CharacterModel> target){
        target.Invoke().DealDamage(skillUser.GetBaseDmg() * dmgMultiplier);
    }
}
