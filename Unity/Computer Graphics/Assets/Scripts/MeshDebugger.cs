using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDebugger : MonoBehaviour
{
    public int highlighted = -1;

    private void OnDrawGizmosSelected()
    {
        Mesh mRef = GetComponent<MeshFilter>().mesh;

        Gizmos.color = Color.red;

        highlighted = Mathf.Clamp(highlighted, -1, mRef.vertexCount - 1);
        if(highlighted != -1)
        {
            Gizmos.DrawSphere(transform.TransformPoint(mRef.vertices[highlighted]), 0.2f);
        }
    }
}
