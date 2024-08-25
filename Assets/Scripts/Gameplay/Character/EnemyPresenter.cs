using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
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
        TurnManager.instance.StopTime(true);
        ExecuteSkill();
    }
    public override void OnCharacterTurnEnd()
    {
        TurnManager.instance.StopTime(false);
    }
    async void ExecuteSkill(){
        List<SkillAction> mySkills = _enemyModel.GetSkills();
        if(mySkills.Count < 0) return;
        CharacterModel opponentTarget = TargetManager.SelectedPlayerTarget;
        await mySkills[Random.Range(0, mySkills.Count)].Execute(_enemyModel, () => TargetManager.GetTargetOrAvailableTarget(opponentTarget));
        await Task.Delay(300);
        OnSkillUsed();
    }
}
