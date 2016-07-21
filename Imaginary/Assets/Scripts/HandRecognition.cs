using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;

public class HandRecognition : MonoBehaviour {

    private Vector3 pos;
    private Vector3 vel;

    // currently HandPoint
    public GameObject PaintPointObject;

    public GameObject PaintPointObject0;

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
        PaintPointObject.transform.position = new Vector3(pos.x, pos.y, pos.z + 0.1F);

        PaintPointObject.GetComponent<Renderer>().material.color =
        new Color(Random.Range(0.0F, 1.0F), Random.Range(0.0F, 1.0F), Random.Range(0.0F, 1.0F));
        // new Color(0.1F, 1.0F, 0.1F);

        //        Material m = PaintPointObject.GetComponent<Renderer>().material as Material;
        //     
        //        m.SetColor("_dynamic_color", new Color(1.0F, 0.1F, 1.0F));

        //       PaintPointObject.GetComponent<Renderer>().material = m;

        // create new Ball if hand is pressed
        if (hand.pressed) {
            GameObject NewPoint = (GameObject)Instantiate(PaintPointObject, 
                new Vector3(pos.x, pos.y, pos.z + 0.1F), Quaternion.identity);
            NewPoint.GetComponent<Renderer>().material.color = PaintPointObject.GetComponent<Renderer>().material.color;
            NewPoint.SetActive(true);
        }
        
    }

}

