using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal.UIElements;
using UnityEngine.UI;
public class MapController : MonoBehaviour
{
    public Image map;

    public GameObject sleepyDave;
    public Image button;
    
    public GameObject q_box;
    public GameObject d_box;

    public bool asked = false;
    
    public InputField inputf1;
    private string text1 = "";
    
    public Image bw_icon;
    public Image c_icon;

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
            QControls();
        }
        else
        {
            MapDeactivate();
            //deactivate notes but save all text
        }
    }

    void MapActivate()
    {
        sleepyDave.SetActive(true);
        NameCheck();
        map.enabled = true;
        
    }

    void MapDeactivate()
    {
        sleepyDave.SetActive(false);
        map.enabled = false;
        d_box.SetActive(false);
        q_box.SetActive(false);
    }


    void VControls()
    {
        //checks if they are false and if they aren't then it activates game state 2
        if (Input.GetKeyUp(KeyCode.N)) // changes into viewing
        {
            global.GetComponent<Global>().gameState = 0; //walkin time
        }
        
    }

    void EControls()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            global.GetComponent<Global>().gameState = 1; //back to map
        }

        //put in editing the input fields here
    }

    void QControls()
    {
        q_box.SetActive(true);
        d_box.SetActive(true);
        NameCheck();
        if (q_box.GetComponentInChildren<Text>().text == "Sleepy Dave" && asked == true)
        {
            d_box.GetComponentInChildren<Text>().text =
                "I know Sleepy Dave, he was more of a father to me than my own Dad was. It's a shame what's happening to his bar, I offered him a job at my store but he wouldn't take it.";
            asked = false;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            global.GetComponent<Global>().gameState = 0;//back to walking
            d_box.GetComponentInChildren<Text>().text = "Ciao! My name is Giovanni!";
        }    
    }

    public void Ask()
    {
        asked = true;
    }

    public void AddName()
    {
        q_box.GetComponentInChildren<Text>().text = "Sleepy Dave";
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
        global.GetComponent<Global>().gameState = 1; //causing problem where game state is changed when the game state is 3
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
        
        if (global.GetComponent<Global>().gameState == 3 && text1 == "Sleepy Dave")
        {
            button.enabled = true;
        }
        else
        {
            button.enabled = false;
        }
    }
}

