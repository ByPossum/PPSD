using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public unsafe class VertexOutput : MonoBehaviour
{
    [SerializeField] private bool vertTest;
    [SerializeField] private Vector3 v_minRange;
    [SerializeField] private Vector3 v_maxRange;
    [SerializeField] private SkinnedMeshRenderer smr;
    [SerializeField] Material restMat;
    [SerializeField] Material animat;
    [SerializeField] Material mat_rbf;
    [SerializeField] private GameObject go_joints;
    [SerializeField] private AnimationData[] ad_viewableData;
    private int disRow;
    Vector3[] restPose;
    private void Start()
    {
        restPose = smr.sharedMesh.vertices;
    }

    public void GenerateAnimData()
    {
        Mesh posedMesh = new Mesh();
        smr.BakeMesh(posedMesh);
        Vector3[] _currentPose = posedMesh.vertices;
        ad_viewableData = CreateAnimData(restPose.Length, _currentPose);

    }

    public void GenerateColourArray()
    {
        // For instance the current pose
        Mesh newMesh = new Mesh();
        smr.BakeMesh(newMesh);
        Vector3[] currentPose = newMesh.vertices;
        int poseWidth = Mathf.RoundToInt(Mathf.Sqrt(restPose.Length));
        int poseHeight = Mathf.Sqrt(restPose.Length) % 2 == 1 ? Mathf.RoundToInt(Mathf.Sqrt(restPose.Length)) : Mathf.RoundToInt(Mathf.Sqrt(restPose.Length)) + 1;
        // Combined, and seperated colours
        Texture2D defColours = new Texture2D(poseWidth, poseWidth, TextureFormat.RGBA32, false);
        Texture2D redColours = new Texture2D(poseWidth, poseWidth, TextureFormat.RGBA32, false);
        Texture2D greenColours = new Texture2D(poseWidth, poseWidth, TextureFormat.RGBA32, false);
        Texture2D blueColours = new Texture2D(poseWidth, poseWidth, TextureFormat.RGBA32, false);
        // Create a texture array to draaw the colours to
        Texture2D[] defs = new Texture2D[4];
        defs[0] = defColours;
        defs[1] = redColours; defs[2] = greenColours; defs[3] = blueColours;
        // Get the colours from the deformations
        Color[,] map = new Color[poseWidth, poseWidth];
        map = PaintDeformations(poseWidth, poseHeight, currentPose, defs);
        defColours.Apply();
        // Output the colours as a material and paint onto the model
        Material colourMap = new Material(Shader.Find("Standard"));
        colourMap.SetTexture("_MainTex", defColours);
        smr.sharedMaterial.EnableKeyword("_MAINTEX");
        smr.sharedMaterial.SetTexture("_MainTex", defColours);
        // Write the displacements to file
        File.WriteAllBytes(Application.dataPath + "/Resources/Poses2D/displacements.png", defColours.EncodeToPNG());
        Debug.Log("Displacements Mapped");
    }

    public void SendFloatData()
    {
        foreach(AnimationData ad in ad_viewableData)
            mat_rbf.SetFloatArray("_transformData", new float[] { ad.f_vTransform[0], ad.f_vTransform[1], ad.f_vTransform[2] } );
    }

    private Color[,] PaintDeformations(int _i_poseWidth, int _i_poseHeight, Vector3[] _A_currentPose, Texture2D[] _tex_outputTexture)
    {
        Color[,] newMap = new Color[_i_poseWidth, _i_poseHeight];
        for (int i = 0; i < _i_poseWidth; i++)
        {
            for (int j = 0; j < _i_poseWidth; j++)
            {
                // 
                Color deformation = new Color((((_A_currentPose[i].x - restPose[i].x) + Mathf.Abs(v_minRange.x)) / (v_maxRange.x - v_minRange.x)),
                    (((_A_currentPose[i].y - restPose[i].y) + Mathf.Abs(v_minRange.y)) / (v_maxRange.y - v_minRange.y)),
                    (((_A_currentPose[i].z - restPose[i].z) + Mathf.Abs(v_maxRange.z)) / (v_maxRange.z - v_minRange.z)));
                Color redDef = new Color((((_A_currentPose[i].x - restPose[i].x) + Mathf.Abs(v_minRange.x)) / (v_maxRange.x - v_minRange.x)), 0.0f, 0.0f);
                Color greenDef = new Color(0.0f, (((_A_currentPose[i].y - restPose[i].y) + Mathf.Abs(v_minRange.y)) / (v_maxRange.y - v_minRange.z)), 0.0f);
                Color blueDef = new Color(0.0f, 0.0f, (((_A_currentPose[i].z - restPose[i].z) + Mathf.Abs(v_minRange.z)) / (v_maxRange.z - v_minRange.z)));

                newMap[i, j] = deformation;
                _tex_outputTexture[0].SetPixel(i, j, newMap[i, j]);
            }
        }
        return newMap;
    }

    private AnimationData[] CreateAnimData(int _size, Vector3[] _currentVerts)
    {
        AnimationData[] _animData = new AnimationData[_size];
        for (int i = 0; i < _size; i++)
            _animData[i] = ConvertToAnimData(_currentVerts[i], v_minRange, v_maxRange, restPose[i], Vector3.zero);
        return _animData;
    }

    private AnimationData ConvertToAnimData(Vector3 _current, Vector3 _min, Vector3 _max, Vector3 _rest, Vector3 _joint)
    {
        Quaternion _jointQuat = Quaternion.Euler(_joint);
        return new AnimationData(((_current.x - _rest.x) + _min.x) / (_max.x - _min.x),
            ((_current.y - _rest.y) + _min.x) / (_max.y - _min.y),
            ((_current.z - _rest.z) + _min.z)/ (_max.z - _min.z),
            _jointQuat);
    }

    private void PaintUVs()
    {

    }

}