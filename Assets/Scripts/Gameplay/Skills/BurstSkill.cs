using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BurstSkill", menuName = "Skills/BurstSkill")]
public class BurstSkill : SkillAction
{
    public float dmgMultiplier;

    public override void ExecuteSkill(CharacterModel skillUser, CharacterModel target)
    {
        //THIS SKILL IS AOE SO:
        if(target is PlayerModel){
            foreach(CharacterModel characterModel in TargetManager.GetAllPlayerModels()){
                characterModel.DealDamage(skillUser.GetBaseDmg() * dmgMultiplier);
            }
        }else{
            foreach(CharacterModel characterModel in TargetManager.GetAllEnemyModels()){
                characterModel.DealDamage(skillUser.GetBaseDmg() * dmgMultiplier);
            }
        }
    }
}
