using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnTimeView : MonoBehaviour
{
    [SerializeField]Image _turnBar;
    [SerializeField]TurnManager _turnManager;

    private void Update() {
        _turnBar.fillAmount = 1 - _turnManager.GetGameTime()/_turnManager.GetMaxTurnTime();
    }
}
