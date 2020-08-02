using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public float speed = 10;
    public float jumpHeight = 10;

    private bool jump = false;
    private float delta = 0f;
    void Update()
    {
        delta = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        rigidbody.AddForce(new Vector2(delta * speed * rigidbody.mass, 0));
        if (jump)
        {
            jump = false;
            rigidbody.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }
    }
}
