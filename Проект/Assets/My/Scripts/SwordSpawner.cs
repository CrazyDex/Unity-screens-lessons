using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSpawner : MonoBehaviour {

    //public Collider2D swordCollider;
    //public bool attackOn;
    public bool block;
    //public GameObject target;
    public int damage;
    public LayerMask layerMask;
    public float force;
    public RaycastHit2D hit;
    public int signDir;
    public Vector2 startPos, endPos;
    public Vector2 targetDir;
    public SpriteRenderer spriteRenderer;
    public bool reload;
    public float reloadTime;
    public GameObject blockGO;

    private void Start()
    {
    }

    private void Update()
    {
        signDir = GameObject.FindGameObjectWithTag("Player").GetComponent<Mover>().signDir;
        block = blockGO.GetComponent<BoxCollider2D>().enabled;
        //block = blockGO.GetComponent<Block>().isBlock;
        if (!reload) Attack();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        //target = collision.gameObject;
    //        collision.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
    //    }
    //}

    void ReloadOff()
    {
        reload = false;
        spriteRenderer.enabled = false;
    }

    void Attack()
    {
        //swordCollider.enabled = Input.GetButtonDown("Sword") && (block == false) ? true : false;
        //bool hitEnemy;
        if (Input.GetButtonDown("Sword") && (block == false))
        {
            spriteRenderer.enabled = true;
            startPos.x = transform.position.x + 0.2f * signDir;
            startPos.y = transform.position.y;

            for (float i = 0; i <= 1; i = i >= 0 ? -i - 0.1f : -i + 0.1f)
            {
                endPos.x = 1.6f * signDir;
                endPos.y = i;
                hit = Physics2D.Raycast(startPos, endPos, 1.8f, layerMask);
                //Debug.DrawRay(startPos, endPos, Color.red, 1f);
                if (hit.collider != null)
                {
                    targetDir = hit.collider.gameObject.transform.position - transform.position;
                    if (hit.collider.tag == "Enemy") hit.collider.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
                    else if (hit.collider.tag == "Weapon") hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(targetDir * force);
                    break;
                }
            }
            reload = true;
            Invoke("ReloadOff", reloadTime);
        }
    }
}
