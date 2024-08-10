using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TargetView : MonoBehaviour
{
    [SerializeField]private Image highlightPlayer, highlightEnemy;
    private void OnEnable() {
        TargetManager.OnPlayerTargetChange += SetPlayerHighlight;
        TargetManager.OnEnemyTargetChange += SetEnemyHighlight;
        TargetManager.OnEmptyTargetClicked += DisableHighlight;
    }
    private void OnDisable() {
        TargetManager.OnPlayerTargetChange -= SetPlayerHighlight;
        TargetManager.OnEnemyTargetChange -= SetEnemyHighlight;
        TargetManager.OnEmptyTargetClicked -= DisableHighlight;
    }

    void SetPlayerHighlight(Transform target){
        highlightPlayer.gameObject.SetActive(true);
        highlightPlayer.transform.position = Camera.main.WorldToScreenPoint(target.position);
    }
    void SetEnemyHighlight(Transform target){
        highlightEnemy.gameObject.SetActive(true);
        highlightEnemy.transform.position = Camera.main.WorldToScreenPoint(target.position);
    }
    void DisableHighlight(){
        highlightPlayer.gameObject.SetActive(false);
        highlightEnemy.gameObject.SetActive(false);
    }
}
