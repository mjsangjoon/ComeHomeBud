using UnityEngine;
using System.Collections;
using System;   //including for convert class
using UnityEngine.SceneManagement;

public static class PlayerPrefManager{

    //  list of things to track
    //  Num times a familiar landmark was seen
    //  Num times a familiar scent was followed
    //  Suburbs complete?
    //  Family friend was encountered
    //  Missing poster event happened
    

    public static void SetSuburbsComplete(bool complete){
        PlayerPrefs.SetInt("SuburbsComplete", Convert.ToInt32(complete));
        //resetting landmark and scent count for next stage
        SetLandmarkCount(0);
        SetScentCount(0);
    }

    public static void SetLandmarkCount(int landmarkCount){
        PlayerPrefs.SetInt("LandmarkCount", landmarkCount);
    }

    public static void SetScentCount(int scentCount){
        PlayerPrefs.SetInt("FamiliarScentCount", scentCount);
    }

    public static void SetFriendFound(bool found){
        PlayerPrefs.SetInt("EncounteredFriend", Convert.ToInt32(found));
    }

    public static void SetPoster(bool poster){
        PlayerPrefs.SetInt("MissingPosterEvent", Convert.ToInt32(poster));
    }
    
    public static bool GetSuburbsComplete(){
        if (PlayerPrefs.HasKey("SuburbsComplete")){
            return Convert.ToBoolean(PlayerPrefs.GetInt("SuburbsComplete"));
        }
        else return false;
    }
    
    public static bool GetFamilyFriendEncountered(){
        if (PlayerPrefs.HasKey("EncounteredFriend")){
            return Convert.ToBoolean(PlayerPrefs.GetInt("EncounteredFriend"));
        }
        else return false;
    }
    
    public static bool GetMissingPosterEvent(){
        if (PlayerPrefs.HasKey("MissingPosterEvent")){
            return Convert.ToBoolean(PlayerPrefs.GetInt("MissingPosterEvent"));
        }
        else return false;
    }
    
    public static int GetLandmarkCount(){
        if(PlayerPrefs.HasKey("LandmarkCount")){
            return PlayerPrefs.GetInt("LandmarkCount");
        }
        else return 0;
    }

    public static int GetScentCount(){
        if(PlayerPrefs.HasKey("FamiliarScentCount")){
            return PlayerPrefs.GetInt("FamiliarScentCount");
        }
        else return 0;
    }

    //saves the current player state for moving to next scene
    public static void SavePlayerState(int landmarkCount, int scentCount, bool familyFriend, bool missingPoster){
        PlayerPrefs.SetInt("LandmarkCount",landmarkCount);
        PlayerPrefs.SetInt("FamiliarScentCount", scentCount);
        //PlayerPrefs.SetInt("EncounteredFriend", Convert.ToInt32(familyFriend));
        //PlayerPrefs.SetInt("MissingPosterEvent", Convert.ToInt32(missingPoster));
    }

    //Set stored player state and variables
    public static void SetPlayerState(int lm, int fs, int subComp){
        Debug.Log("Player State reset. Landmark: " + lm + ", Scent: " + fs + ", Suburbs: " + subComp);
        PlayerPrefs.SetInt("LandmarkCount", lm);
        PlayerPrefs.SetInt("FamiliarScentCount", fs);
        PlayerPrefs.SetInt("SuburbsComplete", subComp);
    }

    public static void ShowPlayerPrefs(){
        string[] values = {"LandmarkCount", "FamiliarScentCount", "SuburbsComplete"};

        foreach(string value in values){
            if(PlayerPrefs.HasKey(value)){
                Debug.Log(value + " = " + PlayerPrefs.GetInt(value));
            } else{
                Debug.Log(value  + " is not set.");
            }
        }
    }
}