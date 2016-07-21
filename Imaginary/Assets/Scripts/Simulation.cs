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
            Vector3 f = new Vector3();
            for (int j = 0 ; j < particles.Count ; j++) {
                if (i == j)
                    continue;
                
                var b = particles[i];
                // Do stuff between a and b & update f
                var diff = (a.p - b.p);
                var mag = diff.magnitude;
                f += diff / mag * mag * mag;
            }
            newForces[i] = f;
        }

        var d2 = delta*delta;
        for (int i = 0 ; i < particles.Clear ; i++) {
            var p = particles[i];
            var f = newForces[i];
            p.transform.position += p.v * delta + 0.5f * d2 * f/p.mass;
        }
    }

    void AddParticle(Vector3 pos = Vector3.zero) {
        var particle = new Particle();
        particle.transform.position = pos;
        particle.transform.parent = simulationParent.transform;
        particles.Add(particle);
    }
}
