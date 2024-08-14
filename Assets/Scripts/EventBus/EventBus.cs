using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace LlwnrEventBus
{
    public static class EventBus<T> where T : IEvent{
        static readonly HashSet<IEventBinding<T>> bindings = new HashSet<IEventBinding<T>>();

        public static void Register(EventBinding<T> binding) => bindings.Add(binding);
        public static void Deregister(EventBinding<T> binding) => bindings.Remove(binding);

        public static void Raise(T @event){
            //Add events to queue for sequential execution in proper order
            EventQueue.AddEventToQueue(() => {
                Debug.Log("Firing: " + @event.GetType());
                foreach (var binding in bindings)
                {
                    binding.OnEvent.Invoke(@event);
                    binding.OnEventNoArgs.Invoke();
                }
            });
            EventQueue.ProcessEvents();
        }

        public static void Clear(){
            Debug.Log($"Clearing {typeof(T).Name} bindings");
            bindings.Clear();
        }
    }

    public static class EventQueue{
        static Queue<Action> EventRaisingAction = new Queue<Action>();
        public static bool isBusy = false;

        public static void AddEventToQueue(Action action){ 
            EventRaisingAction.Enqueue(action); 
        }

        public static async void ProcessEvents(){
            if(isBusy) return;
            isBusy = true;
            await Task.Yield();

            while(EventRaisingAction.Count > 0){
                EventRaisingAction.Dequeue()?.Invoke();
            }
            isBusy = false;
        }

    }
}


