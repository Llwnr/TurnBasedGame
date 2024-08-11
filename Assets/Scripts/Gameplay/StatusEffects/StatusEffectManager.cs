using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LlwnrEventBus;
using UnityEngine;

[RequireComponent(typeof(CharacterModel))]
public class StatusEffectManager : MonoBehaviour
{
    [SerializeField]Dictionary<StatusEffect, int> _statusEffects = new Dictionary<StatusEffect, int>();
    CharacterModel _characterModel;
    //Manage various status effects execution causes
    //For example, when a turn starts, execute the _statusEffects related to it
    public event Action OnTurnStart = delegate {};
    EventBinding<OnTurnStart> _onTurnStart;

    private void Awake() {
        _characterModel = GetComponent<CharacterModel>();
    }

    private void OnEnable() {
        _onTurnStart = new EventBinding<OnTurnStart>(ExecuteOnTurnStart);
        EventBus<OnTurnStart>.Register(_onTurnStart);
    }
    private void OnDisable() {
        EventBus<OnTurnStart>.Deregister(_onTurnStart);
    }

    public void InflictStatusEffect(StatusEffect statusEffect, int stacks){
        Debug.Log("Applying status effect");
        if(_statusEffects.ContainsKey(statusEffect)){
            _statusEffects[statusEffect] += stacks;
        }else{
            _statusEffects.Add(statusEffect, stacks);
        }
    }

    public void ExecuteOnTurnStart(){
        foreach(var statusEffect in _statusEffects.Keys){
            if(statusEffect.TriggerType == StatusEffectTrigger.OnTurnStart){
                int stacks = _statusEffects[statusEffect];
                statusEffect.Execute(_characterModel, stacks);
                Debug.Log("Executing " + statusEffect.name + " Stacks: " + stacks);
                if(statusEffect.DecreaseStacks(stacks) <= 0){
                    RemoveStatusEffect(statusEffect);
                }
            }
        }
    }

    async void RemoveStatusEffect(StatusEffect statusEffect){
        await Task.Yield();
        _statusEffects.Remove(statusEffect);
    }
}


//NEW ENUM TO KNOW WHEN A STATUS EFFECT SHOULD TRIGGER
public enum StatusEffectTrigger{
    OnTurnStart,
}
