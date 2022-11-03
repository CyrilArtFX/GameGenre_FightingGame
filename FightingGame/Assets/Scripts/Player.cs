using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    [SerializeField]
    float runSpeed, jumpForce, dashLength;

    [SerializeField]
    float stompCooldown, dashCooldown;

    [SerializeField]
    Transform groundDetector;
    [SerializeField]
    LayerMask groundMask, obstacleMask;

    [SerializeField]
    GameObject punchCollider, stompCollider;

    [SerializeField]
    Animator anim;
    Rigidbody rb;
    CapsuleCollider cc;

    bool reverseOrientation = false;
    bool disableAction = false;
    bool punch = false;

    float stompCD = 0.0f;
    float dashCD = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        punchCollider.SetActive(false);
        stompCollider.SetActive(false);
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


        //  stomp
        if(stompCD > 0.0f)
        {
            stompCD -= Time.deltaTime;
        }
        if(Input.GetButtonDown("Stomp"))
        {
            if(CanStomp())
            {
                disableAction = true;
                stompCD = stompCooldown;
                anim.SetTrigger("Stomp");
            }
        }


        //  dash
        if(dashCD > 0.0f)
        {
            dashCD -= Time.deltaTime;
        }
        if(Input.GetButtonDown("Dash"))
        {
            if(CanDash())
            {
                dashCD = dashCooldown;
                Dash();
            }
        }
            

        //  global
        anim.SetBool("Grounded", Grounded());
    }


    bool CanJump()
    {
        return Grounded() && !disableAction;
    }

    bool CanStomp()
    {
        return Grounded() && !disableAction && stompCD <= 0.0f;
    }

    bool CanDash()
    {
        return Grounded() && !disableAction && dashCD <= 0.0f;
    }

    bool Grounded()
    {
        Collider[] cols = Physics.OverlapSphere(groundDetector.position, 0.2f, groundMask);
        return cols.Length > 0;
    }


    void Dash()
    {
        rb.AddForce(transform.forward * dashLength * 3000.0f);
    }

    public void Punch()
    {
        punchCollider.SetActive(true);
    }

    public void Stomp()
    {
        stompCollider.SetActive(true);
    }


    public void ResetAttacks()
    {
        punchCollider.SetActive(false);
        stompCollider.SetActive(false);
        disableAction = false;
    }
}
