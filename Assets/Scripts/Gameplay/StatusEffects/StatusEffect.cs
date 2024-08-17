using System.Collections;
using System.Collections.Generic;
using LlwnrEventBus;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject{
    public StatusEffectTrigger TriggerType;
    public abstract void Execute(CharacterModel targetModel, int stacks);
    public abstract int DecreaseStacks(int stacks);
    public string Name;
    [TextArea(3,10)]
    public string Description;
    public Sprite Icon;
}

public class StatusEffectData{
    public StatusEffect StatusEffect;
    int _stacks = 0;

    public void AddStacks(int extraStacks){
        _stacks += extraStacks;
    }
    public void SetStacks(int stacks){
        _stacks = stacks;
    }
    public int GetStacks(){
        return _stacks;
    }
}
