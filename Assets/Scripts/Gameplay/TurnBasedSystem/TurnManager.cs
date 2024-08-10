using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    private void Awake() {
        if(instance == null) instance = this;
        else{
            Debug.LogError("More than 1 turn manager");
        }
    }

    Queue<CharacterPresenter> _playerTurnQueue = new Queue<CharacterPresenter>();
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
            _playerTurnQueue.Enqueue(playerCharacter);
        }
        
        Debug.Log("Num of charas this round: " + _playerTurnQueue.Count);
        NextCharactersTurn();
        
    }

    public void NextCharactersTurn(){
        if(_playerTurnQueue.Count <= 0){
            return;
        }
        _currentPresenter = _playerTurnQueue.Dequeue();
        _currentPresenter.OnCharacterTurnStart();
    }

    public void NextPartysTurn(){
        Debug.Log("Turn switched to next party");

        QueueTurn();
    }
}
