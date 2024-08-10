using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LlwnrEventBus;

public abstract class CharacterModel : MonoBehaviour
{
    [SerializeField]protected CharacterData _characterData;
    //For Health Management
    private HealthManager _healthManager;

    protected virtual void Awake() {
        InitializeHealthComponent();
    }

    void InitializeHealthComponent(){
        _healthManager = new HealthManager(_characterData.maxHealth);
    }

    //FOR SKILLS
    public List<SkillAction> GetSkills(){
        return _characterData.mySkills;
    }
    public void DealDamage(float dmgAmt){
        _healthManager.DealDamage(dmgAmt);
        Debug.Log("Firing 'OnDamageTakenEvent'");
        EventBus<OnDamageTakenEvent>.Raise(new OnDamageTakenEvent{
            HitCharacter = transform,
            DamageAmt = dmgAmt,
            CurrentHealth = _healthManager.CurrentHealth,
            MaxHealth = _healthManager.MaxHealth,
        });
    }
    
    //DATA PART
    public float GetBaseDmg(){
        return 1;
    }
    public float GetFinalSpeed(){
        return _characterData.speed;
    }
    public float GetHealth(){
        return _healthManager.CurrentHealth;
    }
}
