using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal.UIElements;
using UnityEngine.UI;
public class MapController : MonoBehaviour //handles all the UI elements when navigating the map and conflicts and even the story
{
    [Header("Large Map")] 
    public Image Lmap;
    public GameObject LMparent; //parent object to Large Map
    public GameObject[] LMinputs; //input fields
    public string [] LMnames; //hard code later!!
    public Button[] LMbuttons;
    
    
    [Header("Conflicts")] 
    public Image Cpage;
    public GameObject Cparent; //parent object to Conflicts
    public Button[] Cbuttons;
   
    
    [Header("Small Map 1")] 
    public Image Smap1;
    public GameObject [] SM1inputs;
    public GameObject SM1parent; //parent object to Small Map 1
    public string [] SM1names; //hard code later!!
    public Button[] SM1buttons;
    
    
    [Header("Small Map 2")]
    public Image Smap2;
    public GameObject SM2parent; //parent object to Small Map 2
    public GameObject [] SM2inputs;
    public string [] SM2names; //hard code later!!
    public Button [] SM2buttons;

    
    //inkle / story 
    public string current_char; //tracks which character you are currently talking to
    public bool asked = false; //check to see if the player has asked
    string story_input = ""; //put into inkle system
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
        global = GameObject.Find("Game Manager");
        story = GameObject.Find("Story Manager");
        girl = GameObject.Find("Girl");
    }
    
    void Update()
    {
        //viewing the map
        if (global.GetComponent<Global>().currentGS == Global.GameState.Viewing) 
        {
            UIActivate();
            VControls();
        }
        //editing people's names
        else if (global.GetComponent<Global>().currentGS == Global.GameState.Editing) 
        {
            UIActivate();
            EControls();
        }
        //questioning
        else if (global.GetComponent<Global>().currentGS == Global.GameState.Selecting)
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
        if (global.GetComponent<Global>().currentUIS == Global.UIState.LargeMap) 
        {
            LMparent.SetActive(true);
            Lmap.enabled = true;
            NameCheck();
            
            Cparent.SetActive(false);
            Cpage.enabled = false;
            SM1parent.SetActive(false);
            Smap1.enabled = false;
            SM2parent.SetActive(false);
            Smap2.enabled = false;
        }
        if (global.GetComponent<Global>().currentUIS == Global.UIState.Conflicts)
        {
            Cparent.SetActive(true);
            Cpage.enabled = true;
            
            LMparent.SetActive(false);
            Lmap.enabled = false;
            SM1parent.SetActive(false);
            Smap1.enabled = false;
            SM2parent.SetActive(false);
            Smap2.enabled = false;
        }
        if (global.GetComponent<Global>().currentUIS == Global.UIState.SmallMap1)
        {
            SM1parent.SetActive(true);
            Smap1.enabled = true;
            NameCheck();
            
            LMparent.SetActive(false);
            Lmap.enabled = false;
            Cparent.SetActive(false);
            Cpage.enabled = false;
            SM2parent.SetActive(false);
            Smap2.enabled = false;
        }
        if (global.GetComponent<Global>().currentUIS == Global.UIState.SmallMap2)
        {
            SM2parent.SetActive(true);
            Smap2.enabled = true;
            NameCheck();
            
            Cparent.SetActive(false);
            Cpage.enabled = false;
            LMparent.SetActive(false);
            Lmap.enabled = false;
            Cparent.SetActive(false);
            Cpage.enabled = false;
            SM1parent.SetActive(false);
            Smap1.enabled = false;
        }
    }

    void UIDeactivate() //defaults to walking on everything
    {
        //set empty character parent to deactive !
        LMparent.SetActive(false);
        Lmap.enabled = false;
        Cparent.SetActive(false);
        Cpage.enabled = false;
        SM1parent.SetActive(false);
        Smap1.enabled = false;
        SM2parent.SetActive(false);
        Smap2.enabled = false;
        d_box.SetActive(false);
        q_box.SetActive(false);
    }
    
    //_____________________________________________________________________________________________________________________________________________________CONTROLS
    //CONTROLS ____________________________________________________________________________________________________________________________________________________
    void VControls()
    {
        if (Input.GetKeyUp(KeyCode.N)) // changes into viewing
        {
            global.GetComponent<Global>().currentGS = Global.GameState.Walking; //walkin time
            global.GetComponent<Global>().currentUIS = Global.UIState.Walking; //walkin UI
        }
    }

    void EControls() //switching from editing to whatever the fuck you were doing before
    {
        if (Input.GetKeyUp(KeyCode.Escape)) 
        {
            global.GetComponent<Global>().currentGS = global.GetComponent<Global>().prevGS; //back to map
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
            story_input = q_box.GetComponentInChildren<Text>().text.Replace(" ", String.Empty) + "_";
            story.GetComponent<StoryController>().SetKnot(current_char, story_input);
            q_box.GetComponentInChildren<Text>().text = "";
            if (global.GetComponent<Global>().currentUIS == Global.UIState.LargeMap)
            {
                for (int i = 0; i < LMbuttons.Length; i++) //resets buttons
                {
                    LMbuttons[i].interactable = true;
                }
            }
            if (global.GetComponent<Global>().currentUIS == Global.UIState.Conflicts)
            {
                for (int i = 0; i < Cbuttons.Length; i++) //resets buttons
                {
                    Cbuttons[i].interactable = true;
                }
            }
            if (global.GetComponent<Global>().currentUIS == Global.UIState.SmallMap1)
            {
                for (int i = 0; i < SM1buttons.Length; i++) //resets buttons
                {
                    SM1buttons[i].interactable = true;
                }
            }
            if (global.GetComponent<Global>().currentUIS == Global.UIState.SmallMap2)
            {
                for (int i = 0; i < SM2buttons.Length; i++) //resets buttons
                {
                    SM2buttons[i].interactable = true;
                }
            }
            input_num = 0;
            asked = false;
        }
        
        //story.GetComponent<StoryController>().SetKnot(current_char, "Default_");
        story_output = story.GetComponent<StoryController>().output;
        d_box.GetComponentInChildren<Text>().text = story_output;

        if (Input.GetKeyUp(KeyCode.Q)) //exists out of convo
        {
            d_box.GetComponentInChildren<Text>().text = "";
            q_box.GetComponentInChildren<Text>().text = "";
            story_output = "";

            if (global.GetComponent<Global>().currentUIS == Global.UIState.LargeMap)
            {
                for (int i = 0; i < LMbuttons.Length; i++) //resets buttons
                {
                    LMbuttons[i].interactable = true;
                }
            }
            if (global.GetComponent<Global>().currentUIS == Global.UIState.Conflicts)
            {
                for (int i = 0; i < Cbuttons.Length; i++) //resets buttons
                {
                    Cbuttons[i].interactable = true;
                }
            }
            if (global.GetComponent<Global>().currentUIS == Global.UIState.SmallMap1)
            {
                for (int i = 0; i < SM1buttons.Length; i++) //resets buttons
                {
                    SM1buttons[i].interactable = true;
                }
            }
            if (global.GetComponent<Global>().currentUIS == Global.UIState.SmallMap2)
            {
                for (int i = 0; i < SM2buttons.Length; i++) //resets buttons
                {
                    SM2buttons[i].interactable = true;
                }
            }
            input_num = 0;
            story.GetComponent<StoryController>().SetKnot(current_char, "Default_");
            global.GetComponent<Global>().currentGS = Global.GameState.Walking; //back to walking
            global.GetComponent<Global>().currentUIS = Global.UIState.Walking; //back to walking UI
        }    
    }

    //_______________________________________________________________________________________________________________________________________________BUTTON FUNCTIONS
    //BUTTON FUNCTIONS ______________________________________________________________________________________________________________________________________________

    //changing between UI states
    public void LargeMap()
    {
        global.GetComponent<Global>().currentUIS = Global.UIState.LargeMap;
    }
    public void Conflicts()
    {
        global.GetComponent<Global>().currentUIS = Global.UIState.Conflicts;
    }
    public void SmallMap1()
    {
        global.GetComponent<Global>().currentUIS = Global.UIState.SmallMap1;
    }
    public void SmallMap2()
    {
        global.GetComponent<Global>().currentUIS = Global.UIState.SmallMap2;
    }

    //linked to button in order to update the textbox
    public void Ask() 
    {
        asked = true;
    }

    public void ButtonHandler(Button button) //sends up the button name to the story input
    {
        if (input_num == 0)
        {
            q_box.GetComponentInChildren<Text>().text += button.name;
            button.interactable = false;
            input_num = 1;
        } else if (input_num == 1) {
            q_box.GetComponentInChildren<Text>().text += " & ";
            q_box.GetComponentInChildren<Text>().text += button.name;
            button.interactable = false;
            input_num = 2;
        }
    }
    
    void Esc() // overrides the function of escape normally
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (global.GetComponent<Global>().currentUIS == Global.UIState.LargeMap) //LM
            {
                for (int i = 0; i < LMinputs.Length; i++)
                {
                    LMinputs[i].transform.GetChild(0).GetComponent<InputField>().DeactivateInputField();
                    LMnames[i]= LMinputs[i].transform.GetChild(0).GetComponentInChildren<InputField>().text;
                }
            }
            if (global.GetComponent<Global>().currentUIS == Global.UIState.SmallMap1) //SM1
            {
                for (int i = 0; i < SM1inputs.Length; i++)
                {
                    SM1inputs[i].transform.GetChild(0).GetComponent<InputField>().DeactivateInputField();
                    SM1names[i]= SM1inputs[i].transform.GetChild(0).GetComponentInChildren<InputField>().text;
                }
            }
            if (global.GetComponent<Global>().currentUIS == Global.UIState.SmallMap2) //SM2
            {
                for (int i = 0; i < SM2inputs.Length; i++)
                {
                    SM2inputs[i].transform.GetChild(0).GetComponent<InputField>().DeactivateInputField();
                    SM2names[i]= SM2inputs[i].transform.GetChild(0).GetComponentInChildren<InputField>().text;
                }
            }
        }
    }

    public void Save() // saving the text once the input field is called
    {
        if (global.GetComponent<Global>().currentUIS == Global.UIState.LargeMap) //LM
        {
            for (int i = 0; i < LMinputs.Length; i++)
            {
                LMnames[i] = LMinputs[i].transform.GetChild(0).GetComponentInChildren<InputField>().text;
            }
        }
        if (global.GetComponent<Global>().currentUIS == Global.UIState.SmallMap1) //SM1
        {
            for (int i = 0; i < SM1inputs.Length; i++)
            {
                SM1names[i] = SM1inputs[i].transform.GetChild(0).GetComponentInChildren<InputField>().text;
            }
        }
        if (global.GetComponent<Global>().currentUIS == Global.UIState.SmallMap2) //SM2
        {
            for (int i = 0; i < SM2inputs.Length; i++)
            {
                SM2names[i] = SM2inputs[i].transform.GetChild(0).GetComponentInChildren<InputField>().text;
            }
        }
    }

    public void End_Editing() //ending editing - switching to previous mode fixes problem of game states
    {
        global.GetComponent<Global>().currentGS = global.GetComponent<Global>().prevGS; 
        global.GetComponent<Global>().prevGS = global.GetComponent<Global>().currentGS;
    }

    public void Start_Editing() 
    {
        global.GetComponent<Global>().currentGS = Global.GameState.Editing;
        
        //logic to assign the correct previous state to the variable, making sure editing doesn't become one of them
        if (global.GetComponent<Global>().prevGS == Global.GameState.Viewing)
        {
            global.GetComponent<Global>().prevGS = Global.GameState.Viewing;
        }
        else if (global.GetComponent<Global>().prevGS == Global.GameState.Selecting)
        {
            global.GetComponent<Global>().prevGS = Global.GameState.Selecting;
        }
    }

    //_____________________________________________________________________________________________________________________________________________WORKER FUNCTIONS
    //WORKER FUNCTIONS ____________________________________________________________________________________________________________________________________________
    void NameCheck() 
    {
        if (global.GetComponent<Global>().currentUIS == Global.UIState.LargeMap) //LM
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
                
                if (global.GetComponent<Global>().currentGS == Global.GameState.Selecting && LMnames[i] == LMinputs[i].name && input_num < 3) //activating buttons
                {
                    LMinputs[i].transform.GetChild(3).GetComponent<Image>().enabled = true;
                } else {
                    LMinputs[i].transform.GetChild(3).GetComponent<Image>().enabled = false;
                }
            }
        }
        if (global.GetComponent<Global>().currentUIS == Global.UIState.SmallMap1) //SM1
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
                
                if (global.GetComponent<Global>().currentGS == Global.GameState.Selecting && SM1names[i] == SM1inputs[i].name && input_num < 3) //activating buttons
                {
                    SM1inputs[i].transform.GetChild(3).GetComponent<Image>().enabled = true;
                } else {
                    SM1inputs[i].transform.GetChild(3).GetComponent<Image>().enabled = false;
                }
            }
        }
        if (global.GetComponent<Global>().currentUIS == Global.UIState.SmallMap2) //SM2
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
                
                if (global.GetComponent<Global>().currentGS == Global.GameState.Selecting && SM2names[i] == SM2inputs[i].name && input_num < 3) //activating buttons
                {
                    SM2inputs[i].transform.GetChild(3).GetComponent<Image>().enabled = true;
                } else {
                    SM2inputs[i].transform.GetChild(3).GetComponent<Image>().enabled = false;
                }
            }
        }
    }
}

