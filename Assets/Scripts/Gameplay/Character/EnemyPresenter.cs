using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPresenter : CharacterPresenter
{
    private EnemyModel _enemyModel;

    protected override void Start() {
        base.Start();
        _enemyModel = _characterModel as EnemyModel;
    }

    public override void OnCharacterTurnStart()
    {
        _enemyModel.QueueUpSkill();
        TurnManager.instance.NextCharactersTurn();
    }
    public override void OnCharacterTurnEnd()
    {
        
    }
}
