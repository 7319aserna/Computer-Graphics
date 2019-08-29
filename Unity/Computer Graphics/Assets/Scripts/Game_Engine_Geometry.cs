using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

// *****----- Generate a 2D n-gon -----*****

// *****-------------------------------*****

public class Game_Engine_Geometry : MonoBehaviour
{
    #region Private
    private Mesh Custom_Mesh;

    // *****----- Debug Visualizer -----*****
    private bool HaveGizmosBeenCreated = false;

    private GameObject Gizmo_GameObject;
    // *****----------------------------*****
    #endregion

    #region Public
    // *****----- Creating a 3D Cube by Hand -----*****
    public bool Cube_Bool = false;
    // *****--------------------------------------*****

    // *****----- Creating a 2D Pentagon by Hand -----*****
    public bool Pentagon_Bool = false;
    // *****--------------------------------------*****

    // *****----- Creating a 2D Quad by Hand -----*****
    public bool Quad_Bool = false;
    // *****--------------------------------------*****

    // *****----- Generate a 2D Quad -----*****
    public float Generated_Quad_Height;
    public float Generated_Quad_Width;
    // *****------------------------------*****

    // *****----- Generate a 2D n-gon -----*****
    public float NGons_Radius = 0f;
    public int Number_Of_Sides = 0;
    // *****-------------------------------*****

    // *****----- Game Engine Geometry (Subject and Assessment Guide) -----*****
    public bool Has_Object_Been_Initialized = false;

    [HideInInspector]
    public GameObject Custom_Object;
    // *****---------------------------------------------------------------*****
    #endregion

    void Start()
    {
        // *****----- Creating a 2D Quad by Hand -----*****
        Mesh New_Quad_Mesh = new Mesh();
        // *****--------------------------------------*****

        // *****----- Creating a 2D Pentagon by Hand -----*****
        Mesh New_Pentagon_Mesh = new Mesh();
        // *****--------------------------------------*****

        // *****----- Creating a 3D Cube by Hand -----*****
        Mesh New_Cube_Mesh = new Mesh();
        // *****--------------------------------------*****

        // *****----- Generate a 2D Quad -----*****
        Mesh New_Generated_Quad_Mesh = new Mesh();
        // *****------------------------------*****

        // *****----- Generate a 2D n-gon -----*****
        Mesh New_NGon_Mesh = new Mesh();
        // *****-------------------------------*****

        // Vertices
        // locations of vertices

        // *****----- Creating a 2D Quad by Hand -----*****
        Vector3[] Quad_Vertices = new Vector3[4];

        Quad_Vertices[0] = new Vector3(0f, 0f, 0f);
        Quad_Vertices[1] = new Vector3(0f, 1f, 0f);
        Quad_Vertices[2] = new Vector3(1f, 1f, 0f);
        Quad_Vertices[3] = new Vector3(1f, 0f, 0f);

        New_Quad_Mesh.vertices = Quad_Vertices;
        // *****--------------------------------------*****

        // *****----- Creating a 2D Pentagon by Hand -----*****
        Vector3[] Pentagon_Vertices = new Vector3[6];

        // Outside
        Pentagon_Vertices[0] = new Vector3(-1f, 0f, 0f);
        Pentagon_Vertices[1] = new Vector3(0f, 1f, 0f);
        Pentagon_Vertices[2] = new Vector3(1f, 0f, 0f);
        Pentagon_Vertices[3] = new Vector3(.5f, -1f, 0f);
        Pentagon_Vertices[4] = new Vector3(-.5f, -1f, 0f);

        // Inside
        Pentagon_Vertices[5] = new Vector3(0f, 0f, 0f);

        New_Pentagon_Mesh.vertices = Pentagon_Vertices;
        // *****--------------------------------------*****

        // *****----- Creating a 3D Cube by Hand -----*****
        Vector3[] Cube_Vertices = new Vector3[24];

        // Front Side
        Cube_Vertices[0] = new Vector3(0f, 0f, 0f);
        Cube_Vertices[1] = new Vector3(0f, 1f, 0f);
        Cube_Vertices[2] = new Vector3(1f, 1f, 0f);
        Cube_Vertices[3] = new Vector3(1f, 0f, 0f);

        // Top Side
        Cube_Vertices[4] = new Vector3(0f, 1f, 0f);
        Cube_Vertices[5] = new Vector3(0f, 1f, 1f);
        Cube_Vertices[6] = new Vector3(1f, 1f, 1f);
        Cube_Vertices[7] = new Vector3(1f, 1f, 0f);

        // Back Side
        Cube_Vertices[8] = new Vector3(0f, 0f, 1f);
        Cube_Vertices[9] = new Vector3(0f, 1f, 1f);
        Cube_Vertices[10] = new Vector3(1f, 1f, 1f);
        Cube_Vertices[11] = new Vector3(1f, 0f, 1f);

        // Bottom Side
        Cube_Vertices[12] = new Vector3(0f, 0f, 1f);
        Cube_Vertices[13] = new Vector3(0f, 0f, 0f);
        Cube_Vertices[14] = new Vector3(1f, 0f, 0f);
        Cube_Vertices[15] = new Vector3(1f, 0f, 1f);

        // Left Side
        Cube_Vertices[16] = new Vector3(0f, 0f, 1f);
        Cube_Vertices[17] = new Vector3(0f, 1f, 1f);
        Cube_Vertices[18] = new Vector3(0f, 1f, 0f);
        Cube_Vertices[19] = new Vector3(0f, 0f, 0f);

        // Right Side
        Cube_Vertices[20] = new Vector3(1f, 0f, 0f);
        Cube_Vertices[21] = new Vector3(1f, 1f, 0f);
        Cube_Vertices[22] = new Vector3(1f, 1f, 1f);
        Cube_Vertices[23] = new Vector3(1f, 0f, 1f);

        New_Cube_Mesh.vertices = Cube_Vertices;
        // *****--------------------------------------*****

        // *****----- Generate a 2D Quad -----*****
        Vector3[] Generated_Quad_Vertices = new Vector3[4];

        Generated_Quad_Vertices[0] = new Vector3(0f, 0f, 0f);
        Generated_Quad_Vertices[1] = new Vector3(0f, Generated_Quad_Height, 0f);
        Generated_Quad_Vertices[2] = new Vector3(Generated_Quad_Width, 0f, 0f);
        Generated_Quad_Vertices[3] = new Vector3(Generated_Quad_Width, Generated_Quad_Height, 0f);

        New_Generated_Quad_Mesh.vertices = Generated_Quad_Vertices;
        // *****------------------------------*****

        // *****----- Generate a 2D n-gon -----*****
        float Amount_To_Rotate_By;

        Vector3[] NGon_Vertices = new Vector3[Number_Of_Sides + 1];

        if(NGons_Radius != 0 && Number_Of_Sides != 0)
        {
            GameObject Starting_Point_GameObject;
            Starting_Point_GameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Starting_Point_GameObject.name = "Starting Point";
            Starting_Point_GameObject.transform.localScale = new Vector3(Starting_Point_GameObject.transform.localScale.x / 4, Starting_Point_GameObject.transform.localScale.y / 4, Starting_Point_GameObject.transform.localScale.z / 4);
            NGon_Vertices[0] = Starting_Point_GameObject.transform.position;

            GameObject Rotation_Point_GameObject;
            Rotation_Point_GameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Rotation_Point_GameObject.name = "Rotation Point";
            Rotation_Point_GameObject.transform.localScale = new Vector3(Rotation_Point_GameObject.transform.localScale.x / 4, Rotation_Point_GameObject.transform.localScale.y / 4, Rotation_Point_GameObject.transform.localScale.z / 4);
            Rotation_Point_GameObject.transform.parent = Starting_Point_GameObject.gameObject.transform;
            Rotation_Point_GameObject.transform.position = new Vector3(0f, NGons_Radius, 0f);
            NGon_Vertices[1] = Rotation_Point_GameObject.transform.position;

            Amount_To_Rotate_By = 360 / Number_Of_Sides;

            for (int SJ = 2; SJ < NGon_Vertices.Length; SJ++)
            {
                Starting_Point_GameObject.transform.Rotate(0f, 0f, -Amount_To_Rotate_By);
                NGon_Vertices[SJ] = Rotation_Point_GameObject.transform.position;
            }
        }

        New_NGon_Mesh.vertices = NGon_Vertices;
        // *****-------------------------------*****

        // Indices
        // determines which vertices make up an individual triangle
        //
        // this should always be a multiple of three
        //
        // each triangle should be specified in clock-wise order

        // *****----- Creating a 2D Quad by Hand -----*****
        int[] Quad_Indices = new int[6];

        Quad_Indices[0] = 0;
        Quad_Indices[1] = 1;
        Quad_Indices[2] = 2;

        Quad_Indices[3] = 3;
        Quad_Indices[4] = 0;
        Quad_Indices[5] = 2;

        New_Quad_Mesh.triangles = Quad_Indices;
        // *****--------------------------------------*****

        // *****----- Creating a 2D Pentagon by Hand -----*****
        int[] Pentagon_Indices = new int[15];

        Pentagon_Indices[0] = 5;
        Pentagon_Indices[1] = 0;
        Pentagon_Indices[2] = 1;

        Pentagon_Indices[3] = 5;
        Pentagon_Indices[4] = 1;
        Pentagon_Indices[5] = 2;

        Pentagon_Indices[6] = 5;
        Pentagon_Indices[7] = 2;
        Pentagon_Indices[8] = 3;

        Pentagon_Indices[9] = 5;
        Pentagon_Indices[10] = 3;
        Pentagon_Indices[11] = 4;

        Pentagon_Indices[12] = 5;
        Pentagon_Indices[13] = 4;
        Pentagon_Indices[14] = 0;

        New_Pentagon_Mesh.triangles = Pentagon_Indices;
        // *****--------------------------------------*****

        // *****----- Creating a 3D Cube by Hand -----*****
        int[] Cube_Indices = new int[36];

        // Front Side
        Cube_Indices[0] = 0;
        Cube_Indices[1] = 1;
        Cube_Indices[2] = 2;

        Cube_Indices[3] = 0;
        Cube_Indices[4] = 2;
        Cube_Indices[5] = 3;

        // Top Side
        Cube_Indices[6] = 4;
        Cube_Indices[7] = 5;
        Cube_Indices[8] = 6;

        Cube_Indices[9] = 6;
        Cube_Indices[10] = 7;
        Cube_Indices[11] = 4;

        // Back Side (Wack)
        Cube_Indices[12] = 8;
        Cube_Indices[13] = 11;
        Cube_Indices[14] = 10;

        Cube_Indices[15] = 9;
        Cube_Indices[16] = 8;
        Cube_Indices[17] = 10;

        // Bottom Side
        Cube_Indices[18] = 12;
        Cube_Indices[19] = 13;
        Cube_Indices[20] = 14;

        Cube_Indices[21] = 14;
        Cube_Indices[22] = 15;
        Cube_Indices[23] = 12;

        // Left Side
        Cube_Indices[24] = 16;
        Cube_Indices[25] = 17;
        Cube_Indices[26] = 18;

        Cube_Indices[27] = 18;
        Cube_Indices[28] = 19;
        Cube_Indices[29] = 16;

        Cube_Indices[30] = 20;
        Cube_Indices[31] = 21;
        Cube_Indices[32] = 22;

        Cube_Indices[33] = 22;
        Cube_Indices[34] = 23;
        Cube_Indices[35] = 20;

        New_Cube_Mesh.triangles = Cube_Indices;
        // *****--------------------------------------*****

        // *****----- Generate a 2D Quad -----*****
        int[] Generated_Quad_Indices = new int[6];

        Generated_Quad_Indices[0] = 0;
        Generated_Quad_Indices[1] = 1;
        Generated_Quad_Indices[2] = 2;

        Generated_Quad_Indices[3] = 1;
        Generated_Quad_Indices[4] = 3;
        Generated_Quad_Indices[5] = 2;

        New_Generated_Quad_Mesh.triangles = Generated_Quad_Indices;
        // *****------------------------------*****

        // *****----- Generate a 2D n-gon -----*****
        int Amount_Of_Indices = 3 * Number_Of_Sides;
        int Current_Index = 0;
        int[] NGon_Indices = new int[Amount_Of_Indices];

        for(int SJ = 0; SJ < NGon_Indices.Length; SJ++)
        {
            if(SJ == NGon_Indices.Length - 3)
            {
                NGon_Indices[SJ] = 0;
                NGon_Indices[SJ + 1] = Current_Index + 1;
                NGon_Indices[SJ + 2] = 1;
                SJ = NGon_Indices.Length;
            }
            else
            {
                NGon_Indices[SJ] = 0;
                NGon_Indices[SJ + 1] = Current_Index + 1;
                NGon_Indices[SJ + 2] = Current_Index + 2;
                Current_Index++;
                SJ = SJ + 2; 
            }
        }

        New_NGon_Mesh.triangles = NGon_Indices;
        // *****-------------------------------*****

        // Normals
        // describes how light bounces off the surface (at the vertex level)
        //
        // note that this data is interpolated across the surface of the triangle

        // *****----- Creating a 2D Quad by Hand / Generate a 2D Quad -----*****
        Vector3[] Quad_Normals = new Vector3[4];

        Quad_Normals[0] = Vector3.forward;
        Quad_Normals[1] = Vector3.forward;
        Quad_Normals[2] = Vector3.forward;
        Quad_Normals[3] = Vector3.forward;

        New_Quad_Mesh.normals = Quad_Normals;
        New_Generated_Quad_Mesh.normals = Quad_Normals;
        // *****-----------------------------------------------------------*****

        // *****----- Creating a 2D Pentagon by Hand -----*****
        Vector3[] Pentagon_Normals = new Vector3[6];

        Pentagon_Normals[0] = Vector3.forward;
        Pentagon_Normals[1] = Vector3.forward;
        Pentagon_Normals[2] = Vector3.forward;
        Pentagon_Normals[3] = Vector3.forward;
        Pentagon_Normals[4] = Vector3.forward;
        Pentagon_Normals[5] = Vector3.forward;

        New_Pentagon_Mesh.normals = Pentagon_Normals;
        // *****--------------------------------------*****

        // *****----- Creating a 3D Cube by Hand -----*****
        Vector3[] Cube_Normals = new Vector3[24];

        // Front Side
        Cube_Normals[0] = Vector3.forward;
        Cube_Normals[1] = Vector3.forward;
        Cube_Normals[2] = Vector3.forward;
        Cube_Normals[3] = Vector3.forward;

        // Top Side
        Cube_Normals[4] = Vector3.forward;
        Cube_Normals[5] = Vector3.forward;
        Cube_Normals[6] = Vector3.forward;
        Cube_Normals[7] = Vector3.forward;

        // Back Side
        Cube_Normals[8] = Vector3.forward;
        Cube_Normals[9] = Vector3.forward;
        Cube_Normals[10] = Vector3.forward;
        Cube_Normals[11] = Vector3.forward;

        // Bottom Side
        Cube_Normals[12] = Vector3.forward;
        Cube_Normals[13] = Vector3.forward;
        Cube_Normals[14] = Vector3.forward;
        Cube_Normals[15] = Vector3.forward;

        // Left Side
        Cube_Normals[16] = Vector3.forward;
        Cube_Normals[17] = Vector3.forward;
        Cube_Normals[18] = Vector3.forward;
        Cube_Normals[19] = Vector3.forward;

        // Right Side
        Cube_Normals[20] = Vector3.forward;
        Cube_Normals[21] = Vector3.forward;
        Cube_Normals[22] = Vector3.forward;
        Cube_Normals[23] = Vector3.forward;

        New_Cube_Mesh.normals = Cube_Normals;
        // *****--------------------------------------*****

        // *****----- Generate a 2D n-gon -----*****
        Vector3[] NGon_Normals = new Vector3[Number_Of_Sides + 1];

        for(int SJ = 0; SJ < NGon_Normals.Length; SJ++)
        {
            NGon_Normals[SJ] = Vector3.forward;
        }

        New_NGon_Mesh.normals = NGon_Normals;
        // *****-------------------------------*****

        // STs, UVs
        // defines how textures are mapped onto the surface

        // *****----- Creating a 2D Quad by Hand -----*****
        Vector2[] Quad_UVs = new Vector2[4];

        Quad_UVs[0] = new Vector2(0f, 0f);
        Quad_UVs[1] = new Vector2(0f, 1f);
        Quad_UVs[2] = new Vector2(1f, 1f);
        Quad_UVs[3] = new Vector2(1f, 0f);

        New_Quad_Mesh.uv = Quad_UVs;

        if (Quad_Bool == true)
        {
            MeshFilter Quad_MeshFilter = GetComponent<MeshFilter>();
            Quad_MeshFilter.mesh = New_Quad_Mesh;
            Custom_Mesh = New_Quad_Mesh;
        }
        // *****--------------------------------------*****

        // *****----- Creating a 2D Pentagon by Hand -----*****
        Vector2[] Pentagon_UVs = new Vector2[6];

        // Outside
        Pentagon_UVs[0] = new Vector2(-1f, 0f);
        Pentagon_UVs[1] = new Vector2(0f, 1f);
        Pentagon_UVs[2] = new Vector2(1f, 0f);
        Pentagon_UVs[3] = new Vector2(.5f, -1f);
        Pentagon_UVs[4] = new Vector2(-.5f, -1f);

        // Inside
        Pentagon_UVs[5] = new Vector2(0f, 0f);

        New_Pentagon_Mesh.uv = Pentagon_UVs;

        if (Pentagon_Bool == true)
        {
            MeshFilter Pentagon_MeshFilter = GetComponent<MeshFilter>();
            Pentagon_MeshFilter.mesh = New_Pentagon_Mesh;
            Custom_Mesh = New_Pentagon_Mesh;
        }
        // *****--------------------------------------*****

        // *****----- Creating a 3D Cube by Hand -----*****
        Vector2[] Cube_UVs = new Vector2[24];

        // Front Side
        Cube_UVs[0] = new Vector2(0f, 0f);
        Cube_UVs[1] = new Vector2(0f, 1f);
        Cube_UVs[2] = new Vector2(1f, 1f);
        Cube_UVs[3] = new Vector2(1f, 0f);

        // Top Side
        Cube_UVs[4] = new Vector2(0f, 1f);
        Cube_UVs[5] = new Vector2(0f, 1f);
        Cube_UVs[6] = new Vector2(1f, 1f);
        Cube_UVs[7] = new Vector2(1f, 1f);

        // Back Side
        Cube_UVs[8] = new Vector2(0f, 0f);
        Cube_UVs[9] = new Vector2(0f, 1f);
        Cube_UVs[10] = new Vector2(1f, 1f);
        Cube_UVs[11] = new Vector2(1f, 0f);

        // Bottom Side
        Cube_UVs[12] = new Vector2(0f, 0f);
        Cube_UVs[13] = new Vector2(0f, 0f);
        Cube_UVs[14] = new Vector2(1f, 0f);
        Cube_UVs[15] = new Vector2(1f, 0f);

        // Left Side
        Cube_UVs[16] = new Vector2(0f, 0f);
        Cube_UVs[17] = new Vector2(0f, 1f);
        Cube_UVs[18] = new Vector2(0f, 1f);
        Cube_UVs[19] = new Vector2(0f, 0f);

        // Right Side
        Cube_UVs[20] = new Vector2(1f, 0f);
        Cube_UVs[21] = new Vector2(1f, 1f);
        Cube_UVs[22] = new Vector2(1f, 1f);
        Cube_UVs[23] = new Vector2(1f, 0f);

        New_Cube_Mesh.uv = Cube_UVs;

        if(Cube_Bool == true)
        {
            MeshFilter Cube_MeshFilter = GetComponent<MeshFilter>();
            Cube_MeshFilter.mesh = New_Cube_Mesh;
            Custom_Mesh = New_Cube_Mesh;
        }
        // *****--------------------------------------*****

        // *****----- Generate a 2D Quad -----*****
        Vector2[] Generated_Quad_UVs = new Vector2[4];

        Generated_Quad_UVs[0] = new Vector2(0f, 0f);
        Generated_Quad_UVs[1] = new Vector2(0f, Generated_Quad_Height);
        Generated_Quad_UVs[2] = new Vector2(Generated_Quad_Width, 0f);
        Generated_Quad_UVs[3] = new Vector2(Generated_Quad_Width, Generated_Quad_Height);

        New_Generated_Quad_Mesh.uv = Generated_Quad_UVs;

        if(Generated_Quad_Height != 0f && Generated_Quad_Width != 0f && NGons_Radius == 0f && Number_Of_Sides == 0f && Cube_Bool == false && Pentagon_Bool == false && Quad_Bool == false)
        {
            MeshFilter New_Generated_Quad_MeshFilter = GetComponent<MeshFilter>();
            New_Generated_Quad_MeshFilter.mesh = New_Generated_Quad_Mesh;
            Custom_Mesh = New_Generated_Quad_Mesh;
        }
        // *****------------------------------*****

        // *****----- Generate a 2D n-gon -----*****
        Vector2[] NGon_UVs = new Vector2[Number_Of_Sides + 1];

        for(int SJ = 0; SJ < NGon_UVs.Length; SJ++)
        {
            NGon_UVs[SJ] = new Vector2(NGon_Vertices[SJ].x, NGon_Vertices[SJ].y);
        }

        New_NGon_Mesh.uv = NGon_UVs;

        if (Generated_Quad_Height == 0f && Generated_Quad_Width == 0f && NGons_Radius != 0f && Number_Of_Sides != 0f && Cube_Bool == false && Pentagon_Bool == false && Quad_Bool == false)
        {
            MeshFilter New_NGon_MeshFilter = GetComponent<MeshFilter>();
            New_NGon_MeshFilter.mesh = New_NGon_Mesh;
            Custom_Mesh = New_NGon_Mesh;
        }
        // *****-------------------------------*****
    }

    private void Update()
    {
        Debug_Visualizer();

        if (Input.GetKeyDown(KeyCode.T))
        {
            gameObject.transform.position += new Vector3(9.5f, 9.5f);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameObject.transform.localScale += new Vector3(Gizmo_GameObject.transform.localScale.x * 2, Gizmo_GameObject.transform.localScale.y * 2, Gizmo_GameObject.transform.localScale.z * 2);
        }
    }

    // *****----- Game Engine Geometry Functions -----*****

    // *****----- Debug Visualizer -----*****
    private void Debug_Visualizer()
    {
        if(HaveGizmosBeenCreated == false)
        {
            for (int SJ = 0; SJ < Custom_Mesh.vertices.Length; SJ++)
            {
                Gizmo_GameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Gizmo_GameObject.name = "Vertex";
                Gizmo_GameObject.transform.parent = this.gameObject.transform;
                Gizmo_GameObject.transform.position = Custom_Mesh.vertices[SJ];
                Gizmo_GameObject.transform.localScale = new Vector3(Gizmo_GameObject.transform.localScale.x / 4, Gizmo_GameObject.transform.localScale.y / 4, Gizmo_GameObject.transform.localScale.z / 4);
            }
            HaveGizmosBeenCreated = true;
        }
        for (int St_Jo = 0; St_Jo < Custom_Mesh.vertices.Length; St_Jo++)
        {
            Gizmo_GameObject.transform.position = Custom_Mesh.vertices[St_Jo];
        }
    }
    // *****----------------------------*****
    private void OnDestroy()
    {
        if (Custom_Mesh != null)
        {
            Destroy(Custom_Mesh);
        }
    }
    // *****--------------------------------------*****
}