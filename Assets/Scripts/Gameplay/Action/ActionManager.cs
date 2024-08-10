using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static ActionManager instance;
    private void Awake() {
        if(instance == null) instance = this;
        else Debug.LogError("More than 1 ActionManager");
    }
    private Queue<Action> _playerActions = new Queue<Action>(), _enemyActions = new Queue<Action>();
    
    public void AddPlayerAction(Action action){
        _playerActions.Enqueue(action);
    }
    public void RemovePlayerAction(){
        _playerActions.Dequeue();
    }

    public void AddEnemyAction(Action action){
        _enemyActions.Enqueue(action);
    }
    public void RemoveEnemyAction(){
        _enemyActions.Dequeue();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.X)){
            Debug.Log("X key pressed");
            ExecutePlayerActions();
        }
        if(Input.GetKeyDown(KeyCode.Z)){
            Debug.Log("Z key pressed");
            ExecuteEnemyActions();
        }
    }

    async void ExecutePlayerActions(){
        while(_playerActions.Count > 0){
            _playerActions.Dequeue().Invoke();
            await Task.Delay(300);
        }
        if(_playerActions.Count <= 0){
            await Task.Delay(1000);
            TurnManager.instance.NextPartysTurn();
        }
    }
    void ExecuteEnemyActions(){
        while(_enemyActions.Count > 0){
            _enemyActions.Dequeue().Invoke();
        }
    }
}
