using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

    public Vector3 v;
    public float mass;
    LineRenderer lr;

    public Vector3 p {
        get { return transform.position; }
        set { transform.position = value; }
    }

    void Awake() {
        lr = gameObject.GetComponentInChildren<LineRenderer>();
    }
	
	void Update () {
        if (lr != null) {
            lr.SetPosition(1, v * Time.deltaTime * 5);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        
        MeshCollider collider = (MeshCollider)other;

        // Mesh mesh = collider.sharedMesh;
        
        /* Vector3[] normals = mesh.normals;
        int[] triangles = mesh.triangles;
        */
        

        // other.gameObject.
         
        v = -v; // new Vector3();
        Debug.Log("OnTriggerEnter...");
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("OnControllerColliderHit: Normal vector we collided at: " + hit.normal);
        // v = -v; // new Vector3();
        v = Vector3.Reflect(v, hit.normal);

    }
}
