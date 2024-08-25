using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    // public static ActionManager instance;
    // private void Awake() {
    //     if(instance == null) instance = this;
    //     else Debug.LogError("More than 1 ActionManager");
    // }
    // private List<Func<Task>> _actions = new List<Func<Task>>();
    
    // public void AddAction(Func<Task> action){
    //     _actions.Add(action);
    // }
    // public void RemoveLastAction(){
    //     _actions.Remove(_actions[0]);
    // }

    // private void Update() {
    //     if(Input.GetKeyDown(KeyCode.X)){
    //         Debug.Log("X key pressed");
    //         ExecuteQueueActions();
    //     }
    // }

    // async void ExecuteQueueActions(){
    //     while(_actions.Count > 0){
    //         TurnManager.instance.StopTime(true);
    //         Func<Task> skillAction = _actions[0];
    //         await skillAction.Invoke();//Wait for the skill, its animations and all to be activated
    //         RemoveLastAction();
    //     }
    //     TurnManager.instance.StopTime(false);
    //     if(_actions.Count <= 0){
    //         await Task.Delay(1000);
    //         TurnManager.instance.StartNextGameTurn();
    //     }
    // }
}
