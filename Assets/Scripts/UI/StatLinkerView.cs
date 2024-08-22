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
}
