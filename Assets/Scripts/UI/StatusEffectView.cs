using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectView : MonoBehaviour
{
    [SerializeField]private StatusEffectManager _statusEffectManager;
    [SerializeField]private Image _statusEffectIconPrefab;
    [SerializeField]private GameObject _container;
    [SerializeField]private float yOffset;

    int count = 0;
    private void Start() {
        AlignToModelPos();
    }

    void AlignToModelPos(){
        _container.transform.position = Camera.main.WorldToScreenPoint(_statusEffectManager.transform.position  + new Vector3(0,yOffset,0));
    }

    // Update is called once per frame
    void Update()
    {
        if(count < _statusEffectManager.GetMyStatusEffects().Count){
            count++;
            Image newIcon = Instantiate(_statusEffectIconPrefab, _container.transform);
            newIcon.sprite = _statusEffectManager.GetMyStatusEffects()[0].StatusEffect.Icon;
            newIcon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _statusEffectManager.GetMyStatusEffects()[0].GetStacks().ToString();
        }
    }
}
