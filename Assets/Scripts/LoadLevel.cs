using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class LoadLevel : MonoBehaviour
{


    [SerializeField] Text pointText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] AudioSource playGameOver;

    int size = 100; // розмір сфери (відображення на екрані зверху)


    private void Awake()
    {
        PlayerSphere.SizeWasReduced.AddListener(RefreshText);
    }


   

    public void GoToMenu() // повернення до головного меню 
    {

        SceneManager.LoadScene(0);


    }



    public void LoadNextLevel() // загрузка наступного рівня 
    {

        if(PlayerPrefs.GetInt("Level") < 4)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }




    }


    void RefreshText() // оновлення інформації про розмір сфери 
    {
        size = size - 2;
      
        
        if (size > 28)
        {
            
            pointText.text = "Size: " + size.ToString() + "(Min 30)";
        }
        else
        {
            gameOverPanel.SetActive(true);
            playGameOver.Play();
        }
        

    }




}
