using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : INinjaStates
{
    public void OnAttack(NinjaController ninjaController, EnemyController enemy)
    {
        
    }

    public void OnEnter(NinjaController ninjaController)
    {
        ninjaController.anim.SetBool("isGrounded", true);
        ninjaController.anim.SetFloat("speed", Mathf.Abs(ninjaController.rb.velocity.x)); // Set speed to 0 for idle animation
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
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ninjaController.Move(-1);
            ninjaController.spriteRenderer.flipX = true; // Flip the sprite to face left
            ninjaController.isFacingLeft = true; // Set facing left state
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            ninjaController.Move(1);
            ninjaController.spriteRenderer.flipX = false; // Flip the sprite to face right
            ninjaController.isFacingLeft = false; // Set facing right state
        }
        else
        {
            ninjaController.Move(0); // Stop moving
            ninjaController.ChangeState(ninjaController.idleState); // Change to idle state if no movement keys are pressed
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ninjaController.ChangeState(ninjaController.jumpState);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ninjaController.ChangeState(ninjaController.attackState); // Change to attack state if attack key is pressed while jumping
        }
    }
}
