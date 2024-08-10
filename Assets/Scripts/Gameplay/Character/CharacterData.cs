using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterData", order = 0)]
public class CharacterData : ScriptableObject {
    [Header("Basic Information")]
    public string Name;
    public string Description;

    [Header("Core Stats")]
    public float MaxHealth;
    public float Speed;

    [Header("Skills")]
    public List<SkillAction> MySkills;
}
