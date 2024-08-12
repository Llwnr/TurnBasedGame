using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LlwnrEventBus;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField]private Transform _myCharacter;
    [SerializeField]private Image _healthBar;
    [SerializeField]bool debug;
    EventBinding<OnDamageTakenEvent> _onDamageTaken;
    EventBinding<OnStatusEffectDamageTakenEvent> _onStatusEffectDmgTakenEvent;
    EventBinding<OnDeathEvent> _onDeathEvent;
    private void Start() {
        AlignToModelPos();
    }

    void AlignToModelPos(){
        _healthBar.transform.position = Camera.main.WorldToScreenPoint(_myCharacter.position  + new Vector3(0,0.7f,0));
    }
   
    private void OnEnable() {
        _onDamageTaken = new EventBinding<OnDamageTakenEvent>(UpdateHealthBar);
        EventBus<OnDamageTakenEvent>.Register(_onDamageTaken);

        _onDeathEvent = new EventBinding<OnDeathEvent>(DeactivateNextFrame);
        EventBus<OnDeathEvent>.Register(_onDeathEvent);

        _onStatusEffectDmgTakenEvent = new EventBinding<OnStatusEffectDamageTakenEvent>(UpdateHealthBar);
        EventBus<OnStatusEffectDamageTakenEvent>.Register(_onStatusEffectDmgTakenEvent);
    }
    private void OnDisable() {
        EventBus<OnDamageTakenEvent>.Deregister(_onDamageTaken);
        EventBus<OnDeathEvent>.Deregister(_onDeathEvent);
        EventBus<OnStatusEffectDamageTakenEvent>.Deregister(_onStatusEffectDmgTakenEvent);
    }

    void UpdateHealthBar(OnDamageTakenEvent eventData){
        if(eventData.HitCharacter == _myCharacter){
            _healthBar.fillAmount = eventData.CurrentHealth/eventData.MaxHealth;
            Log("Updating health bar by listening to OnDamageTakenEvent");
        }
    }
    void UpdateHealthBar(OnStatusEffectDamageTakenEvent eventData){
        if(eventData.HitCharacter == _myCharacter){
            _healthBar.fillAmount = eventData.CurrentHealth/eventData.MaxHealth;
            Log("Updating health bar by listening to OnSEDamageTakenEvent");
        }
    }
    async void DeactivateNextFrame(OnDeathEvent eventData){
        if(eventData.DiedCharacter == null){
            Debug.LogWarning("Enemy has died. View should deactivate");
            return;
        }
        await Task.Yield(); 
        if(eventData.DiedCharacter == transform) gameObject.SetActive(false);
    }



    void Log(object data){
        if(!debug) return;
        Debug.Log(data);
    }
}
