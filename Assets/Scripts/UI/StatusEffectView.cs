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

    List<StatusEffectData> _statusEffectDatas = new List<StatusEffectData>();
    List<GameObject> _statusEffectUIs = new List<GameObject>();

    private void Start() {
        AlignToModelPos();
    }

    private void OnEnable() {
        _statusEffectManager.OnStatusEffectAdded += AddStatusEffectUI;
        _statusEffectManager.OnStatusEffectUpdated += UpdateStatusEffectUI;
        _statusEffectManager.OnStatusEffectRemoved += RemoveStatusEffectUI;
    }
    private void OnDisable() {
        if(!_statusEffectManager) return;
        _statusEffectManager.OnStatusEffectAdded -= AddStatusEffectUI;
        _statusEffectManager.OnStatusEffectUpdated -= UpdateStatusEffectUI;
        _statusEffectManager.OnStatusEffectRemoved -= RemoveStatusEffectUI;
    }

    void AlignToModelPos(){
        _container.transform.position = Camera.main.WorldToScreenPoint(_statusEffectManager.transform.position  + new Vector3(0,yOffset,0));
    }

    void AddStatusEffectUI(StatusEffectData data){
        _statusEffectDatas.Add(data);

        Image newIcon = Instantiate(_statusEffectIconPrefab, _container.transform);
        newIcon.sprite = data.StatusEffect.Icon;
        newIcon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = data.GetStacks().ToString();
        newIcon.name = data.StatusEffect.GetType().ToString();
        _statusEffectUIs.Add(newIcon.gameObject);
    }
    void UpdateStatusEffectUI(StatusEffectData data){
        int myStacks;
        int index = 0;
        for(int i=0; i<_statusEffectDatas.Count; i++){
            if(_statusEffectDatas[i].StatusEffect.GetType() == data.StatusEffect.GetType()){
                Debug.Log("Foind duplingca");
                index = i;
                break;
            }
            if(i == _statusEffectDatas.Count-1){
                Debug.LogError("No status effect of the type " + data + " stored in list");
            }
        }
        myStacks = data.GetStacks();
        _statusEffectUIs[index].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = myStacks.ToString();
    }
    void RemoveStatusEffectUI(StatusEffectData data){
        int index = _statusEffectDatas.IndexOf(data);
        Destroy(_statusEffectUIs[index].gameObject);
        _statusEffectUIs.RemoveAt(index);
    }
}
