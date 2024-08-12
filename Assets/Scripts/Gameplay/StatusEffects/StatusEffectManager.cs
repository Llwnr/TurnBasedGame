using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using LlwnrEventBus;
using UnityEngine;

[RequireComponent(typeof(CharacterModel))]
public class StatusEffectManager : MonoBehaviour
{
    Dictionary<StatusEffectTrigger, List<StatusEffectData>> _statusEffectsDatas = new Dictionary<StatusEffectTrigger, List<StatusEffectData>>();
    CharacterModel _characterModel;
    //Manage various status effects execution causes
    //For example, when a turn starts, execute the _statusEffects related to it
    EventBinding<OnTurnStart> _onTurnStart;
    EventBinding<OnDamageTakenEvent> _onDamageTaken;

    private void Awake() {
        _characterModel = GetComponent<CharacterModel>();

        _onTurnStart = new EventBinding<OnTurnStart>(() => ExecuteStatusEffects(StatusEffectTrigger.OnTurnStart));
        EventBus<OnTurnStart>.Register(_onTurnStart);
        _onDamageTaken = new EventBinding<OnDamageTakenEvent>(() => ExecuteStatusEffects(StatusEffectTrigger.OnDamageTakenEvent));
        EventBus<OnDamageTakenEvent>.Register(_onDamageTaken);
    }

    private void OnDestroy() {
        EventBus<OnTurnStart>.Deregister(_onTurnStart);
        EventBus<OnDamageTakenEvent>.Deregister(_onDamageTaken);
    }

    public void InflictStatusEffect(StatusEffect statusEffect, int stacks){
        
        //If there is no key in dictionary, initialize
        if(!_statusEffectsDatas.ContainsKey(statusEffect.TriggerType)){
            _statusEffectsDatas[statusEffect.TriggerType] = new List<StatusEffectData>();
        }
        
        StatusEffectData myStatusEffect = GetExistingStatusEffect(_statusEffectsDatas[statusEffect.TriggerType], statusEffect);
        //If the status effect doesn't exist in the dictionary, make a new one and add it
        if(myStatusEffect == null){
            myStatusEffect = new StatusEffectData{
                StatusEffect = statusEffect,
            };
            myStatusEffect.AddStacks(stacks);
            _statusEffectsDatas[statusEffect.TriggerType].Add(myStatusEffect);
        }else{
            myStatusEffect = GetExistingStatusEffect(_statusEffectsDatas[statusEffect.TriggerType], statusEffect);
            //Add the status effect data to the dictionary based on its trigger type
            if(myStatusEffect == null){
                myStatusEffect = new StatusEffectData{
                    StatusEffect = statusEffect,
                };
            }
            myStatusEffect.AddStacks(stacks);
        }
        
    }

    void ExecuteStatusEffects(StatusEffectTrigger trigger){
        if(!_statusEffectsDatas.ContainsKey(trigger)) return;
        foreach(var statusEffectData in _statusEffectsDatas[trigger]){
            StatusEffect statusEffect = statusEffectData.StatusEffect;
            statusEffect.Execute(_characterModel, statusEffectData.GetStacks());
            if(statusEffect.DecreaseStacks(statusEffectData.GetStacks()) <= 0){
                RemoveStatusEffect(statusEffect, trigger);
            }
        }
    }

    async void RemoveStatusEffect(StatusEffect statusEffect, StatusEffectTrigger trigger){
        await Task.Yield();
        RemoveStatusEffect(_statusEffectsDatas[trigger], statusEffect);
    }
    void RemoveStatusEffect(List<StatusEffectData> statusEffectDatas, StatusEffect targetStatusEffect){
        for(int i=0; i<statusEffectDatas.Count; i++){
            StatusEffectData statusEffectData = statusEffectDatas[i];
            if(statusEffectData.StatusEffect.GetType() == targetStatusEffect.GetType()){
                statusEffectDatas.Remove(statusEffectData);
                i--;
            }
        }
    }

    //Will return the StatusEffectData object that contains the targetted statusEffect.
    StatusEffectData GetExistingStatusEffect(List<StatusEffectData> statusEffectDatas, StatusEffect targetStatusEffect){
        foreach(var statusEffectData in statusEffectDatas){
            if(statusEffectData.StatusEffect.GetType() == targetStatusEffect.GetType()) return statusEffectData;
        }
        return null;
    }
    
    public List<StatusEffectData> GetMyStatusEffects(){
        List<StatusEffectData> statusEffects = new List<StatusEffectData>();
        foreach(StatusEffectTrigger trigger in Enum.GetValues(typeof(StatusEffectTrigger))){
            if(!_statusEffectsDatas.ContainsKey(trigger)) continue;
            foreach(StatusEffectData statusEffectData in _statusEffectsDatas[trigger]){
                statusEffects.Add(statusEffectData);
            }
        }

        return statusEffects;
    }
}


//NEW ENUM TO KNOW WHEN A STATUS EFFECT SHOULD TRIGGER
public enum StatusEffectTrigger{
    OnTurnStart,
    OnDamageTakenEvent
}
