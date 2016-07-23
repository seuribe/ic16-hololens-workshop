using UnityEngine;
using System.Collections;

public class Trefoil : MonoBehaviour {

    public float R1 = 3;
    public float R2 = 1;

    public void Build(Simulation sim, int numPoints) {
        var max = Mathf.PI * 2;
        Particle first = null;
        Particle prev = null;
        for (int i = 0 ; i < numPoints ; i++) {
			var t = max * i / numPoints;
            Vector3 pos = TrefoilPoint(t);
            Particle par = sim.AddParticle(pos);
			if (i == 0)
            	first = par;
            else
            	sim.AddSpring(par, prev, 1f, 100f);

            prev = par;
        }
        sim.AddSpring(prev, first, 1f, 100f);
    }

    Vector3 TrefoilPoint(float t) {
        var phi = 3 * t;
        var theta = 2 * t;
        return new Vector3(
            (R1 + R2 * Mathf.Cos(phi)) * Mathf.Cos(theta),
            (R1 + R2 * Mathf.Cos(phi)) * Mathf.Sin(theta),
            (R2 * Mathf.Sin(phi)));
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
