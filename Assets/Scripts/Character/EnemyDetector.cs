using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public EnemyController enemyController; // Reference to the enemy controller
    private NinjaController ninjaController; // Reference to the player attack script

    // Start is called before the first frame update
    void Start()
    {
        ninjaController = GetComponentInParent<NinjaController>(); // Get the NinjaController from the parent object
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyController = collision.gameObject.GetComponent<EnemyController>();
            ninjaController.detectedEnemy = enemyController; // Set the detected enemy in the NinjaController
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyController = null; // Clear the reference when the enemy exits the trigger
            ninjaController.detectedEnemy = null; // Clear the detected enemy in the NinjaController
        }
    }
}
