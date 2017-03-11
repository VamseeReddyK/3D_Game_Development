using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class VoiceRecognizer : MonoBehaviour {

    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    public bool isFruitGodActive = false;
    private bool set = false;

    // Use this for initialization
    void Start () {
        keywords.Add("fruit god", () =>{ 
            Debug.Log("fruit god");
            set = true;
        });
        keywords.Add("hello", () => { Debug.Log("hello"); });
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray(),ConfidenceLevel.Low);
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction)) {
            keywordAction.Invoke();
        }
    }

    // Update is called once per frame
    void Update () {
        if (FruitGod())
            isFruitGodActive = true;
        else
            isFruitGodActive = false;
    }

    bool FruitGod()
    {
        // from voice recognizer
        if (set) {
            set = false;
            return true;
        }
        return false;
    }
}
