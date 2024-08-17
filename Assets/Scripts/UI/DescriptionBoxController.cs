using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionBoxController : MonoBehaviour
{
    [SerializeField]private Image _icon;
    [SerializeField]private TextMeshProUGUI _name, _description;

    public void Initialize(string name, string description, Sprite icon){
        _name.text = name;
        _description.text = description;
    }
}
