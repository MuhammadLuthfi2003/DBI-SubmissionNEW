using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Settings")]
    public int health = 1;
    public float speed = 2f;
    public float walkDistance = 5f; // Distance the enemy will walk before turning around
    public bool isGoingLeft = true; // Direction the enemy is currently facing

    private Vector3 currentPosition;
    private Vector3 leftMostPosition;
    private Vector3 rightMostPosition;

    private Animator anim;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.position;
        leftMostPosition = currentPosition - new Vector3(walkDistance, 0, 0);
        rightMostPosition = currentPosition + new Vector3(walkDistance, 0, 0);

        // get component
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
            return;
        }

        Move();
    }

    public void Die()
    {
        anim.SetTrigger("die"); // Trigger the death animation
        StartCoroutine(DeleteEnemy()); 
    }

    IEnumerator DeleteEnemy()
    {
        yield return new WaitForSeconds(1f); 
        Destroy(gameObject); // Destroy the enemy game object
    }

    void Move()
    {
        if (isGoingLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (transform.position.x <= leftMostPosition.x)
            {
                isGoingLeft = false; // Change direction
                spriteRenderer.flipX = true; 

            }
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (transform.position.x >= rightMostPosition.x)
            {
                isGoingLeft = true; // Change direction
                spriteRenderer.flipX = false;
            }
        }
    }
}
