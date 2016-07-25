using UnityEngine;

/// <summary>
/// A generic force that is applied between pairs of particles
/// </summary>
public interface ForceApplier {
    Vector3 CalculateForce(Particle a, Particle other);
}
