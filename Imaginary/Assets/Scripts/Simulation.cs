using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Simulation : MonoBehaviour {

    readonly List<Particle> particles = new List<Particle>();

    public GameObject simulationParent;

    List<ForceApplier> forceAppliers = new List<ForceApplier>();
    Vector3[] newForces;

    void Awake() {
        if (simulationParent == null)
            simulationParent = gameObject;
    }

	void Start () {
        LoadParticles();
        //var springForce = new SpringForce();
        //springForce.AddSpring(particles[0], particles[1], 2, 1);
        //forceAppliers.Add(springForce);
        var isf = new InverseSquareForce(1);
        forceAppliers.Add(isf);
    }

    void LoadParticles() {
        particles.AddRange(simulationParent.GetComponentsInChildren<Particle>());
        newForces = new Vector3[particles.Count];
    }
	
	// Update is called once per frame
	void Update () {
        Simulate(Time.deltaTime);
	}

    void Simulate(float delta) {
        for (int i = 0 ; i < particles.Count ; i++) {
            var a = particles[i];
            Vector3 f = new Vector3(0, 0, 0);
            for (int j = 0 ; j < particles.Count ; j++) {
                if (i == j)
                    continue;
                
                var b = particles[j];

                foreach (var force in forceAppliers) {
                    f += force.CalculateForce(a, b);
                }
            }
            newForces[i] = f;
        }

        for (int i = 0 ; i < particles.Count ; i++) {
            var par = particles[i];
            var f = newForces[i];
            par.v += delta * f/par.mass;
            par.p += par.v * delta;

            par.transform.GetComponentInChildren<LineRenderer>().SetPosition(1, par.v);
        }
    }
/*
    void AddParticle(Vector3? pos = null) {
        if (pos == null)
            pos = Vector3.zero;

        var particle = new Particle();
        particle.transform.position = pos.Value;
        particle.transform.parent = simulationParent.transform;
        particles.Add(particle);
    }
*/
}
