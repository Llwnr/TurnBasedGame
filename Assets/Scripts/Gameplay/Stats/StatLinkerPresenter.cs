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
        TargetManager.OnPlayerTargetChange += ShowLinks;
        TargetManager.OnPlayerTargetChange += TryAttachEndLink;
        TargetManager.OnEmptyTargetClicked += HideLinks;
    }
    private void OnDestroy() {
        TargetManager.OnPlayerTargetChange -= ShowLinks;
        TargetManager.OnPlayerTargetChange -= TryAttachEndLink;
        TargetManager.OnEmptyTargetClicked -= HideLinks;
    }

    void StartLinking(){
        _linkerView.SetLinkStart(gameObject.transform);
        _linkingStarted = true;
    }
    //Will try to finish up the linking process if possible
    void TryAttachEndLink(Transform target){
        if(!_linkingStarted) return;
        if(_linkingStarted && target != transform){
            _linkerView.SetLinkEnd(target);
            _linkerModel.AddLinkedPlayer(target.GetComponent<PlayerModel>());
            _linkingStarted = false;
        }
    }
    void ShowLinks(Transform target){
        //Do nothing if the linking process is still going on
        if(!target || target!=transform) return;
        _linkerView.ShowLinks(transform, _linkerModel.GetLinkedPlayers());
    }
    void HideLinks(){
        _linkerView.HideLinks();
    }
}
