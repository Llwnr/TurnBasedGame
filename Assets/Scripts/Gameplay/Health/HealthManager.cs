using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HealthManager
{
    public float CurrentHealth{get; private set;}
    public float MaxHealth{get; private set;}

    public event Action<float, float> OnHealthChanged;

    public HealthManager(float maxHealth){
        this.CurrentHealth = maxHealth;
        this.MaxHealth = maxHealth;
    }

    public void DealDamage(float dmgAmt){
        CurrentHealth -= dmgAmt;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }
}
