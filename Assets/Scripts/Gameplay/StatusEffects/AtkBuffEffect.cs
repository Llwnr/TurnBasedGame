using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AtkBuffEffect", menuName = "StatusEffects/AtkBuffEffect")]
public class AtkBuffEffect : StatsModificationEffect
{
    public float AtkBuff;
    public StatModifierType ModifierType;

    public override float GetStatModificationData(StatModifierType modType){
        if(modType == ModifierType) return AtkBuff;
        else return 0;
    }
    public override int DecreaseStacks(int stacks){
        stacks--;
        //Decrease the stacks then return the result
        return stacks;
    }
}
