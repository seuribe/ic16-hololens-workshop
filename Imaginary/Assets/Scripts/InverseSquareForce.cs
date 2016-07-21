using System;
using UnityEngine;

public class InverseSquareForce : ForceApplier
{
    float g;
    public InverseSquareForce(float g = 1) {
        this.g = g;
    }

    public Vector3 CalculateForce(Particle a, Particle other) {
        var diff = other.p - a.p;
        var distance = Vector3.Distance(a.p, other.p);

        if (distance == 0)
            return Vector3.zero;

        return g * (diff / (distance * distance * distance + 0.1F));
    }
}

