using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    public abstract void Execute();
    /*
    Deal fire damage to the one who has this status effect
    Reduce the stacks by 50%
    */
}
