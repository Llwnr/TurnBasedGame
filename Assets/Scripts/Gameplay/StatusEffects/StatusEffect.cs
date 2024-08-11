using System.Collections;
using System.Collections.Generic;
using LlwnrEventBus;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    public StatusEffectTrigger TriggerType;
    public abstract void Execute(CharacterModel targetModel, int stacks);
    public abstract int DecreaseStacks(int stacks);
}
