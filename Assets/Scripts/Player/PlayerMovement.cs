
using System;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    
    private bool canMove = false;
    
    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;
    
    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Wall Jumping")] 
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;
    
    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;



    private void Awake(){
       
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    private void Start()
    {
        animator.SetTrigger("SpawnTrigger");
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    private void Update()
    {
        if (!canMove)
        {
            return;
        }
        horizontalInput = Input.GetAxis("Horizontal");
        
        //flip player when moving left-right

        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(5,5,5);
        }
        
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-5, 5, 5);
        }
        
        
        //set animator parameters
        
        animator.SetBool("run",horizontalInput != 0);
        animator.SetBool("grounded",isGrounded());
        
        //jump
        if (canMove && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("jump");
            Jump();
        }
        
        //adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);
        }

        if (onWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 7;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (isGrounded())
            {
                coyoteCounter = coyoteTime; //reset when on ground
                jumpCounter = extraJumps;
            }
            else
            {
                coyoteCounter -= Time.deltaTime;
            }
        }
        
    }

    private void Jump()
    {
        if (coyoteCounter < 0 && !onWall() && jumpCounter <= 0)
        {
            return;
        }
        SoundManager.instance.PlaySound(jumpSound);

        if (onWall())
        {
            wallJump();
        }
        else
        {
            if (isGrounded())
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            }
            else
            {
                if (coyoteCounter > 0)
                {
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                }
                else
                {
                    if (jumpCounter > 0)
                    {
                        body.velocity = new Vector2(body.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }

            coyoteCounter = 0;
        }
        
        
        
    }

    private void wallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,groundLayer);
        return raycastHit.collider != null;
    }
    
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,new Vector2(transform.localScale.x,0),0.1f,wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
