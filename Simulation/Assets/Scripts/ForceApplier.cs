using UnityEngine;

public interface ForceApplier {
    Vector3 CalculateForce(Particle a, Particle other);
}
