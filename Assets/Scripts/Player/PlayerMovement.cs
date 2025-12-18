
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
    
    [Header("Dash Info")]
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    private bool isDashing;
    private float dashTimer;
    
    
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    
    private bool canMove = false;
    
    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;
    
   
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
        
        if (isDashing)
        {
            return; 
        }

        
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer <= 0 && GameManager.instance.IsSkillUnlocked("Dash"))
        {
            StartCoroutine(Dash());
        }

        dashTimer -= Time.deltaTime;
        
        horizontalInput = Input.GetAxisRaw("Horizontal");
        
        //flip player when moving left-right

        /*
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(5,5,5);
        }
        
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-5, 5, 5);
        }
        */
        float currentScaleX = Mathf.Abs(transform.localScale.x);
        float currentScaleY = transform.localScale.y;
        float currentScaleZ = transform.localScale.z;

        if (horizontalInput > 0.01f)
        {
            
            transform.localScale = new Vector3(currentScaleX, currentScaleY, currentScaleZ);
        }
        else if (horizontalInput < -0.01f)
        {
            
            transform.localScale = new Vector3(-currentScaleX, currentScaleY, currentScaleZ);
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
        
        bool canWallJump = GameManager.instance.IsSkillUnlocked("WallJump");

        if (canWallJump && onWall())
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
                if (body.velocity.y <= 0.1f)
                {
                    coyoteCounter = coyoteTime; //reset when on ground
                    jumpCounter = GameManager.instance.IsSkillUnlocked("DoubleJump") ? 1 : 0;
                }
                
            }
            else
            {
                coyoteCounter -= Time.deltaTime;
            }
        }
        
    }
    
    private IEnumerator Dash()
    {
        isDashing = true;
        
        
        float originalGravity = body.gravityScale;
        body.gravityScale = 0;

      
        float direction = Mathf.Sign(transform.localScale.x);
        body.velocity = new Vector2(direction * dashSpeed, 0);

        
        animator.SetTrigger("dash"); 

        yield return new WaitForSeconds(dashDuration);

        
        body.gravityScale = originalGravity;
        isDashing = false;
        dashTimer = dashCooldown;
    }

    private void Jump()
    {
        bool canWallJump = GameManager.instance.IsSkillUnlocked("WallJump") && onWall();
        
        if (coyoteCounter < 0 && !canWallJump && jumpCounter <= 0)
        {
            return;
        }
        SoundManager.instance.PlaySound(jumpSound);

        if (canWallJump)
        {
            wallJump();
        }
        else
        {
            if (isGrounded())
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                coyoteCounter = 0;
            }
            else
            {
                if (coyoteCounter > 0)
                {
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                    coyoteCounter = 0;
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
