using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LlwnrEventBus;

public abstract class CharacterModel : MonoBehaviour
{
    [SerializeField]protected CharacterData _characterData;
    [SerializeField]protected StatusEffectManager _statusEffectManager;
    //For Health Management
    private HealthManager _healthManager;

    protected virtual void Awake() {
        InitializeHealthComponent();
    }

    void InitializeHealthComponent(){
        _healthManager = new HealthManager(_characterData.MaxHealth);
    }
    public void SubscribeToHealthChange(Action<float, float> action){
        _healthManager.OnHealthChanged += action;
    }
    public void UnsubscribeToHealthChange(Action<float, float> action){
        _healthManager.OnHealthChanged -= action;
    }

    //FOR SKILLS
    public List<SkillAction> GetSkills(){
        return _characterData.MySkills;
    }
    public bool DealDamage(float dmgAmt){
        _healthManager.DealDamage(dmgAmt);

        EventBus<OnDamageTakenEvent>.Raise(new OnDamageTakenEvent{
            HitCharacter = transform,
            CharacterModel = this,
            DamageAmt = dmgAmt,
            CurrentHealth = _healthManager.CurrentHealth,
            MaxHealth = _healthManager.MaxHealth,
        });
        //MANAGING WHEN A CHARACTER DIES
        if(_healthManager.CurrentHealth <= 0){
            gameObject.SetActive(false);
            NotifyDeath();
        }
        return true;//Returns true if damage is dealt successfully to this character
    }
    public void DealStatusEffectDamage(float dmgAmt){
        _healthManager.DealDamage(dmgAmt);

        EventBus<OnStatusEffectDamageTakenEvent>.Raise(new OnStatusEffectDamageTakenEvent{
            HitCharacter = transform,
            CharacterModel = this,
            DamageAmt = dmgAmt,
            CurrentHealth = _healthManager.CurrentHealth,
            MaxHealth = _healthManager.MaxHealth,
        });
        //MANAGING WHEN A CHARACTER DIES
        if(_healthManager.CurrentHealth <= 0){
            gameObject.SetActive(false);
            NotifyDeath();
        }
    }
    void NotifyDeath(){
        // Debug.Log("Firing 'OnDeathEvent'");
        EventBus<OnDeathEvent>.Raise(new OnDeathEvent{
            DiedCharacter = transform,
        });
    }

    //STATUS EFFECT PART
    public void InflictStatusEffect(StatusEffect statusEffect, int stacks){//Mostly to inflict SE when damage is not taken, like self buffs
        _statusEffectManager.InflictStatusEffect(statusEffect, stacks);
    }
    
    //DATA PART
    public float GetBaseDmg(){
        return 1;
    }
    public float GetFinalSpeed(){
        return _characterData.Speed;
    }
    public float GetHealth(){
        return _healthManager.CurrentHealth;
    }
}
