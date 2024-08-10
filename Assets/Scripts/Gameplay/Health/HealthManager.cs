using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager
{
    public float CurrentHealth{get; private set;}
    public float MaxHealth{get; private set;}
    public HealthManager(float maxHealth){
        this.CurrentHealth = maxHealth;
        this.MaxHealth = maxHealth;
    }

    public void DealDamage(float dmgAmt){
        CurrentHealth -= dmgAmt;
        Debug.Log(" health: " + CurrentHealth);
    }
}
