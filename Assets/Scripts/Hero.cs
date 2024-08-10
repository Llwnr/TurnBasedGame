using LlwnrEventBus;
using UnityEngine;

public class Hero : MonoBehaviour {
    int hp;
    int man;

    EventBinding<TestEvent> testEvent;
    EventBinding<PlayerEvent> playerEvent;
    EventBinding<WhatIsGoingOnEvent> whatEvent;

    private void OnEnable() {
        testEvent = new EventBinding<TestEvent>(HandleTestEvent);
        EventBus<TestEvent>.Register(testEvent);
        
        playerEvent = new EventBinding<PlayerEvent>(HandlePlayerEvent);
        playerEvent.Add(DisplayPlayerHealthEvent);
        EventBus<PlayerEvent>.Register(playerEvent);   

        whatEvent = new EventBinding<WhatIsGoingOnEvent>(SooooWhatsTheUseCase);
        EventBus<WhatIsGoingOnEvent>.Register(whatEvent);    
    }

    private void OnDisable() {
        EventBus<TestEvent>.Deregister(testEvent);
        EventBus<PlayerEvent>.Deregister(playerEvent);
    }

    private void Awake() {
        hp = 100;
        man = 200;
        Debug.Log("HMM");
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.T)){
            Debug.Log("T key pressed");
            EventBus<TestEvent>.Raise(new TestEvent());
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Space key pressed");
            EventBus<PlayerEvent>.Raise(new PlayerEvent{
                health = hp,
                mana = man
            });
        }
        if(Input.GetKeyDown(KeyCode.X)){
            Debug.Log("X key pressed");
            EventBus<WhatIsGoingOnEvent>.Raise(new WhatIsGoingOnEvent());
        }
    }

    void HandleTestEvent(){
        Debug.Log("Test event received");
    }
    void HandlePlayerEvent(PlayerEvent playerEvent){
        Debug.Log(playerEvent.health + " Mana: " + playerEvent.mana);
    }
    void DisplayPlayerHealthEvent(PlayerEvent playerEvent){
        Debug.Log(playerEvent.health);
    }
    void SooooWhatsTheUseCase(){
        Debug.Log("Help");
    }
}