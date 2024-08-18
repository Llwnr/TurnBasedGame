using System;
using System.Threading.Tasks;

public static class DelayedExecution
{
    public static async void OneFrame(Action callback){
        await Task.Yield();
        callback.Invoke();
    }
}
