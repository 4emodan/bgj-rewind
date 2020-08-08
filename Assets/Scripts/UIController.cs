using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    public bool play;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (GetComponent<Collider2D>().OverlapPoint(mousePosition))
            {
                if (play)
                {
                    SceneManager.LoadScene("IntroLevel");
                }
                else
                {
                    SceneManager.LoadScene("Controls");
                }
            }
        }
    }
}
