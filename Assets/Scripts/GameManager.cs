using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //include to reference UI elements
using UnityEngine.SceneManagement; //include to manipulate SceneManager

public class GameManager : MonoBehaviour
{
    //static refernce to game manager so it can be called from other scripts directly
    public static GameManager gameManager;

    //levels to move to
    public int numTimesLandmarkNeedsToBeSeen;
    public int numTimesFamiliarScentNeedsToBeFound;
    public bool instantWinPossible;
    public bool skipToNextStagePossible;

    //game performance
    int numTimesFamiliarLandmark;
    int numTimesFamiliarScent;
    bool familyFriendFound;
    bool missingPosterEncountered;
    //public int warinessLevel;

    //private variables
    GameObject _player;
    Vector3 _spawnLocation;
    Scene _scene;

    //set things up here
    void Awake(){
        //setup reference to game manager
        if(gameManager == null)
            gameManager = this.GetComponent<GameManager>();

        //setup all variables, UI, provide errors if things not setup properly
        setupDefaults();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void setupDefaults(){
        //setup reference to player
        if(_player == null)
            _player = GameObject.FindGameObjectWithTag("Player");
        
        if(_player == null)
            Debug.LogError("Player not found in Game Manager");

        //get current scene
        _scene = SceneManager.GetActiveScene();

        // get stored player prefs
        refreshPlayerState();
    }
    
    void refreshPlayerState(){
        numTimesFamiliarLandmark = PlayerPrefManager.GetLandmarkCount();
        numTimesFamiliarScent = PlayerPrefManager.GetScentCount();
        familyFriendFound = PlayerPrefManager.GetFamilyFriendEncountered();
        missingPosterEncountered = PlayerPrefManager.GetMissingPosterEvent();
    }

    public void AddLandmarkCount(){
        numTimesFamiliarLandmark += 1;
    }

    public void AddScentCount(){
        numTimesFamiliarScent += 1;
    }

    public int getNumTimesFamiliarLandmark(){
        return numTimesFamiliarLandmark;
    }

    public int getNumTimesFamiliarScent(){
        return numTimesFamiliarScent;
    }
    
    public void LevelComplete(string nextLevel){
        string s = nextLevel;
        if(nextLevel == "")
            s = _scene.name;

        PlayerPrefManager.SavePlayerState(numTimesFamiliarLandmark, numTimesFamiliarScent,familyFriendFound, missingPosterEncountered);
        StartCoroutine(LoadNextLevel(s));
    }

    public void resetPlayerPrefsForPlayButton(){
        PlayerPrefs.DeleteAll();
        this.LevelComplete("SuburbsStart");
    }

    IEnumerator LoadNextLevel(string nextLevel){
        yield return new WaitForSeconds(1f);
        Debug.Log("Moving to next level: " + nextLevel);
        SceneManager.LoadScene(nextLevel);
    }
}
