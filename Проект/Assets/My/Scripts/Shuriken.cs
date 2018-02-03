using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour {

    public float lifeTime;
    public int damage;
    public AudioClip audioSHit, audioSThrow, audioSMiss;

	void Start()
    {
        //сюрикен дестроется через ...
        Destroy(gameObject, lifeTime);
        AudioSource.PlayClipAtPoint(audioSThrow, transform.position, 1f);
    }

    private void Update()
    {
        damage = (int) GetComponent<Rigidbody2D>().velocity.magnitude;
    }

    //при триггер коллизии ...
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //если с землей, то триггер выкл дабы не наносить урон
        if (collision.gameObject.tag == "Ground")
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            AudioSource.PlayClipAtPoint(audioSMiss, transform.position, 1f);
        }
        //если с противником, то тоже выключает триггер, но попутно дамажит
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
            gameObject.GetComponent<Collider2D>().enabled = false;
            AudioSource.PlayClipAtPoint(audioSHit, transform.position, 1f);
            //делаем папкой того в кого втюрились
            gameObject.transform.parent = collision.gameObject.transform;
            //отрубаем всю физику к чертякам
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().simulated = false;

        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            gameObject.GetComponent<Collider2D>().enabled = false;
            AudioSource.PlayClipAtPoint(audioSHit, transform.position, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
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
