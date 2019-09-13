using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Management_Backup : MonoBehaviour
{

    #region Private
    private bool Enable_Camera_Movement = false;
    private bool Enable_Timer;
    private bool Has_Camera_Been_Set = false;
    private bool Has_Camera_Reached_Offset = false;

    private BoxCollider BC;

    private Camera C;

    private Camera_Collision_Behavior C_C_B;

    private float Camera_Offset_Per_Second;
    private float GameObject_Offset_Per_Second;
    private float Original_Camera_Offset;
    private float Temporary_Camera_Offset_Per_Second;
    private float Temporary_Time_Offset;
    // T_O = Time Offset
    private float T_O = 0f;
    private float Temporary_Camera_Offset;

    private GameObject Camera_Collision_Check;
    private GameObject Player_GameObject;
    #endregion

    #region Public
    public float Camera_Offset = 0f;
    public float Time_Offset = 0f;

    public List<Camera> Camera_List = new List<Camera>();
    #endregion

    private void Start()
    {
        C = Camera_Select("Main Camera");
        Camera_Enabler("Main Camera");

        Camera_Offset_Per_Second = (Camera_Offset / Time_Offset) / 100f;

        Player_GameObject = GameObject.FindGameObjectWithTag("Player");

        C.transform.position = new Vector3(C.transform.position.x, C.transform.position.y, Player_GameObject.transform.position.z);

        GameObject_Offset_Per_Second = (Camera_Offset * .01f) / 100f;

        Temporary_Camera_Offset = Camera_Offset;
        Temporary_Camera_Offset_Per_Second = Camera_Offset_Per_Second;
        Temporary_Time_Offset = Time_Offset;
    }

    private void Update() { Camera_Positioning(); }

    private Camera Camera_Select(string _Camera)
    {
        Camera Selected_Camera = new Camera();
        for (int SJ = 0; SJ < Camera_List.Count; SJ++)
        {
            if (Camera_List[SJ].name == _Camera) { Selected_Camera = Camera_List[SJ]; }
        }
        return Selected_Camera;
    }

    public void Camera_Enabler(string _Camera)
    {
        for (int SJ = 0; SJ < Camera_List.Count; SJ++)
        {
            if (Camera_List[SJ].name != _Camera)
            {
                Camera_List[SJ].enabled = false;
            }
            else { Camera_List[SJ].enabled = true; }
        }
    }

    private void Camera_Positioning()
    {
        Debug.Log("T_C_O: " + Temporary_Camera_Offset);
        Debug.Log("T_T_O: " + Temporary_Time_Offset);
        if (Has_Camera_Been_Set == false)
        {
            Camera_Collision_Check = new GameObject("Camera Collision Check");
            Camera_Collision_Check.gameObject.AddComponent<Camera_Collision_Behavior>();
            Camera_Collision_Check.transform.parent = C.gameObject.transform;
            Camera_Collision_Check.transform.position = C.gameObject.transform.position;

            C_C_B = Camera_Collision_Check.GetComponent<Camera_Collision_Behavior>();

            Enable_Timer = true;

            BC = Camera_Collision_Check.gameObject.AddComponent<BoxCollider>();
            BC.size = new Vector3(BC.size.x / 2f, BC.size.y / 2f, BC.size.z / 2f);
            BC.isTrigger = true;

            Has_Camera_Been_Set = true;
        }

        bool Has_Collision_Occured = C_C_B.Has_Caused_Collision();

        Debug.Log("Has collision occured" + Has_Collision_Occured);

        if (Enable_Timer) { T_O += Time.deltaTime; }
        // Debug.Log("Time: " + T_O);

        if (!Enable_Camera_Movement)
        {
            if (Has_Collision_Occured == false && T_O >= Time_Offset) { Has_Camera_Reached_Offset = true; }

            if (Has_Collision_Occured == false && T_O < Time_Offset) { Camera_Collision_Check.transform.position += new Vector3(0f, 0f, Temporary_Camera_Offset_Per_Second); }
            else if (Has_Collision_Occured == true)
            {
                Camera_Collision_Check.transform.position = C.transform.position;
                Camera_Collision_Check.transform.rotation = Player_GameObject.transform.rotation;
                Temporary_Camera_Offset /= 2f;
                Temporary_Time_Offset = Time_Offset / 2f;
                Temporary_Camera_Offset_Per_Second = (Temporary_Camera_Offset / Temporary_Time_Offset) / 100f;
                Debug.Log("Oh God, I Love Memes");
                Enable_Timer = true;
                T_O = 0f;
            }

            if (Has_Camera_Reached_Offset) { Enable_Camera_Movement = true; }
        }
        else
        {
            if (Temporary_Camera_Offset != Camera_Offset)
            {
                if (Enable_Timer == true)
                {
                    Camera_Collision_Check.transform.position = C.transform.position;
                    Camera_Collision_Check.transform.rotation = Player_GameObject.transform.rotation;
                    Enable_Timer = false;
                    T_O = 0f;
                }
                T_O += Time.deltaTime;
                if (T_O < Time_Offset) { C.transform.position += new Vector3(0f, 0f, Temporary_Camera_Offset_Per_Second); }
                else
                {
                    Enable_Camera_Movement = false;
                    Temporary_Camera_Offset *= 2f;
                    Temporary_Time_Offset = Time_Offset * 2f;
                    Temporary_Camera_Offset_Per_Second = (Temporary_Camera_Offset / Temporary_Time_Offset) / 100f;
                }
            }
            else
            {
                if (Enable_Timer == true)
                {
                    Camera_Collision_Check.transform.position = C.transform.position;
                    Camera_Collision_Check.transform.rotation = Player_GameObject.transform.rotation;
                    Enable_Timer = false;
                    T_O = 0f;
                }
                T_O += Time.deltaTime;

                if (T_O < Time_Offset) { C.transform.position += new Vector3(0f, 0f, Temporary_Camera_Offset_Per_Second); }
                else { T_O = Time_Offset; }
            }
        }
    }
}