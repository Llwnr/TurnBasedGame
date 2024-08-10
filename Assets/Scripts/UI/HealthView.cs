using System.Collections;
using System.Collections.Generic;
using LlwnrEventBus;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField]private Transform _myCharacter;
    [SerializeField]private Image _healthBar;
    [SerializeField]bool debug;
    EventBinding<OnDamageTakenEvent> _onDamageTaken;
    private void Start() {
        AlignToModelPos();
    }

    void AlignToModelPos(){
        _healthBar.transform.position = Camera.main.WorldToScreenPoint(_myCharacter.position  + new Vector3(0,0.7f,0));
    }
   
    private void OnEnable() {
        _onDamageTaken = new EventBinding<OnDamageTakenEvent>(UpdateHealthBar);
        EventBus<OnDamageTakenEvent>.Register(_onDamageTaken);
    }
    private void OnDisable() {
        EventBus<OnDamageTakenEvent>.Deregister(_onDamageTaken);
    }
    void UpdateHealthBar(OnDamageTakenEvent eventData){
        if(eventData.HitCharacter == _myCharacter){
            _healthBar.fillAmount = eventData.CurrentHealth/eventData.MaxHealth;
            DebugOut("Updating health bar by listening to OnDamageTakenEvent");
        }
    }
    void DebugOut(object data){
        if(!debug) return;
        Debug.Log(data);
    }
}
