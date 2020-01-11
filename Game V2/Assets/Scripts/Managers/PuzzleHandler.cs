using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHandler : MonoBehaviour
{
    public GameObject story;
    public GameObject global;
    
    //solutions
    private string puzzle1 = "Dave.TheBees&Mrs.Riccobono";
    
    
    public void Start()
    {
        global = GameObject.Find("Game Manager");
        story = GameObject.Find("Story Manager");
    }

    public void Check() //when the button is pressed it checks if it satisfies
    {
        if (story.GetComponent<StoryController>().currentKnot == puzzle1)
        {
            global.GetComponent<Global>().puzzle1 = true;
        }
    }
}
