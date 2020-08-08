using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIDeathController : MonoBehaviour
{
    public LayerMask deathLayers;
    public AIController aIController;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if ((deathLayers & 1 << collider.gameObject.layer) > 0)
        {
            aIController.Death();
        }
    }
}
