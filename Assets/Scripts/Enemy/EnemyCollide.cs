using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollide : MonoBehaviour
{
    private Collider2D collider;
    private EnemyController enemyController;

    [Header("Enemy Settings")]
    public float knockbackForce = 5f; // Set the knockback force

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        enemyController = GetComponent<EnemyController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<NinjaController>() != null)
        {
            NinjaController ninja = collision.gameObject.GetComponent<NinjaController>();

            if (ninja.currentState is HurtState)
            {
                return;
            }

            ninja.TakeDamage(enemyController);
        }
    }
}
