using System;
using System.Collections.Generic;
using UnityEngine;

public static class TargetManager
{
    static CharacterModel _playerTargetModel, _enemyTargetModel;
    public static CharacterModel SelectedPlayerTarget{
        get{
            return _playerTargetModel;
        }
        set{
            _playerTargetModel = value;
            OnPlayerTargetChange.Invoke(_playerTargetModel.transform);
        }
    }
    public static CharacterModel SelectedEnemyTarget{
        get{
            return _enemyTargetModel;
        }
        set{
            _enemyTargetModel = value;
            OnEnemyTargetChange.Invoke(_enemyTargetModel.transform);
        }
    }
    public static event Action<Transform> OnPlayerTargetChange;
    public static event Action<Transform> OnEnemyTargetChange;
    public static event Action OnEmptyTargetClicked;
    //This will manage the target to set incase the current target is already dead. So, now have to find a new target
    public static CharacterModel GetTargetOrAvailableTarget(CharacterModel target){
        //If the target is dead already, then find a new target
        if(!target || !target.gameObject.activeSelf){
            if(target is PlayerModel)   target = SelectedPlayerTarget;
            else if(target is EnemyModel) target = SelectedEnemyTarget;
        }
        //If no target is available then send an error
        if(target == null){
            Debug.LogError("No target found for " + target.GetType());
        }
        return target;
    }
    
    //Let target setters to set the player targets and enemy targets
    public static void NonTargetClicked(){
        OnEmptyTargetClicked.Invoke();
    }
    public static void SetPlayerTargetModel(CharacterModel target){
        if(target==null) return;
        SelectedPlayerTarget = target;
    }
    public static void SetEnemyTargetModel(CharacterModel target){
        if(target==null) return;
        SelectedEnemyTarget = target;
    }
    public static PlayerModel[] GetAllPlayerModels(){
        return UnityEngine.Object.FindObjectsOfType<PlayerModel>();
    }
    public static EnemyModel[] GetAllEnemyModels(){
        return UnityEngine.Object.FindObjectsOfType<EnemyModel>();
    }
}
