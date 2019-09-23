using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

public enum State { /* Fleeing and Seeking Behavior (FSB) */ FSB, }

public class Steering_Behaviors : MonoBehaviour
{
    #region Private
        #region Bool
        private bool IsCurrentlyFleeing;
        private bool IsCurrentlySeeking;
        #endregion
        
        #region Rigidbody
        private new Rigidbody rigidbody;
        #endregion
    
        #region Vector3
        private Vector3 CurrentVelocity;
        private Vector3 Force;
        private Vector3 Velocity;
        private Vector3 WanderingPosition;
        #endregion
    #endregion

    #region Public
        #region Bool
        public bool Has_Object_Collided = false;
        [HideInInspector]
        public bool IsSeekBehaviorEnabled;
        [HideInInspector]
        public bool IsFleeBehaviorEnabled;
        public bool Is_Within_Distance = false;
        #endregion

        #region Float
        public float MaximumVelocity;
        #endregion

        #region GameObject
        [HideInInspector]
        public GameObject FleeTarget;
        [HideInInspector]
        public GameObject SeekTarget;
        #endregion

        #region List
        public List<GameObject> Collision_GameObjects = new List<GameObject>();
        #endregion

        #region State
        [HideInInspector]
        public State CurrentState;
        #endregion
    #endregion

    private void Update()
    {
        CurrentVelocity = Velocity;

        switch (CurrentState)
        {
            case State.FSB:
                FleeBehavior(FleeTarget);
                SeekBehavior(SeekTarget);
                break;

            default:
                Debug.LogError("Invalid state!");
                break;
        }
    }

    private float Vector3Magnitude(Vector3 V3)
    {
        float Vector3X = V3.x * V3.x;
        float Vector3Y = V3.y * V3.y;
        float Vector3Z = V3.z * V3.z;
        float Vector3XYZ = Vector3X + Vector3Y + Vector3Z;
        float Magnitude = Mathf.Sqrt(Vector3XYZ);
        return Magnitude;
    }
    private Vector3 NormalizeVector3(Vector3 V3)
    {
        float Mag = Vector3Magnitude(V3);
        V3.x /= Mag;
        V3.y /= Mag;
        V3.z /= Mag;
        return V3;
    }
    private Vector3 Scalar(Vector3 LHS, float RHS)
    {
        LHS = new Vector3(LHS.x * RHS, LHS.y * RHS, LHS.z * RHS);
        return LHS;
    }
    private void FleeBehavior(GameObject Target)
    {
        Collider[] FleeHitColliders = Physics.OverlapBox(this.gameObject.transform.position, this.gameObject.transform.localScale * 12);

        int i = 0;

        foreach (Collider Hit in FleeHitColliders)
        {
            if (Hit.gameObject == Target)
            {
                IsCurrentlyFleeing = true;
                // Calculate a vector from the agent to it's target
                Vector3 Vector3Target = Target.GetComponent<Transform>().position;
                Vector3 Vector3Agent = this.gameObject.GetComponent<Transform>().position;
                // Calculate vector away from target
                Vector3 V3AgentSubtractTarget = Vector3Agent - Vector3Target;
                Vector3 Normalise = NormalizeVector3(V3AgentSubtractTarget);
                // Scale the vector by our maximum velocity (scalar)
                Vector3 CalculatedVector3 = Scalar(Normalise, MaximumVelocity);
                // Subtract agent's current velocity (vector) from vector to obtain force required to change agent's direction towards it's target
                Force = CalculatedVector3 - CurrentVelocity;
                // Apply force to agent's velocity
                Velocity += Force * Time.fixedDeltaTime;
                // Update agent's position
                rigidbody.position += Velocity * Time.fixedDeltaTime;
            }
            else { IsCurrentlyFleeing = false; }
            i++;
        }
    }
    private void SeekBehavior(GameObject Target)
    {
        Collider[] SeekHitColliders = Physics.OverlapBox(this.gameObject.transform.position, this.gameObject.transform.localScale * 12);
        int i = 0;
        foreach (Collider Hit in SeekHitColliders)
        {
            if (Hit.gameObject == Target)
            {
                IsCurrentlySeeking = true;
                // Calculate a vector from the agent to it's target
                Vector3 Vector3Target = Target.GetComponent<Transform>().position;
                Vector3 Vector3Agent = this.gameObject.GetComponent<Transform>().position;
                Vector3 V3TargetSubtractAgent = Vector3Target - Vector3Agent;
                Vector3 Normalise = NormalizeVector3(V3TargetSubtractAgent);
                // Scale the vector by our maximum velocity (scalar)
                Vector3 CalculatedVector3 = Scalar(Normalise, MaximumVelocity);
                // Subtract agent's current velocity (vector) from vector to obtain force required to change agent's direction towards it's target
                Force = CalculatedVector3 - CurrentVelocity;
                // Apply force to agent's velocity
                Velocity += Force * Time.fixedDeltaTime;
                // Update agent's position
                rigidbody.position += Velocity * Time.fixedDeltaTime;
            }
            else { IsCurrentlySeeking = false; }
            i++;
        }
    }
    public void Check_For_Collision(List<GameObject> _GO)
    {
        for(int SJ = 0; SJ < _GO.Count; SJ++)
        {
            Collider[] SeekHitColliders = Physics.OverlapBox(this.gameObject.transform.position, this.gameObject.transform.localScale * 1.25f);
            foreach (Collider Hit in SeekHitColliders)
            {
                if (Hit.gameObject == _GO[SJ])
                {
                    this.Has_Object_Collided = true;
                }
            }
        }
    }
    public void Setup(float Maximum_Velocity, GameObject AI_Agent)
    {
        CurrentState = State.FSB;

        IsCurrentlyFleeing = false;
        IsCurrentlySeeking = false;

        MaximumVelocity = Maximum_Velocity;

        rigidbody = AI_Agent.GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
}