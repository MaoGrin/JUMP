using System;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public PlayerModel model;
    public Rigidbody2D rb2d;
    public BoxCollider2D feet;
    private Animator anim;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        feet = GetComponent<BoxCollider2D>();
    }
    void Update()
    {

    }

    public void SetModel(PlayerModel model)
    {
        this.model = model;
    }

    public void SetAnimBool(string name, bool val)
    {
        if (anim == null) return;
        anim.SetBool(name, val);
    }

    public void SetAnimTrigger(string name)
    {
        if (anim == null) return;
        anim.SetTrigger(name);
    }   

    public bool GetAnimBool(string name)
    {
        if (anim == null)
        {
            return false;
        }
        return anim.GetBool(name);
    }


}