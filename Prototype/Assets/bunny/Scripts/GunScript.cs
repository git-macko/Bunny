using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Animator animator;
    bool facingRight = true;
    void Update()
    {
        GunAnimation();

        //Movement flip
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x && facingRight)
        {
            flip();
        }
        else if (mousePos.x > transform.position.x && !facingRight)
        {
            flip();
        }
    }

    void GunAnimation()
    {
        if(Input.GetMouseButton(0))
        {
            animator.SetTrigger("Fire");
        }
    }

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(180f, 0f, 0f);
    }
}
