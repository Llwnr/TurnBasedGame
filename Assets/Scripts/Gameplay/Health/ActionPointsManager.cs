using System;
using UnityEngine;

public class ActionPointsManager{
    public event Action<float, float, CharacterModel> OnActionPointsChanged;
    public float ActionThreshold;
    float _currActionPoints;
    CharacterModel _model;

    public ActionPointsManager(float maxActionPoints, CharacterModel model){ 
        ActionThreshold = maxActionPoints;
        if(model is EnemyModel){
            _currActionPoints = ActionThreshold*0.6f;
        }else{
            _currActionPoints = ActionThreshold*0.8f;
        }
        _model = model;
    }
    

    public void IncreaseActionPoints(float charSpeed){
        _currActionPoints += charSpeed;
        OnActionPointsChanged?.Invoke(_currActionPoints, ActionThreshold, _model);
    }
    public bool HasAp() => _currActionPoints > ActionThreshold;

    public void ReduceAp(){
        _currActionPoints -= ActionThreshold;
        OnActionPointsChanged?.Invoke(_currActionPoints, ActionThreshold, _model);
    }
}