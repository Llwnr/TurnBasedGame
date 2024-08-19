using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RuptureEffect", menuName = "StatusEffects/RuptureEffect")]
public class RuptureEffect : AnomalyEffect
{
    public float ruptureDmg;

    public override void Execute(CharacterModel targetModel, int stacks)
    {
        targetModel.DealStatusEffectDamage(this, ruptureDmg*stacks);
    }

    public override int DecreaseStacks(int stacks)
    {
        stacks = Mathf.FloorToInt(stacks*0.5f);
        //Decrease the stacks then return the result
        return stacks;
    }
}
