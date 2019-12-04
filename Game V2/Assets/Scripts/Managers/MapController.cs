using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal.UIElements;
using UnityEngine.UI;
public class MapController : MonoBehaviour
{
    public Image map;

    public GameObject [] characters;
    public string [] c_names; //hard code later!!!
    public string current_char;
    
    //public Image button;
    
    public GameObject q_box;
    public GameObject d_box;

    public bool asked = false;

    public Text question;
    public Text answer; 
    
    public GameObject global;

    private bool editing = false;

    void Start()
    {
        global = GameObject.Find("Game Manager");
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
        NameCheck();
        map.enabled = true;
        
    }

    void MapDeactivate()
    {
        //set empty character parent to deactive !!!!!
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

        //put in editing the input fields here
    }

    void SControls()
    {
        string input;
        string output;
        q_box.SetActive(true);
        d_box.SetActive(true);
        NameCheck();
        
        //call the Story Controller function
        if (asked == true)
        {
            //filters out the spaces for input into Inkle
            input = d_box.GetComponentInChildren<Text>().text.Replace(" ", String.Empty);
            //thing to sent into inle need to refernce object
            //create tags
            //create parent object for easy deactiation and such
            SetKnot(current_char, input); 
            //read in output string from Story contorler
            d_box.GetComponentInChildren<Text>().text = output;
                asked = false;
        }

        if (Input.GetKeyUp(KeyCode.Q)) //exists out of convo
        {
            global.GetComponent<Global>().currentGS = Global.GameState.Walking;//back to walking
        }    
    }

    public void Ask() //linked to button in order to update the textbox
    {
        asked = true;
    }

    public void AddName() //adds name to text box by pressing button ( button specific)
    {
        q_box.GetComponentInChildren<Text>().text = "Sleepy Dave";
    }
    
    void Esc() // overrides the function of escape normally
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            //deactviates the input field
            //inputf1.DeactivateInputField(); // 
            //inputf1.text = text1;
        }
    }
    
    public void Save() // saving the text once the input field is called
    {
        for(int i = 0; i < characters.Length; i++)
        { 
            c_names[i]= characters[i].transform.GetChild(0).GetComponentInChildren<Text>().text;
        }
        
    }

    public void E_Editing() //ending editing - switching to previous mode fixes problem of game states
    {
        global.GetComponent<Global>().currentGS = global.GetComponent<Global>().prevGS; 
        global.GetComponent<Global>().prevGS = global.GetComponent<Global>().currentGS;
    }

    public void St_Editing()
    {
        //logic to assign the correct previous state to the variable, making sure editing doesn't become one of them
        if (global.GetComponent<Global>().prevGS == Global.GameState.Walking)
        {
            global.GetComponent<Global>().prevGS = global.GetComponent<Global>().currentGS;
        }
        else if (global.GetComponent<Global>().prevGS == Global.GameState.MViewing)
        {
            global.GetComponent<Global>().prevGS = Global.GameState.MViewing;
        }
        else if (global.GetComponent<Global>().prevGS == Global.GameState.Selecting)
        {
            global.GetComponent<Global>().prevGS = Global.GameState.Selecting;
        }
        
        global.GetComponent<Global>().currentGS = Global.GameState.MEditing;
    }

    void NameCheck()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (c_names[i] == characters[i].name)
            {
                characters[i].transform.GetChild(1).GetComponent<Image>().enabled = false; //bw icon disabled
                characters[i].transform.GetChild(2).GetComponent<Image>().enabled = true; //color icon enabled
            } else {
                characters[i].transform.GetChild(1).GetComponent<Image>().enabled = true; //bw icon disabled
                characters[i].transform.GetChild(2).GetComponent<Image>().enabled = false; //color icon enabled
            }
            
            if (global.GetComponent<Global>().currentGS == Global.GameState.Selecting && c_names[i] == characters[i].name)
            {
                characters[i].transform.GetChild(3).GetComponent<GameObject>().SetActive(true);
            } else {
                characters[i].transform.GetChild(3).GetComponent<GameObject>().SetActive(false);
            }
        }

        
    }
}

