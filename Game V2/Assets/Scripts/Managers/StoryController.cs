using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class StoryController : MonoBehaviour
{
    private Story story;
    public TextAsset inkJSONAsset;
    public string currentKnot;
    public string output;

    void Awake()
    {
        story = new Story(inkJSONAsset.text);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (story.canContinue)
        {
            output = story.Continue();
        }
    }

    public void SetKnot(string character, string input) //set knot before you continue the story
    {
        currentKnot = character + "." + input;
        story.ChoosePathString(currentKnot);
    }
}
