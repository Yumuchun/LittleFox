using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : Enemy
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    //private Collider2D Coll;
    public float speed;
    public Transform TopPoint,BottonPoint;
    public float TopY,BottonY;
    private bool isUp = true;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //Coll = GetComponent<Collider2D>();

        TopY = TopPoint.position.y;
        BottonY = BottonPoint.position.y;

        Destroy(TopPoint.gameObject);
        Destroy(BottonPoint.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement(){
        if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x,speed);
            if (transform.position.y> TopY)
            {
                isUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x,-speed);
            if (transform.position.y< BottonY)
            {
                isUp = true;
            }
        }
    }
}
