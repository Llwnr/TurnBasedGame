using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatLinkerView : MonoBehaviour
{
    LineRenderer _lineRenderer;
    bool lineFollowMouse = false;
    private void Start() {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    public void SetLinkStart(Transform target){
        _lineRenderer.SetPosition(0, target.position);
        lineFollowMouse = true;
    }
    public void SetLinkEnd(Transform target){
        lineFollowMouse = false;
        _lineRenderer.SetPosition(1, target.position);
    }

    private void Update() {
        if(lineFollowMouse){
            _lineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if(lineFollowMouse && Input.GetMouseButtonDown(0)){
            lineFollowMouse = false;
        }
    }

    public void ShowLinks(Transform mainTarget, List<PlayerModel> playerLinks){
        _lineRenderer.enabled = true;
        _lineRenderer.positionCount = Math.Max(2,playerLinks.Count*2);
        _lineRenderer.SetPosition(0,Vector2.zero);
        _lineRenderer.SetPosition(1,Vector2.zero);
        for(int i=0; i<playerLinks.Count*2; i+=2){
            Transform linkedTarget = playerLinks[i/2].transform;

            _lineRenderer.SetPosition(i, mainTarget.position);
            _lineRenderer.SetPosition(i+1, linkedTarget.position);
        }
    }
    public void HideLinks(){
        _lineRenderer.enabled = false;
    }
}
