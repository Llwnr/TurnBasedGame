using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicSkill", menuName = "Skills/BasicSkill")]
public class BasicSkill : SkillAction
{
    public float dmgMultiplier;

    public override void Execute(CharacterModel skillUser, CharacterModel target)
    {
        ActionManager.instance.AddPlayerAction(() => target.DealDamage(skillUser.GetBaseDmg() * dmgMultiplier));
    }
}
