using UnityEngine;

public class SpringRenderer : MonoBehaviour {

    LineRenderer lr;
    public SpringForce.Spring spring;

	void Awake () {
		lr = GetComponent<LineRenderer>();
	}
	
	void Update () {
        lr.SetPosition(0, spring.a.p);
        lr.SetPosition(1, spring.b.p);
        lr.SetWidth(0.1f, 0.1f);
        var dist = Vector3.Distance(spring.a.p, spring.b.p);
        bool stretched = spring.restLength < dist;
        float k = stretched ? spring.restLength / dist : dist / spring.restLength;
        var col = Color.Lerp(stretched ? Color.white : Color.black, Color.green, k);
        lr.material.SetColor("_Color", col);
    }
}
