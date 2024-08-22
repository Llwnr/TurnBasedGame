using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
    ONLY FOR GAMEOBJECTS NOT UI OBJECTS
*/
public class HoldToOpenUI : MonoBehaviour
{
    [SerializeField]private float _maxHoldDuration = 0.5f;
    [SerializeField]private GameObject _objToOpen;
    bool _isHolding = false;

    void OnMouseDown() {
        OpenUI();
    }
    void OnMouseUp() {
        _isHolding = false;
    }
    private void Update() {
        if(Input.GetMouseButtonDown(0)) ManageClicks();
    }

    void ManageClicks(){
        PointerEventData pointerData = new PointerEventData(EventSystem.current){
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        foreach(RaycastResult result in results){
            if(result.gameObject == _objToOpen) return;
        }
        DeactivateObject();
    }
    void DeactivateObject(){
        _objToOpen.SetActive(false);
    }
    async void OpenUI(){
        if(_isHolding || _objToOpen.activeSelf) return;//Skip execution if already holding or object is active
        _isHolding = true;
        await Task.Delay(Mathf.FloorToInt(_maxHoldDuration * 1000));
        if(_isHolding){
            _objToOpen.SetActive(true);
        }
    }
}
