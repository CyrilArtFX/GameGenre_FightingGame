using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    LayerMask attackMask;

    Rigidbody rb;


    bool playerAtLeft = true;

    int inCombo = 0;
    float stun = 0.0f;
    Vector3 projectionAccumulation = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(player.position.z > transform.position.z)
        {
            playerAtLeft = false;
        }
        else
        {
            playerAtLeft = true;
        }


        if(inCombo > 0)
        {
            stun -= Time.deltaTime;
            if(stun < 0.0f)
            {
                inCombo = 0;
                Vector3 reelProjection = new Vector3(0.0f, projectionAccumulation.y, projectionAccumulation.z * (playerAtLeft ? 1.0f : -1.0f));
                rb.AddForce(reelProjection * 300.0f * rb.mass);
                projectionAccumulation = Vector3.zero;
            }
        }


        if(Input.GetButtonUp("Punch"))
        {
            stun = 0.0f;
        }
    }

    void OnTriggerEnter(Collider hitCol)
    {
        if ((attackMask & (1 << hitCol.gameObject.layer)) == 0) return;

        AttackInfos hitInfos = hitCol.gameObject.GetComponent<AttackInfos>();

        if(inCombo == 0)
        {
            Vector3 projection = hitInfos.firstHitProjection;
            Vector3 reelProjection = new Vector3(0.0f, projection.y, projection.z * (playerAtLeft ? 1.0f : -1.0f));
            rb.AddForce(reelProjection * 300.0f * rb.mass);
        }
        else
        {
            projectionAccumulation += hitInfos.hitProjectionAccumulation / (hitInfos.bypassProjectionDiminution ? 1 : inCombo);
        }

        stun = hitInfos.maxStunTime * (1 - hitInfos.stunTimeDiminution * inCombo);
        inCombo++;
    }
}
