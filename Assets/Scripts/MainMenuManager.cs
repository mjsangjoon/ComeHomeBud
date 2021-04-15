using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour{
    // reference to Submenus
    public GameObject _MainMenu;
    public GameObject _Credits;
    public GameObject _Instructions;
    
    //reference to Button GameObjects
    public GameObject MenuDefaultButton;
    public GameObject AboutDefaultButton;
    public GameObject CreditsDefaultButton;
    public GameObject QuitButton;

    // reference titleText to change dynamically
    public Text titleText;

    //initial title that we can set back
    private string _mainTitle;

    // init menu
    void Awake(){
        //store initial title
        _mainTitle = titleText.text;
        displayQuitWhenAppropriate();
        ShowMenu("MainMenu");
    }

    void displayQuitWhenAppropriate(){
        switch (Application.platform) {
			// platforms that should have quit button
			case RuntimePlatform.WindowsPlayer:
			case RuntimePlatform.OSXPlayer:
			case RuntimePlatform.LinuxPlayer:
				QuitButton.SetActive(true);
				break;

			// all other platforms default to no quit button
			default:
				QuitButton.SetActive(false);
				break;
		}
    }

    public void ShowMenu(string name){
        _MainMenu.SetActive(false);
        _Credits.SetActive(false);
        _Instructions.SetActive(false);

        switch(name){
            case "MainMenu":
                _MainMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(MenuDefaultButton);
                titleText.text = _mainTitle;
                break;
            case "Instructions":
                _Instructions.SetActive(true);
                EventSystem.current.SetSelectedGameObject(AboutDefaultButton);
                titleText.text = "Instructions";
                break;
            case "Credits":
                _Credits.SetActive(true);
                EventSystem.current.SetSelectedGameObject(CreditsDefaultButton);
                titleText.text = "About";
                break;
        }
    }

    public void loadGame(){
        //start new game, initialize player state
        PlayerPrefManager.SetPlayerState(0,0,0);
        SceneManager.LoadScene("SuburbsStart");
    }

    public void QuitGame(){
        Application.Quit();
    }

}
