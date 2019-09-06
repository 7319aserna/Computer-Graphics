using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Button_Interaction_Behavior : MonoBehaviour {

    #region Private
    private Animation_I A_I;
    private Animation_II A_II;

    private bool Is_Player_On_Trigger = false;

    private GameObject Player_GameObject;

    private Quaternion Player_Rotation;
    #endregion

    #region Public
    public float Distance_Between_Objects;

    public TextMeshProUGUI Button_Text;
    #endregion

    void Start () {
        A_I = GameObject.FindGameObjectWithTag("Player").GetComponent<Animation_I>();

        Button_Text.enabled = false;
	}

    private void Update()
    {
        if(Vector3.Distance(this.gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < Distance_Between_Objects)
        {
            if(A_I.Are_Player_Controls_Enabled == true) { Button_Text.enabled = true; }
            if (Input.GetKeyDown(KeyCode.E) && Is_Player_On_Trigger == false)
            {
                A_I.Are_Player_Controls_Enabled = false;

                Button_Text.enabled = false;

                Camera.main.transform.parent = null;

                Enable_Button();

                Is_Player_On_Trigger = true;

                Player_GameObject = GameObject.FindGameObjectWithTag("Player");

                Player_GameObject.transform.position = new Vector3(0.625f, 0.1f, -0.625f);
                Player_GameObject.transform.eulerAngles = new Vector3(0f, 180.0f, 0f);

                Camera.main.transform.eulerAngles = new Vector3(0f, -90f, 0f);
                Camera.main.transform.position = new Vector3(1.25f, 0.625f, -0.75f);

                Camera.main.transform.parent = Player_GameObject.transform;
            }
        }
        else { Button_Text.enabled = false; Is_Player_On_Trigger = false; }
    }

    private void Enable_Button()
    {
        A_II = GameObject.FindGameObjectWithTag("Player").GetComponent<Animation_II>();

        A_II.Left_Hand_Object = this.gameObject.transform;
        A_II.Is_IK_Active = true;
    }
}