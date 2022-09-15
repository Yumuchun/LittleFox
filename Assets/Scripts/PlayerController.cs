using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public float speed;
    public float jumpforce;
    [SerializeField] private Animator anim;
    public LayerMask ground;
    public Collider2D coll;
    public int Cherry;
    public Text CherryNum;
    private bool isHurt;


    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isHurt)
        {
            Movement();
        }
        SwitchAnim();
    }
    void Movement()
    {
        float Horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
        if (Horizontalmove != 0)
        {
            rb.velocity = new Vector2(Horizontalmove * speed, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            anim.SetBool("jumping", true);
        }
    }
    //切换动画效果
    void SwitchAnim()
    {
        anim.SetBool("idle", false);
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling",true);
        }
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);

            }
        }
        else if (isHurt)
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("running",0);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                isHurt = false;
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);

            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("idle", true);
            anim.SetBool("falling", false);
        }
    }
    //收集物品
    private void OnTriggerEnter2D(Collider2D Collision)
    {
        if (Collision.tag == "Collection")
        {
            Destroy(Collision.gameObject);
            Cherry += 1;
            CherryNum.text = Cherry.ToString();
        }
    }
    //击杀怪物
    private void OnCollisionEnter2D(Collision2D Collision)
    {
        if (Collision.gameObject.tag == "Enemies")
        {
            Enemy enemy = Collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("falling"))
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                anim.SetBool("jumping", true);
            }
            else if (transform.position.x < Collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > Collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                isHurt = true;
            }
        }
    }
}
