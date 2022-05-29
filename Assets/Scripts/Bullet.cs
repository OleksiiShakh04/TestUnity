using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    bool isHit = false; // �� ������ ���� �������
    Vector3 startPosition; // �������� ������� ��� 

    float startSize = 1.5f; // ���������� ����� ��� 
    float radiusOfInfection = 1f; // ����� ��������� 
    float stepToIncreaseSize = 1.02f; // ����, �� ���� ���� ������������ ���� 

    [SerializeField] float speed = 15.0f; // �������� ���� ��� 
    [SerializeField] LayerMask layerMask;
    [SerializeField] AudioSource destroyObject;



    public float RadiusOfInfection
    {

        get { return radiusOfInfection; }
        set { radiusOfInfection = value; }
    }

    public bool IsHit {

        get { return isHit; }
        set { isHit = value; } 
    }



    private void Start()
    {
        startPosition = gameObject.transform.position;
        PlayerSphere.LaneIsClear.AddListener(BulletOff);
        PlayerSphere.SizeWasReduced.AddListener(ChangeSizeBullet);
      
    }

    private void Update()
    {
        if (isHit == true) { HitDirect(); }
    }

    void HitDirect() // ���� �������� ������ � ������ � ����� ��������� 
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.right, speed * Time.deltaTime);
       
    }


   void ChangeSizeBullet() // ���� ������ ��� 
    {

        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * stepToIncreaseSize, gameObject.transform.localScale.y * stepToIncreaseSize, gameObject.transform.localScale.z * stepToIncreaseSize);


    }

    private void OnTriggerEnter(Collider collision) // ���� ���� ������ � ���������, �������� ��������� � ����� ���
    {
        if(collision.gameObject.layer == 8)
        {

            isHit = false;
            ToInfectObstacles();
            StartCoroutine(ResetBullet());

        }
    }


    IEnumerator ResetBullet() // ���������� ��� � ���������� ����� � �� ��������� �������
    {
        MeshRenderer meshBullet = gameObject.GetComponent<MeshRenderer>();

        meshBullet.enabled = false;
        transform.position = startPosition;
        transform.localScale =  new Vector3(startSize, startSize, startSize);
        radiusOfInfection = 1f;
        yield return new WaitForSeconds(0.5f);
        meshBullet.enabled = true;

    }


    void ToInfectObstacles() // ����� ��������� �������� 
    {

     
       Collider[] allObjects = Physics.OverlapSphere(transform.position, radiusOfInfection, layerMask);

        foreach(Collider el in allObjects)
        {

            el.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            StartCoroutine(KillObstacles(el));
        }

    }


    IEnumerator KillObstacles(Collider col) // �����, �� ������� �� �������� ��������� 
    {
        yield return new WaitForSeconds(1f);
        destroyObject.Play();
        Destroy(col.gameObject);


    }

    void BulletOff() // ��������� ���, ���� ��� �����������
    {

        gameObject.SetActive(false);

    }

}
