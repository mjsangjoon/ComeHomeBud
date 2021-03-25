using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // needed since editor script

public class DeleteAllPlayerPrefs : ScriptableObject
{
    // Delete PlayerPrefs after confirmation dialog box
    static string dialog1 = "Delete all player preferences?";
    static string dialog2 = "Are you sure you want to delete all player preferences? This action cannot be undone.";

    [MenuItem ("Editor/Delte Player Prefs")]
    static void DeletePrefs(){
        if(EditorUtility.DisplayDialog(dialog1, dialog2, "Yes", "No" )){
            PlayerPrefs.DeleteAll();
        }
    }
}
