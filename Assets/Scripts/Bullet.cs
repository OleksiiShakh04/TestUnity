using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    bool isHit = false; // чи готова куля стріляти
    Vector3 startPosition; // стартова позиція кулі 

    float startSize = 1.5f; // початковий розмір кулі 
    float radiusOfInfection = 1f; // радуіс зараження 
    float stepToIncreaseSize = 1.02f; // крок, на який буде збільшуватись куля 

    [SerializeField] float speed = 15.0f; // швидкість руху кулі 
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

    void HitDirect() // куля рухається вперед і попадає в першу перешкоду 
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.right, speed * Time.deltaTime);
       
    }


   void ChangeSizeBullet() // зміна розміру кулі 
    {

        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * stepToIncreaseSize, gameObject.transform.localScale.y * stepToIncreaseSize, gameObject.transform.localScale.z * stepToIncreaseSize);


    }

    private void OnTriggerEnter(Collider collision) // коли куля попала у перешкоду, робиться зараження і ресет кулі
    {
        if(collision.gameObject.layer == 8)
        {

            isHit = false;
            ToInfectObstacles();
            StartCoroutine(ResetBullet());

        }
    }


    IEnumerator ResetBullet() // повернення кулі у початковий розмір і на початкову позицію
    {
        MeshRenderer meshBullet = gameObject.GetComponent<MeshRenderer>();

        meshBullet.enabled = false;
        transform.position = startPosition;
        transform.localScale =  new Vector3(startSize, startSize, startSize);
        radiusOfInfection = 1f;
        yield return new WaitForSeconds(0.5f);
        meshBullet.enabled = true;

    }


    void ToInfectObstacles() // метод зараження перешкод 
    {

     
       Collider[] allObjects = Physics.OverlapSphere(transform.position, radiusOfInfection, layerMask);

        foreach(Collider el in allObjects)
        {

            el.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            StartCoroutine(KillObstacles(el));
        }

    }


    IEnumerator KillObstacles(Collider col) // метод, що видаляє усі зараженні перешкоди 
    {
        yield return new WaitForSeconds(1f);
        destroyObject.Play();
        Destroy(col.gameObject);


    }

    void BulletOff() // вимкнення кулі, коли гра завершилась
    {

        gameObject.SetActive(false);

    }

}
