using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : CharacterModel
{
    protected override void Awake() {
        base.Awake();
    }

    public void QueueUpSkill(){
        return;
        CharacterModel target = TargetManager.SelectedPlayerTarget;
        ActionManager.instance.AddAction(() => {
            _characterData.MySkills[Random.Range(0, _characterData.MySkills.Count)].Execute(this, () => TargetManager.GetTargetOrAvailableTarget(target));
        });
    }
}
