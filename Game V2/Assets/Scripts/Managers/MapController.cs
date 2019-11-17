using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal.UIElements;
using UnityEngine.UI;
public class MapController : MonoBehaviour
{
    public Image map;
    
    public InputField inputf1;
    private string text1 = "";
    
    public Image bw_icon;
    public Image c_icon;

    public GameObject global;
    
    private bool editing = false;
    
    void Start()
    {
        global = GameObject.Find("Game Manager");
    }
    
    void Update()
    {
        if (global.GetComponent<Global>().gameState == 1)
        {
            MapActivate();
            VControls();
        }
        else if (global.GetComponent<Global>().gameState == 2) 
        {
            MapActivate();
            EControls();
        }
        else if (global.GetComponent<Global>().gameState == 3)
        {
            MapActivate();
        }
        else
        {
            MapDeactivate();
            //deactivate notes but save all text
        }
    }

    void MapActivate()
    {
        map.enabled = true;
        NameCheck();
        inputf1.image.enabled = true;
        inputf1.placeholder.enabled = true;
        inputf1.textComponent.enabled = true;
    }

    void MapDeactivate()
    {
        map.enabled = false;
        bw_icon.enabled = false;
        c_icon.enabled = false; 
        inputf1.image.enabled = false;
        inputf1.placeholder.enabled = false;
        inputf1.textComponent.enabled = false;
    }


    void VControls()
    {
        //checks if they are false and if they aren't then it activates game state 2
        if (Input.GetKeyUp(KeyCode.N)) // changes into viewing
        {
            global.GetComponent<Global>().gameState = 0;
        }
        
    }

    void EControls()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            global.GetComponent<Global>().gameState = 1; //back to notes
        }

        //put in editing the input fields here
    }
    
    void Esc() // overrides the function of escape normally
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            inputf1.DeactivateInputField(); // 1
            inputf1.text = text1;

            editing = false;
        }
    }
    
    public void Save() // saving the text once the input field is called
    {
        text1 = inputf1.text;
    }

    public void E_Editing() //ending editing - switching to view mode
    {
        global.GetComponent<Global>().gameState = 1;
    }

    public void St_Editing()
    {
        global.GetComponent<Global>().gameState = 2;
    }

    void NameCheck()
    {
        if (text1 == "Sleepy Dave")
        {
            bw_icon.enabled = false;
            c_icon.enabled = true;
        }
        else
        {
            bw_icon.enabled = true;
            c_icon.enabled = false; 
        }
    }
}

