using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : INinjaStates
{
    public void OnAttack(NinjaController ninjaController, EnemyController enemy)
    {
        throw new System.NotImplementedException();
    }

    public void OnEnter(NinjaController ninjaController)
    {
        ninjaController.anim.SetTrigger("attack"); // Trigger attack animation

        if (ninjaController.detectedEnemy != null)
        {
            // If an enemy is detected, deal damage to the enemy
            ninjaController.detectedEnemy.health--;
            if (ninjaController.detectedEnemy.health <= 0)
            {
                ninjaController.detectedEnemy.Die(); // Call the Die method on the enemy
            }
        }
    }

    public void OnExit(NinjaController ninjaController)
    {
        
    }

    public void OnHurt(NinjaController ninjaController, EnemyController enemy)
    {
        if (ninjaController.health <= 0)
        {
            ninjaController.ChangeState(ninjaController.dieState);
            return;
        }

        ninjaController.ChangeState(ninjaController.hurtState);
    }

    public void OnUpdate(NinjaController ninjaController)
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            ninjaController.ChangeState(ninjaController.runState); // Change to run state if run keys are pressed
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ninjaController.ChangeState(ninjaController.attackState); // Stay in attack state if attack key is pressed again
        }
        else
        {
            ninjaController.ChangeState(ninjaController.idleState); // Change to idle state if no action is taken
        }
    }
}
