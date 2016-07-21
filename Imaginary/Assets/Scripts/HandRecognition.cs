using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;

public class HandRecognition : MonoBehaviour {

    private Vector3 HandPosition;
    private Vector3 HandVelocity;

    public GameObject PaintPoint;

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

        Vector3 paintPoint = HandPosition + 0.1F * GazeDirection;

        PaintPoint.transform.position = paintPoint;

        if (hand.pressed) {
            GameObject NewPoint = (GameObject)Instantiate(PaintPoint, paintPoint, Quaternion.identity);
            NewPoint.SetActive(true);
        }
        
    }

}

