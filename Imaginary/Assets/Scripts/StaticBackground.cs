using UnityEngine;
 
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Static Background")]
public class StaticBackground : MonoBehaviour {
    public Texture2D background;
 
	// This is not supported anymore in Unity 3.x
	// [RenderBeforeQueues( 1000 )]
    // void OnRenderObject( int queueIndex ) {
	// OnPreRender is used instead ( onPreCull works too )
 
	void OnPreRender (){
        if( background != null )
            Graphics.Blit( background, RenderTexture.active );
    }
 
}