using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Simulation : MonoBehaviour {

    readonly List<Particle> particles = new List<Particle>();

    public GameObject simulationParent;
    public int initialParticles = 0;
    public Vector3 initialAreaSize = new Vector3(10, 10, 10);
    public GameObject particlePrefab;
    public GameObject springsHolder;
    public GameObject springPrefab;

    public float damping = 0.9f;

    public float InverseSquareForce = -0.5f;

    List<ForceApplier> forceAppliers = new List<ForceApplier>();
    SpringForce springForce;

    void Awake() {
        if (simulationParent == null)
            simulationParent = gameObject;
    }

	void Start () {
        springForce = new SpringForce();
        forceAppliers.Add(springForce);
        forceAppliers.Add(new InverseSquareForce(InverseSquareForce));
    }

    public void AddTrefoil(int numNodes = 48) {
        new Trefoil().Build(this, numNodes);
    }

    public void AddRandomParticles(int numParticles = 50) {
        for (int i = 0 ; i < numParticles ; i++) {
            var particle = Instantiate<GameObject>(particlePrefab);
            particle.transform.position = new Vector3(
                    Random.Range(-initialAreaSize.x, initialAreaSize.x),
                    Random.Range(-initialAreaSize.y, initialAreaSize.y),
                    Random.Range(-initialAreaSize.z, initialAreaSize.z));
            particles.Add(particle.GetComponent<Particle>());
        }
    }

    public void AddRandomSprings() {
        for (int i = 0 ; i < particles.Count ; i++) {
            int n = Random.Range(0, particles.Count);
            if (n != i) {
                AddSpring(particles[i], particles[n], 2, 1);
            }
        }
    }

    public void ClearSimulation() {
        particles.Clear();
        springForce.Clear();
    }

    public void LoadParticles() {
        particles.AddRange(simulationParent.GetComponentsInChildren<Particle>());
    }
	
	void Update () {
        Simulate(Time.deltaTime);
	}

    void Simulate(float delta) {
        Vector3[] newForces = new Vector3[particles.Count];
        for (int i = 0 ; i < particles.Count ; i++) {
            var a = particles[i];
            for (int j = 0 ; j < particles.Count ; j++) {
                if (i == j)
                    continue;
                
                var b = particles[j];

                for (int k = 0 ; k < forceAppliers.Count ; k++) {
                    var force = forceAppliers[k];
                    newForces[i] += force.CalculateForce(a, b);
                }
            }
        }

        for (int i = 0 ; i < particles.Count ; i++) {
            var par = particles[i];
            var f = newForces[i];
            par.v += delta * f/par.mass * damping;
            par.p += par.v * delta;
        }
    }

    public Particle AddParticle(Vector3? pos = null) {
        if (pos == null)
            pos = Vector3.zero;

        var particleGo = Instantiate<GameObject>(particlePrefab);
        var particle = particleGo.GetComponent<Particle>();
        particle.transform.position = pos.Value;
        particle.transform.parent = simulationParent.transform;
        particles.Add(particle);
        return particle;
    }

    public SpringForce.Spring? AddSpring(Particle a, Particle b, float l, float k) {
        var spring = springForce.AddSpring(a, b, l, k);
        if (spring != null) {
            var springGO = Instantiate<GameObject>(springPrefab);
            springGO.GetComponent<SpringRenderer>().spring = spring.Value;
        }

        return spring;
    }

}
