using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float runSpeed, jumpForce;

    [SerializeField]
    Transform groundDetector;
    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    GameObject punchCollider;

    [SerializeField]
    Animator anim;
    Rigidbody rb;

    bool reverseOrientation = false;
    bool disableAction = false;
    bool punch = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        punchCollider.SetActive(false);
    }

    void Update()
    {
        //  move
        rb.velocity = new Vector3(0.0f, rb.velocity.y, 0.0f);
        float playerMov = Input.GetAxisRaw("Horizontal");
        if(playerMov > 0)
        {
            reverseOrientation = false;
            rb.velocity += Vector3.forward * runSpeed;
        }
        else if(playerMov < 0)
        {
            reverseOrientation = true;
            rb.velocity += Vector3.back * runSpeed;
        }
        transform.LookAt((reverseOrientation ? Vector3.back : Vector3.forward) * 100000.0f);
        anim.SetFloat("Speed", Mathf.Abs(playerMov));

        
        //  jump
        if(Input.GetButtonDown("Jump"))
        {
            if(CanJump())
            {
                rb.AddForce(Vector3.up * jumpForce);
                anim.SetTrigger("Jump");
            }
        }


        //  punch
        if(Input.GetButtonDown("Punch"))
        {
            if(!disableAction)
            {
                punch = true;
            }
        }
        if(Input.GetButtonUp("Punch"))
        {
            punch = false;
        }
        anim.SetBool("Punch", punch);


        //  global
        anim.SetBool("Grounded", Grounded());
    }

    bool CanJump()
    {
        return Grounded() && !disableAction;
    }

    bool Grounded()
    {
        Collider[] cols = Physics.OverlapSphere(groundDetector.position, 0.2f, groundMask);
        return cols.Length > 0;
    }

    public void Punch()
    {
        punchCollider.SetActive(true);
    }

    public void EndPunch()
    {
        punchCollider.SetActive(false);
    }
}
