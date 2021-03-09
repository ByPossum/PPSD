using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserverBase
{
    void OnNotify(EventBase _eb);
}
