using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal.UIElements;
using UnityEngine.UI;
public class MapController : MonoBehaviour //handles all the UI elements when navigating the map and conflicts and even the story
{
    [Header("Tabs")] 
    public GameObject mapT;
    public GameObject rumorsT;
    public GameObject idT;


    [Header("Large Map")]
    public GameObject LMparent; //parent object to Large Map
    public GameObject[] LMinputs; //things NOT THE INPUT FIELDS
    public string [] LMnames; //hard code later!!
    public Button[] LMbuttons;
    
    
    [Header("Conflicts")]
    public GameObject Cparent; //parent object to Conflicts
    public Button[] Cbuttons;
    
    
    [Header("ID")]
    public GameObject IDparent; //parent object to ID
    public Button[] IDbuttons;
   
    
    [Header("Small Map 1")]
    public GameObject SM1parent; //parent object to Small Map 1
    public GameObject [] SM1inputs; //things NOT THE INPUT FIELDS
    public string [] SM1names; //name storage
    public Button[] SM1buttons;
    
    /*
    [Header("Small Map 2")]
    public GameObject SM2parent; //parent object to Small Map 2
    public GameObject [] SM2inputs;
    public string [] SM2names; //hard code later!!
    public Button [] SM2buttons;
    */
    
    //inkle / story 
    [Header("Story")]
    public string current_char; //tracks which character you are currently talking to
    public bool asked = false; //check to see if the player has asked
    string story_input1 = ""; //put into inkle system
    string story_input2 = "";
    string story_output = ""; //what gets outputted
    public GameObject q_box;
    public GameObject d_box;
    public int input_num = 0; //checks how many inputs have been selected
    
    //managers and globals
    public GameObject global;
    public GameObject story;
    public GameObject girl;
    
    void Start()
    {
        story = GameObject.Find("Story Manager");
        girl = GameObject.Find("Girl");
    }
    
    void Update()
    {
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
        mapT.SetActive((true));
        rumorsT.SetActive((true));
        idT.SetActive((true));
        
        if (Global.me.currentUIS == Global.UIState.LargeMap) 
        {
            LMparent.SetActive(true);
            NameCheck();
            if (Global.me.currentGS == Global.GameState.Selecting)
            {
                for (int i = 0; i < LMbuttons.Length; i++) {
                    LMbuttons[i].gameObject.SetActive(true); }
            }
            
            Cparent.SetActive(false);
            for (int i = 0; i < Cbuttons.Length; i++) {
                Cbuttons[i].gameObject.SetActive(false); }
            IDparent.SetActive(false);
            for (int i = 0; i < IDbuttons.Length; i++) {
                IDbuttons[i].gameObject.SetActive(false); }
            SM1parent.SetActive(false);
            for (int i = 0; i < SM1buttons.Length; i++) {
                SM1buttons[i].gameObject.SetActive(false); }
            
            //SM2parent.SetActive(false);
        }
        if (Global.me.currentUIS == Global.UIState.Conflicts)
        {
            Cparent.SetActive(true);
            if (Global.me.currentGS == Global.GameState.Selecting)
            {
                for (int i = 0; i < Cbuttons.Length; i++)
                { Cbuttons[i].gameObject.SetActive(true); }
            }

            LMparent.SetActive(false);
            for (int i = 0; i < LMbuttons.Length; i++) {
                LMbuttons[i].gameObject.SetActive(false); }
            IDparent.SetActive(false);
            for (int i = 0; i < IDbuttons.Length; i++) {
                IDbuttons[i].gameObject.SetActive(false); }
            SM1parent.SetActive(false);
            for (int i = 0; i < SM1buttons.Length; i++) {
                SM1buttons[i].gameObject.SetActive(false); }
            
            //SM2parent.SetActive(false);
        }
        if (Global.me.currentUIS == Global.UIState.ID)
        {
            IDparent.SetActive(true);
            if (Global.me.currentGS == Global.GameState.Selecting)
            {
                for (int i = 0; i < IDbuttons.Length; i++) {
                    IDbuttons[i].gameObject.SetActive(true); }
            }
            
            LMparent.SetActive(false);
            for (int i = 0; i < LMbuttons.Length; i++) {
                LMbuttons[i].gameObject.SetActive(false); }
            Cparent.SetActive(false);
            for (int i = 0; i < Cbuttons.Length; i++) {
                Cbuttons[i].gameObject.SetActive(false); }
            SM1parent.SetActive(false);
            for (int i = 0; i < SM1buttons.Length; i++) {
                SM1buttons[i].gameObject.SetActive(false); }
            //SM2parent.SetActive(false);
        }
        if (Global.me.currentUIS == Global.UIState.SmallMap1)
        {
            SM1parent.SetActive(true);
            NameCheck();
            if (Global.me.currentGS == Global.GameState.Selecting)
            {
                for (int i = 0; i < SM1buttons.Length; i++) {
                    SM1buttons[i].gameObject.SetActive(true); }
            }
            
            LMparent.SetActive(false);
            for (int i = 0; i < LMbuttons.Length; i++) {
                LMbuttons[i].gameObject.SetActive(false); }
            Cparent.SetActive(false);
            for (int i = 0; i < Cbuttons.Length; i++) {
                Cbuttons[i].gameObject.SetActive(false); }
            IDparent.SetActive(false);
            for (int i = 0; i < IDbuttons.Length; i++) {
                IDbuttons[i].gameObject.SetActive(false); }
            
            //SM2parent.SetActive(false);
        }
        /*
        if (Global.me.currentUIS == Global.UIState.SmallMap2)
        {
            //SM2parent.SetActive(true);
            NameCheck();
            
            LMparent.SetActive(false);
            Cparent.SetActive(false);
            IDparent.SetActive(false);
            SM1parent.SetActive(false);
        }
        */
    }

    void UIDeactivate() //defaults to walking on everything
    {
        mapT.SetActive(false);
        rumorsT.SetActive(false);
        idT.SetActive(false);
        
        LMparent.SetActive(false);
        Cparent.SetActive(false);
        IDparent.SetActive(false);
        SM1parent.SetActive(false);
        //SM2parent.SetActive(false);
        
        for (int i = 0; i < Cbuttons.Length; i++) {
            LMbuttons[i].gameObject.SetActive(false); }
        for (int i = 0; i < Cbuttons.Length; i++) {
            Cbuttons[i].gameObject.SetActive(false); }
        for (int i = 0; i < Cbuttons.Length; i++) {
            IDbuttons[i].gameObject.SetActive(false); }
        for (int i = 0; i < Cbuttons.Length; i++) {
            SM1buttons[i].gameObject.SetActive(false); }
        
        d_box.SetActive(false);
        q_box.SetActive(false);
    }
    
    //_____________________________________________________________________________________________________________________________________________________CONTROLS
    //CONTROLS ____________________________________________________________________________________________________________________________________________________
    void VControls()
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

    void SControls() 
    {
        q_box.SetActive(true);
        d_box.SetActive(true);
        NameCheck();
        
        //calls the Story Controller function
        if (asked == true)
        {
            //filters out the spaces for input into Inkle
            story.GetComponent<StoryController>().SetKnot(current_char, story.GetComponent<StoryController>().Sort(story_input1,story_input2));
            q_box.GetComponentInChildren<Text>().text = "";
            if (Global.me.currentUIS == Global.UIState.LargeMap)
            {
                for (int i = 0; i < LMbuttons.Length; i++) //resets buttons
                {
                    LMbuttons[i].gameObject.SetActive(true);
                }
            }
            if (Global.me.currentUIS == Global.UIState.Conflicts)
            {
                for (int i = 0; i < Cbuttons.Length; i++) //resets buttons
                {
                    Cbuttons[i].gameObject.SetActive(true);
                }
            }
            if (Global.me.currentUIS == Global.UIState.ID)
            {
                for (int i = 0; i < IDbuttons.Length; i++) //resets buttons
                {
                    IDbuttons[i].gameObject.SetActive(true);
                }
            }
            if (Global.me.currentUIS == Global.UIState.SmallMap1)
            {
                for (int i = 0; i < SM1buttons.Length; i++) //resets buttons
                {
                    SM1buttons[i].gameObject.SetActive(true);
                }
            }
            /*
            if (Global.me.currentUIS == Global.UIState.SmallMap2)
            {
                for (int i = 0; i < SM2buttons.Length; i++) //resets buttons
                {
                    SM2buttons[i].interactable = true;
                }
            }
            */
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

            if (Global.me.currentUIS == Global.UIState.LargeMap)
            {
                for (int i = 0; i < LMbuttons.Length; i++) //resets buttons
                {
                    LMbuttons[i].gameObject.SetActive(false);
                }
            }
            if (Global.me.currentUIS == Global.UIState.Conflicts)
            {
                for (int i = 0; i < Cbuttons.Length; i++) //resets buttons
                {
                    Cbuttons[i].gameObject.SetActive(false); 
                }
            }
            if (Global.me.currentUIS == Global.UIState.ID)
            {
                for (int i = 0; i < IDbuttons.Length; i++) //resets buttons
                {
                    IDbuttons[i].gameObject.SetActive(false);
                }
            }
            if (Global.me.currentUIS == Global.UIState.SmallMap1)
            {
                for (int i = 0; i < SM1buttons.Length; i++) //resets buttons
                {
                    SM1buttons[i].gameObject.SetActive(false); 
                }
            }
            /*
            if (Global.me.currentUIS == Global.UIState.SmallMap2)
            {
                for (int i = 0; i < SM2buttons.Length; i++) //resets buttons
                {
                    SM2buttons[i].interactable = true;
                }
            }
            */
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
    public void Conflicts()
    {
        Global.me.currentUIS = Global.UIState.Conflicts;
    }
    public void ID()
    {
        Global.me.currentUIS = Global.UIState.ID;
    }
    public void SmallMap1()
    {
        Global.me.currentUIS = Global.UIState.SmallMap1;
    }
    public void SmallMap2()
    {
        Global.me.currentUIS = Global.UIState.SmallMap2;
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
    }
    
    public void Tab() // overrides the function of escape normally for input functions
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            if (Global.me.currentUIS == Global.UIState.LargeMap) //LM
            {
                for (int i = 0; i < LMinputs.Length; i++)
                {
                    LMinputs[i].transform.GetChild(0).GetComponent<InputField>().DeactivateInputField();
                    LMnames[i]= LMinputs[i].transform.GetChild(0).GetComponentInChildren<InputField>().text; //assigns the text that is being inputted to the larger name
                }
            }
            if (Global.me.currentUIS == Global.UIState.SmallMap1) //SM1
            {
                for (int i = 0; i < SM1inputs.Length; i++)
                {
                    SM1inputs[i].transform.GetChild(0).GetComponent<InputField>().DeactivateInputField();
                    SM1names[i]= SM1inputs[i].transform.GetChild(0).GetComponentInChildren<InputField>().text; //assigns the text that is being inputted to the larger name
                }
            }
            /*
            if (Global.me.currentUIS == Global.UIState.SmallMap2) //SM2
            {
                for (int i = 0; i < SM2inputs.Length; i++)
                {
                    SM2inputs[i].transform.GetChild(0).GetComponent<InputField>().DeactivateInputField();
                    SM2names[i]= SM2inputs[i].transform.GetChild(0).GetComponentInChildren<InputField>().text;
                }
            }
            */
        }
    }

    public void Save() // saving the text once the input field is called
    {
        if (Global.me.currentUIS == Global.UIState.LargeMap) //LM
        {
            for (int i = 0; i < LMinputs.Length; i++)
            {
                LMnames[i] = LMinputs[i].transform.GetChild(0).GetComponentInChildren<InputField>().text;
            }
        }
        if (Global.me.currentUIS == Global.UIState.SmallMap1) //SM1
        {
            for (int i = 0; i < SM1inputs.Length; i++)
            {
                SM1names[i] = SM1inputs[i].transform.GetChild(0).GetComponentInChildren<InputField>().text;
            }
        }
        /*
        if (Global.me.currentUIS == Global.UIState.SmallMap2) //SM2
        {
            for (int i = 0; i < SM2inputs.Length; i++)
            {
                SM2names[i] = SM2inputs[i].transform.GetChild(0).GetComponentInChildren<InputField>().text;
            }
        }
        */
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
    void NameCheck() 
    {
        if (Global.me.currentUIS == Global.UIState.LargeMap) //LM
        {
            for (int i = 0; i < LMinputs.Length; i++)
            {
                if (LMnames[i] == LMinputs[i].name)
                {
                    LMinputs[i].transform.GetChild(1).GetComponent<Image>().enabled = false; //bw icon disabled
                    LMinputs[i].transform.GetChild(2).GetComponent<Image>().enabled = true; //color icon enabled
                } else {
                    LMinputs[i].transform.GetChild(1).GetComponent<Image>().enabled = true; //bw icon disabled
                    LMinputs[i].transform.GetChild(2).GetComponent<Image>().enabled = false; //color icon enabled
                }
                
                if (Global.me.currentGS == Global.GameState.Selecting && LMnames[i] == LMinputs[i].name) //activating buttons
                {
                    LMinputs[i].transform.GetChild(3).GetComponent<Button>().interactable = true;
                } else {
                    LMinputs[i].transform.GetChild(3).GetComponent<Button>().interactable = false;
                }
            }
        }
        if (Global.me.currentUIS == Global.UIState.SmallMap1) //SM1
        {
            for (int i = 0; i < SM1inputs.Length; i++)
            {
                if (SM1names[i] == SM1inputs[i].name)
                {
                    SM1inputs[i].transform.GetChild(1).GetComponent<Image>().enabled = false; //bw icon disabled
                    SM1inputs[i].transform.GetChild(2).GetComponent<Image>().enabled = true; //color icon enabled
                } else {
                    SM1inputs[i].transform.GetChild(1).GetComponent<Image>().enabled = true; //bw icon disabled
                    SM1inputs[i].transform.GetChild(2).GetComponent<Image>().enabled = false; //color icon enabled
                }
                
                if (Global.me.currentGS == Global.GameState.Selecting && SM1names[i] == SM1inputs[i].name) //activating buttons
                {
                    SM1inputs[i].transform.GetChild(3).GetComponent<Image>().enabled = true;
                } else {
                    SM1inputs[i].transform.GetChild(3).GetComponent<Image>().enabled = false;
                }
            }
        }
        /*
        if (Global.me.currentUIS == Global.UIState.SmallMap2) //SM2
        {
            for (int i = 0; i < SM2inputs.Length; i++)
            {
                if (SM2names[i] == SM2inputs[i].name)
                {
                    SM2inputs[i].transform.GetChild(1).GetComponent<Image>().enabled = false; //bw icon disabled
                    SM2inputs[i].transform.GetChild(2).GetComponent<Image>().enabled = true; //color icon enabled
                } else {
                    SM2inputs[i].transform.GetChild(1).GetComponent<Image>().enabled = true; //bw icon disabled
                    SM2inputs[i].transform.GetChild(2).GetComponent<Image>().enabled = false; //color icon enabled
                }
                
                if (Global.me.currentGS == Global.GameState.Selecting && SM2names[i] == SM2inputs[i].name && input_num < 3) //activating buttons
                {
                    SM2inputs[i].transform.GetChild(3).GetComponent<Image>().enabled = true;
                } else {
                    SM2inputs[i].transform.GetChild(3).GetComponent<Image>().enabled = false;
                }
            }
        }
        */
    }
}

