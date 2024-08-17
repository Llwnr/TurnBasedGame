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
    EventBinding<OnSkillDamageTakenEvent> _onDamageTaken;
    //To notify when a status effect is added, removed or changed
    public event Action<StatusEffectData> OnStatusEffectAdded, OnStatusEffectRemoved, OnStatusEffectUpdated;

    private void Awake() {
        _characterModel = GetComponent<CharacterModel>();

        // Register event bindings
        _onTurnStart = new EventBinding<OnTurnStart>((OnTurnStart turnStart) => ExecuteStatusEffects(StatusEffectTrigger.OnTurnStart));
        EventBus<OnTurnStart>.Register(_onTurnStart);
        _onDamageTaken = new EventBinding<OnSkillDamageTakenEvent>((OnSkillDamageTakenEvent onDamageTakenEvent) => {
            ExecuteStatusEffects(StatusEffectTrigger.OnDamageTakenEvent, onDamageTakenEvent.CharacterModel);
        });
        EventBus<OnSkillDamageTakenEvent>.Register(_onDamageTaken);
    }

    private void OnDestroy() {
        EventBus<OnTurnStart>.Deregister(_onTurnStart);
        EventBus<OnSkillDamageTakenEvent>.Deregister(_onDamageTaken);
    }

    public void InflictStatusEffect(StatusEffect statusEffect, int stacks){
        //If there is no key in dictionary, initialize
        if(!_statusEffectsDatas.ContainsKey(statusEffect.TriggerType)){
            _statusEffectsDatas[statusEffect.TriggerType] = new List<StatusEffectData>();
        }
        StatusEffectData myStatusEffect = FindStatusEffectByType(_statusEffectsDatas[statusEffect.TriggerType], statusEffect);
        bool dataExists = myStatusEffect != null;
        //If the status effect doesn't exist in the dictionary, make a new one and add it
        if(myStatusEffect == null){
            myStatusEffect = new StatusEffectData{
                StatusEffect = statusEffect,
            };
            _statusEffectsDatas[statusEffect.TriggerType].Add(myStatusEffect);
        }
        myStatusEffect.AddStacks(stacks); 
        //Notify whether the status effect was added or updated
        if(dataExists){
            OnStatusEffectUpdated?.Invoke(myStatusEffect);
        }else{
            OnStatusEffectAdded?.Invoke(myStatusEffect);
        }
    }

    void ExecuteStatusEffects(StatusEffectTrigger trigger, CharacterModel targetModel = null){
        //// Check if this is the target model
        if(targetModel != null && targetModel != _characterModel) return;
        // Check if there are any status effects to execute for the trigger event
        if(!_statusEffectsDatas.ContainsKey(trigger)) return;

        //Finally, execute all the status effects in the respective trigger key
        for(int i=0; i<_statusEffectsDatas[trigger].Count; i++){
            StatusEffectData statusEffectData = _statusEffectsDatas[trigger][i];
            StatusEffect statusEffect = statusEffectData.StatusEffect;
            statusEffect.Execute(_characterModel, statusEffectData.GetStacks());
            //Reduce the stacks after executing the status effect
            int remainingStacks = statusEffect.DecreaseStacks(statusEffectData.GetStacks());
            statusEffectData.SetStacks(remainingStacks);
            OnStatusEffectUpdated?.Invoke(statusEffectData);
            if(remainingStacks <= 0){//If no stacks remain, remove the status effect and notify its removal
                RemoveStatusEffect(statusEffect, trigger);
                OnStatusEffectRemoved?.Invoke(statusEffectData);
                i--;
            }
        }
    }

    void RemoveStatusEffect(StatusEffect statusEffect, StatusEffectTrigger trigger){
        List<StatusEffectData> statusEffectDatas = _statusEffectsDatas[trigger];
        for(int i=0; i<statusEffectDatas.Count; i++){
            StatusEffectData statusEffectData = statusEffectDatas[i];
            if(statusEffectData.StatusEffect.GetType() == statusEffect.GetType()){
                statusEffectDatas.Remove(statusEffectData);
                i--;
            }
        }
    }

    // Finds a status effect by the type of trigger event that'll fire/execute it
    StatusEffectData FindStatusEffectByType(List<StatusEffectData> statusEffectDatas, StatusEffect targetStatusEffect){
        foreach(var statusEffectData in statusEffectDatas){
            if(statusEffectData.StatusEffect.GetType() == targetStatusEffect.GetType()) return statusEffectData;
        }
        return null;
    }
    
    // Gets all status effects on the character
    public List<StatusEffectData> GetMyStatusEffects(){
        List<StatusEffectData> statusEffects = new List<StatusEffectData>();
        foreach(StatusEffectTrigger trigger in Enum.GetValues(typeof(StatusEffectTrigger))){
            if (_statusEffectsDatas.TryGetValue(trigger, out List<StatusEffectData> effects)) {
                statusEffects.AddRange(effects);
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
