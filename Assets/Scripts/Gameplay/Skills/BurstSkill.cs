using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BurstSkill", menuName = "Skills/BurstSkill")]
public class BurstSkill : SkillAction
{
    public float dmgMultiplier;

    public override void Execute(CharacterModel skillUser, CharacterModel target)
    {
        ActionManager.instance.AddPlayerAction(() => {
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
            
            Debug.Log("Dealing burst dmg: " + dmgMultiplier);
        });
    }
}
