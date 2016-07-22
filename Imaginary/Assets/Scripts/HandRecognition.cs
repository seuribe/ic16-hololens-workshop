using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;

public class HandRecognition : MonoBehaviour {

    private Vector3 pos;
    private Vector3 vel;

	// currently HandPoint
    public GameObject PaintPointObject;
	public GameObject ParticleParent;

    public GameObject SimulationSystem;

    void Awake()
    {
        // Register for hand and finger events to know where your hand
        InteractionManager.SourceUpdated += InteractionManager_SourceUpdated;
        
    }
    
    private void InteractionManager_SourceUpdated(InteractionSourceState hand)
    {
        
        hand.properties.location.TryGetPosition(out pos);
        hand.properties.location.TryGetVelocity(out vel);

        // Handposition rendering
        var v = new Vector3(pos.x, pos.y, pos.z + 0.01F);
        PaintPointObject.transform.position = v;
        
        
		// create new Ball if hand is pressed
        if (hand.pressed) {
            /*
            GameObject NewPoint = (GameObject)Instantiate(PaintPointObject, new Vector3(0,0,0), Quaternion.identity);
			NewPoint.transform.parent = ParticleParent.transform;
			NewPoint.SetActive(true);
            */
            SimulationSystem.GetComponent<Simulation>().AddParticle(v);

        }
        
    }

}

