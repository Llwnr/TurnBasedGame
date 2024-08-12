using System.Collections;
using System.Collections.Generic;
using LlwnrEventBus;
using UnityEngine;

public class PopupView : MonoBehaviour
{
    [SerializeField]bool debug;
    [SerializeField]private Canvas _canvas;
    [SerializeField]private PopupManager popupPrefab;
    EventBinding<OnDamageTakenEvent> _onDamageTaken;
    EventBinding<OnStatusEffectDamageTakenEvent> _onSEDamageTakenEvent;

    private void OnEnable() {
        _onDamageTaken = new EventBinding<OnDamageTakenEvent>(CreateDmgPopup);
        EventBus<OnDamageTakenEvent>.Register(_onDamageTaken);

        _onSEDamageTakenEvent = new EventBinding<OnStatusEffectDamageTakenEvent>(CreateDmgPopup);
        EventBus<OnStatusEffectDamageTakenEvent>.Register(_onSEDamageTakenEvent);
    }
    private void OnDisable() {
        EventBus<OnDamageTakenEvent>.Deregister(_onDamageTaken);
        EventBus<OnStatusEffectDamageTakenEvent>.Deregister(_onSEDamageTakenEvent);
    }

    void CreateDmgPopup(OnDamageTakenEvent eventData){
        PopupManager newPopup = Instantiate(popupPrefab, _canvas.transform, false);
        newPopup.transform.position = Camera.main.WorldToScreenPoint(eventData.HitCharacter.position);
        newPopup.Initialize(eventData.DamageAmt.ToString());
        DebugOut("Creating dmg popups by listening to OnDamageTakenEvent");
    }
    void CreateDmgPopup(OnStatusEffectDamageTakenEvent eventData){
        PopupManager newPopup = Instantiate(popupPrefab, _canvas.transform, false);
        newPopup.transform.position = Camera.main.WorldToScreenPoint(eventData.HitCharacter.position);
        newPopup.Initialize(eventData.DamageAmt.ToString());
        DebugOut("Creating dmg popups by listening to OnStEfDamageTakenEvent");
    }

    void DebugOut(object data){
        if(!debug) return;
        Debug.Log(data);
    }
}
