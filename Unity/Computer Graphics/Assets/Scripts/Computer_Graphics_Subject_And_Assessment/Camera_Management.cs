using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Management : MonoBehaviour
{
    #region Private
    private Camera C;

    private GameObject Player_GameObject;
    #endregion

    #region Public
    public List<Camera> Camera_List = new List<Camera>();
    #endregion

    private void Start()
    {
        C = Camera_Select("Main Camera");
        Camera_Enabler("Main Camera");

        Player_GameObject = GameObject.FindGameObjectWithTag("Player");
    }

    public Camera Camera_Select(string _Camera)
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
}