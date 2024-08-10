using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterPresenter : MonoBehaviour
{
    [SerializeField]protected CharacterModel _characterModel;  

    protected virtual void Start(){

    }  

    public abstract void OnCharacterTurnStart();
    public abstract void OnCharacterTurnEnd();

    public void OnSkillUsed(){
        OnCharacterTurnEnd();
        TurnManager.instance.NextCharactersTurn();
    }
    public float GetFinalSpeed(){
        return _characterModel.GetFinalSpeed();
    }
}
