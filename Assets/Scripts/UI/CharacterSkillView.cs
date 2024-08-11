using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSkillView : MonoBehaviour
{
    [SerializeField]private Transform _container;
    [SerializeField]private Button _skillBtnPrefab;
    private CharacterPresenter _characterPresenter;
    private List<Button> _skillButtons = new List<Button>();

    //Set required references
    public void InitializeView(CharacterPresenter myCharacterPresenter){
        _characterPresenter = myCharacterPresenter;
    }

    //Create skill buttons based on the list of skills the character has
    public void InstantiateSkillButtons(List<SkillAction> skills, CharacterModel skillOwner){
        foreach(SkillAction skill in skills){
            Button newSkillBtn = Instantiate(_skillBtnPrefab, _container);
            newSkillBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = skill.name;
            SetSkill(newSkillBtn, skill, skillOwner);
            _skillButtons.Add(newSkillBtn);
        }
    }

    //Set skills to execute on the button
    public void SetSkill(Button btn, SkillAction action, CharacterModel skillOwner){
        btn.onClick.AddListener(() => {
            CharacterModel target = TargetManager.SelectedEnemyTarget;
            ActionManager.instance.AddAction(() => action.Execute(skillOwner, () => TargetManager.GetTargetOrAvailableTarget(target)));
            _characterPresenter.OnSkillUsed();
        });
    }

    public void SetSkillsVisible(bool value){
        _container.gameObject.SetActive(value);
    }
}
