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
    
    //Let target setters to set the player targets and enemy targets
    public static void NonTargetClicked(){
        OnEmptyTargetClicked.Invoke();
    }
    public static void SetPlayerTargetModel(CharacterModel target){
        SelectedPlayerTarget = target;
    }
    public static void SetEnemyTargetModel(CharacterModel target){
        SelectedEnemyTarget = target;
    }
    public static PlayerModel[] GetAllPlayerModels(){
        return UnityEngine.Object.FindObjectsOfType<PlayerModel>();
    }
    public static EnemyModel[] GetAllEnemyModels(){
        return UnityEngine.Object.FindObjectsOfType<EnemyModel>();
    }
}
