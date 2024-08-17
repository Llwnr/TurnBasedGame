using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupPropertyManager : MonoBehaviour
{
    float _timeDuration = 0.5f;
    [SerializeField]private TextMeshProUGUI textBox;
    [SerializeField]private Image icon;

    public PopupPropertyManager Initialize(string text, Sprite icon){
        textBox.text = text;
        if(!icon) this.icon.color = new Color(0,0,0,0);
        this.icon.sprite = icon;

        return this;
    }
    public PopupPropertyManager AnimatePopup(float height, float time, float scale){
        LeanTween.moveY(gameObject, transform.position.y + height, time).setEaseOutQuart();
        LeanTween.scale(gameObject, transform.localScale*scale, time).setEaseOutQuart();
        return this;
    }
    public PopupPropertyManager SetScale(float scale){
        transform.localScale = new Vector3(scale, scale, 1);
        return this;
    }
    // Update is called once per frame
    void Update()
    {
        _timeDuration -= Time.deltaTime;
        if(_timeDuration < 0) Destroy(gameObject);
    }
}
