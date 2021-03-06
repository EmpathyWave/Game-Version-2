﻿using System;
using System.Collections;
using System.Collections.Generic;
//using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour ///delete and rename input field and button handler 
{
    bool asked = false;
    public GameObject map;
    // Start is called before the first frame update
    void Start()
    {
        //global = GameObject.Find("Game Manager");
        map = GameObject.Find("Map Manager");
    }

    public void ButtonHandler(Button button) //sends up the button name to the story input
    {
        Debug.Log("clicked");
        if (map.GetComponent<MapController>().input_num == 0)
        {
            map.GetComponent<MapController>().q_box.GetComponentInChildren<Text>().text += button.name;
            map.GetComponent<MapController>().story_input1 = button.name.Replace(" ", String.Empty);
            map.GetComponent<MapController>().input_num = 1;
        } else if (map.GetComponent<MapController>().input_num == 1) {
            map.GetComponent<MapController>().q_box.GetComponentInChildren<Text>().text += " & ";
            map.GetComponent<MapController>().q_box.GetComponentInChildren<Text>().text += button.name;
            map.GetComponent<MapController>().story_input2 = button.name.Replace(" ", String.Empty);
            //button.interactable = false;
            map.GetComponent<MapController>().input_num = 2;
        }
        //changing the ui to onl;y be selected state when the button is clicked
        if (map.GetComponent<MapController>().input_num == 2)
        {
            for (int i = 0; i < map.GetComponent<MapController>().tButtons.Length; i++) //resets buttons
            {
                map.GetComponent<MapController>().tButtons[i].gameObject.GetComponent<Button>().interactable = false; 
                //tButtons[i].gameObject.SetActive(false);
            }
            
            
            for (int i = 0; i < map.GetComponent<MapController>().treeButtons.Length; i++) //resets buttons
            {
                map.GetComponent<MapController>().treeButtons[i].gameObject.GetComponent<Button>().interactable = false; 
                //idButtons[i].gameObject.SetActive(false);
            }
            
            for (int i = 0; i < map.GetComponent<MapController>().hButtons.Length; i++) //resets buttons
            {
                map.GetComponent<MapController>().hButtons[i].gameObject.GetComponent<Button>().interactable = false; 
                //hButtons[i].gameObject.SetActive(false);
            }
        }
    }
    
    public void Save() // saving the text once the input field is called
    {
        if (Global.me.currentUIS == Global.UIState.Hill) //Hill
        {
            for (int i = 0; i < map.GetComponent<MapController>().hInputs.Length; i++)
            {
                map.GetComponent<MapController>().hNames[i] = map.GetComponent<MapController>().hInputs[i].transform.GetChild(0).GetComponentInChildren<InputField>().text;
            }
        }

    }
    
    public void End_Editing() //ending editing - switching to previous mode fixes problem of game states
    {
        Global.me.currentGS = Global.me.prevGS; 
        Global.me.prevGS = Global.me.currentGS;
    }
    
    public void Start_Editing() 
    {
        Global.me.currentGS = Global.GameState.Editing;
        
        //logic to assign the correct previous state to the variable, making sure editing doesn't become one of them
        if (Global.me.prevGS == Global.GameState.Viewing)
        {
            Global.me.prevGS = Global.GameState.Viewing;
        }
        else if (Global.me.prevGS == Global.GameState.Selecting)
        {
            Global.me.prevGS = Global.GameState.Selecting;
        }
    }
    
    
    public void Ask() 
    {
        asked = true;
    }

    public void Piazza()
    {
        Global.me.currentUIS = Global.UIState.Piazza;
    }
    public void PiazzaMov()
    {
        Global.me.currentLocation = Global.LocationState.Piazza;
    }
    
    public void Docks()
    {
        Debug.Log("UI");
        Global.me.currentUIS = Global.UIState.Docks;
    }

    public void DocksMov()
    {
        Global.me.currentLocation = Global.LocationState.Docks;
    }
    
    public void Hill()
    {
        Global.me.currentUIS = Global.UIState.Hill;
    }
    public void HillsMov()
    {
        Global.me.currentLocation = Global.LocationState.Hills;
    }
    
    public void tree()
    {
        Global.me.currentUIS = Global.UIState.Tree;
    }
    
    public void Timeline()
    {
        Global.me.currentUIS = Global.UIState.Timeline;
    }
    
    public void LargeMap()
    {
        Global.me.currentUIS = Global.UIState.LargeMap;
    }
}
