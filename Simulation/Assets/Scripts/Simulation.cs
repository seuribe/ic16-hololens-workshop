using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Simulation : MonoBehaviour {

    readonly List<Particle> particles = new List<Particle>();

    public GameObject simulationParent;

    void Awake() {
        if (simulationParent == null)
            simulationParent = gameObject;
    }

	void Start () {
        LoadParticles();
    }

    void LoadParticles() {
        particles.AddRange(simulationParent.GetComponentsInChildren<Particle>());
    }
	
	// Update is called once per frame
	void Update () {
        Simulate(Time.deltaTime);
	}

    void Simulate(float delta) {
        var newForces = new Vector3[particles.Count];
        for (int i = 0 ; i < particles.Count ; i++) {
            var a = particles[i];
            Vector3 f = new Vector3(0, 0, 0);
            for (int j = 0 ; j < particles.Count ; j++) {
                if (i == j)
                    continue;
                
                var b = particles[j];
                // Do stuff between a and b & update f
                var diff = b.p - a.p;
                var distance = Vector3.Distance(a.transform.position, b.transform.position);
                Debug.LogFormat("distance: {0}", distance);
                if (distance == 0)
                    continue;

                f += (diff / (distance * distance * distance));
                Debug.LogFormat("F updated: {0} -> {1}", f, diff);
            }
            newForces[i] = f;
            Debug.LogFormat("F: {0}", f);
        }

        var d2 = delta*delta;
        for (int i = 0 ; i < particles.Count ; i++) {
            var par = particles[i];
            var f = newForces[i];
            Debug.LogFormat("par.v: {0}, par.p: {1}, par.mass: {2}", par.v, par.p, par.mass);
            Debug.LogFormat("delta: {0}, f: {1}", delta, f);
            par.v += delta * f/par.mass;
//            par.p += par.v * delta + 0.5f * d2 * f/par.mass;
            par.p += par.v * delta;
        }
    }

    void AddParticle(Vector3? pos = null) {
        if (pos == null)
            pos = Vector3.zero;

        var particle = new Particle();
        particle.transform.position = pos.Value;
        particle.transform.parent = simulationParent.transform;
        particles.Add(particle);
    }
}
