using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LlwnrEventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupView : MonoBehaviour
{
    [SerializeField]bool debug;
    [SerializeField]private Canvas _canvas;
    [SerializeField]private PopupPropertyManager popupPrefab;
    EventBinding<OnSkillDamageTakenEvent> _onDamageTaken;
    EventBinding<OnStatusEffectDamageTakenEvent> _onSEDamageTakenEvent;

    private void OnEnable() {
        _onDamageTaken = new EventBinding<OnSkillDamageTakenEvent>(CreateSkillDmgPopup);
        EventBus<OnSkillDamageTakenEvent>.Register(_onDamageTaken);

        _onSEDamageTakenEvent = new EventBinding<OnStatusEffectDamageTakenEvent>(CreateStatusEffectDmgPopup);
        EventBus<OnStatusEffectDamageTakenEvent>.Register(_onSEDamageTakenEvent);
    }
    private void OnDisable() {
        EventBus<OnSkillDamageTakenEvent>.Deregister(_onDamageTaken);
        EventBus<OnStatusEffectDamageTakenEvent>.Deregister(_onSEDamageTakenEvent);
    }

    void CreateSkillDmgPopup(OnSkillDamageTakenEvent eventData){
        CreatePopup(eventData.HitCharacter.position)
            .Initialize(eventData.DamageAmt.ToString(), null)
            .AnimatePopup(80, 0.5f, 1.2f);
        
    }
    async void CreateStatusEffectDmgPopup(OnStatusEffectDamageTakenEvent eventData){
        await Task.Delay(30);
        CreatePopup(eventData.HitCharacter.position)
            .Initialize(eventData.DamageAmt.ToString(), eventData.StatusEffect.Icon)
            .SetScale(0.9f).AnimatePopup(0, 0.7f, 1.2f);
    }

    PopupPropertyManager CreatePopup(Vector2 position){
        PopupPropertyManager newPopup = Instantiate(popupPrefab, _canvas.transform, false);
        newPopup.transform.SetParent(_canvas.transform, false);
        newPopup.transform.position = Camera.main.WorldToScreenPoint(position);
        return newPopup;
    }
}
