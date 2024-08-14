using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupPropertyManager : MonoBehaviour
{
    float _timeDuration = 0.5f;
    [SerializeField]private TextMeshProUGUI textBox;

    public void Initialize(string text){
        textBox.text = text;
        LeanTween.moveY(gameObject, transform.position.y + 100, 0.2f).setEaseOutQuart();
    }
    // Update is called once per frame
    void Update()
    {
        _timeDuration -= Time.deltaTime;
        if(_timeDuration < 0) Destroy(gameObject);
    }
}
