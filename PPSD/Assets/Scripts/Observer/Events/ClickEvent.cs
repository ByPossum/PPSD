using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : EventBase
{
    public Vector3 v_localPoint;
    public Vector3 v_worldSpacePoint;
    public ClickEvent(Vector3 _local, Vector3 _world)
    {
        v_localPoint = _local;
        v_worldSpacePoint = _world;
    }
}
