using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    public GameObject PaintPoint;

    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start()
    {
        keywords.Add("Mesh On", () => {
            SpatialMapping.Instance.SetMappingEnabled(true);
            SpatialMapping.Instance.DrawVisualMeshes = true;
        });

        keywords.Add("Mesh Off", () => {
            SpatialMapping.Instance.SetMappingEnabled(false);
        });

        keywords.Add("Hide Mesh", () => {
            SpatialMapping.Instance.DrawVisualMeshes = false;

        });

        keywords.Add("Point Smaller", () => {
            PaintPoint.transform.localScale = PaintPoint.transform.localScale / 2.0F;

        });

        keywords.Add("Point Bigger", () => {
            PaintPoint.transform.localScale = PaintPoint.transform.localScale * 2.0F;

        });

        keywords.Add("Point Cube", () => {
            PaintPoint.GetComponent<MeshFilter>().mesh = Resources.GetBuiltinResource<Mesh>("Cube") as Mesh;

        });

        keywords.Add("Point Sphere", () => {
            PaintPoint.GetComponent<MeshFilter>().mesh = Resources.GetBuiltinResource<Mesh>("Sphere") as Mesh;

        });

        keywords.Add("Color Blue", () => {

            PaintPoint.GetComponent<Renderer>().material = Resources.Load("Blue") as Material;

        });
        keywords.Add("Color Red", () => {

            PaintPoint.GetComponent<Renderer>().material = Resources.Load("Red") as Material;

        });
        keywords.Add("Color Green", () => {

            PaintPoint.GetComponent<Renderer>().material = Resources.Load("Green") as Material;

        });
        keywords.Add("Color Yellow", () => {

            PaintPoint.GetComponent<Renderer>().material = Resources.Load("Yellow") as Material;

        });
        keywords.Add("Color White", () => {

            PaintPoint.GetComponent<Renderer>().material = Resources.Load("White") as Material;

        });
        keywords.Add("Color Grey", () => {

            PaintPoint.GetComponent<Renderer>().material = Resources.Load("Grey") as Material;

        });
        keywords.Add("Color Brown", () => {

            PaintPoint.GetComponent<Renderer>().material = Resources.Load("Brown") as Material;

        });

        keywords.Add("Color Glas", () => {

            PaintPoint.GetComponent<Renderer>().material = Resources.Load("Glas") as Material;

        });

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}
