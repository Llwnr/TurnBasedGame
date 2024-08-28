using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public abstract class CharacterPresenter : MonoBehaviour
{
    [SerializeField]protected CharacterModel _characterModel;  

    protected virtual void Start(){

    }  

    public abstract void OnCharacterTurnStart();
    public abstract void OnCharacterTurnEnd();

    public void OnSkillUsed(){
        _characterModel.SkillUsed();
        OnCharacterTurnEnd();
    }
    public bool CanAct(){
        return _characterModel.CanAct();
    }
    public void UpdateTime(){
        _characterModel.IncreaseActionPoints();
    }
    public float GetCurrentSpeed(){
        return _characterModel.GetFinalSpeed();
    }
}
