using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LlwnrEventBus;

public abstract class CharacterModel : MonoBehaviour, IDamagable
{
    [SerializeField]protected CharacterData _characterData;
    [SerializeField]protected StatusEffectManager _statusEffectManager;
    
    //For Health Management
    private HealthManager _healthManager;
    private ActionPointsManager _actionPointsManager;

    protected virtual void Awake() {
        InitializeSprite();
        InitializeHealthComponent();
        InitializeActionPointsComponent();
    }

    void InitializeSprite(){
        GetComponent<SpriteRenderer>().sprite = GetSprite();
    }
    void InitializeHealthComponent(){
        _healthManager = new HealthManager(_characterData.MaxHealth);
    }
    void InitializeActionPointsComponent(){
        _actionPointsManager = new ActionPointsManager(_characterData.ActionThreshold, this);
    }
    public void SubscribeToHealthChange(Action<float, float> action){
        _healthManager.OnHealthChanged += action;
    }
    public void UnsubscribeToHealthChange(Action<float, float> action){
        _healthManager.OnHealthChanged -= action;
    }
    public void SubscribeToActionPointChange(Action<float, float, CharacterModel> action){
        _actionPointsManager.OnActionPointsChanged += action;
    }
    public void UnsubscribeToActionPointChange(Action<float, float, CharacterModel> action){
        _actionPointsManager.OnActionPointsChanged -= action;
    }

    //FOR TIME RELATED ACTIONS
    public void IncreaseActionPoints() => _actionPointsManager.IncreaseActionPoints(GetFinalSpeed());

    //To know if the player can act
    public bool CanAct(){
        return _actionPointsManager.HasAp();
    }
    public void SkillUsed(){
        _actionPointsManager.ReduceAp();
    }

    //FOR SKILLS
    public virtual bool DealSkillDamage(float dmgAmt, bool dealLinkedDmg = true){
        _healthManager.DealDamage(dmgAmt);
        RaiseSkillDmgTakenEvent(dmgAmt);
        DeathOnEmptyHealth();
        
        return true;//Returns true if damage is dealt successfully to this character
    }
    public void DealStatusEffectDamage(StatusEffect statusEffect, float dmgAmt){
        _healthManager.DealDamage(dmgAmt);
        RaiseStatusEffectDmgTakenEvent(statusEffect, dmgAmt);
        DeathOnEmptyHealth();
    }
    protected void RaiseSkillDmgTakenEvent(float dmgAmt){
        EventBus<OnSkillDamageTakenEvent>.Raise(new OnSkillDamageTakenEvent{
            HitCharacter = transform,
            CharacterModel = this,
            DamageAmt = dmgAmt,
            CurrentHealth = _healthManager.CurrentHealth,
            MaxHealth = _healthManager.MaxHealth,
        });
    }
    protected void RaiseStatusEffectDmgTakenEvent(StatusEffect statusEffect, float dmgAmt){
        EventBus<OnStatusEffectDamageTakenEvent>.Raise(new OnStatusEffectDamageTakenEvent{
            HitCharacter = transform,
            CharacterModel = this,
            DamageAmt = dmgAmt,
            CurrentHealth = _healthManager.CurrentHealth,
            MaxHealth = _healthManager.MaxHealth,
            StatusEffect = statusEffect,
        });
    }
    //MANAGING WHEN A CHARACTER DIES
    protected void DeathOnEmptyHealth(){
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

        Destroy(_statusEffectManager);
    }

    //STATUS EFFECT PART
    public void InflictStatusEffect(StatusEffect statusEffect, int stacks){
        _statusEffectManager.InflictStatusEffect(statusEffect, stacks);
    }
    
    //DATA PART
    public List<SkillAction> GetSkills(){
        return _characterData.MySkills;
    }
    public Sprite GetSprite(){
        return _characterData.Image;
    }
    public float GetBaseDmgMod(){
        return _characterData.CharacterStats.Attack;
    }
    public float GetFinalDmgMod(){
        return StatsManager.GetFinalData(this).Attack;
    }
    public float GetFinalSpeed(){
        return StatsManager.GetFinalData(this).Speed;
    }
    public float GetHealth(){
        return _healthManager.CurrentHealth;
    }
    public CharacterStat GetBaseStatsData(){
        return _characterData.CharacterStats;
    }
    public StatusEffectManager GetStatusEffectManager(){
        return _statusEffectManager;
    }
}
