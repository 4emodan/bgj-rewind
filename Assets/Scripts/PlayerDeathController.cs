using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathController : MonoBehaviour
{

    public LayerMask deathLayers;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((deathLayers & 1 << collision.collider.gameObject.layer) > 0)
        {
            death();
        }
    }

    void death()
    {
        Debug.Log("Death");
    }
}
