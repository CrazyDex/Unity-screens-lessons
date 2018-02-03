using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenSpawner : MonoBehaviour {

    public GameObject shuriken, shurikenSpawner, spawnedShuriken;
    public bool facingRight;
    public float angle;
    public float force;
    public Vector3 worldMousePosition;
    public Vector3 targetDir;
    public Vector3 normalVector;
    public int signDir;
    public bool reloading;
    public float reloadingTime;

    void Awake()
    {
        reloading = false;
    }

    void Update()
    {
        if (!reloading)
        {
            Aiming();
            Attack();
        }
            
    }

    void Aiming()
    {
        facingRight = GameObject.FindGameObjectWithTag("Player").GetComponent<Mover>().facingRight;
        signDir = facingRight ? 1 : -1;

        worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetDir = worldMousePosition - transform.position;
        normalVector = new Vector3(1, 0, 0);
        angle = Vector2.Angle(normalVector * signDir, targetDir);

        if (transform.position.y > targetDir.y) angle *= -1;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    void Attack()
    {
        if (Input.GetButtonDown("Shuriken"))
        {
            spawnedShuriken = Instantiate(shuriken, shurikenSpawner.transform.position, shurikenSpawner.transform.rotation);
            spawnedShuriken.GetComponent<Rigidbody2D>().AddForce(new Vector2(targetDir.x, targetDir.y) * force);
            spawnedShuriken.GetComponent<Rigidbody2D>().AddTorque(-3f * signDir);
            ReloadingStart();
        }
    }

    void ReloadingStart()
    {
        reloading = true;
        Invoke("ReloadingEnd", reloadingTime);
    }

    void ReloadingEnd()
    {
        reloading = false;
    }
}
