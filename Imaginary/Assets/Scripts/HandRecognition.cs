using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;

public class HandRecognition : MonoBehaviour {

    private Vector3 pos;
    private Vector3 vel;

    public GameObject Object;

    void Awake()
    {
        // Register for hand and finger events to know where your hand
        InteractionManager.SourceUpdated += InteractionManager_SourceUpdated;

    }
    
    private void InteractionManager_SourceUpdated(InteractionSourceState hand)
    {
        
        hand.properties.location.TryGetPosition(out pos);
        hand.properties.location.TryGetVelocity(out vel);

        Object.transform.position = new Vector3(pos.x, pos.y, pos.z + 0.1F);

        if (hand.pressed) {
            GameObject NewPoint = (GameObject)Instantiate(Object, new Vector3(pos.x, pos.y, pos.z + 0.1F), Quaternion.identity);
            NewPoint.SetActive(true);
        }
        
    }

}

