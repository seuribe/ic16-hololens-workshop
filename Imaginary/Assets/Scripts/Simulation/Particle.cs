using UnityEngine;
using System.Collections;

/// <summary>
/// The basic element of the simulations.
/// </summary>
public class Particle : MonoBehaviour {

    public Vector3 v;
    public float mass;
    // Used for visualizing v
    LineRenderer lr;

    public Vector3 p {
        get { return transform.position; }
        set { transform.position = value; }
    }

    void Awake() {
        lr = gameObject.GetComponentInChildren<LineRenderer>();
    }
	
	void Update ()
    {
        if (lr != null)
            lr.SetPosition(1, v * Time.deltaTime * 5);
	}

    void OnTriggerEnter(Collider other)
    {
        // Just bounce away
        v = -v;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        v = Vector3.Reflect(v, hit.normal);
    }
}
