namespace LlwnrEventBus{
    public interface IEvent{ }

    public struct TestEvent : IEvent{ }

    public struct PlayerEvent : IEvent{
        public int health;
        public int mana;
    }

    public struct WhatIsGoingOnEvent : IEvent{
        public int age;
        public string myName;
    }

    //When a character takes damage
    public struct OnDamageTakenEvent : IEvent{
        public UnityEngine.Transform HitCharacter;
        public float DamageAmt, CurrentHealth, MaxHealth;
    }
}