using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lane : MonoBehaviour
{

    float stepToDecreaseSize = 0.98f;
    private void Awake()
    {
        PlayerSphere.SizeWasReduced.AddListener(ChangeSizeLane);

    }

   void ChangeSizeLane() // зменшує розмір доріжки згідно розміру сфери
    {

        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z * stepToDecreaseSize);

    }

}
