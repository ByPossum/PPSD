using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    [SerializeField] MonoBehaviour[] in_objectsToInit;
    // Start is called before the first frame update
    void Start()
    {
        foreach (MonoBehaviour objToInit in in_objectsToInit)
        {
            objToInit?.GetComponent<IInitable>().Init();
        }
    }
}
