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
    public GameObject idT;


    [Header("Large Map")]
    public GameObject lmParent; //parent object to Large Map


    [Header("Timeline")]
    public GameObject tParent; //parent object to Timeline
    public GameObject[] tInputs; //character
    public string [] tNames; 
    public Button[] tButtons;
    
    
    [Header("ID")]
    public GameObject idParent; //parent object to ID
    public Button[] idButtons;
   
    
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
        }
        //editing people's names
        else if (Global.me.currentGS == Global.GameState.Editing) 
        {
            UIActivate();
            EControls();
        }
        //questioning
        else if (Global.me.currentGS == Global.GameState.Selecting)
        {
            UIActivate();
            SControls();
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
        idT.SetActive((true));
        
        if (Global.me.currentUIS == Global.UIState.LargeMap) //activating large map UI
        {
            lmParent.SetActive(true);
            
            tParent.SetActive(false);
            for (int i = 0; i < tButtons.Length; i++) {
                tButtons[i].gameObject.SetActive(false); }
            
            idParent.SetActive(false);
            for (int i = 0; i < idButtons.Length; i++) {
                idButtons[i].gameObject.SetActive(false); }
            
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

            idParent.SetActive(false);
            for (int i = 0; i < idButtons.Length; i++) {
                idButtons[i].gameObject.SetActive(false); }
            
            hParent.SetActive(false);
            for (int i = 0; i < hButtons.Length; i++) {
                hButtons[i].gameObject.SetActive(false); }
            
   
        }
        if (Global.me.currentUIS == Global.UIState.ID) //activating ID UI
        {
            idParent.SetActive(true);
            if (Global.me.currentGS == Global.GameState.Selecting)
            {
                for (int i = 0; i < idButtons.Length; i++) {
                    idButtons[i].gameObject.SetActive(true); }
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
            
            idParent.SetActive(false);
            for (int i = 0; i < idButtons.Length; i++) {
                idButtons[i].gameObject.SetActive(false); }
            
            //SM2parent.SetActive(false);
        }
        
    }

    void UIDeactivate() //defaults to walking on everything
    {
        canvas.SetActive(false);
        mapT.SetActive(false);
        timeT.SetActive(false);
        idT.SetActive(false);
        
        lmParent.SetActive(false);
        tParent.SetActive(false);
        idParent.SetActive(false);
        hParent.SetActive(false);
        //SM2parent.SetActive(false);
        
        for (int i = 0; i < tButtons.Length; i++) {
            tButtons[i].gameObject.SetActive(false); }
        for (int i = 0; i < idButtons.Length; i++) {
            idButtons[i].gameObject.SetActive(false); }
        for (int i = 0; i < hButtons.Length; i++) {
            hButtons[i].gameObject.SetActive(false); }
        
        d_box.SetActive(false);
        q_box.SetActive(false);
    }
    
    //_____________________________________________________________________________________________________________________________________________________CONTROLS
    //CONTROLS ____________________________________________________________________________________________________________________________________________________
    void VControls() //viewing controls
    {
        if (Input.GetKey(KeyCode.Escape)) // changes into walking 
        {
            Global.me.currentUIS = Global.UIState.Walking; //walkin UI
            Global.me.currentGS = Global.GameState.Walking; //walkin time
        }
    }

    void EControls() //switching from editing to whatever the fuck you were doing before
    {
        Tab();
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
            
                for (int i = 0; i < idButtons.Length; i++) //resets buttons
                {
                    idButtons[i].gameObject.SetActive(true);
                    idButtons[i].gameObject.GetComponent<Button>().interactable = true; 
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
            
            
                for (int i = 0; i < idButtons.Length; i++) //resets buttons
                {
                    idButtons[i].gameObject.GetComponent<Button>().interactable = true; 
                    idButtons[i].gameObject.SetActive(false);
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

    //_______________________________________________________________________________________________________________________________________________BUTTON & INPUT FUNCTIONS
    //BUTTON & INPUT FUNCTIONS ______________________________________________________________________________________________________________________________________________

    //changing between UI states
    public void LargeMap()
    {
        Global.me.currentUIS = Global.UIState.LargeMap;
    }
    public void Timeline()
    {
        Global.me.currentUIS = Global.UIState.Timeline;
    }
    public void ID()
    {
        Global.me.currentUIS = Global.UIState.ID;
    }
    public void Hill()
    {
        Global.me.currentUIS = Global.UIState.Hill;
    }
    public void Docks()
    {
        Global.me.currentUIS = Global.UIState.Docks;
    }

    //linked to button in order to update the textbox
    public void Ask() 
    {
        asked = true;
    }

    
    
    public void ButtonHandler(Button button) //sends up the button name to the story input
    {
        Debug.Log("clicked");
        if (input_num == 0)
        {
            q_box.GetComponentInChildren<Text>().text += button.name;
            story_input1 = button.name.Replace(" ", String.Empty);
            button.interactable = false;
            input_num = 1;
        } else if (input_num == 1) {
            q_box.GetComponentInChildren<Text>().text += " & ";
            q_box.GetComponentInChildren<Text>().text += button.name;
            story_input2 = button.name.Replace(" ", String.Empty);
            button.interactable = false;
            input_num = 2;
        }
        
        if (input_num == 2)
        {
            for (int i = 0; i < tButtons.Length; i++) //resets buttons
            {
                tButtons[i].gameObject.GetComponent<Button>().interactable = false; 
                //tButtons[i].gameObject.SetActive(false);
            }
            
            
            for (int i = 0; i < idButtons.Length; i++) //resets buttons
            {
                idButtons[i].gameObject.GetComponent<Button>().interactable = false; 
                //idButtons[i].gameObject.SetActive(false);
            }
            
            for (int i = 0; i < hButtons.Length; i++) //resets buttons
            {
                hButtons[i].gameObject.GetComponent<Button>().interactable = false; 
                //hButtons[i].gameObject.SetActive(false);
            }
        }
    }
    
    public void Tab() // overrides the function of escape normally for input functions
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            if (Global.me.currentUIS == Global.UIState.Timeline) //timeline
            {
                for (int i = 0; i < tInputs.Length; i++)
                {
                    tInputs[i].transform.GetChild(0).GetComponent<InputField>().DeactivateInputField();
                    tNames[i]= tInputs[i].transform.GetChild(0).GetComponentInChildren<InputField>().text; //assigns the text that is being inputted to the larger name
                }
            }
            if (Global.me.currentUIS == Global.UIState.Hill) //Hill
            {
                for (int i = 0; i < hInputs.Length; i++)
                {
                    hInputs[i].transform.GetChild(0).GetComponent<InputField>().DeactivateInputField();
                    hNames[i]= hInputs[i].transform.GetChild(0).GetComponentInChildren<InputField>().text; //assigns the text that is being inputted to the larger name
                }
            }
            
        }
    }

    public void Save() // saving the text once the input field is called
    {
        if (Global.me.currentUIS == Global.UIState.Timeline) //timeline
        {
            for (int i = 0; i < tInputs.Length; i++)
            {
                tNames[i] = tInputs[i].transform.GetChild(0).GetComponentInChildren<InputField>().text;
            }
        }
        if (Global.me.currentUIS == Global.UIState.Hill) //Hill
        {
            for (int i = 0; i < hInputs.Length; i++)
            {
                hNames[i] = hInputs[i].transform.GetChild(0).GetComponentInChildren<InputField>().text;
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

    //_____________________________________________________________________________________________________________________________________________WORKER FUNCTIONS
    //WORKER FUNCTIONS ____________________________________________________________________________________________________________________________________________
    void NameCheck() //checking if the names and inputs are correct and displaying the correct info if they are //called whcn acvtivating the UI
    {

        if (Global.me.currentUIS == Global.UIState.Timeline)
        {
            for (int i = 0; i < tInputs.Length; i++)
            {
                if (tNames[i] == tInputs[i].name)
                {
                    //tInputs[i].transform.GetChild(1).GetComponent<Image>().enabled = false; //bw icon disabled
                    //tInputs[i].transform.GetChild(2).GetComponent<Image>().enabled = true; //color icon enabled
                } else {
                    //tInputs[i].transform.GetChild(1).GetComponent<Image>().enabled = true; //bw icon disabled
                    //tInputs[i].transform.GetChild(2).GetComponent<Image>().enabled = false; //color icon enabled
                }
                
                if (Global.me.currentGS == Global.GameState.Selecting && tNames[i] == tInputs[i].name) //activating buttons
                {
                    tInputs[i].transform.GetChild(2).GetComponent<Image>().enabled = true;
                } else {
                    tInputs[i].transform.GetChild(2).GetComponent<Image>().enabled = false;
                }
            }
        }
        
        if (Global.me.currentUIS == Global.UIState.Hill) //Hill
        {
            for (int i = 0; i < hInputs.Length; i++)
            {
                if (hNames[i] == hInputs[i].name)
                {
                    hInputs[i].transform.GetChild(1).GetComponent<Image>().enabled = false; //bw icon disabled
                    hInputs[i].transform.GetChild(2).GetComponent<Image>().enabled = true; //color icon enabled
                } else {
                    hInputs[i].transform.GetChild(1).GetComponent<Image>().enabled = true; //bw icon disabled
                    hInputs[i].transform.GetChild(2).GetComponent<Image>().enabled = false; //color icon enabled
                }

                if (Global.me.currentGS == Global.GameState.Selecting && hNames[i] == hInputs[i].name) //activating buttons
                {
                    hInputs[i].transform.GetChild(3).GetComponent<Image>().enabled = true;
                } else {
                    hInputs[i].transform.GetChild(3).GetComponent<Image>().enabled = false;
                }
                
                if (Global.me.currentGS == Global.GameState.Selecting && hNames[i] == "Uncle Lucca")
                {
                    hInputs[i].transform.GetChild(4).GetComponent<Image>().enabled = true;
                } else if (Global.me.currentGS == Global.GameState.Selecting && hNames[i] == "Uncle Lucca"){
                    hInputs[i].transform.GetChild(4).GetComponent<Image>().enabled = false;
                } 
                
            }
        }
        
    }
}

