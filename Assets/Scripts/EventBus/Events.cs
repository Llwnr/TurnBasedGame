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
    //When a status effect is inflicted
    public struct OnStatusEffectInflicted : IEvent{
        public StatusEffectManager StatusEffectManager;
    }
    //When a character takes damage, fired by CharacterModel
    public struct OnSkillDamageTakenEvent : IEvent{
        public Transform HitCharacter;
        public CharacterModel CharacterModel;
        public float DamageAmt, CurrentHealth, MaxHealth;
        public SkillAction Skill;
    }
    //When a character takes status effect related damage
    public struct OnStatusEffectDamageTakenEvent : IEvent{
        public Transform HitCharacter;
        public CharacterModel CharacterModel;
        public float DamageAmt, CurrentHealth, MaxHealth;
        public StatusEffect StatusEffect;
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