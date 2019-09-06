using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Computer_Graphics_Subject_And_Assessment : MonoBehaviour {
    #region Private
    private float Default_Metallic = 0f;
    private float Default_Smoothness = 0.5f;
    private float Default_Value = 0f;

    private Mesh Custom_Mesh;

    private Renderer Custom_Renderer;
    #endregion

    #region Public
    [HideInInspector]
    public bool Can_Object_Be_Reset = false;
    [HideInInspector]
    public bool Has_Object_Been_Initialized = false;

    [HideInInspector]
    public float Custom_Metallic = 0f;
    [HideInInspector]
    public float Custom_Smoothness = 0f;
    [HideInInspector]
    public float Custom_Object_Depth;
    [HideInInspector]
    public float Custom_Object_Height;
    [HideInInspector]
    public float Custom_Object_Width;

    [HideInInspector]
    public MeshFilter Custom_MeshFilter;

    [HideInInspector]
    public Texture Custom_Texture;
    #endregion

    //public void Create

    public void Object_Initialization()
    {
        Has_Object_Been_Initialized = true;
    }

    public void Object_Reset()
    {
        Custom_MeshFilter.mesh = null;
        Custom_Object_Depth = Default_Value;
        Custom_Object_Height = Default_Value;
        Custom_Object_Width = Default_Value;
        Set_Objects_Metallic_And_Smoothness(Default_Metallic, Default_Smoothness);
    }

    public void Object_Setup()
    {
        Mesh New_Custom_Mesh = new Mesh();

        // Vertices
        // locations of vertices
        Vector3[] Custom_Vertices = new Vector3[24];

        // Front Side
        Custom_Vertices[0] = new Vector3(0f, 0f, 0f);
        Custom_Vertices[1] = new Vector3(0f, Custom_Object_Height, 0f);
        Custom_Vertices[2] = new Vector3(Custom_Object_Width, Custom_Object_Height, 0f);
        Custom_Vertices[3] = new Vector3(Custom_Object_Width, 0f, 0f);

        // Top Side
        Custom_Vertices[4] = new Vector3(0f, Custom_Object_Height, 0f);
        Custom_Vertices[5] = new Vector3(0f, Custom_Object_Height, Custom_Object_Depth);
        Custom_Vertices[6] = new Vector3(Custom_Object_Width, Custom_Object_Height, Custom_Object_Depth);
        Custom_Vertices[7] = new Vector3(Custom_Object_Width, Custom_Object_Height, 0f);

        // Back Side
        Custom_Vertices[8] = new Vector3(Custom_Object_Width, 0f, Custom_Object_Depth);
        Custom_Vertices[9] = new Vector3(Custom_Object_Width, Custom_Object_Height, Custom_Object_Depth);
        Custom_Vertices[10] = new Vector3(0f, Custom_Object_Height, Custom_Object_Depth);
        Custom_Vertices[11] = new Vector3(0f, 0f, Custom_Object_Depth);

        // Bottom Side
        Custom_Vertices[12] = new Vector3(0f, 0f, Custom_Object_Depth);
        Custom_Vertices[13] = new Vector3(0f, 0f, 0f);
        Custom_Vertices[14] = new Vector3(Custom_Object_Width, 0f, 0f);
        Custom_Vertices[15] = new Vector3(Custom_Object_Width, 0f, Custom_Object_Depth);

        // Left Side
        Custom_Vertices[16] = new Vector3(0f, 0f, Custom_Object_Depth);
        Custom_Vertices[17] = new Vector3(0f, Custom_Object_Height, Custom_Object_Depth);
        Custom_Vertices[18] = new Vector3(0f, Custom_Object_Height, 0f);
        Custom_Vertices[19] = new Vector3(0f, 0f, 0f);

        // Right Side
        Custom_Vertices[20] = new Vector3(Custom_Object_Width, 0f, 0f);
        Custom_Vertices[21] = new Vector3(Custom_Object_Width, Custom_Object_Height, 0f);
        Custom_Vertices[22] = new Vector3(Custom_Object_Width, Custom_Object_Height, Custom_Object_Depth);
        Custom_Vertices[23] = new Vector3(Custom_Object_Width, 0f, Custom_Object_Depth);

        New_Custom_Mesh.vertices = Custom_Vertices;

        // Indices
        // determines which vertices make up an individual triangle
        //
        // this should always be a multiple of three
        //
        // each triangle should be specified in clock-wise order
        int[] Custom_Indices = new int[36];

        // Front Side
        Custom_Indices[0] = 0;
        Custom_Indices[1] = 1;
        Custom_Indices[2] = 2;

        Custom_Indices[3] = 0;
        Custom_Indices[4] = 2;
        Custom_Indices[5] = 3;

        // Top Side
        Custom_Indices[6] = 4;
        Custom_Indices[7] = 5;
        Custom_Indices[8] = 6;

        Custom_Indices[9] = 6;
        Custom_Indices[10] = 7;
        Custom_Indices[11] = 4;

        // Back Side (Wack)
        Custom_Indices[12] = 8;
        Custom_Indices[13] = 9;
        Custom_Indices[14] = 10;

        Custom_Indices[15] = 10;
        Custom_Indices[16] = 11;
        Custom_Indices[17] = 8;

        // Bottom Side
        Custom_Indices[18] = 12;
        Custom_Indices[19] = 13;
        Custom_Indices[20] = 14;

        Custom_Indices[21] = 14;
        Custom_Indices[22] = 15;
        Custom_Indices[23] = 12;

        // Left Side
        Custom_Indices[24] = 16;
        Custom_Indices[25] = 17;
        Custom_Indices[26] = 18;

        Custom_Indices[27] = 18;
        Custom_Indices[28] = 19;
        Custom_Indices[29] = 16;

        // Right Side
        Custom_Indices[30] = 20;
        Custom_Indices[31] = 21;
        Custom_Indices[32] = 22;

        Custom_Indices[33] = 22;
        Custom_Indices[34] = 23;
        Custom_Indices[35] = 20;

        New_Custom_Mesh.triangles = Custom_Indices;

        // Normals
        // describes how light bounces off the surface (at the vertex level)
        //
        // note that this data is interpolated across the surface of the triangle
        Vector3[] Custom_Normals = new Vector3[24];

        // Front Side
        Custom_Normals[0] = Vector3.forward;
        Custom_Normals[1] = Vector3.forward;
        Custom_Normals[2] = Vector3.forward;
        Custom_Normals[3] = Vector3.forward;

        // Top Side
        Custom_Normals[4] = Vector3.forward;
        Custom_Normals[5] = Vector3.forward;
        Custom_Normals[6] = Vector3.forward;
        Custom_Normals[7] = Vector3.forward;

        // Back Side
        Custom_Normals[8] = Vector3.forward;
        Custom_Normals[9] = Vector3.forward;
        Custom_Normals[10] = Vector3.forward;
        Custom_Normals[11] = Vector3.forward;

        // Bottom Side
        Custom_Normals[12] = Vector3.forward;
        Custom_Normals[13] = Vector3.forward;
        Custom_Normals[14] = Vector3.forward;
        Custom_Normals[15] = Vector3.forward;

        // Left Side
        Custom_Normals[16] = Vector3.forward;
        Custom_Normals[17] = Vector3.forward;
        Custom_Normals[18] = Vector3.forward;
        Custom_Normals[19] = Vector3.forward;

        // Right Side
        Custom_Normals[20] = Vector3.forward;
        Custom_Normals[21] = Vector3.forward;
        Custom_Normals[22] = Vector3.forward;
        Custom_Normals[23] = Vector3.forward;

        New_Custom_Mesh.normals = Custom_Normals;

        // STs, UVs
        // defines how textures are mapped onto the surface
        Vector2[] Custom_UVs = new Vector2[24];

        // Front Side
        Custom_UVs[0] = new Vector2(0f, 0f);
        Custom_UVs[1] = new Vector2(0f, 1f);
        Custom_UVs[2] = new Vector2(1f, 1f);
        Custom_UVs[3] = new Vector2(1f, 0f);

        // Top Side
        Custom_UVs[4] = new Vector2(0f, 0f);
        Custom_UVs[5] = new Vector2(0f, 1f);
        Custom_UVs[6] = new Vector2(1f, 1f);
        Custom_UVs[7] = new Vector2(1f, 0f);
        //Custom_UVs[4] = new Vector2(.875f, .875f);
        //Custom_UVs[5] = new Vector2(.875f, 1f);
        //Custom_UVs[6] = new Vector2(1f, 1f);
        //Custom_UVs[7] = new Vector2(1f, .875f);

        // Back Side
        Custom_UVs[8] = new Vector2(0f, 0f);
        Custom_UVs[9] = new Vector2(0f, 1f);
        Custom_UVs[10] = new Vector2(1f, 1f);
        Custom_UVs[11] = new Vector2(1f, 0f);

        // Bottom Side
        Custom_UVs[12] = new Vector2(0f, 0f);
        Custom_UVs[13] = new Vector2(0f, 1f);
        Custom_UVs[14] = new Vector2(1f, 1f);
        Custom_UVs[15] = new Vector2(1f, 0f);

        // Left Side
        Custom_UVs[16] = new Vector2(0f, 0f);
        Custom_UVs[17] = new Vector2(0f, 1f);
        Custom_UVs[18] = new Vector2(1f, 1f);
        Custom_UVs[19] = new Vector2(1f, 0f);

        // Right Side
        Custom_UVs[20] = new Vector2(0f, 0f);
        Custom_UVs[21] = new Vector2(0f, 1f);
        Custom_UVs[22] = new Vector2(1f, 1f);
        Custom_UVs[23] = new Vector2(1f, 0f);

        New_Custom_Mesh.uv = Custom_UVs;
        
        Custom_MeshFilter = GetComponent<MeshFilter>();
        Custom_MeshFilter.mesh = New_Custom_Mesh;
        Custom_Mesh = New_Custom_Mesh;

        Custom_Renderer = this.gameObject.GetComponent<Renderer>();
        Custom_Renderer.material.mainTexture = Custom_Texture;
    }

    public void Set_Objects_Metallic_And_Smoothness(float _Metallic, float _Smoothness)
    {
        if(Custom_Renderer != null)
        {
            Custom_Renderer.material.SetFloat("_Metallic", _Metallic);
            Custom_Renderer.material.SetFloat("_Glossiness", _Smoothness);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Computer_Graphics_Subject_And_Assessment))]
public class Computer_Graphics_Subject_And_Assessment_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Computer_Graphics_Subject_And_Assessment GEG = (Computer_Graphics_Subject_And_Assessment)target;

        // GEG.Has_Object_Been_Initialized = EditorGUILayout.Toggle("Initialize Object?", GEG.Has_Object_Been_Initialized);

        if(GEG.Can_Object_Be_Reset == false)
        {
            if (GEG.Has_Object_Been_Initialized == false)
            {
                if (GUILayout.Button("Create a 3D object"))
                {
                    GEG.Object_Initialization();
                }
            }
            else
            {
                GEG.Custom_Object_Depth = EditorGUILayout.FloatField("Object Depth: ", GEG.Custom_Object_Depth);
                GEG.Custom_Object_Height = EditorGUILayout.FloatField("Object Height: ", GEG.Custom_Object_Height);
                GEG.Custom_Object_Width = EditorGUILayout.FloatField("Object Width: ", GEG.Custom_Object_Width);
                GEG.Custom_Texture = EditorGUILayout.ObjectField("Texture", GEG.Custom_Texture, typeof(Texture), true) as Texture;
                if (GUILayout.Button("Finalize Properties")) { GEG.Can_Object_Be_Reset = true; GEG.Object_Setup(); }
            }
        }
        else
        {
            GEG.Custom_Metallic = EditorGUILayout.FloatField("Metallic: ", GEG.Custom_Metallic);
            GEG.Custom_Smoothness = EditorGUILayout.FloatField("Smoothness: ", GEG.Custom_Smoothness);

            if(GUILayout.Button("Reset Metallic/Smoothness to default")) { GEG.Set_Objects_Metallic_And_Smoothness(0f, .5f); }
            if(GUILayout.Button("Set Metallic/Smoothness"))
            {
                if (GEG.Custom_Metallic > 1f) { GEG.Custom_Metallic = 1f; }
                if (GEG.Custom_Smoothness > 1f) { GEG.Custom_Smoothness = 1f; }
                GEG.Set_Objects_Metallic_And_Smoothness(GEG.Custom_Metallic, GEG.Custom_Smoothness);
            }
            else if(GUILayout.Button("Reinitialize Object"))
            {
                GEG.Can_Object_Be_Reset = false;
                GEG.Object_Reset();
            }
        }
    }
}
#endif