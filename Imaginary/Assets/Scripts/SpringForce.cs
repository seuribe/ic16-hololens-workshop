using System;
using System.Collections.Generic;
using UnityEngine;

public class SpringForce : ForceApplier
{
    public struct Spring {
        public Particle a;
        public Particle b;
        public float restLength;
        public float k;
    }

    public List<Spring> springs = new List<Spring>();

    public Spring? AddSpring(Particle a, Particle b, float restLength, float k) {

        if (GetSpring(a, b) != null) {
            return null;
        }
        var spring = new Spring { a = a, b = b, restLength = restLength, k = k};
        springs.Add(spring);
        return spring;
    }

    Spring? GetSpring(Particle a, Particle b) {
        foreach (var spring in springs) {
            if ((spring.a == a && spring.b == b) ||
                (spring.a == b && spring.b == a))
                return spring;
        }    
        return null;
    }

    public Vector3 CalculateForce(Particle a, Particle other) {
        var spr = GetSpring(a, other);
        if (spr == null)
            return Vector3.zero;

        Spring spring = spr.Value;

        float distance = Vector3.Distance(a.p, other.p);
        return (other.p - a.p).normalized *
            (distance - spring.restLength) *
            spring.k;
    }
}

