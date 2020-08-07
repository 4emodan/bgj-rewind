using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashController : MonoBehaviour
{
    public Animator animator;
    public float dashTime = 0.25f;
    public float dashForce = 100f;
    public float cooldown = 1f;

    private float dashTimeRemaining = 0f;
    private float cooldownRemaining = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && dashTimeRemaining == 0f && cooldownRemaining == 0f)
        {
            dashTimeRemaining = dashTime;
            animator.SetBool("IsDashing", true);

            var direction = Mathf.Sign(transform.localScale.x);

            GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * dashForce, 0f), ForceMode2D.Impulse);
        }
        else if (dashTimeRemaining > 0f)
        {
            dashTimeRemaining -= Time.deltaTime;

            if (dashTimeRemaining <= 0f)
            {
                cooldownRemaining = cooldown;
                dashTimeRemaining = 0f;
                animator.SetBool("IsDashing", false);
            }
        }
        else if (cooldownRemaining > 0f)
        {
            cooldownRemaining -= Time.deltaTime;
            if (cooldownRemaining <= 0f) {
                cooldownRemaining = 0f;
            }
        }
    }
}
