using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Management : MonoBehaviour {

    private Camera C;

    public List<Camera> Camera_List = new List<Camera>();

    private void Start() { C = Camera_Select("Main Camera"); }

    public void Camera_Enabler(string _Camera)
    {
        for(int SJ = 0; SJ < Camera_List.Count; SJ++)
        {
            if(Camera_List[SJ].name != _Camera)
            {
                Camera_List[SJ].enabled = false;
            }
            else { Camera_List[SJ].enabled = true; }
        }
    }

    private void Camera_Positioning()
    {
        BoxCollider BC;
        BC = C.gameObject.AddComponent<BoxCollider>();
        BC.isTrigger = true;

        Collider Camera_Collider;

    }

    private Camera Camera_Select(string _Camera)
    {
        Camera Selected_Camera = new Camera();
        for (int SJ = 0; SJ < Camera_List.Count; SJ++)
        {
            if(Camera_List[SJ].name == _Camera) { Selected_Camera = Camera_List[SJ]; }
        }
        return Selected_Camera;
    }
}
