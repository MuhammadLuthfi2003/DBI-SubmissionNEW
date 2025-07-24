using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : INinjaStates
{
    public void OnAttack(NinjaController ninjaController, EnemyController enemy)
    {

    }

    public void OnEnter(NinjaController ninjaController)
    {
        ninjaController.anim.SetTrigger("die"); // Trigger die animation
        ninjaController.reloadText.enabled = true; // Show reload text
        
    }

    public void OnExit(NinjaController ninjaController)
    {

    }

    public void OnHurt(NinjaController ninjaController, EnemyController enemy)
    {

    }

    public void OnUpdate(NinjaController ninjaController)
    {
        //reload scene when player press R
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}
