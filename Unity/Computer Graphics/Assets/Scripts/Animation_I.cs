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

    // *****----- Computer Graphics Subject And Assessment -----*****
    [HideInInspector]
    public bool Are_Player_Controls_Enabled = true;
    // *****----------------------------------------------------*****
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

        // *****----- Computer_Graphics_Subject_And_Assessment -----*****
        if (Are_Player_Controls_Enabled == true)
        {
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
                if (Move_Vertical > 0) { Current_Speed += Time.deltaTime; }
                else { Current_Speed -= Time.deltaTime; }
            }
            else { Current_Speed -= Time.deltaTime; }
            // *****-------------------------------*****

            // Turn Left / Right
            if (Current_Speed != 0)
            {
                // Run / Walk (Left / Right)
                // Left
                if (Input.GetKeyDown(KeyCode.A)) { Player_Animator.SetBool("Is_Turning_Right", false); Player_Animator.SetBool("Is_Turning_Left", true); }
                else if (Input.GetKeyUp(KeyCode.A)) { Player_Animator.SetBool("Is_Turning_Left", false); }
                // Right
                if (Input.GetKeyDown(KeyCode.D)) { Player_Animator.SetBool("Is_Turning_Left", false); Player_Animator.SetBool("Is_Turning_Right", true); }
                else if (Input.GetKeyUp(KeyCode.D)) { Player_Animator.SetBool("Is_Turning_Right", false); }
            }
            else if (Current_Speed <= 0f)
            {
                // Idle (Left / Right)
                // Left
                if (Input.GetKeyDown(KeyCode.A)) { Player_Animator.SetBool("Is_Turning_Right", false); Player_Animator.SetBool("Is_Turning_Left", true); }
                else if (Input.GetKeyUp(KeyCode.A)) { Player_Animator.SetBool("Is_Turning_Left", false); }
                // Right
                if (Input.GetKeyDown(KeyCode.D)) { Player_Animator.SetBool("Is_Turning_Left", false); Player_Animator.SetBool("Is_Turning_Right", true); }
                else if (Input.GetKeyUp(KeyCode.D)) { Player_Animator.SetBool("Is_Turning_Right", false); }

                Turn_Reset();
                Player_Animator.SetTrigger("Trigger_Idle");
            }
        }
        else { Current_Speed = 0f; Player_Animator.SetFloat("Current_Speed", Current_Speed); }
        // *****----------------------------------------------------*****
    }

    // *****----- Computer_Graphics_Subject_And_Assessment -----*****
    public void Turn_Reset()
    {
        Player_Animator.SetBool("Is_Turning_Left", false);
        Player_Animator.SetBool("Is_Turning_Right", false);
        Player_Animator.SetTrigger("Trigger_Idle");
    }
    // *****----------------------------------------------------*****
}
