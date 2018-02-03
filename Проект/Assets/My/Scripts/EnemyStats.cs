using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

    public int HP;
    public int maxHP;
    public int armor;
    public int forwardSpeed;
    public int backSpeed;
    public int jumpForce;

    private void Update()
    {
    }

    void DeathTest()
    {
        if (HP <= 0)
        {
            //надо еще прописать Instantiate мясцо какое нить

            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        DeathTest();
    }
}
