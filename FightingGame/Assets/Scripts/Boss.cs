using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    float runSpeed, jumpForce, dashLength, ultiDuration, ultiAttackSpeedBoost;

    [SerializeField]
    float waveCooldown, dashCooldown, ultiCooldown;

    [SerializeField]
    Transform groundDetector;
    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    GameObject punchCollider, waveCollider;

    [SerializeField]
    Renderer bodyRenderer;
    [SerializeField]
    Material baseMat, ultiMat;

    [SerializeField]
    ParticleSystem waveParticles, ultiParticles, endUltiParticles, dashParticles;

    [SerializeField]
    ActifBar waveBar;
    [SerializeField]
    UltiBar ultiBar;

    [SerializeField]
    Animator anim;
    Rigidbody rb;

    bool reverseOrientation = false;
    bool disableAction = false;
    bool punch = false;
    float ulti = 0.0f;
    bool ultiActivation = false;

    float waveCD = 0.0f;
    float dashCD = 0.0f;
    float ultiCD = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        punchCollider.SetActive(false);
        waveCollider.SetActive(false);
        ultiCD = ultiCooldown;
        anim.SetFloat("AttackSpeedBoost", 1.0f);
    }

    void Update()
    {
        //  ulti activation logic
        if (ultiActivation)
        {
            return;
        }


        //  ulti boost logic
        if (ulti > 0.0f)
        {
            ulti -= Time.deltaTime;
            if (ulti <= 0.0f)
            {
                EndUlti();
            }
        }


        //  move
        rb.velocity = new Vector3(0.0f, rb.velocity.y, 0.0f);
        float playerMov = Input.GetAxisRaw("Horizontal");
        if (playerMov > 0)
        {
            reverseOrientation = false;
            rb.velocity += Vector3.forward * runSpeed;
        }
        else if (playerMov < 0)
        {
            reverseOrientation = true;
            rb.velocity += Vector3.back * runSpeed;
        }
        transform.LookAt((reverseOrientation ? Vector3.back : Vector3.forward) * 100000.0f);
        anim.SetFloat("Speed", Mathf.Abs(playerMov));


        //  jump
        if (Input.GetButtonDown("Jump"))
        {
            if (CanJump())
            {
                rb.AddForce(Vector3.up * jumpForce);
                anim.SetTrigger("Jump");
            }
        }


        //  punch
        if (Input.GetButtonDown("Punch"))
        {
            if (!disableAction)
            {
                punch = true;
            }
        }
        if (Input.GetButtonUp("Punch"))
        {
            punch = false;
        }
        anim.SetBool("Punch", punch);


        //  wave
        /*if (waveCD > 0.0f)
        {
            waveCD -= Time.deltaTime * (ulti > 0.0f ? 2.0f : 1.0f);
        }
        waveBar.SetActifState(1.0f - waveCD / waveCooldown);
        if (Input.GetButtonDown("Stomp"))
        {
            if (CanStomp())
            {
                disableAction = true;
                waveCD = waveCooldown;
                anim.SetTrigger("Stomp");
            }
        }*/


        /*//  dash    //should be replaced by a shield
        if (dashCD > 0.0f)
        {
            dashCD -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Dash"))
        {
            if (CanDash())
            {
                dashCD = dashCooldown;
                Dash();
            }
        }*/


        //  ulti
        /*if (ultiCD > 0.0f)
        {
            ultiCD -= Time.deltaTime;
        }
        ultiBar.SetUltiState(1.0f - ultiCD / ultiCooldown);
        if (Input.GetButtonDown("Ulti"))
        {
            if (CanUlti())
            {
                disableAction = true;
                ultiCD = ultiCooldown;
                anim.SetTrigger("Ulti");
            }
        }*/


        //  global
        anim.SetBool("Grounded", Grounded());
    }


    bool CanJump()
    {
        return Grounded() && !disableAction;
    }

    bool CanStomp()
    {
        return Grounded() && !disableAction && waveCD <= 0.0f;
    }

    bool CanDash()
    {
        return Grounded() && !disableAction && dashCD <= 0.0f;
    }

    bool CanUlti()
    {
        return !disableAction && ultiCD <= 0.0f && ulti <= 0.0f;
    }

    bool Grounded()
    {
        Collider[] cols = Physics.OverlapSphere(groundDetector.position, 0.2f, groundMask);
        return cols.Length > 0;
    }


    void Dash()
    {
        rb.AddForce(transform.forward * dashLength * 3000.0f);
        dashParticles.Play();
    }

    void StartUlti()
    {
        bodyRenderer.material = ultiMat;
        anim.SetFloat("AttackSpeedBoost", ultiAttackSpeedBoost);
    }

    void EndUlti()
    {
        anim.SetFloat("AttackSpeedBoost", 1.0f);
        bodyRenderer.material = baseMat;
        endUltiParticles.Play();
    }


    public void Punch()
    {
        punchCollider.SetActive(true);
    }

    public void Stomp()
    {
        waveCollider.SetActive(true);
        waveParticles.Play();
    }

    public void ActivateUlti()
    {
        punchCollider.SetActive(false);
        waveCollider.SetActive(false);
        ultiActivation = true;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        ultiParticles.Play();
    }

    public void Ulti()
    {
        ultiActivation = false;
        disableAction = false;
        rb.useGravity = true;
        ulti = ultiDuration;
        StartUlti();
    }


    public void ResetAttacks()
    {
        punchCollider.SetActive(false);
        waveCollider.SetActive(false);
        disableAction = false;
    }
}
