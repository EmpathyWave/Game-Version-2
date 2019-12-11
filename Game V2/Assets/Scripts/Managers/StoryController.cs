﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class StoryController : MonoBehaviour
{
    public TextAsset inkJSONAsset;
    private Story story;
    public string currentKnot;
    public string output;
    public GameObject global;
    public GameObject map;

    void Awake()
    {
        story = new Story(inkJSONAsset.text);
    }

    void Start()
    {
        global = GameObject.Find("Game Manager");
        map = GameObject.Find("Map Manager");
    }

    // Update is called once per frame
    void Update()
    {
        if (story.canContinue)
        {
            if (global.GetComponent<Global>().currentGS == Global.GameState.Selecting)
            {
                output = story.Continue();
            }else {//
                SetKnot(map.GetComponent<MapController>().current_char,"Default_");
            }
        }
    }

    public void SetKnot(string character, string input) //set knot before you continue the story
    {
        currentKnot = character + "." + input;
        story.ChoosePathString(currentKnot);
    }
}
