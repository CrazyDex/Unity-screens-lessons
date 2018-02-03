using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawner : MonoBehaviour {

    public GameObject mine, mineSpawner, spawnedMine;
    public bool facingRight;
    public float angle;
    public float force;
    public Vector3 worldMousePosition;
    public Vector3 targetDir;
    public Vector3 normalVector;
    public int signDir;

    private void Awake()
    {
    }

    void Start()
    {
    }

    private void Update()
    {
        //facingRight = GameObject.FindGameObjectWithTag("Player").GetComponent<Mover>().facingRight;
        //signDir = facingRight ? 1 : -1;

        //worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //targetDir = worldMousePosition - transform.position;
        //normalVector = new Vector3(1, 0, 0);
        //angle = Vector2.Angle(normalVector * signDir, targetDir);

        //if (transform.position.y > targetDir.y) angle *= -1;
        //transform.localRotation = Quaternion.Euler(0, 0, angle);



        //Attack(); // почему то лучше работает в апдейте
    }

    void FixedUpdate()
    {

    }

    void LateUpdate()
    {
        Attack();
    }

    void Attack()
    {



        if (Input.GetButtonUp("Mine"))
        {
            spawnedMine = Instantiate(mine, mineSpawner.transform.position, mineSpawner.transform.rotation);
            //spawnedMine.GetComponent<Rigidbody2D>().AddForce(new Vector2(targetDir.x, targetDir.y) * force);
            //spawnedMine.GetComponent<Rigidbody2D>().AddTorque(-3f * signDir);
        }


    }
}
