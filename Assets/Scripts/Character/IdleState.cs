using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : INinjaStates
{
    

    public void OnAttack(NinjaController ninjaController, EnemyController enemy)
    {
        
    }

    public void OnEnter(NinjaController ninjaController)
    {
        ninjaController.anim.SetBool("isGrounded", true);
        ninjaController.anim.SetFloat("speed", 0f); // Set speed to 0 for idle animation
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ninjaController.ChangeState(ninjaController.jumpState);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            ninjaController.ChangeState(ninjaController.runState);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ninjaController.ChangeState(ninjaController.runState);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ninjaController.ChangeState(ninjaController.attackState);
        }
    }
}
