using System;
using UnityEngine;

public class InverseSquareForce : ForceApplier
{
    float g;
    public InverseSquareForce(float g = 1) {
        this.g = g;
    }
    Vector3 diff = new Vector3();
    public Vector3 CalculateForce(Particle a, Particle other) {
        diff = other.p - a.p;

        var distance = Vector3.Distance(a.p, other.p);

        return g * (diff / (distance * distance * distance + 0.1F));
    }
}

