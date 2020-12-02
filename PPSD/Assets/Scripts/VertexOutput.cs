using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VertexOutput : MonoBehaviour
{
    [SerializeField] private bool vertTest;
    [SerializeField] private SkinnedMeshRenderer smr;
    [SerializeField] Material restMat;
    [SerializeField] Material animat;
    private int disRow;
    Vector3[] restPose;
    private void Start()
    {
        restPose = smr.sharedMesh.vertices;
    }
    public void GenerateColourArray()
    {
        // For instance the current pose
        Mesh newMesh = new Mesh();
        smr.BakeMesh(newMesh);
        Vector3[] currentPose = newMesh.vertices;
        int poseWidth = Mathf.RoundToInt(Mathf.Sqrt(restPose.Length));
        int poseHeight = Mathf.Sqrt(restPose.Length) % 2 == 1 ? Mathf.RoundToInt(Mathf.Sqrt(restPose.Length)) : Mathf.RoundToInt(Mathf.Sqrt(restPose.Length)) + 1;
        Texture2D defColours = new Texture2D(poseWidth, poseWidth, TextureFormat.RGBA32, false);
        Color[,] map = new Color[poseWidth, poseWidth];
        map = PaintDeformations(poseWidth, poseHeight, currentPose, defColours);
        defColours.Apply();
        Material colourMap = new Material(Shader.Find("Standard"));
        colourMap.SetTexture("_MainTex", defColours);
        smr.sharedMaterial.EnableKeyword("_MAINTEX");
        smr.sharedMaterial.SetTexture("_MainTex", defColours);
        File.WriteAllBytes(Application.dataPath + "/Poses2D/displacements.png", defColours.EncodeToPNG());
        Debug.Log("Displacements Mapped");
    }

    private Color[,] PaintDeformations(int _i_poseWidth, int _i_poseHeight, Vector3[] _A_currentPose, Texture2D _tex_outputTexture)
    {
        Color[,] newMap = new Color[_i_poseWidth, _i_poseHeight];
        for (int i = 0; i < _i_poseWidth; i++)
        {
            for (int j = 0; j < _i_poseWidth; j++)
            {
                Color deformation = new Color(_A_currentPose[i].x - restPose[i].x, _A_currentPose[i].y - restPose[i].y, _A_currentPose[i].z - restPose[i].z);
                newMap[i, j] = deformation;
                _tex_outputTexture.SetPixel(i, j, newMap[i, j]);
            }
        }
        return newMap;
    }

    private void PaintUVs()
    {

    }

}