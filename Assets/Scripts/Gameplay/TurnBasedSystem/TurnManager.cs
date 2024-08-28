using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    private float _gameTime = 0;
    [SerializeField]private float _maxTurnTime;
    bool _stopTime = false;
    bool _turnActive = false;


    List<CharacterPresenter> _myCharacters = new List<CharacterPresenter>();
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        StartFirstTurn();
    }  
    private void Update() {
        if(_stopTime || _turnActive) return;
        UpdateGameTime();
        UpdateCharacterTime();
        GiveTurnToAvailableCharacters();
    }
    void UpdateGameTime(){
        _gameTime += Time.deltaTime;
        if(_gameTime > _maxTurnTime){
            _gameTime = 0;
            StartNewTurn();
        }
    }
    void UpdateCharacterTime(){
        foreach(CharacterPresenter presenter in _myCharacters){
            presenter.UpdateTime();
        }
    }
    async void GiveTurnToAvailableCharacters(){
        foreach(CharacterPresenter presenter in _myCharacters){
            if(presenter.CanAct() && !_stopTime){
                presenter.OnCharacterTurnStart();
            }
            while(presenter.CanAct() && _stopTime){
                _turnActive = true;
                await Task.Yield();
            }
        }
        _turnActive = false;
    }
    void StartFirstTurn(){
        EventBus<OnTurnStart>.Raise(new OnTurnStart{});
        _myCharacters = FindObjectsOfType<CharacterPresenter>().ToList();
        _myCharacters = _myCharacters.OrderBy(x => x.GetCurrentSpeed()).ToList();
    }
    public void StartNewTurn(){
        EventBus<OnTurnStart>.Raise(new OnTurnStart{});
    }
    public float GetGameTime(){
        return _gameTime;
    }
    public float GetMaxTurnTime(){
        return _maxTurnTime;
    }
    public void StopTime(bool value){
        _stopTime = value;
    }
}
