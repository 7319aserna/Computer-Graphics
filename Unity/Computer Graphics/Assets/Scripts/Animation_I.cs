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

        Vector3 Target_Direction = new Vector3(Move_Horizontal, 0f, Move_Vertical);
        Target_Direction = Camera.main.transform.TransformDirection(Target_Direction);
        Target_Direction.y = 0f;

        Player_Animator.SetFloat("Current_Speed", Current_Speed);

        if (Current_Speed < 0)
        {
            Current_Speed = 0f;
            Player_Animator.SetTrigger("Trigger_Idle");
        }
        // If Current Speed is bigger than the threshold (1)
        else if (Current_Speed > 1f) { Current_Speed = 1f; }

        if (Move_Vertical != 0)
        {
            if(Move_Vertical > 0) { Current_Speed += Time.deltaTime; }
            else { Current_Speed -= Time.deltaTime; }
        }
        else { Current_Speed -= Time.deltaTime; }
        // *****-------------------------------*****

        // *****----- Computer_Graphics_Subject_And_Assessment -----*****
        // Turn Left / Right
        if(Current_Speed != 0)
        {
            // Run / Walk (Left / Right)
            // Left
            if (Input.GetKeyDown(KeyCode.Q)) { Player_Animator.SetBool("Is_Turning_Right", false); Player_Animator.SetBool("Is_Turning_Left", true); }
            else if (Input.GetKeyUp(KeyCode.Q)) { Player_Animator.SetBool("Is_Turning_Left", false); }
            // Right
            if (Input.GetKeyDown(KeyCode.E)) { Player_Animator.SetBool("Is_Turning_Left", false); Player_Animator.SetBool("Is_Turning_Right", true); }
            else if (Input.GetKeyUp(KeyCode.E)) { Player_Animator.SetBool("Is_Turning_Right", false); }
        }
        else if(Current_Speed <= 0f)
        {
            // Idle (Left / Right)
            // Left
            if (Input.GetKeyDown(KeyCode.Q)) { Player_Animator.SetBool("Is_Turning_Right_Idle", false); Player_Animator.SetBool("Is_Turning_Left_Idle", true); }
            else if(Input.GetKeyUp(KeyCode.Q)) { Player_Animator.SetBool("Is_Turning_Left_Idle", false); }
            // Right
            if (Input.GetKeyDown(KeyCode.E)) { Player_Animator.SetBool("Is_Turning_Left_Idle", false); Player_Animator.SetBool("Is_Turning_Right_Idle", true); }
            else if (Input.GetKeyUp(KeyCode.E)) { Player_Animator.SetBool("Is_Turning_Right_Idle", false); }

            Turn_Reset();
            Player_Animator.SetTrigger("Trigger_Idle");
        }

        // 
        //// Idle Turn
        //// Turn left (Q)
        //if (Input.GetKeyDown(KeyCode.Q)) { Player_Animator.SetTrigger("Trigger_Turn_Left"); }
        //else if (Input.GetKeyUp(KeyCode.Q)) { Player_Animator.SetTrigger("Trigger_Idle"); }
        ////Turn right (E)
        //if (Input.GetKeyDown(KeyCode.E)) { Player_Animator.SetTrigger("Trigger_Turn_Right"); }
        //else if (Input.GetKeyUp(KeyCode.E)) { Player_Animator.SetTrigger("Trigger_Idle"); }
        // *****----------------------------------------------------*****
    }

    // *****----- Computer_Graphics_Subject_And_Assessment -----*****
    private void Turn_Reset()
    {
        Player_Animator.SetBool("Is_Turning_Left", false);
        Player_Animator.SetBool("Is_Turning_Left_Idle", false);
        Player_Animator.SetBool("Is_Turning_Right_Idle", false);
        Player_Animator.SetBool("Is_Turning_Right_Run", false);
        Player_Animator.SetBool("Is_Turning_Right_Walk", false);
    }
    // *****----------------------------------------------------*****
}
