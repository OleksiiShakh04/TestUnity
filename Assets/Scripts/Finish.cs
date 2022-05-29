using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{


  [SerializeField] GameObject levelPassed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerSphere") // коли сфера попадає у колайдер, гра завершується
        {


            levelPassed.SetActive(true);
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex+1);
            PlayerPrefs.Save();


        }

    }
    
}
