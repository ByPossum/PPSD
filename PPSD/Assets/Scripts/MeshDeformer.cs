using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDeformer : MonoBehaviour, IObserverBase, IInitable
{
    [SerializeField] GameObject tinySphere;

    public void Init()
    {

    }

    public void OnNotify(EventBase _eb)
    {
        switch (_eb)
        {
            case ClickEvent clicked:
                GameObject tiny = Instantiate(tinySphere);
                tiny.transform.position = clicked.v_worldSpacePoint;
                break;
        }
    }
    
}
