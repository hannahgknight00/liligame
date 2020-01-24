using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{

    public static DialogueSystem instance;
    public ELEMENTS elements;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Say something and it displays.
    public void Say(string speech, string speaker)
    {
        StopSpeaking();
        speaking = StartCoroutine(speaking(speech, speaker));
        
    }

    public void StopSpeaking()
    {
        if (isSpeaking)
        {
            StopCoroutine(speaking);
        }
        speaking = null;
    }

    public bool isSpeaking { get { return speaking != null; } }
    [HideInInspector] public bool isWaitingForUserInput = false;

    Coroutine speaking = null;
    IEnumerator Speaking(string targetSpeech, string speaker)
    {
        Textbox.SetActive(true);
        Speechtext.text = "";
        CNametext.text = speaker;//temporary
        isWaitingForUserInput = false;

        while(Textbox.text != targetSpeech)
        {
            Textbox.text += targetSpeech[Textbox.text.Length - 1];
            yield return new WaitForEndOfFrame();
        }

        //Text is finished.
        isWaitingForUserInput = true;
        while(isWaitingForUserInput)
            yield return new WaitForEndOfFrame();

        StopSpeaking();

    }

    [System.Serializable]
    public class ELEMENTS
    {
        // the panel with dialogue elements
        public gameObject Textbox;
        public text CNametext;
        public text Speechtext;

    }

    public gameObject Textbox { get { return elements.Textbox; } }
    public text CNametext { get { return elements.CNametext; } }
    public text Speechtext { get { return elements.Speechtext; } }

}
