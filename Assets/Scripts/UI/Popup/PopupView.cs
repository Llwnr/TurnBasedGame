using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LlwnrEventBus;
using UnityEngine;

public class PopupView : MonoBehaviour
{
    [SerializeField]bool debug;
    [SerializeField]private Canvas _canvas;
    [SerializeField]private PopupPropertyManager popupPrefab;
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
        CreateDmgPopup(eventData.CharacterModel.transform.position, eventData.DamageAmt);
        
    }
    void CreateDmgPopup(OnStatusEffectDamageTakenEvent eventData){
        CreateDmgPopup(eventData.CharacterModel.transform.position, eventData.DamageAmt, 100);
    }

    async void CreateDmgPopup(Vector2 pos, float dmgAmt, int delay=0){
        await Task.Delay(delay);
        PopupPropertyManager newPopup = Instantiate(popupPrefab, _canvas.transform, false);
        if(delay>0) newPopup.transform.localScale *= 0.7f;
        newPopup.transform.position = Camera.main.WorldToScreenPoint(pos);
        newPopup.Initialize(dmgAmt.ToString());
    }
}
