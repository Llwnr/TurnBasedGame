using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterData", order = 0)]
public class CharacterData : ScriptableObject {
    [Header("Basic Information")]
    public new string name;
    public string description;

    [Header("Core Stats")]
    public float maxHealth;
    public float speed;

    [Header("Skills")]
    public List<SkillAction> mySkills;
}
