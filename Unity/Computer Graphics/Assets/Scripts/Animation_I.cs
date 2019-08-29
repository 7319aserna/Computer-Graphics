using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
// *****----- Computer_Graphics_Subject_And_Assessment -----*****

// *****----------------------------------------------------*****
public class Animation_I : MonoBehaviour {
    #region Private
    // *****----- Animation Selection -----*****
    private Animator Player_Animator;
    // *****-------------------------------*****

    // *****----- From Idle to Walking -----*****
    private bool Animator_Playback = false;

    private float Current_Speed = 0f;
    // *****-------------------------------*****
    #endregion

    #region Public
    // *****----- From Idle to Walking -----*****
    public int Maximum_Speed;

    private Rigidbody RB;
    // *****-------------------------------*****
    #endregion

    void Start () {
        // *****----- Animation Selection -----*****
        Player_Animator = gameObject.GetComponent<Animator>();
        // *****-------------------------------*****

        // *****----- From Idle to Walking -----*****
        RB = gameObject.GetComponent<Rigidbody>();
        // *****-------------------------------*****
}

    void Update()
    {
        // *****----- Animation Selection -----*****
        // if (Input.GetKeyDown(KeyCode.I)) { Player_Animator.SetTrigger("Trigger_Idle"); }
        // else if (Input.GetKeyDown(KeyCode.W)) { Player_Animator.SetTrigger("Trigger_Walk"); }
        // else if (Input.GetKeyDown(KeyCode.R)) { Player_Animator.SetTrigger("Trigger_Run"); }
        // *****-------------------------------*****

        // *****----- From Idle to Walking -----*****
        float Move_Horizontal = Input.GetAxis("Horizontal");
        float Move_Vertical = Input.GetAxis("Vertical");

        Player_Animator.SetFloat("Current_Speed", Current_Speed);

        if (Current_Speed < 0)
        {
            Current_Speed = 0f;
            Player_Animator.SetTrigger("Trigger_Idle");
        }

        if (Move_Vertical != 0)
        {
            if(Move_Vertical > 0) { Current_Speed += 1f * Time.deltaTime; }
            else { Current_Speed -= 1f * Time.deltaTime; }
        }
        else { Current_Speed -= 1f * Time.deltaTime; }

        Vector3 Movement = new Vector3(Move_Horizontal * Time.deltaTime, 0.0f, Move_Vertical * Time.deltaTime);

        gameObject.transform.position += Movement;
        // *****-------------------------------*****

        // *****----- Computer_Graphics_Subject_And_Assessment -----*****
        // When Q and E are pressed, have the player turn
        // Turn left
        if (Input.GetKeyDown(KeyCode.Q)) { Player_Animator.SetTrigger("Trigger_Turn_Left"); }
        else if (Input.GetKeyUp(KeyCode.Q)) { Player_Animator.SetTrigger("Trigger_Turn_Left"); }
        //Turn right
        if (Input.GetKeyDown(KeyCode.E)) { Player_Animator.SetTrigger("Trigger_Turn_Right"); }
        else if (Input.GetKeyUp(KeyCode.E)) { Player_Animator.SetTrigger("Trigger_Turn_Right"); }
        // *****----------------------------------------------------*****
    }
}
