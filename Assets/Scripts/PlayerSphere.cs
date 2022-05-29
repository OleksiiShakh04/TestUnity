using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.Events;

public class PlayerSphere : MonoBehaviour
{

    float currentSize = 1f; // розмір сфери у даний момент 
    float minSize = 0.3f; // мінімальний допустимий розмір сфери
    float stepToDecreaseSize = 0.98f; // крок, з яким буде зменшуватись сфера(2 % за один раз)
    float speed = 15f; // швидкість руху сфери до фінішу

    bool tapTime = true;
    bool gameWin = false; 
    
    RaycastHit hit;
    [SerializeField] LayerMask Layermask; // леєр сфери
    [SerializeField] LayerMask LayermaskObstacles; // леєр перешкод
    [SerializeField] Bullet bullet;
    [SerializeField] Transform leftRayCast; 
    [SerializeField] Transform RightRayCast;
    [SerializeField] AudioSource playWin; 
    

    public static UnityEvent SizeWasReduced = new UnityEvent(); // івент, який спрацьовує, коли було зменшено розмір кулі один раз 
    public static UnityEvent LaneIsClear = new UnityEvent(); // івент, який спрацьовує, коли на шляху більше немає перешкод




    void Update()
    {

        // слідкує і спрацьовує, коли було нажато на сферу
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 

        if(Physics.Raycast(ray, out hit, 300f, Layermask) && Input.GetButton("Fire1"))
        {

            if (tapTime == true) { StartCoroutine(TimeToHit()); }

        }

        if (Physics.Raycast(ray, out hit, 300f, Layermask) && Input.GetButtonUp("Fire1"))
        {

            bullet.IsHit = true; // коли припинилось натискання, куля стріляє 
        }

        RayCastCheck(); 

        // якщо на шляху більше немає перешкод, куля рухається до фінішу
        if(gameWin == true)
        {
          transform.position = Vector3.MoveTowards(transform.position, transform.right, speed * Time.deltaTime);


        }


    }


    
    IEnumerator TimeToHit() // скільки разів за секунду ми можемо зменшити розмір сфери
    {
        tapTime = false;
     
        PrepareToHit();
        yield return new WaitForSeconds(0.25f);

        tapTime = true;
    }




    void PrepareToHit( ) // зменшення розміру сфери один раз
    {

        if(currentSize >= minSize)
        {
            

            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * stepToDecreaseSize, gameObject.transform.localScale.y * stepToDecreaseSize, gameObject.transform.localScale.z * stepToDecreaseSize);


            currentSize = currentSize - (1 - stepToDecreaseSize);

            bullet.RadiusOfInfection += 1f;
            SizeWasReduced.Invoke();

        }

    }



    void RayCastCheck() // перевіряє, чи є на шляху сфери ще якісь перешкоди (випускає три рейкасти по бокам і по центру)
    {


        bool left,right,centre;
        
        if (Physics.Raycast(leftRayCast.position, leftRayCast.right, 300f, LayermaskObstacles))
        {
            left = true;
        }
        else { left = false; }

        if (Physics.Raycast(RightRayCast.position, RightRayCast.right, 300f, LayermaskObstacles))
        {
            right = true;
        }
        else { right = false; }

        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.right, 300f, LayermaskObstacles))
        {
            centre = true;
        }
        else { centre = false; }



        if(gameWin == false)
        {

            if (left == false && right == false && centre == false) // якщо всі три рейкасти більши не попадають в якусь перешкоду
            {
                LaneIsClear.Invoke();
          

                 gameWin = true;
                playWin.Play();

            }

        }
        
    }

  
}
