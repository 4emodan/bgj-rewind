using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform gounded1;
    public Transform gounded2;
    public Transform obstacleCheker;

    public LayerMask obstacles;

    public Material material;
    public AILine line = null;
    public bool moveRight = true;
    public PlayerDetector playerDetecor;
    public float defaultSpeed = 10;
    public float chaseSpeed = 15;
    private Rigidbody2D rb;
    private Vector3 m_Velocity = Vector3.zero;
    private bool m_FacingRight = true;
    private Transform target = null;
    private FlagCarier detected = null;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

    class FlagCarier
    {
        public bool flag;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!moveRight)
        {
            Flip();
        }
    }
    private IEnumerator LooseFocus()
    {
        if (detected != null)
        {
            detected.flag = false;
        }
        var cur_detected = new FlagCarier();
        cur_detected.flag = true;
        detected = cur_detected;
        yield return new WaitForSeconds(3f);
        if (cur_detected.flag)
        {
            detected = null;
            target = null;
            Turn();
        }
    }

    void LateUpdate()
    {
        playerDetecor.transform.position = transform.position;

        if (playerDetecor.isPlayerDetected())
        {
            target = playerDetecor.player.transform;
            StartCoroutine(LooseFocus());
        }
        if (target != null)
        {
            chase();
        }
        else
        {
            patrol();
        }

    }

    void chase()
    {
        float moveDelta = defaultSpeed / 10;
        if (transform.position.x < target.position.x)
        {
            Move(moveDelta);
        }
        else
        {
            Move(-moveDelta);
        }
    }

    void patrol()
    {
        RaycastHit2D hit;
        if (m_FacingRight)
        {
            hit = Physics2D.Raycast(obstacleCheker.position, transform.right, 0.1f, obstacles);
            Debug.DrawRay(obstacleCheker.position, transform.right * 0.1f);
        }
        else
        {
            hit = Physics2D.Raycast(obstacleCheker.position, -transform.right, 0.1f, obstacles);
            Debug.DrawRay(obstacleCheker.position, -transform.right * 0.1f);

        }
        if (hit.collider != null)
        {
            Debug.Log(hit.collider);
            Turn();
            return;
        }
        float left, right;
        if (line.p0.x < line.p1.x)
        {
            left = line.p0.x;
            right = line.p1.x;
        }
        else
        {
            left = line.p1.x;
            right = line.p0.x;
        }

        left += line.transform.position.x;
        right += line.transform.position.x;
        float moveDelta = defaultSpeed / 10;
        if (moveRight)
        {
            if (transform.position.x > right)
            {
                moveRight = false;
                return;
            }
            Move(moveDelta);
        }
        else
        {
            if (transform.position.x < left)
            {
                moveRight = true;
                return;
            }
            Move(-moveDelta);
        }
    }

    void Move(float move)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        if (move > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (move < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        playerDetecor.Flip();
        transform.localScale = theScale;
    }

    public void Turn()
    {
        Flip();
        moveRight = !moveRight;
    }
}
