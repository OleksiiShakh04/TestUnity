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

    int size = 100; // ����� ����� (����������� �� ����� ������)


    private void Awake()
    {
        PlayerSphere.SizeWasReduced.AddListener(RefreshText);
    }


   

    public void GoToMenu() // ���������� �� ��������� ���� 
    {

        SceneManager.LoadScene(0);


    }



    public void LoadNextLevel() // �������� ���������� ���� 
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


    void RefreshText() // ��������� ���������� ��� ����� ����� 
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
