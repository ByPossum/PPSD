using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexOutput : MonoBehaviour
{

    [SerializeField] private SkinnedMeshRenderer smr;
    public void GenerateColourArray()
    {
        Vector3[] verts = smr.sharedMesh.vertices;
        smr.BakeMesh(smr.sharedMesh);
        Vector3[] currentPose = smr.sharedMesh.vertices;
        Color[] map = new Color[verts.Length];
        for(int i = 0; i < verts.Length; i++)
        {
            Debug.Log(string.Format("Rest Pose {0} Animated Pose {1}", verts[i], currentPose[i]));
        }
    }
}
