using System.Collections;
using System.Collections.Generic;
using LlwnrEventBus;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{

    EventBinding<PlayerEvent> playerEvent;
    private void OnEnable() {
        playerEvent = new EventBinding<PlayerEvent>(DisplayAnimation);
        EventBus<PlayerEvent>.Register(playerEvent);
    }
    void DisplayAnimation(PlayerEvent playerEvent){
        Debug.Log("Animating health change to: " + playerEvent.health);
    }
}
