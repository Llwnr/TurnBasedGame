using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LlwnrEventBus;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    private void Awake() {
        if(instance == null) instance = this;
        else{
            Debug.LogError("More than 1 turn manager");
        }

        Debug.Log("Resetting leantween from here");
        Debug.Log("Setting application frame rate here");
        Application.targetFrameRate = 30;
        LeanTween.reset();
    }

    Queue<CharacterPresenter> _turnQueue = new Queue<CharacterPresenter>();
    CharacterPresenter _currentPresenter;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        QueueTurn();
    }  

    //Create a queue at start(Players turn first)
    void QueueTurn(){
        List<CharacterPresenter> myQueue = FindObjectsOfType<CharacterPresenter>().ToList();
        myQueue = myQueue.OrderByDescending(x => x.GetFinalSpeed()).ToList();
        
        foreach(var playerCharacter in myQueue){
            _turnQueue.Enqueue(playerCharacter);
        }
        
        Debug.Log("Num of charas this round: " + _turnQueue.Count);
        NextCharactersTurn();
        
    }

    public void NextCharactersTurn(){
        if(_turnQueue.Count <= 0){
            return;
        }
        _currentPresenter = _turnQueue.Dequeue();
        _currentPresenter.OnCharacterTurnStart();
    }

    public void SetNextGameTurn(){
        QueueTurn();
        EventBus<OnTurnStart>.Raise(new OnTurnStart{});
    }
}
