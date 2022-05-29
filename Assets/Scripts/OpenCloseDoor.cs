using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour
{

    [SerializeField] GameObject openDoor;
    [SerializeField] GameObject closeDoor;
 

    private void OnTriggerEnter(Collider other) // якщо сфери попала у цей колайдер, то відкриваються двері
    {
        
       
        
        if (other.gameObject.name == "PlayerSphere")
        {


            openDoor.SetActive(true);
            closeDoor.SetActive(false);


        }
    }

   

}
