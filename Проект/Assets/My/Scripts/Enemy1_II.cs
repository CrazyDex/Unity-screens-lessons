using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_II : MonoBehaviour {

    public GameObject hitGO;
    public bool facingRight;
    public int signDir;
    public Vector2 startPos;
    public Collider2D lookerCollider;
    public RaycastHit2D hit;
    public LayerMask layerMask;
    public bool lookReloading;
    public float lookReloadTime;
    public Vector2 endPos;
    public bool angry;
    public Vector3 dir;
    public float forwardSpeed, backSpeed;
    public float temp;
    public Collider2D playerCollider;
    public bool angryReload;
    public float angryReloadTime;
    public GameObject playerGO;

    private void Start()
    {
        angry = true;
        //shurikenReload = true;
        forwardSpeed = 7f;
        backSpeed = 3f;
    }
    void Update()
    {
        signDir = transform.lossyScale.x >= 0 ? 1 : -1;
        if (angry) Look();
        else if (!lookReloading) Look();
        if (angry) Tactic();
    }
    

    void FaceToTarget()
    {
        float x = 0;
        if (hitGO != null) x = hitGO.transform.position.x - transform.position.x;
        if (((x < 0) && (signDir > 0)) || ((x > 0) && (signDir < 0))) Flip();
    }

    void Tactic()
    {
        //FaceToTarget();
        if (Mathf.Abs(hitGO.transform.position.x - transform.position.x) >= 5f)
        {
            if (!shurikenReload && (hitGO.tag != "OnPlayer") && (block == false)) RangeAttack();
            else SaveDistance();
        }
        else if (Mathf.Abs(hitGO.transform.position.y - transform.position.y) < 5f)
        {
            if (!swordReload && (hitGO.tag != "OnPlayer") && (block == false)) MelleAttack();
            else Catch();
        }
        hitGO = null;
            //else в зависимости от того где игрок и где противник есть ли дыры, ящики и т.д.
    }


    //void Look()
    //{
    //    if (lookerCollider.IsTouching(playerCollider)) angry = true;
    //    else
    //    {
    //        angry = false;
    //        lookReload = true;
    //        Invoke("LookReloadOff", lookReloadTime);
    //    }
    //}

    void Look()
    {
        startPos = transform.position;
        for (float i = 0; i <= 10; i = i >= 0 ? -i - 1f : -i + 1f)
        {
            endPos.x = (20f - Mathf.Abs(i)) * signDir;
            endPos.y = i;
            hit = Physics2D.Raycast(startPos, endPos, 20f - Mathf.Abs(i), layerMask);

            Debug.DrawRay(startPos, endPos, Color.red, 0.01f);
            if (hit.collider != null)
            {
                hitGO = hit.collider.gameObject;
                if ((hitGO.tag == "Player") || (hitGO.tag == "OnPlayer"))
                {
                    angry = true;
                }
                break;
            }
            //else if (!angryReload)
            //{
            //    Sick();
            //    //Invoke("AngryReloadOff", angryReloadTime);
            //}
            else
            {
                playerGO = null;
                AngryReloadingStart();
                //angry = false;
                lookReloading = true;
                Invoke("LookReloadOff", lookReloadTime);
                //Sick();
            }
        }
    }

    public float angryReloadingTime;

    void AngryReloadingStart()
    {
        angry = true;
        if (!IsInvoking("AngryReloadingEnd")) Invoke("AngryReloadingEnd", angryReloadingTime);
    }

    void AngryReloadingEnd()
    {
        angry = false;
    }

    //void LookAround()
    //{
    //    startPos = transform.position;
    //    var x = 0f;
    //    var y = 2f;


    //    for (int i = 0; i < 13; i++)
    //    {
    //        var alpha = -10f;
    //        var rx = startPos.x - x-80;
    //        var ry = startPos.y - y-82;
    //        var c = Mathf.Cos(alpha);
    //        var s = Mathf.Sin(alpha);
    //        x = x + rx * c - ry * s; //var x1=
    //        y = y + rx * s + ry * c; //var y1=

    //        endPos.x = x;
    //        endPos.y = y;

    //        hit = Physics2D.Raycast(startPos, endPos, 20f, layerMask);

    //        Debug.DrawRay(startPos, endPos, Color.red, 0.03f);
    //        if (hit.collider != null)
    //        {
    //            hitGO = hit.collider.gameObject;
    //            if ((hitGO.tag == "Player") || (hitGO.tag == "OnPlayer"))
    //            {
    //                angry = true;
    //            }
    //            break;

    //        }
    //    }






    //for (float i = 0; i <= 10; i = i >= 0 ? -i - 1f : -i + 1f)
    //{
    //    endPos.x = (20f - Mathf.Abs(i)) * signDir;
    //    endPos.y = i;

    //    }
    //    else if (!angryReload)
    //    {
    //        Sick();
    //        //Invoke("AngryReloadOff", angryReloadTime);
    //    }
    //    else
    //    {
    //        playerGO = null;
    //        angry = false;
    //        lookReload = true;
    //        Invoke("LookReloadOff", lookReloadTime);
    //    }
    //}
    //}

    public Vector2 tempV2;

    void Sick()
    {
        if (playerGO == null) playerGO = FindObjectOfType<PlayerStats>().gameObject;
        tempV2 = transform.position - playerGO.transform.position;
        if ((((tempV2.x <= 20) && (tempV2.x >= -20)) && ((tempV2.y <= 20) && (tempV2.y >= -20))))
        {
            if ((tempV2.x >= 0) && (signDir == -1)) Flip();
            else Flip(); //if ((tempV2.x < 0) && (signDir == 1)) Flip();
            //LookAround();
        }

    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Follow()
    {

    }

    void LookReloadOff() { lookReloading = false; }

    void SaveDistance()
    {
        //расстояние между врагом и персом
        temp = hitGO.transform.position.x - transform.position.x;
        //если перс близко противник отходит, если далеко подходит
        dir.x = (temp >= 5f) && (temp < 15f) ? -1 : 1;
        //эта строка чтобы враги не тупили как черти на заданной дальней дистанции
        if (((dir.x == -1) && (temp >= 14.5f)) || ((dir.x == 1) && (temp <= -14.5f))) dir.x = 0;
        //если враг наступает, то идет со стандартной скоростью
        if ((dir.x > 0 && signDir > 0) || (dir.x < 0 && signDir < 0)) transform.position += dir * forwardSpeed * Time.deltaTime;
        //если враг отступает, то идет с замедленной скоростью
        else if ((dir.x > 0 && signDir < 0) || (dir.x < 0 && signDir > 0)) transform.position += dir * backSpeed * Time.deltaTime;
    }

    void Catch()
    {
        //расстояние между врагом и персом
        temp = hitGO.transform.position.x - transform.position.x;
        //если перс далеко противник наступает
        if ((temp > 1.5f) && (temp < 5f)) dir.x = 1;
        else if ((temp < -1.5f) && (temp < -5f)) dir.x = -1;
        else dir.x = 0;
        //if () dir.x = 0;

        //dir.x = (temp > 1f) && (temp < 5f) ? 1 : 0;
        //..........if ((dir.x > 0 && signDir > 0) || (dir.x < 0 && signDir < 0)) transform.position += dir * forwardSpeed * Time.deltaTime;
        transform.position += dir * forwardSpeed * Time.deltaTime;
        //..........else if ((dir.x > 0 && signDir < 0) || (dir.x < 0 && signDir > 0)) transform.position += dir * backSpeed * Time.deltaTime;
    }

    void Patrol()
    {

    }

    void StayPoint()
    {

    }

    public SpriteRenderer swordSprite;
    public Collider2D swordCollider;
    public float swordForce;
    //public Vector2 targetDir;
    public float swordReloadTime;
    public bool swordReload;
    public bool block;
    public EnemySwordSpawner enemySwordSpawner;
    public int swordDamage;

    void MelleAttack()
    {
        swordCollider.enabled = true;
        swordSprite.enabled = true;
        foreach (Collider2D col in enemySwordSpawner.cols)
        {
            try
            {
                targetDir = col.gameObject.transform.position - transform.position;
                print(col.tag);
                if (col.tag == "Player")
                {
                    col.gameObject.GetComponent<PlayerStats>().TakeDamage(swordDamage);
                    col.gameObject.GetComponent<Rigidbody2D>().AddForce(targetDir * swordForce);

                    //AudioSource.PlayClipAtPoint(audioSHit, transform.position, 1f);
                }
                if ((col.tag == "Weapon") || (col.tag == "EnemyWeapon") || (col.tag == "OnPlayer"))
                {
                    col.gameObject.GetComponent<Rigidbody2D>().AddForce(targetDir * swordForce);
                }
            }
            catch
            {
                print("error");
            }
        }
        Invoke("SCO", 0.1f);
        //swordCollider.enabled = false;
        swordReload = true;
        Invoke("SwordReloadOff", swordReloadTime);
        enemySwordSpawner.cols.Clear();
    }

    void SCO()
    {
        swordCollider.enabled = false;
    }

    void SwordReloadOff()
    {
        swordSprite.enabled = false;
        swordReload = false;
    }







    public GameObject shuriken, spawnedShuriken, shurikenSpawner;
    public float shurikenForce;
    public Vector2 targetDir;
    public float shurikenReloadTime;
    public bool shurikenReload;

    void RangeAttack()
    {
        targetDir = hitGO.transform.position - shurikenSpawner.transform.position;
        targetDir.y += 0.5f;
        spawnedShuriken = Instantiate(shuriken, shurikenSpawner.transform.position, shurikenSpawner.transform.rotation);
        spawnedShuriken.GetComponent<Rigidbody2D>().AddForce(targetDir * shurikenForce);
        spawnedShuriken.GetComponent<Rigidbody2D>().AddTorque(-3f * signDir);
        shurikenReload = true;
        Invoke("ShurikenReloadOff", shurikenReloadTime);
    }
    void ShurikenReloadOff()
    {
        shurikenReload = false;
    }


}
