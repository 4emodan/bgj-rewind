using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
    public Color color = Color.yellow;
    public bool disabled = false;

    private void OnDrawGizmos()
    {
        if (!disabled)
        {
            Gizmos.color = color;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
    }
}
