using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillAction : ScriptableObject
{
    public abstract void Execute(CharacterModel skillUser, CharacterModel target);
}
