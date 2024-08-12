using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BurnEffect", menuName = "StatusEffects/BurnEffect")]
public class BurnEffect : StatusEffect
{
    public float burnDmg;

    public override void Execute(CharacterModel targetModel, int stacks)
    {
        targetModel.DealStatusEffectDamage(burnDmg*stacks);
        Debug.Log("Burn dmg dealt: " + burnDmg*stacks);
    }

    public override int DecreaseStacks(int stacks)
    {
        //Decrease the stacks then return the result
        return 0;
    }
}
