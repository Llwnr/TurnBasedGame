using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : CharacterModel
{
    protected override void Awake() {
        base.Awake();
    }

    public void QueueUpSkill(){
        _characterData.mySkills[Random.Range(0, _characterData.mySkills.Count-1)].Execute(this, TargetManager.SelectedPlayerTarget);
    }
}
