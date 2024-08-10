using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    float _timeDuration = 0.5f;
    [SerializeField]private TextMeshProUGUI textBox;

    public void SetText(string text){
        textBox.text = text;
    }
    // Update is called once per frame
    void Update()
    {
        _timeDuration -= Time.deltaTime;
        if(_timeDuration < 0) Destroy(gameObject);
    }
}
