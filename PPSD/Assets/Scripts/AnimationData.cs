using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public unsafe struct AnimationData
{
    [SerializeField] public fixed float f_vTransform[3];
    [SerializeField] public fixed float f_vRotation[4];

    public AnimationData(float x, float y, float z, float w, float i, float j, float k)
    {
        f_vTransform[0] = x;
        f_vTransform[1] = y;
        f_vTransform[2] = z;

        f_vRotation[0] = i;
        f_vRotation[1] = j;
        f_vRotation[2] = k;
        f_vRotation[3] = w;
    }
    /// <summary>
    /// Convert Transform & Quaternion to float 3 & 4
    /// </summary>
    /// <param name="_pos">Vertex Position</param>
    /// <param name="_joint">Joint Rotation</param>
    public AnimationData(Vector3 _pos, Quaternion _joint)
    {
        f_vTransform[0] = _pos.x;
        f_vTransform[1] = _pos.y;
        f_vTransform[2] = _pos.z;

        f_vRotation[0] = _joint.x;
        f_vRotation[1] = _joint.y;
        f_vRotation[2] = _joint.z;
        f_vRotation[3] = _joint.w;
    }
    public AnimationData(float x, float y, float z, Quaternion _joint)
    {
        f_vTransform[0] = x;
        f_vTransform[1] = y;
        f_vTransform[2] = z;

        f_vRotation[0] = _joint.x;
        f_vRotation[1] = _joint.y;
        f_vRotation[2] = _joint.z;
        f_vRotation[3] = _joint.w;
    }
}
