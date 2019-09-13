using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Terry : MonoBehaviour {

    // how fast the camera moves
    public float speed;

    // local space
    public Vector3 offset;

    // time taken to traverse
    private float travelTime;
    private Vector3 travelDirection;

	// Use this for initialization
	void Start ()
    {
        Vector3 goalPosition = transform.position + offset;

        travelDirection = (goalPosition - transform.position).normalized;
        travelTime = (goalPosition - transform.position).magnitude / speed;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float delta = Mathf.Min(travelTime, Time.deltaTime);
        travelTime = Mathf.Clamp(travelTime - Time.deltaTime, 0.0f, float.MaxValue);
        transform.localPosition += travelDirection * speed * delta;
	}
}
