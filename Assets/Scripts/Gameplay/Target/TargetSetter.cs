using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TargetSetter : MonoBehaviour
{
    private void Start() {
        //Setting default targets
        TargetManager.SetPlayerTargetModel(FindObjectOfType<PlayerModel>());
        TargetManager.SetEnemyTargetModel(FindObjectOfType<EnemyModel>());
        //Hide the highlights from view
        TargetManager.NonTargetClicked();
    }
    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            TrySetTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
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
