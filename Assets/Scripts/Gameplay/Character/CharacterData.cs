using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterData", order = 0)]
public class CharacterData : ScriptableObject {
    [Header("Basic Information")]
    public string Name;
    public string Description;

    [Header("Core Stats")]
    public float MaxHealth;
    public CharacterStat CharacterStats;

    [Header("Skills")]
    public List<SkillAction> MySkills;
}
[Serializable]
public struct CharacterStat{
    public float Attack;
    public float Defense;
    public float Speed;


    public CharacterStat(float atk, float def, float spd){
        Attack = atk;
        Defense = def;
        Speed = spd;
    }

    public static CharacterStat operator +(CharacterStat stat1, CharacterStat stat2) {
        return new CharacterStat(stat1.Attack + stat2.Attack, stat1.Defense + stat2.Defense, stat1.Speed + stat2.Speed);
    }
}
