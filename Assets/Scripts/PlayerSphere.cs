using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.Events;

public class PlayerSphere : MonoBehaviour
{

    float currentSize = 1f; // ����� ����� � ����� ������ 
    float minSize = 0.3f; // ��������� ���������� ����� �����
    float stepToDecreaseSize = 0.98f; // ����, � ���� ���� ������������ �����(2 % �� ���� ���)
    float speed = 15f; // �������� ���� ����� �� �����

    bool tapTime = true;
    bool gameWin = false; 
    
    RaycastHit hit;
    [SerializeField] LayerMask Layermask; // ��� �����
    [SerializeField] LayerMask LayermaskObstacles; // ��� ��������
    [SerializeField] Bullet bullet;
    [SerializeField] Transform leftRayCast; 
    [SerializeField] Transform RightRayCast;
    [SerializeField] AudioSource playWin; 
    

    public static UnityEvent SizeWasReduced = new UnityEvent(); // �����, ���� ���������, ���� ���� �������� ����� ��� ���� ��� 
    public static UnityEvent LaneIsClear = new UnityEvent(); // �����, ���� ���������, ���� �� ����� ����� ���� ��������




    void Update()
    {

        // ����� � ���������, ���� ���� ������ �� �����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 

        if(Physics.Raycast(ray, out hit, 300f, Layermask) && Input.GetButton("Fire1"))
        {

            if (tapTime == true) { StartCoroutine(TimeToHit()); }

        }

        if (Physics.Raycast(ray, out hit, 300f, Layermask) && Input.GetButtonUp("Fire1"))
        {

            bullet.IsHit = true; // ���� ����������� ����������, ���� ������ 
        }

        RayCastCheck(); 

        // ���� �� ����� ����� ���� ��������, ���� �������� �� �����
        if(gameWin == true)
        {
          transform.position = Vector3.MoveTowards(transform.position, transform.right, speed * Time.deltaTime);


        }


    }


    
    IEnumerator TimeToHit() // ������ ���� �� ������� �� ������ �������� ����� �����
    {
        tapTime = false;
     
        PrepareToHit();
        yield return new WaitForSeconds(0.25f);

        tapTime = true;
    }




    void PrepareToHit( ) // ��������� ������ ����� ���� ���
    {

        if(currentSize >= minSize)
        {
            

            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * stepToDecreaseSize, gameObject.transform.localScale.y * stepToDecreaseSize, gameObject.transform.localScale.z * stepToDecreaseSize);


            currentSize = currentSize - (1 - stepToDecreaseSize);

            bullet.RadiusOfInfection += 1f;
            SizeWasReduced.Invoke();

        }

    }



    void RayCastCheck() // ��������, �� � �� ����� ����� �� ���� ��������� (������� ��� �������� �� ����� � �� ������)
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

            if (left == false && right == false && centre == false) // ���� �� ��� �������� ����� �� ��������� � ����� ���������
            {
                LaneIsClear.Invoke();
          

                 gameWin = true;
                playWin.Play();

            }

        }
        
    }

  
}
