using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    protected Animator Anim;
    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
    }
    public void Death(){
        Destroy(gameObject);
    }
    public void JumpOn(){
        Anim.SetTrigger("death");
    }
    
}
