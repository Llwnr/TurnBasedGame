using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : CharacterModel
{
    protected override void Awake() {
        base.Awake();
    }

    public void QueueUpSkill(){
        CharacterModel target = TargetManager.SelectedPlayerTarget;
        if(_characterData.MySkills.Count <= 0) return;
        ActionManager.instance.AddAction(() => {
            _characterData.MySkills[Random.Range(0, _characterData.MySkills.Count)].Execute(this, () => TargetManager.GetTargetOrAvailableTarget(target));
        });
    }
}
