using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSkillView : MonoBehaviour
{
    [SerializeField]private Transform _container;
    [SerializeField]private Button _skillBtnPrefab;
    [SerializeField]private TextMeshProUGUI _labelText;
    [SerializeField]private DescriptionBoxController _descriptionBox;
    private CharacterPresenter _characterPresenter;
    private List<Button> _skillButtons = new List<Button>();

    //Set required references
    public void InitializeView(CharacterPresenter myCharacterPresenter){
        _characterPresenter = myCharacterPresenter;
    }

    //Create skill buttons based on the list of skills the character has
    public void InstantiateSkillButtons(List<SkillAction> skills, CharacterModel skillOwner){
        _labelText.text = skillOwner.name + ":";
        foreach(SkillAction skill in skills){
            Button newSkillBtn = Instantiate(_skillBtnPrefab, _container);
            newSkillBtn.image.sprite = skill.Icon;
            SetSkill(newSkillBtn, skill, skillOwner);
            SetOnHoverDescriptionBox(newSkillBtn, skill);
            _skillButtons.Add(newSkillBtn);
        }
    }

    //Set skills to execute on the button
    public void SetSkill(Button btn, SkillAction action, CharacterModel skillOwner){
        btn.onClick.AddListener(() => {
            CharacterModel target = TargetManager.SelectedEnemyTarget;
            ActionManager.instance.AddAction(async () => {
                await action.Execute(skillOwner, () => TargetManager.GetTargetOrAvailableTarget(target));
            });
            _characterPresenter.OnSkillUsed();
        });
    }

    //Display the description on button hover
    public void SetOnHoverDescriptionBox(Button btn, SkillAction skillData){
        EventTrigger eventTrigger = btn.gameObject.AddComponent<EventTrigger>();
        btn.onClick.AddListener(() => ClearDescription());

        EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry(){eventID = EventTriggerType.PointerEnter};
        pointerEnterEntry.callback.AddListener((data) => { 
            float xOffset = btn.GetComponent<RectTransform>().rect.width*0.5f + _descriptionBox.GetComponent<RectTransform>().rect.width*0.5f;
            DisplayDescription(skillData, btn.transform.position+new Vector3(xOffset,0,0)); 
        });
        eventTrigger.triggers.Add(pointerEnterEntry);

        EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry(){eventID = EventTriggerType.PointerExit};
        pointerExitEntry.callback.AddListener((data) => { ClearDescription(); });
        eventTrigger.triggers.Add(pointerExitEntry);
    }

    void DisplayDescription(SkillAction skillData, Vector2 screenPos){
        string description = skillData.Description;
        //Change description to include the data about the skill
        description = AddStatusEffectInflictDescription(description);
        description = KeywordsStylizer.GetStylizedString(description);
        string dmgAmtToString = $"<color=red><size=120%><font=\"{KeywordsStylizer.slugfest_font}\">" + skillData.SkillDmgMultiplier.ToString() + "</font></size></color>";
        description = Regex.Replace(description, "/dmg/", dmgAmtToString);

        //Creating the description box
        _descriptionBox.Initialize(skillData.name, description, null);
        _descriptionBox.transform.position = screenPos;
        _descriptionBox.gameObject.SetActive(true);

        //Describe all the status effects to be inflicted by the skill
        string AddStatusEffectInflictDescription(string desc){
            foreach(var statusEffectData in skillData.SelfStatusEffects){
                desc += "\n"+"Inflicts "+statusEffectData.StatusEffect.Name+" x" + statusEffectData.StacksToApply.ToString() + " to self";
            }
            foreach(var statusEffectData in skillData.EnemyStatusEffects){
                desc += "\n"+"Inflicts "+statusEffectData.StatusEffect.Name+" x" + statusEffectData.StacksToApply.ToString() + " to target";
            }
            return desc;
        }
    }

    void ClearDescription(){
        _descriptionBox.gameObject.SetActive(false);
    }

    public void SetSkillsVisible(bool value){
        _container.gameObject.SetActive(value);
    }
}
