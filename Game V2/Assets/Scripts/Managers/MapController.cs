using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal.UIElements;
using UnityEngine.UI;
public class MapController : MonoBehaviour
{
    public Image map;

    public GameObject parent;
    public GameObject [] inputs;
    public string [] names; //hard code later!!!
    public Button[] buttons;
    public string current_char;
    
    //public Image button;
    
    public GameObject q_box;
    public GameObject d_box;

    public bool asked = false;

    public Text question;
    public Text answer; 
    
    public GameObject global;
    public GameObject story;
    public GameObject girl;
    
    string story_input = "";
    string story_output = "";
    
    public int input_num = 0;

    void Start()
    {
        global = GameObject.Find("Game Manager");
        story = GameObject.Find("Story Manager");
        girl = GameObject.Find("Girl");
    }
    
    void Update()
    {
        if (global.GetComponent<Global>().currentGS == Global.GameState.MViewing) //viewing the map
        {
            MapActivate();
            VControls();
        }
        else if (global.GetComponent<Global>().currentGS == Global.GameState.MEditing) //editing people's names
        {
            MapActivate();
            EControls();
        }
        else if (global.GetComponent<Global>().currentGS == Global.GameState.Selecting) //questioning
        {
            MapActivate();
            SControls();
        }
        else //walking mode
        {
            MapDeactivate();
            //deactivate notes but save all text 
        }
    }

    void MapActivate()
    {
        //set empty character parent to active !!!!
        parent.SetActive(true);
        NameCheck();
        map.enabled = true;
        
    }

    void MapDeactivate()
    {
        //set empty character parent to deactive !!!!!
        parent.SetActive(false);
        map.enabled = false;
        d_box.SetActive(false);
        q_box.SetActive(false);
    }


    void VControls()
    {
        if (Input.GetKeyUp(KeyCode.N)) // changes into viewing
        {
            global.GetComponent<Global>().currentGS = Global.GameState.Walking; //walkin time
        }
    }

    void EControls()
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
        
        //call the Story Controller function
        if (asked == true)
        {
            //filters out the spaces for input into Inkle
            story_input = q_box.GetComponentInChildren<Text>().text.Replace(" ", String.Empty) + "_";
            story.GetComponent<StoryController>().SetKnot(current_char, story_input);
            q_box.GetComponentInChildren<Text>().text = "";
            for (int i = 0; i < buttons.Length; i++) //resets buttons
            {
                buttons[i].interactable = true;
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
            for (int i = 0; i < buttons.Length; i++) //resets buttons
            {
                buttons[i].interactable = true;
            }
            input_num = 0;
            story.GetComponent<StoryController>().SetKnot(current_char, "Default_");
            global.GetComponent<Global>().currentGS = Global.GameState.Walking;//back to walking
        }    
    }

    public void Ask() //linked to button in order to update the textbox
    {
        asked = true;
    }

    public void ButtonHandler(Button button)
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
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i].transform.GetChild(0).GetComponent<InputField>().DeactivateInputField();
                names[i]= inputs[i].transform.GetChild(0).GetComponentInChildren<InputField>().text;
            }
        }
    }
    
    public void Save() // saving the text once the input field is called
    {
        for(int i = 0; i < inputs.Length; i++)
        { 
            names[i] = inputs[i].transform.GetChild(0).GetComponentInChildren<InputField>().text;
        }
        
    }

    public void E_Editing() //ending editing - switching to previous mode fixes problem of game states
    {
        
        global.GetComponent<Global>().currentGS = global.GetComponent<Global>().prevGS; 
        global.GetComponent<Global>().prevGS = global.GetComponent<Global>().currentGS;
    }

    public void St_Editing()
    {
        global.GetComponent<Global>().currentGS = Global.GameState.MEditing;
        //logic to assign the correct previous state to the variable, making sure editing doesn't become one of them

        if (global.GetComponent<Global>().prevGS == Global.GameState.MViewing)
        {
            global.GetComponent<Global>().prevGS = Global.GameState.MViewing;
        }
        else if (global.GetComponent<Global>().prevGS == Global.GameState.Selecting)
        {
            global.GetComponent<Global>().prevGS = Global.GameState.Selecting;
        }
        
        
    }

    void NameCheck() //can be seperated and put into two seprate event functions 
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            if (names[i] == inputs[i].name)
            {
                inputs[i].transform.GetChild(1).GetComponent<Image>().enabled = false; //bw icon disabled
                inputs[i].transform.GetChild(2).GetComponent<Image>().enabled = true; //color icon enabled
            } else {
                inputs[i].transform.GetChild(1).GetComponent<Image>().enabled = true; //bw icon disabled
                inputs[i].transform.GetChild(2).GetComponent<Image>().enabled = false; //color icon enabled
            }
            
            if (global.GetComponent<Global>().currentGS == Global.GameState.Selecting && names[i] == inputs[i].name && input_num < 3) //ativating buttons
            {
                inputs[i].transform.GetChild(3).GetComponent<Image>().enabled = true;
            } else {
                inputs[i].transform.GetChild(3).GetComponent<Image>().enabled = false;
            }
        }
    }
}

