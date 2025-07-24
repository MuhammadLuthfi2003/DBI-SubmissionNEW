using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class HurtState : INinjaStates
{
    private float hurtTime = 0;
    private float cooldown = 1.5f; // Time to wait before allowing another hurt
    private bool hasPlayedJumpAnimation = false;

    public void OnAttack(NinjaController ninjaController, EnemyController enemy)
    {
 
    }

    public void OnEnter(NinjaController ninjaController)
    {
        ninjaController.health -= 1;
        ninjaController.anim.SetTrigger("hurt"); // Trigger the hurt animation

        if (ninjaController.health <= 0)
        {
            ninjaController.ChangeState(ninjaController.dieState);
        }
    }

    public void OnExit(NinjaController ninjaController)
    {
        hurtTime = 0; // Reset hurt time
        hasPlayedJumpAnimation = false; // Reset jump animation flag
    }

    public void OnHurt(NinjaController ninjaController, EnemyController enemy)
    {

    }

    public void OnUpdate(NinjaController ninjaController)
    {
        hurtTime += Time.deltaTime;
        
        if (hurtTime >= cooldown - 1 && !hasPlayedJumpAnimation) // trigger jump animation before cooldown ends
        {
            if (!ninjaController.anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                ninjaController.anim.SetTrigger("jump");
                hasPlayedJumpAnimation = true; 
            }
        }

        if (hurtTime >= cooldown)
        {

            if (Mathf.Abs(ninjaController.rb.velocity.y) < 0.01f && Mathf.Abs(ninjaController.rb.velocity.x) < 0.01f)
            {
                ninjaController.ChangeState(ninjaController.idleState); // Change to idle state if not jumping

            }
            else if (Mathf.Abs(ninjaController.rb.velocity.y) < 0.01f && Mathf.Abs(ninjaController.rb.velocity.x) > 0.01f)
            {
                ChangeToRunState(ninjaController); // check if isgrounded and button is pressed
            }
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
