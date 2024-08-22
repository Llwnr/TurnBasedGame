using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatLinkerPresenter : MonoBehaviour
{
    [SerializeField]private StatLinkerModel _linkerModel;
    [SerializeField]private StatLinkerView _linkerView;
    [SerializeField]private Button _linkButton;
    bool _linkingStarted = false;

    private void Start() {
        _linkButton.onClick.AddListener(StartLinking);
        TargetManager.OnPlayerTargetChange += TryAttachEndLink;
    }
    private void OnDestroy() {
        TargetManager.OnPlayerTargetChange -= TryAttachEndLink;
    }

    void StartLinking(){
        _linkerView.SetLinkStart(gameObject.transform);
        _linkingStarted = true;
    }
    //Will try to finish up the linking process if possible
    void TryAttachEndLink(Transform target){
        if(_linkingStarted && target != transform){
            _linkerView.SetLinkEnd(target);
            _linkerModel.AddLinkedPlayer(target.GetComponent<PlayerModel>());
            _linkingStarted = false;
        }else{
            Debug.Log("Invalid transform " + target);
        }
    }
}
