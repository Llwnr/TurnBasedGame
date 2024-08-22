using System;
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

    private Dictionary<CharacterModel, Queue<Action>> _dmgPopupQueue = new Dictionary<CharacterModel, Queue<Action>>();
    private bool _queueRunning = false;
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
        AddPopupCreationToQueue(eventData.CharacterModel, () => {
            CreatePopup(eventData.HitCharacter.position)
            .Initialize(eventData.DamageAmt.ToString(), null)
            .AnimatePopup(80, 0.5f, 1.2f);
        });
    }
    void CreateStatusEffectDmgPopup(OnStatusEffectDamageTakenEvent eventData){
        AddPopupCreationToQueue(eventData.CharacterModel, () => {
            CreatePopup(eventData.HitCharacter.position)
            .Initialize(eventData.DamageAmt.ToString(), eventData.StatusEffect.Icon)
            .SetScale(0.9f).AnimatePopup(0, 0.7f, 1.2f);
        });
    }

    void AddPopupCreationToQueue(CharacterModel model, Action action){
        if(!_dmgPopupQueue.ContainsKey(model)) _dmgPopupQueue[model] = new Queue<Action>();
        _dmgPopupQueue[model].Enqueue(action);
        ExecuteDmgPopups();
    }
    async void ExecuteDmgPopups(){
        if(_queueRunning) return;
        _queueRunning = true;
        await Task.Yield();
        List<Task> tasks = new List<Task>();
        foreach(var kvp in _dmgPopupQueue){
            tasks.Add(Execute(kvp.Value));
        }
        await Task.WhenAll(tasks);
        _queueRunning = false;
    }
    async Task Execute(Queue<Action> actions){
        while(actions.Count > 0){
            actions.Dequeue().Invoke();
            await Task.Delay(50);
        }
    }

    PopupPropertyManager CreatePopup(Vector2 position){
        PopupPropertyManager newPopup = Instantiate(popupPrefab, _canvas.transform, false);
        newPopup.transform.SetParent(_canvas.transform, false);
        newPopup.transform.position = Camera.main.WorldToScreenPoint(position);
        return newPopup;
    }
}
