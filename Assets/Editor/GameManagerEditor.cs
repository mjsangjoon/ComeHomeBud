using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //needed since this script uses Unity Editor

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor //extend Editor class
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //reference to GameManager script on target gameObject
        GameManager myGM = (GameManager)target;

        //add custom button to inspector component
        if(GUILayout.Button("Reset Player State")){
            PlayerPrefManager.SetPlayerState(0,0,0);
        }
        if(GUILayout.Button("Output Player State")){
            PlayerPrefManager.ShowPlayerPrefs();
        }
        if(GUILayout.Button("Reset Player State for City")){
            PlayerPrefManager.SetPlayerState(0,0,1);
        }
    }
}
