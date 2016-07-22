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

    Dictionary<SpringForce.Spring, LineRenderer> springRenderers
        = new Dictionary<SpringForce.Spring, LineRenderer>();

    List<ForceApplier> forceAppliers = new List<ForceApplier>();
    SpringForce springForce;
    Vector3[] newForces;

    void Awake() {
        if (simulationParent == null)
            simulationParent = gameObject;
        Time.timeScale = 0.1f;
    }

	void Start () {
        if (initialParticles > 0) {
            for (int i = 0 ; i < initialParticles ; i++) {
                var particle = Instantiate<GameObject>(particlePrefab);
                particle.transform.position = new Vector3(
                    initialAreaSize.x + Random.Range(-initialAreaSize.x, initialAreaSize.x),
                    initialAreaSize.y + Random.Range(-initialAreaSize.y, initialAreaSize.y),
                    initialAreaSize.z + Random.Range(-initialAreaSize.z, initialAreaSize.z));
                particles.Add(particle.GetComponent<Particle>());
            }

        } else {
            LoadParticles();
        }

        newForces = new Vector3[particles.Count];
        InitializeSprings();
        var isf = new InverseSquareForce(1);
        forceAppliers.Add(isf);
    }

    void InitializeSprings() {
        springForce = new SpringForce();
        for (int i = 0 ; i < particles.Count ; i++) {
            int n = Random.Range(0, particles.Count);
            if (n != i) {
                var spring = springForce.AddSpring(particles[i], particles[n], 0.5f, 1);
                if (spring != null) {
                    var springGO = Instantiate<GameObject>(springPrefab);
                    springGO.transform.parent = springsHolder.transform;
                    var lr = springGO.GetComponent<LineRenderer>();
                    Debug.LogFormat("lr: {0}, spring: {1}", lr, spring);
                    springRenderers.Add(spring.Value, lr);
                }
            }
        }
        forceAppliers.Add(springForce);
    }

    void UpdateSprings() {
        foreach (var spring in springRenderers.Keys) {
            var lr = springRenderers[spring];
            lr.SetPosition(0, spring.a.p);
            lr.SetPosition(1, spring.b.p);
            lr.SetWidth(0.1f, 0.1f);
            var dist = Vector3.Distance(spring.a.p, spring.b.p);
            bool stretched = spring.restLength < dist;
            float maxStretch = spring.restLength * 2;
            float k = stretched ? spring.restLength / dist : dist / spring.restLength;
            var col = Color.Lerp(stretched ? Color.red : Color.yellow, Color.green, k);
            lr.material.SetColor("_Color", col);
        }
    }

    void LoadParticles() {
        particles.AddRange(simulationParent.GetComponentsInChildren<Particle>());
    }
	
	// Update is called once per frame
	void Update () {
        Simulate(Time.deltaTime);
	}

    void Simulate(float delta) {
        for (int i = 0 ; i < particles.Count ; i++) {
            var a = particles[i];
            newForces[i].Set(0,0,0);
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
            par.v += delta * f/par.mass;
            par.p += par.v * delta;
        }
        UpdateSprings();
    }

    void AddParticle(Vector3? pos = null) {
        if (pos == null)
            pos = Vector3.zero;

        var particle = Instantiate<GameObject>(particlePrefab);
        particle.transform.position = pos.Value;

        // particle.transform.parent = simulationParent.transform;

        particles.Add(particle.GetComponent<Particle>());

    }

}
