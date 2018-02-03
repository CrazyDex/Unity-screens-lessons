using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    public Collider2D fallCollider;
    public Collider2D activateCollider;
    public Collider2D boomCollider;
    public Sprite mineAwake;
    public Sprite mineBoom;
    public float lifeTime;
    public int damage;
    public float boomForce;
    public float boomDelay;
    public float boomAreaHeight;
    public float boomAreaWidth;
    Collider2D[] objects;

    private bool mineAwakeFlag = false;

    public GameObject go;//----------------
    public Collider2D col2d;//---------------
    public AudioClip audioMBOOM, audioMFall;//------------------

    void Start()
    {
        //мина дестроется через ...
        Destroy(gameObject, lifeTime);
        //включить когда будет соответствующий звук
        //AudioSource.PlayClipAtPoint(audioMFall, transform.position, 1f);
    }

    //при триггер коллизии ...
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("тригер сработал");
        activateCollider.enabled = false;
        activateCollider.isTrigger = false;
        if (mineAwakeFlag == false)
        {
            MineAwake();
            mineAwakeFlag = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //сюда музыку надо будет поставить
        }
    }

    void MineAwake()
    {
        print("мина эвейк запустился");
        GetComponent<SpriteRenderer>().sprite = mineAwake;
        Invoke("Boom", boomDelay);
    }
    
    void Boom()
    {
        print("бум сработал");
        //задаем точку середины бокса
        Vector2 vS = transform.position;
        vS.y += boomAreaHeight / 2;
        //задаем ширину и высоту бокса
        Vector2 vE = new Vector2(boomAreaWidth, boomAreaHeight);
        LayerMask layerMask = ((1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("Weapon")) | (1 << LayerMask.NameToLayer("Player")));
        //цепляем всех - игрока, противников, оружие
        objects = Physics2D.OverlapBoxAll(vS, vE, 0f, layerMask);

        //даем пинка каждому =)
        foreach (Collider2D obj in objects)
        {
            // Получаем риджитбоди врага
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Получаем вектор направления от мины к объекту
                Vector3 deltaPos = rb.transform.position - transform.position;
                // Создаем толчок в этом направлении
                rb.AddForce(deltaPos.normalized * boomForce);
            }
            if (obj.tag == "Enemy")
            {
                obj.GetComponent<EnemyStats>().TakeDamage(damage);
                gameObject.GetComponent<Collider2D>().enabled = false;
                //AudioSource.PlayClipAtPoint(audioSHit, transform.position, 1f);
            }
            if (obj.tag == "Player")
            {
                obj.GetComponent<PlayerStats>().TakeDamage(damage);
                gameObject.GetComponent<Collider2D>().enabled = false;
                //AudioSource.PlayClipAtPoint(audioSHit, transform.position, 1f);
            }
        }
        DestroyEffectMine();
    }

    void DestroyEffectMine()
    {
        Destroy(gameObject);
    }

    void OnHit()
    {
    }
    void OnCriticalHit()
    {
    }
    void OnMiss()
    {
    }
}
