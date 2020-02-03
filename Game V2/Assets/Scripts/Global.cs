using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
//keeps track of the game state and other global variables that need to stay constant in the scene
//part of the game manager
{
   public static Global me;

   //STATE MACHINES
   public enum GameState //Dom State
   {
      Walking, 
      Viewing, 
      Editing, 
      Selecting
   };
   public enum UIState //Sub State
   {
      Walking, //default 
      LargeMap,
      Conflicts,
      ID,
      SmallMap1,
      SmallMap2
   };
   
   public GameState currentGS;
   public GameState prevGS; 
   public UIState currentUIS;
   
   
   //PUZZLE TRACKER
   public bool puzzle1 = false;

   void Awake()
   {
      me = this;
   }
   void Start() //defaulting
   {
      currentGS = GameState.Walking;
      prevGS = GameState.Walking;
      currentUIS = UIState.Walking;
   }
}
