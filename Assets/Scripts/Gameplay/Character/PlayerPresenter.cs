using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenter : CharacterPresenter {
    [SerializeField] protected CharacterSkillView _characterSkillView;
    bool _isExecuting = false;

    protected override void Start() {
        base.Start();
        InitializeView();
    }

    void InitializeView() {
        _characterSkillView.InitializeView(this);
        List<SkillAction> mySkills = _characterModel.GetSkills();
        _characterSkillView?.InstantiateSkillButtons(mySkills);
        _characterSkillView?.SetLabel(name);
    }

    public override void OnCharacterTurnStart() {
        _characterSkillView.SetSkillsVisible(true);
        TurnManager.instance.StopTime(true);
    }

    public override void OnCharacterTurnEnd() {
        _characterSkillView.SetSkillsVisible(false);
        TurnManager.instance.StopTime(false);
    }
    public async void ExecuteSkill(SkillAction skillAction) {
        if (_isExecuting) return;
        _isExecuting = true;
        CharacterModel target = TargetManager.SelectedEnemyTarget;
        TurnManager.instance.StopTime(true);
        await skillAction.Execute(_characterModel, () => TargetManager.GetTargetOrAvailableTarget(target));
        OnSkillUsed();
        TurnManager.instance.StopTime(false);
        _isExecuting = false;
    }

}
