using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;

public class ObjectCommands : MonoBehaviour {

    private void RendererOn()
    {
        this.GetComponent<Renderer>().enabled = true;
    }

    private void RendererOff()
    {
        this.GetComponent<Renderer>().enabled = false;
    }
}
