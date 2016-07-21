using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;

public class HandRecognition : MonoBehaviour {

    private Vector3 HandPosition;
    private Vector3 HandVelocity;

    public GameObject Object;

    void Awake()
    {
        // Register for hand and finger events to know where your hand
        InteractionManager.SourceUpdated += InteractionManager_SourceUpdated;

    }
    
    private void InteractionManager_SourceUpdated(InteractionSourceState hand)
    {
        
        hand.properties.location.TryGetPosition(out HandPosition);
        hand.properties.location.TryGetVelocity(out HandVelocity);

        var HeadPosition = Camera.main.transform.position;
        var GazeDirection = Camera.main.transform.forward;

        Vector3 PaintPoint = HandPosition + 0.1F * GazeDirection;

        Object.transform.position = PaintPoint;

        if (hand.pressed) {
            GameObject NewPoint = (GameObject)Instantiate(Object, PaintPoint, Quaternion.identity);
            NewPoint.SetActive(true);
        }
        
    }

}

