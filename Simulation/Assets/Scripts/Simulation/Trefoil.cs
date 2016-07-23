using UnityEngine;

public class Trefoil {

    public float R1 = 6;
    public float R2 = 2f;

    public float springLength = 2.1f;
    public float springStrength = 80f;

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
            	sim.AddSpring(par, prev, springLength, springStrength);

            prev = par;
        }
        sim.AddSpring(prev, first, springLength, springStrength);
    }

    Vector3 TrefoilPoint(float t) {
        var phi = 3 * t;
        var theta = 2 * t;
        return new Vector3(
            (R1 + R2 * Mathf.Cos(phi)) * Mathf.Cos(theta),
            (R1 + R2 * Mathf.Cos(phi)) * Mathf.Sin(theta),
            (R2 * Mathf.Sin(phi)));
    }
}
