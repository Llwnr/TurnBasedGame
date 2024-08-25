using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
    private PlayerPresenter _playerPresenter;
    private List<Button> _skillButtons = new List<Button>();

    //Set required references
    public void InitializeView(PlayerPresenter myCharacterPresenter){
        _playerPresenter = myCharacterPresenter;
    }

    public void SetLabel(string characterName){
        _labelText.text = characterName;
    }

    //Create skill buttons based on the list of skills the character has
    public void InstantiateSkillButtons(List<SkillAction> skills){
        foreach(SkillAction skill in skills){
            Button newSkillBtn = Instantiate(_skillBtnPrefab, _container);
            newSkillBtn.image.sprite = skill.Icon;
            SetSkill(newSkillBtn, skill);
            SetOnHoverDescriptionBox(newSkillBtn, skill);
            _skillButtons.Add(newSkillBtn);
        }
    }

    //Set skills to execute on the button
    public void SetSkill(Button btn, SkillAction action){
        btn.onClick.AddListener(() => _playerPresenter.ExecuteSkill(action));
    }

    //Display the description box on skill button hover
    public void SetOnHoverDescriptionBox(Button btn, SkillAction skillData){
        EventTrigger btnEventTrigger = btn.gameObject.AddComponent<EventTrigger>();
        btn.onClick.AddListener(() => ClearDescription());

        EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry(){eventID = EventTriggerType.PointerEnter};
        pointerEnterEntry.callback.AddListener((data) => { 
            float xOffset = btn.GetComponent<RectTransform>().rect.width*0.5f + _descriptionBox.GetComponent<RectTransform>().rect.width*0.5f;
            float yOffset = btn.GetComponent<RectTransform>().rect.height*0.5f + _descriptionBox.GetComponent<RectTransform>().rect.height*0.5f;
            // Child of btn so hover works across both btn and its descriptionBox.
            _descriptionBox.transform.SetParent(btn.transform, false);
            DisplayDescription(skillData, btn.transform.position+new Vector3(0,yOffset,0)); 
        });
        btnEventTrigger.triggers.Add(pointerEnterEntry);

        EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry(){eventID = EventTriggerType.PointerExit};
        pointerExitEntry.callback.AddListener((data) => { ClearDescription(); });
        btnEventTrigger.triggers.Add(pointerExitEntry);
    }

    // Displays the description box with the given skill data.
    void DisplayDescription(SkillAction skillData, Vector2 screenPos){
        string description = skillData.Description;
        // Add descriptions for status effects inflicted by the skill.
        description = AddStatusEffectInflictDescription(description);
        // Stylize the description using KeywordsDescriptionStylizer.
        description = KeywordsDescriptionStylizer.GetStylizedString(description);
        // Replace "/dmg/" in the description with the skill's damage multiplier.
        string dmgAmtToString = $"<color=red><size=120%><font=\"{KeywordsDescriptionStylizer.Goodtimes_font}\">" + skillData.SkillDmgMultiplier.ToString() + "</font></size></color>";
        description = Regex.Replace(description, "/dmg/", dmgAmtToString);

        // Initialize and display the description box.
        _descriptionBox.Initialize(skillData.name, description, null);
        _descriptionBox.transform.position = screenPos;
        _descriptionBox.gameObject.SetActive(true);

        //Make the description box not activate the parent when it(desc box is clicked)
        EventTrigger descClickEvent = _descriptionBox.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry(){eventID = EventTriggerType.PointerDown};
        pointerDownEntry.callback.AddListener((data) => {});
        descClickEvent.triggers.Add(pointerDownEntry);

        // Helper function to add descriptions for status effects.
        string AddStatusEffectInflictDescription(string desc){
            foreach (var statusEffectData in skillData.SelfStatusEffects){
                desc += "\n" + "Inflicts " + statusEffectData.StatusEffect.Name + " x" + statusEffectData.StacksToApply.ToString() + " to self";
            }
            foreach (var statusEffectData in skillData.EnemyStatusEffects){
                desc += "\n" + "Inflicts " + statusEffectData.StatusEffect.Name + " x" + statusEffectData.StacksToApply.ToString() + " to target";
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
