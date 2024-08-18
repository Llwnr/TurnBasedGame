using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatusEffectsKeyToDescToolkit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]TextMeshProUGUI _skillDescBox, _statusEffectDescBox;
    [SerializeField]string _hoveredWord;
    Coroutine _hoverCoroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverCoroutine = StartCoroutine(GetWordsOnHover(eventData));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(_hoverCoroutine);
    }

    IEnumerator GetWordsOnHover(PointerEventData eventData){
        while(true){
            int wordIndex = TMP_TextUtilities.FindIntersectingWord(_skillDescBox, eventData.position, null);
            if(wordIndex != -1){
                _hoveredWord = _skillDescBox.textInfo.wordInfo[wordIndex].GetWord();
            }
            bool validKey = false;
            foreach(var kvp in KeywordsDescriptionStylizer.StatusEffectsDescription){
                if(kvp.Key == _hoveredWord){
                    validKey = true;
                    _hoveredWord = kvp.Value;
                    _statusEffectDescBox.transform.parent.gameObject.SetActive(true);
                    _statusEffectDescBox.text = _hoveredWord;
                }
                if(!validKey){
                    _statusEffectDescBox.transform.parent.gameObject.SetActive(false);
                }
            }
            yield return null;
        }        
    }
}
