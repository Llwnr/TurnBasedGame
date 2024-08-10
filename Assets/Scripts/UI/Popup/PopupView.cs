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

    private void OnEnable() {
        _onDamageTaken = new EventBinding<OnDamageTakenEvent>(CreateDmgPopup);
        EventBus<OnDamageTakenEvent>.Register(_onDamageTaken);
    }
    private void OnDisable() {
        EventBus<OnDamageTakenEvent>.Deregister(_onDamageTaken);
    }

    void CreateDmgPopup(OnDamageTakenEvent eventData){
        PopupManager newPopup = Instantiate(popupPrefab, _canvas.transform, false);
        newPopup.transform.position = Camera.main.WorldToScreenPoint(eventData.HitCharacter.position);
        newPopup.SetText(eventData.DamageAmt.ToString());
        DebugOut("Creating dmg popups by listening to OnDamageTakenEvent");
    }

    void DebugOut(object data){
        if(!debug) return;
        Debug.Log(data);
    }
}
