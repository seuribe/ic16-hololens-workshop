using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Simulation : MonoBehaviour {

    readonly List<Particle> particles = new List<Particle>();

    public GameObject simulationParent;

    ForceApplier forceApplier;

    void Awake() {
        if (simulationParent == null)
            simulationParent = gameObject;

    }

	void Start () {
        LoadParticles();
        forceApplier = new SpringForce();
        (forceApplier as SpringForce).AddSpring(particles[0], particles[1], 2, 1);
    }

    void LoadParticles() {
        particles.AddRange(simulationParent.GetComponentsInChildren<Particle>());
    }
	
	// Update is called once per frame
	void Update () {
        Simulate(forceApplier, Time.deltaTime);
	}

    void Simulate(ForceApplier force, float delta) {
        var newForces = new Vector3[particles.Count];
        for (int i = 0 ; i < particles.Count ; i++) {
            var a = particles[i];
            Vector3 f = new Vector3(0, 0, 0);
            for (int j = 0 ; j < particles.Count ; j++) {
                if (i == j)
                    continue;
                
                var b = particles[j];

                f += force.CalculateForce(a, b);
            }
            newForces[i] = f;
        }

        for (int i = 0 ; i < particles.Count ; i++) {
            var par = particles[i];
            var f = newForces[i];
            par.v += delta * f/par.mass;
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
