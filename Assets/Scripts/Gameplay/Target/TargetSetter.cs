using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using LlwnrEventBus;
using UnityEngine;

public class TargetSetter : MonoBehaviour
{
    EventBinding<OnDeathEvent> _OnDeathEvent;
    private void Start() {
        SetDefaultTarget();
    }
    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            TrySetTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
    private void OnEnable() {
        _OnDeathEvent = new EventBinding<OnDeathEvent>(SetDefaultTarget);
        EventBus<OnDeathEvent>.Register(_OnDeathEvent);
    }
    private void OnDisable() {
        EventBus<OnDeathEvent>.Deregister(_OnDeathEvent);
    }

    void SetDefaultTarget(){
        //Setting default targets
        TargetManager.SetPlayerTargetModel(FindObjectOfType<PlayerModel>());
        TargetManager.SetEnemyTargetModel(FindObjectOfType<EnemyModel>());
        //Hide the highlight boxes that notify users of the selected characters
        TargetManager.NonTargetClicked();
    }
    void TrySetTarget(Vector2 clickedPos){
        RaycastHit2D[] hits = Physics2D.RaycastAll(clickedPos, Vector2.zero);
        if(hits.Length <= 0){
            TargetManager.NonTargetClicked();
        }
        foreach(var hit in hits){
            if(hit.collider == null) return;
            Transform hitTransform = hit.transform;
            if(hitTransform.CompareTag("Player")){
                TargetManager.SetPlayerTargetModel(hitTransform.GetComponent<CharacterModel>());
            }else if(hitTransform.CompareTag("Enemy")){
                TargetManager.SetEnemyTargetModel(hitTransform.GetComponent<CharacterModel>());
            }else{
                TargetManager.NonTargetClicked();
            }
        }
    }
}
