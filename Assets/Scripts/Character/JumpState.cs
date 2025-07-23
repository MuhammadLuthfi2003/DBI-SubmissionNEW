using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : INinjaStates
{
    public void OnAttack(NinjaController ninjaController, EnemyController enemy)
    {
        
    }

    public void OnEnter(NinjaController ninjaController)
    {
        ninjaController.rb.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
        ninjaController.anim.SetTrigger("jump"); // Trigger jump animation
        ninjaController.anim.SetBool("isGrounded", false);
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
        if (Mathf.Abs(ninjaController.rb.velocity.y) < 0.01f && Mathf.Abs(ninjaController.rb.velocity.x) < 0.01f)
        {
            ninjaController.ChangeState(ninjaController.idleState); // Change to idle state if not jumping
        }
        else if (Mathf.Abs(ninjaController.rb.velocity.y) < 0.01f && Mathf.Abs(ninjaController.rb.velocity.x) > 0.01f)
        {
            ChangeToRunState(ninjaController); // check if isgrounded and button is pressed
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ninjaController.ChangeState(ninjaController.attackState); // Change to attack state if attack key is pressed while jumping
        }
    }


    void ChangeToRunState(NinjaController ninjaController)
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ninjaController.ChangeState(ninjaController.runState); // Change to run state if moving left while jumping
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            ninjaController.ChangeState(ninjaController.runState); // Change to run state if moving right while jumping
        }
        else
        {
            ninjaController.ChangeState(ninjaController.idleState); // Change to idle state if not moving while jumping
        }
    }
}
