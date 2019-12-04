using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
//keeps track of the game state and other global variables that need to stay constant in the scene
//part of the game manager
{
   // 0 - walking  | 1 - map viewing  2 - map editing | 3 - choosing topic to talk about
   public enum GameState {Walking, MViewing, MEditing, Selecting};

   public GameState currentGS;
   public GameState prevGS;
   void Start()
   {
      currentGS = GameState.Walking;
      prevGS = GameState.Walking;
   }
}
