using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal.UIElements;
using UnityEngine.UI;

public class MapController : MonoBehaviour //handles all the UI elements when navigating the map and Timeline and even the story
{
    public GameObject canvas;
    
    [Header("Tabs")] 
    public GameObject mapT;
    public GameObject timeT;
    public GameObject treeT;


    [Header("Large Map")]
    public GameObject lmParent; //parent object to Large Map


    [Header("Timeline")]
    public GameObject tParent; //parent object to Timeline
    public Button[] tButtons;
    
    
    [Header("ID")]
    public GameObject treeParent; //parent object to tree
    public Button[] treeButtons;
   
    
    [Header("Hill")]
    public GameObject hParent; //parent object to Small Map 1
    public GameObject [] hInputs; //character
    public string [] hNames; //name storage
    public Button[] hButtons;
    
    /*
    [Header("Docks")]
    public GameObject dParent; //parent object to Small Map 1
    public GameObject [] dInputs; //parent of the input fields NOT THE INPUT FIELDS
    public string [] dNames; //name storage
    public Button[] dButtons;
    
    [Header("Piazza")]
    public GameObject pParent; //parent object to Small Map 1
    public GameObject [] pInputs; //parent of the input fields NOT THE INPUT FIELDS
    public string [] pNames; //name storage
    public Button[] pButtons;
    */
    
    //inkle / story 
    [Header("Story")]
    public string current_char; //tracks which character you are currently talking to
    public bool asked = false; //check to see if the player has asked
    public string story_input1 = ""; //put into inkle system
    public string story_input2 = "";
    string story_output = ""; //what gets outputted
    public GameObject q_box;
    public GameObject d_box;
    public int input_num = 0; //checks how many inputs have been selected
    
    //managers and globals
    //public GameObject global;
    public GameObject story;
    public GameObject girl;
    
    void Start()
    {
        story = GameObject.Find("Story Manager");
        girl = GameObject.Find("Girl");
    }
    
    void Update()
    {
        Debug.Log(d_box.GetComponentInChildren<Text>().text);
        //viewing the map
        if (Global.me.currentGS == Global.GameState.Viewing) 
        {
            UIActivate();
            VControls();
            TextUpdate();
        }
        //editing people's names
        else if (Global.me.currentGS == Global.GameState.Editing) 
        {
            UIActivate();
            EControls();
            TextUpdate();
        }
        //questioning
        else if (Global.me.currentGS == Global.GameState.Selecting)
        {
            UIActivate();
            SControls();
            TextUpdate();
        }
        else //walking mode
        {
            UIDeactivate();
            //deactivate notes but save all text 
        }
    }
    
    //____________________________________________________________________________________________________________________________________________________UI FUNCTIONS
    //UI FUNCTIONS____________________________________________________________________________________________________________________________________________________
    void UIActivate() //activates the corresponding UI elements 
    {
        canvas.SetActive(true);
        mapT.SetActive((true));
        timeT.SetActive((true));
        treeT.SetActive((true));
        
        if (Global.me.currentUIS == Global.UIState.LargeMap) //activating large map UI
        {
            lmParent.SetActive(true);
            
            tParent.SetActive(false);
            for (int i = 0; i < tButtons.Length; i++) {
                tButtons[i].gameObject.SetActive(false); }
            
            treeParent.SetActive(false);
            for (int i = 0; i < treeButtons.Length; i++) {
                treeButtons[i].gameObject.SetActive(false); }
            
            hParent.SetActive(false);
            for (int i = 0; i < hButtons.Length; i++) {
                hButtons[i].gameObject.SetActive(false); }
            
        }
        if (Global.me.currentUIS == Global.UIState.Timeline) //activating Timeline UI
        {
            tParent.SetActive(true);
            NameCheck();
            if (Global.me.currentGS == Global.GameState.Selecting)
            {
                for (int i = 0; i < tButtons.Length; i++)
                { tButtons[i].gameObject.SetActive(true); }
            }

            lmParent.SetActive(false);

            treeParent.SetActive(false);
            for (int i = 0; i < treeButtons.Length; i++) {
                treeButtons[i].gameObject.SetActive(false); }
            
            hParent.SetActive(false);
            for (int i = 0; i < hButtons.Length; i++) {
                hButtons[i].gameObject.SetActive(false); }
            
   
        }
        if (Global.me.currentUIS == Global.UIState.Tree) //activating tree UI
        {
            treeParent.SetActive(true);
            if (Global.me.currentGS == Global.GameState.Selecting)
            {
                for (int i = 0; i < treeButtons.Length; i++) {
                    treeButtons[i].gameObject.SetActive(true); }
            }
            
            lmParent.SetActive(false);

            tParent.SetActive(false);
            for (int i = 0; i < tButtons.Length; i++) {
                tButtons[i].gameObject.SetActive(false); }
            
            hParent.SetActive(false);
            for (int i = 0; i < hButtons.Length; i++) {
                hButtons[i].gameObject.SetActive(false); }
            
        }
        if (Global.me.currentUIS == Global.UIState.Hill) //activating hill map UI
        {
            hParent.SetActive(true);
            NameCheck();
            if (Global.me.currentGS == Global.GameState.Selecting)
            {
                for (int i = 0; i < hButtons.Length; i++) {
                    hButtons[i].gameObject.SetActive(true); }
            }
            
            lmParent.SetActive(false);
            
            tParent.SetActive(false);
            for (int i = 0; i < tButtons.Length; i++) {
                tButtons[i].gameObject.SetActive(false); }
            
            treeParent.SetActive(false);
            for (int i = 0; i < treeButtons.Length; i++) {
                treeButtons[i].gameObject.SetActive(false); }
            
        }
        
        //add docks and piazza
        
    }

    void UIDeactivate() //defaults to walking on everything
    {
        canvas.SetActive(false);
        mapT.SetActive(false);
        timeT.SetActive(false);
        treeT.SetActive(false);
        
        lmParent.SetActive(false);
        tParent.SetActive(false);
        treeParent.SetActive(false);
        hParent.SetActive(false);

        for (int i = 0; i < tButtons.Length; i++) {
            tButtons[i].gameObject.SetActive(false); }
        for (int i = 0; i < treeButtons.Length; i++) {
            treeButtons[i].gameObject.SetActive(false); }
        for (int i = 0; i < hButtons.Length; i++) {
            hButtons[i].gameObject.SetActive(false); }
        
        //add docks and piazza
        
        d_box.SetActive(false);
        q_box.SetActive(false);
    }
    
    //_____________________________________________________________________________________________________________________________________________________CONTROLS
    //CONTROLS ____________________________________________________________________________________________________________________________________________________
    void VControls() //viewing controls
    {
        if (Input.GetKey(KeyCode.Escape)) // changes into walking 
        {
            Debug.Log("Fuck");
            Global.me.currentUIS = Global.UIState.Walking; //walkin UI
            Global.me.currentGS = Global.GameState.Walking; //walkin time
        }
    }

    void EControls() //switching from editing to whatever the fuck you were doing before
    {
        //Tab();
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Global.me.currentGS = Global.me.prevGS; //back to map
        }
        
    }

    void SControls() //selecting controls
    {
        
        q_box.SetActive(true);
        d_box.SetActive(true);
        NameCheck();
        
        //calls the Story Controller function
        if (asked)
        {
            //filters out the spaces for input into Inkle
            story.GetComponent<StoryController>().SetKnot(current_char, story.GetComponent<StoryController>().Sort(story_input1,story_input2));
            //Debug.Log(story.GetComponent<StoryController>().currentKnot);
            q_box.GetComponentInChildren<Text>().text = "";
            
            
                for (int i = 0; i < tButtons.Length; i++) //resets buttons
                {
                    tButtons[i].gameObject.SetActive(true);
                    tButtons[i].gameObject.GetComponent<Button>().interactable = true; 
                }
            
                for (int i = 0; i < treeButtons.Length; i++) //resets buttons
                {
                    treeButtons[i].gameObject.SetActive(true);
                    treeButtons[i].gameObject.GetComponent<Button>().interactable = true; 
                }
            
                for (int i = 0; i < hButtons.Length; i++) //resets buttons
                {
                    hButtons[i].gameObject.SetActive(true);
                    hButtons[i].gameObject.GetComponent<Button>().interactable = true; 
                }
            

            //d_box.GetComponentInChildren<Text>().text = story_output;
            input_num = 0;
            asked = false;
        }
        
        //story.GetComponent<StoryController>().SetKnot(current_char, "Default_");
        story_output = story.GetComponent<StoryController>().output;
        d_box.GetComponentInChildren<Text>().text = story_output;

        if (Input.GetKey(KeyCode.Escape)) //exists out of convo
        {
            d_box.GetComponentInChildren<Text>().text = "";
            q_box.GetComponentInChildren<Text>().text = "";
            story_output = "";
            
                for (int i = 0; i < tButtons.Length; i++) //resets buttons
                {
                    tButtons[i].gameObject.GetComponent<Button>().interactable = true; 
                    tButtons[i].gameObject.SetActive(false);
                }
            
            
                for (int i = 0; i < treeButtons.Length; i++) //resets buttons
                {
                    treeButtons[i].gameObject.GetComponent<Button>().interactable = true; 
                    treeButtons[i].gameObject.SetActive(false);
                }
            
            
                for (int i = 0; i < hButtons.Length; i++) //resets buttons
                {
                    hButtons[i].gameObject.GetComponent<Button>().interactable = true; 
                    hButtons[i].gameObject.SetActive(false);
                }
            
            input_num = 0;
            //story.GetComponent<StoryController>().SetKnot(current_char, "Default_");
            Global.me.currentUIS = Global.UIState.Walking; //back to walking UI
            Global.me.currentGS = Global.GameState.Walking; //back to walking
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

    //_____________________________________________________________________________________________________________________________________________WORKER FUNCTIONS
    //WORKER FUNCTIONS ____________________________________________________________________________________________________________________________________________
    
    public void TextUpdate()
    {
        if (Global.me.currentUIS == Global.UIState.Hill) //Hill
        {
            for (int i = 0; i < hInputs.Length; i++)
            {
                hNames[i] = hInputs[i].transform.GetChild(2).GetComponent<InputField>().text;
                hInputs[i].transform.GetChild(2).GetComponent<InputField>().text = hNames[i];
                hInputs[i].transform.GetChild(3).GetComponent<InputField>().text= hNames[i];
            }
        }
    }
    
    
    
    void NameCheck() //checking if the names and inputs are correct and displaying the correct info if they are //called whcn acvtivating the UI
    {
        if (Global.me.currentUIS == Global.UIState.Hill) //Hill
        {
            for (int i = 0; i < hInputs.Length; i++)
            {
                if (hNames[i] == hInputs[i].name) //just seeing if the names are right
                {
                    //0 = incorrect sprite
                    //1 = correct sprite
                    //2 = incorrect input
                    //3 = correct input
                    //4 = button
                    
                    //setting the target graphic
                    hInputs[i].transform.GetChild(0).gameObject.SetActive(false);
                    hInputs[i].transform.GetChild(1).gameObject.SetActive(true);
                    hInputs[i].transform.GetChild(2).gameObject.SetActive(false);
                    hInputs[i].transform.GetChild(3).gameObject.SetActive(true);
                    //Global.me.currentGS = Global.GameState.Selecting;
                    
                } else {
                    hInputs[i].transform.GetChild(0).gameObject.SetActive(true);
                    hInputs[i].transform.GetChild(1).gameObject.SetActive(false);
                    hInputs[i].transform.GetChild(2).gameObject.SetActive(true);
                    hInputs[i].transform.GetChild(3).gameObject.SetActive(false);
                    //set interactable or not 
                    
                }

                if (Global.me.currentGS == Global.GameState.Selecting && hNames[i] == hInputs[i].name) //activating buttons
                {
                    Debug.Log(hInputs[i].transform.GetChild(4).gameObject.name+ "true");
                    hInputs[i].transform.GetChild(4).gameObject.SetActive(true);
                    //Global.me.currentGS = Global.GameState.Selecting;
                } else {
                    Debug.Log(hInputs[i].transform.GetChild(4).gameObject.name + "false");
                    hInputs[i].transform.GetChild(4).gameObject.SetActive(false);
                }
                
            }
        }
        
    }
}

