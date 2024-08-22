using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]private bool isWorldview = true;
    [SerializeField]private Transform _target;

    private void OnEnable() {
        AlignToTarget();
    }
    // Update is called once per frame
    void Update()
    {
        AlignToTarget();
    }
    void AlignToTarget(){
        if(isWorldview){
            transform.position = Camera.main.WorldToScreenPoint(_target.position);
        }else{
            transform.position = _target.position;
        }
    }
}
