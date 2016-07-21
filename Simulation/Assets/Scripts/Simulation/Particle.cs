using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

    public Vector3 v;
    public float mass;

    public Vector3 p {
        get { return transform.position; }
        set { transform.position = value; }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
