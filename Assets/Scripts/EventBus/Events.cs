using UnityEngine;
namespace LlwnrEventBus{
    public interface IEvent{}

    public struct TestEvent : IEvent{ }

    public struct PlayerEvent : IEvent{
        public int health;
        public int mana;
    }
    public struct WhatIsGoingOnEvent : IEvent{
        public int age;
        public string myName;
    }
    //When a character takes damage, fired by CharacterModel
    public struct OnDamageTakenEvent : IEvent{
        public Transform HitCharacter;
        public CharacterModel CharacterModel;
        public float DamageAmt, CurrentHealth, MaxHealth;
    }
    //When a character dies, fired by CharacterModel
    public struct OnDeathEvent : IEvent{
        public Transform DiedCharacter;
    }

    //Turn related events, fired by TurnManager
    public struct OnCharacterTurnStart : IEvent{

    }
    public struct OnTurnStart : IEvent{
        
    }
}