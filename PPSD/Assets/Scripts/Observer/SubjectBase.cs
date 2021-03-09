using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectBase : MonoBehaviour
{
    private IObserverBase[] observers = new IObserverBase[0];
    public void AddObserver(IObserverBase _newObserver)
    {
        observers = Utils.AddToArray(observers, _newObserver);
    }

    protected void Notify(EventBase _eb)
    {
        foreach (IObserverBase observer in observers)
        {
            observer.OnNotify(_eb);
        }
    }
}
