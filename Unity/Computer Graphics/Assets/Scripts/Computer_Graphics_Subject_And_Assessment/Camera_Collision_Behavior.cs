using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Collision_Behavior : MonoBehaviour
{
    private bool False_Or_True = false;

    private void OnTriggerExit(Collider other) { False_Or_True = false; }
    private void OnTriggerEnter(Collider other) { Debug.Log("hi, it's your boy"); False_Or_True = true; }

    public bool Has_Caused_Collision() { return False_Or_True; }
}