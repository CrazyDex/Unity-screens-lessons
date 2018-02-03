using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
    
    public float forwardSpeed; // 3 шаг, 5 бег, 7 спринт
    public float backSpeed;
    public float jumpForce;
    public bool facingRight = true; //направление рожи перса
    public bool grounded; //стоит ли на "земле"
    public Collider2D gCheckerCollider2D;
    public Vector3 worldMousePosition;
    public Vector3 targetDir; // 
    public Vector3 normalVector;// = new Vector3(1, 0, 0); //необходим для упрощения вычисления угла
    public float angle;
    public int signDir; //переменная для упрощения кода зависит от направления рожи перса
    public Vector3 dir;
    public Vector2 jumpCheckerDistance;
    public Vector2 jumpForceVector;

    private void Awake()
    {
        jumpForce = GetComponent<Rigidbody2D>().mass / 4 * 1000;
        jumpCheckerDistance.x = 0f;
        jumpCheckerDistance.y = -1.1f;
        jumpForceVector.x = 0f;
        jumpForceVector.y = jumpForce;
    }

    void Update()
    {
        //переводим буль в инт для удобства
        signDir = facingRight ? 1 : -1;
        worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //вычисляем положение мышки относительно перса
        targetDir = worldMousePosition - transform.position;

        //вычисляем угол от перса к мышке и разворачиваем перса в сторону мышки
        normalVector = new Vector3(1, 0, 0);
        angle = Vector2.Angle(normalVector * signDir, targetDir);
        if ((angle > -89.9f && angle < 89.9f) != true)
        {
            Flip();
        }

        Move();
        if (Input.GetButtonDown("Jump")) Jump();
    }

    public float tryjump;
    public LayerMask jumpLayerMask;
    void Jump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, jumpCheckerDistance, tryjump, jumpLayerMask);
        if (hit.collider != null) GetComponent<Rigidbody2D>().AddForce(jumpForceVector);
    }

    //реализация движения
    void Move()
    {
        //если идет по направлению рожи, то норм, если спиной, то замедленно
        dir.x = Input.GetAxis("Horizontal");
        if ((dir.x > 0 && signDir > 0) || (dir.x < 0 && signDir < 0)) transform.position += dir * forwardSpeed * Time.deltaTime;
        else if ((dir.x > 0 && signDir < 0) || (dir.x < 0 && signDir > 0)) transform.position += dir * (backSpeed) * Time.deltaTime;
    }

    //реализация направления рожи перса
    void Flip()
    {
        //смена состояния для прочих эдлементов
        facingRight = !facingRight;

        //берем скейл перса и меняем знак по иксу для разворота перса
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
