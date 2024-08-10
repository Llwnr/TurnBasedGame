using System;

namespace LlwnrEventBus{
    internal interface IEventBinding<T>{
        Action<T> OnEvent { get; set; }
        Action OnEventNoArgs {get; set;}
    }

    public class EventBinding<T> : IEventBinding<T> where T : IEvent{
        public Action<T> onEvent = delegate(T t){}; 
        public Action onEventNoArgs = delegate {}; 

        Action<T> IEventBinding<T>.OnEvent{
            get=>onEvent;
            set => onEvent = value; 
        }
        Action IEventBinding<T>.OnEventNoArgs { 
            get => onEventNoArgs; 
            set => onEventNoArgs = value; 
        }

        public EventBinding(Action<T> onEvent) => this.onEvent = onEvent;
        public EventBinding(Action onEventNoArgs) => this.onEventNoArgs = onEventNoArgs;

        public void Add(Action onEvent) => this.onEventNoArgs += onEvent;
        public void Remove(Action onEvent) => this.onEventNoArgs -= onEvent;

        public void Add(Action<T> onEvent) => this.onEvent += onEvent;
        public void Remove(Action<T> onEvent) => this.onEvent -= onEvent;
    }
}