using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class HurtState : INinjaStates
{
    private float hurtTime = 0;
    private float cooldown = 0.5f; // Time to wait before allowing another hurt

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
        
    }

    public void OnHurt(NinjaController ninjaController, EnemyController enemy)
    {

    }

    public void OnUpdate(NinjaController ninjaController)
    {
        hurtTime += Time.deltaTime;
        
        if (hurtTime >= cooldown - 1) // trigger jump animation before cooldown ends
        {
            if (!ninjaController.anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                ninjaController.anim.SetTrigger("jump");
            }
        }

        if (hurtTime >= cooldown)
        {
            hurtTime = 0; // Reset the hurt time
            ninjaController.ChangeState(ninjaController.idleState); // Change to idle state after cooldown
        }
    }
}
