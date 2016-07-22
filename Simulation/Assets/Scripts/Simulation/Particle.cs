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
}
