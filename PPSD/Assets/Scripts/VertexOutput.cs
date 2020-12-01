using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VertexOutput : MonoBehaviour
{
    [SerializeField] private bool vertTest;
    [SerializeField] private SkinnedMeshRenderer smr;
    [SerializeField] GameObject meshPoints;
    [SerializeField] Material restMat;
    [SerializeField] Material animat;
    private int disRow;
    Vector3[] restPose;
    GameObject[] restPosePoints;
    GameObject[] animPosePoints;
    private void Start()
    {
        restPose = smr.sharedMesh.vertices;
        restPosePoints = new GameObject[restPose.Length];
    }
    public void GenerateColourArray()
    {
        Mesh newMesh = new Mesh();
        smr.BakeMesh(newMesh);
        Vector3[] currentPose = newMesh.vertices;
        if(animPosePoints != null)
            DeleteArrays();
        restPosePoints = new GameObject[restPose.Length];
        animPosePoints = new GameObject[currentPose.Length];
        Debug.Log(restPose.Length);
        int poseWidth = Mathf.RoundToInt(Mathf.Sqrt(restPose.Length));
        int poseHeight = Mathf.Sqrt(restPose.Length) % 2 == 1 ? Mathf.RoundToInt(Mathf.Sqrt(restPose.Length)) : Mathf.RoundToInt(Mathf.Sqrt(restPose.Length)) + 1;
        Texture2D defColours = new Texture2D(poseWidth, poseWidth, TextureFormat.RGBA32, false);
        Debug.Log(poseWidth);
        Color[,] map = new Color[poseWidth, poseWidth];
        for(int i = 0; i < poseWidth; i++)
        {
            for (int j = 0; j < poseWidth; j++)
            {
                Color deformation = new Color(restPose[i].x - currentPose[i].x, restPose[i].y - currentPose[i].y, restPose[i].z - currentPose[i].z);
                map[i, j] = deformation;
                defColours.SetPixel(i, j, map[i, j]);
            }
        }
        File.WriteAllBytes(Application.dataPath + "/Poses2D/displacements.png", defColours.EncodeToPNG());
        Debug.Log("Displacements Mapped");
    }

    private void DeleteArrays()
    {
        for (int i = 0; i < animPosePoints.Length; i++)
            Destroy(animPosePoints[i]);
        for (int j = 0; j < restPosePoints.Length; j++)
            Destroy(restPosePoints[j]);
    }
}
/*
 *             if (vertTest)
            {
                GameObject ap = Instantiate(meshPoints);
                ap.GetComponentInChildren<MeshRenderer>().sharedMaterial = restMat;
                ap.transform.position = currentPose[i];
                animPosePoints[i] = ap;
                GameObject rp = Instantiate(meshPoints);
                rp.GetComponentInChildren<MeshRenderer>().sharedMaterial = animat;
                rp.transform.position = restPose[i];
                restPosePoints[i] = rp;
            }
 */