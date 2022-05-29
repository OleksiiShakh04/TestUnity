using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{

    int currentLevel;
    [SerializeField] Text textLevel;
    private void Start()
    {

        
        if(PlayerPrefs.HasKey("Level")) {

            currentLevel = PlayerPrefs.GetInt("Level");
            textLevel.text = "Level " + currentLevel.ToString();

        }
        else
        {
            PlayerPrefs.SetInt("Level",1);
            PlayerPrefs.Save();
            currentLevel = PlayerPrefs.GetInt("Level");
            textLevel.text = "Level " + currentLevel.ToString();

        }

    }


    public void StartGame() // загрузка даного рівня 
    {


        SceneManager.LoadScene(currentLevel);



    }


    public void ExitGame()
    {



        Application.Quit();


    }


}
