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
    private List<Action> _actions = new List<Action>();
    
    public void AddAction(Action action){
        _actions.Add(action);
    }
    public void RemoveLastAction(){
        _actions.Remove(GetLastItem(_actions));
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.X)){
            Debug.Log("X key pressed");
            ExecutePlayerActions();
        }
    }

    async void ExecutePlayerActions(){
        while(_actions.Count > 0){
            GetLastItem(_actions).Invoke();
            RemoveLastAction();
            await Task.Delay(300);
        }
        if(_actions.Count <= 0){
            await Task.Delay(2000);
            TurnManager.instance.SetNextGameTurn();
        }
    }

    Action GetLastItem(List<Action> actions){
        return actions[actions.Count-1];
    }
}
