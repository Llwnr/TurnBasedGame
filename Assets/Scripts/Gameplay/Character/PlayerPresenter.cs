using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenter : CharacterPresenter
{
    [SerializeField]protected CharacterSkillView _characterSkillView;
    protected override void Start()
    {
        base.Start();
        InitializeView();
    }

    void InitializeView(){
        _characterSkillView.InitializeView(this);
        List<SkillAction> mySkills = _characterModel.GetSkills();
        _characterSkillView?.InstantiateSkillButtons(mySkills, _characterModel);
    }

    public override void OnCharacterTurnStart()
    {
        _characterSkillView.SetSkillsVisible(true);
    }

    public override void OnCharacterTurnEnd()
    {
        _characterSkillView.SetSkillsVisible(false);
    }
}
