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
        _healthManager = new HealthManager(_characterData.MaxHealth);
    }

    //FOR SKILLS
    public List<SkillAction> GetSkills(){
        return _characterData.MySkills;
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
        //MANAGING WHEN A CHARACTER DIES
        if(_healthManager.CurrentHealth <= 0){
            gameObject.SetActive(false);
            NotifyDeath();
        }
        
        
    }
    void NotifyDeath(){
        Debug.Log("Firing 'OnDeathEvent'");
        EventBus<OnDeathEvent>.Raise(new OnDeathEvent{
            DiedCharacter = transform,
        });
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
