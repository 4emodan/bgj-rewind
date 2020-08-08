using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDeathController : MonoBehaviour
{

    public LayerMask deathLayers;
    public PlayerDashController playerDash;
    public CheckPoints checkPoints;
    private int idx;
    private Vector3 lastCheckPoint;
    void Start()
    {
        idx = 0;
        lastCheckPoint = new Vector3(-25.5f, -15.6f, 0);
    }
    void Update()
    {
        while (idx < checkPoints.points.Length && checkPoints.points[idx].x < transform.position.x)
        {
            lastCheckPoint = checkPoints.points[idx];
            idx++;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((deathLayers & 1 << collision.collider.gameObject.layer) > 0 && !playerDash.locked)
        {
            death();
        }
    }

    void death()
    {
        transform.position = lastCheckPoint;
        // int scene = SceneManager.GetActiveScene().buildIndex;
        // SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
