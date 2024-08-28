using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LlwnrEventBus;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderView : MonoBehaviour {
    [SerializeField] private RectTransform turnOrderBar;
    List<CharacterModel> aliveCharacters = new List<CharacterModel>();
    [SerializeField]private Transform uiImage;
    List<Transform> uiImages = new List<Transform>();

    EventBinding<OnTurnStart> _turnStartEvent;
    // Start is called before the first frame update
    void Start() {
        _turnStartEvent = new EventBinding<OnTurnStart>(StoreCharacters);
        StoreCharacters();
    }

    void StoreCharacters() {
        UnsubscribeBeforeReset();
        aliveCharacters = FindObjectsOfType<CharacterModel>().ToList();
        foreach (var character in aliveCharacters) {
            character.SubscribeToActionPointChange(LerpTurnOrder);
        }
        for(int i=0; i<aliveCharacters.Count; i++){
            uiImages.Add(Instantiate(uiImage, transform));
        }
    }
    void UnsubscribeBeforeReset(){
        foreach (var character in aliveCharacters) {
            character.UnsubscribeToActionPointChange(LerpTurnOrder);
        }
        foreach (var uiImg in uiImages){
            Destroy(uiImg.gameObject);
        }
        uiImages.Clear();
    }
    private void OnDestroy() {
        UnsubscribeBeforeReset();
    }

    void LerpTurnOrder(float currValue, float maxValue, CharacterModel model) {
        int index = aliveCharacters.IndexOf(model);
        float normalizedValue = currValue / maxValue;
        float yTop = turnOrderBar.position.y + turnOrderBar.rect.yMax;
        float yBot= turnOrderBar.position.y + turnOrderBar.rect.yMin;
        uiImages[index].GetComponent<Image>().sprite = model.GetSprite();
        uiImages[index].position = new Vector2(uiImages[index].position.x, Mathf.Lerp(yTop, yBot, normalizedValue));
    }
}
