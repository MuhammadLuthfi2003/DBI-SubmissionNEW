using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface INinjaStates
{
    void OnEnter(NinjaController ninjaController);
    void OnExit(NinjaController ninjaController);
    void OnUpdate(NinjaController ninjaController);
    void OnHurt(NinjaController ninjaController, EnemyController enemy);
    void OnAttack(NinjaController ninjaController, EnemyController enemy);
}


public class NinjaController : MonoBehaviour
{
    // state machine
    public INinjaStates currentState;

    public HurtState hurtState = new HurtState();
    public IdleState idleState = new IdleState();
    public AttackState attackState = new AttackState();
    public DieState dieState = new DieState();
    public RunState runState = new RunState();
    public JumpState jumpState = new JumpState();

    //components
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    // player stats
    [Header("Player Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int health = 3;
    public bool isFacingLeft = false;

    [Header("UI Settings")]
    public TMPro.TextMeshProUGUI reloadText;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        ChangeState(idleState);

        reloadText.enabled = false; // Hide reload text at the start
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate(this);

        //reload scene when player press R
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }

    public void ChangeState(INinjaStates newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        currentState.OnEnter(this);
    }

    public void Move(float direction)
    {
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y); // Move left or right
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x)); // Set speed for animation
    }

    public void TakeDamage(EnemyController enemy)
    {
        if (health <= 0)
        {
            ChangeState(dieState);
            return;
        }

        if (currentState != null)
        {
            // add random knockback force to the player
            Vector2 knockbackDirection = (transform.position - enemy.transform.position).normalized; 
            rb.AddForce(knockbackDirection * 5f, ForceMode2D.Impulse); // Apply knockback force

            currentState.OnHurt(this, enemy); 
        }
    }
}
