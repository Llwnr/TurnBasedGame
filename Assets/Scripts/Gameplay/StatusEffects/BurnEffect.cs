using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BurnEffect", menuName = "StatusEffects/BurnEffect")]
public class BurnEffect : AnomalyEffect
{
    public float burnDmg;

    public override void Execute(CharacterModel targetModel, int stacks)
    {
        targetModel.DealStatusEffectDamage(this, burnDmg*stacks);
    }

    public override int DecreaseStacks(int stacks)
    {
        stacks = Mathf.FloorToInt(stacks*0.5f);
        //Decrease the stacks then return the result
        return stacks;
    }
}
