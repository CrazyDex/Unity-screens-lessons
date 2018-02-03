using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public float speed; // 3 шаг, 5 бег, 7 спринт
    public float jumpForce = 1000f;
    private bool facingRight = true; // true смотрит вправо, false смотрит влево
    private bool grounded;
    public Collider2D gCheckerCollider2D;

    Vector3 dir; //для управления персом

    // инициализация
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        
        Jump();
    }
    void Jump()
    {
        grounded = Physics2D.IsTouchingLayers(gCheckerCollider2D, LayerMask.GetMask("Ground"));
        if (grounded && Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }
    }
   
    void Move()
    {
        dir.x = Input.GetAxis("Horizontal");
        if (dir.x > 0 && !facingRight) Flip();
        else if (dir.x < 0 && facingRight) Flip();
        if (dir.x != 0) transform.position += dir * speed * Time.fixedDeltaTime;
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
