using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public int HP;
    public int maxHP;
    public int armor;
    public int forwardSpeed;
    public int backSpeed;
    public float jumpForce;


    //-----
    public InterfaceHealth InterfaceHealth;
    //-----
    
    // Use this for initialization
    void Awake()
    {
        
    }

    void LateUpdate()
    {
        DeathTest();
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

        InterfaceHealth.currentHealth.width = Procent(maxHP, HP)*2;
        print(HP);
        print(InterfaceHealth.currentHealth.width);
    }

    float Procent(float max, float current)
    {
        float res = max / 100f; //1 procent
        res = current / res; //iskomoe
        return res;
    }
}
