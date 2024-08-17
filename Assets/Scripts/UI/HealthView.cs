using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LlwnrEventBus;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField]private CharacterModel _myCharacter;
    [SerializeField]private Image _healthBar;
    [SerializeField]bool debug;

    private void Start() {
        AlignToModelPos();
        _myCharacter.SubscribeToHealthChange(UpdateHealthBar);
    }

    void AlignToModelPos(){
        _healthBar.transform.position = Camera.main.WorldToScreenPoint(_myCharacter.transform.position  + new Vector3(0,0.7f,0));
    }

    private void OnDestroy() {
        _myCharacter.UnsubscribeToHealthChange(UpdateHealthBar);
    }

    void UpdateHealthBar(float currHealth, float maxHealth){
        _healthBar.fillAmount = currHealth/maxHealth;
    }

    private void LateUpdate() {
        AlignToModelPos();
    }
}
