using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// *****----- IK Targets -----*****

// *****----------------------*****

// *****----- Head IK -----*****

// *****-------------------*****

// *****----- IK Targets / Head IK -----*****

// *****--------------------------------*****

public class Animation_II : MonoBehaviour {
    #region Private
    // *****----- IK Targets / Head IK -----*****
    private Animator IK_Animator_Controller;
    // *****--------------------------------*****
    #endregion

    #region Public
    // *****----- IK Targets -----*****
    public Transform Left_Hand_Object = null;
    public Transform Right_Hand_Object = null;
    // *****----------------------*****

    // *****----- Head IK -----*****
    //public Transform Head_Object = null;
    // *****-------------------*****

    // *****----- IK Targets / Head IK -----*****
    public bool Is_IK_Active = false;

    public Transform Look_Object = null;
    // *****--------------------------------*****
    #endregion Public

    void Start () {
        // *****----- IK Targets / Head IK -----*****
        IK_Animator_Controller = gameObject.GetComponent<Animator>();
        // *****--------------------------------*****
    }

    // A callback for calculating IK
    private void OnAnimatorIK()
    {
        if (IK_Animator_Controller)
        {
            // If the IK is active, set the position and rotation directly to the goal
            if (Is_IK_Active)
            {
                // Set the look target position, if one has been assigned
                {
                    if (Look_Object != null)
                    {
                        IK_Animator_Controller.SetLookAtWeight(1f);
                        IK_Animator_Controller.SetLookAtPosition(Look_Object.position);
                    }
                    if(Left_Hand_Object != null)
                    {
                        // Set the left hand target's position and rotation, if one has been assigned
                        IK_Animator_Controller.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
                        IK_Animator_Controller.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
                        IK_Animator_Controller.SetIKPosition(AvatarIKGoal.LeftHand, Left_Hand_Object.position);
                        IK_Animator_Controller.SetIKRotation(AvatarIKGoal.LeftHand, Left_Hand_Object.rotation);
                    }
                    if(Right_Hand_Object != null)
                    {
                        // Set the right hand target's position and rotation, if one has been assigned
                        IK_Animator_Controller.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
                        IK_Animator_Controller.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
                        IK_Animator_Controller.SetIKPosition(AvatarIKGoal.RightHand, Right_Hand_Object.position);
                        IK_Animator_Controller.SetIKRotation(AvatarIKGoal.RightHand, Right_Hand_Object.rotation);
                    }

                    // *****----- IK Targets -----*****

                    // *****----------------------*****
                }
            }
            // If the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                IK_Animator_Controller.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
                IK_Animator_Controller.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);

                IK_Animator_Controller.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
                IK_Animator_Controller.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
                IK_Animator_Controller.SetLookAtWeight(0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameObject.FindGameObjectWithTag("Trigger")) 
        {
            Is_IK_Active = true;
            Look_Object = other.gameObject.transform.parent;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == GameObject.FindGameObjectWithTag("Trigger"))
        {
            Is_IK_Active = false;
            Look_Object = null;
        }
    }
}
