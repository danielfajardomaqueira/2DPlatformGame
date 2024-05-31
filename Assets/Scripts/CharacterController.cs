using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //----PUBLIC VARIABLES----
    [Header("Variables")]
    public int maxJumps;

    public float velocity;
    public float jumpForce;
    public float hitForceReceived;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;

    static public bool canDash = false;
    static public bool canMove = true;
    static public bool canAttack = true;
    static public bool canJump = true;
    static public bool isDead = false;

    [Header("Layers")]
    public LayerMask groundLayer;

    [Header("Sounds")]
    public AudioClip jumpSoundFX;
    public AudioClip walkSoundFX;
    public AudioClip damageSoundFX;
    public AudioClip attackSoundFX;
    public AudioClip dashingSoundFX;

    [Header("Particles")]
    public ParticleSystem particlesPowerUp;
    public ParticleSystem particlesAttack;
    public ParticleSystem particlesHealth;


    //----PRIVATES VARIABLES----
    private bool rightOrientation = true;
    private bool isDashing;
    private int restJumps;
    private float inputMovement;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private TrailRenderer trailRenderer;
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        restJumps = maxJumps;

        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        trailRenderer = GetComponent<TrailRenderer>();

        Cursor.lockState = CursorLockMode.Locked;

        Respawn();
    }

    void Update()
    {
        if (isDashing) // Check that it is executed once and does not do any more checks within the Update() method
        {

            return;
        }

        if (isDead == true) //Check that it is executed once and does not do any more checks within the Update() method

        {

            return;
        }

        Attack();
        Movement();
        FlipCharacterSprite(inputMovement);
        Jumping();
        CharacterDash();
        
    }

    void FixedUpdate()
    {
        if (isDashing)
        {

            return;
        }

        if (isDead == true) //Check that it is executed once and does not do any more checks within the Update() method
        {

            return;
        }
    }


    #region CHARACTER MOVEMENT
    private bool IsGrounded()
    {                                          //Limit of the collider, its center, size limits of the collider, angle of the box (0f, no angle), direction of the box towards the ground (down), distance of the box (0.2f), layer mask with which to collide
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center -Vector3.up * 0.2f , new Vector2(boxCollider.bounds.size.x -0.2f, boxCollider.bounds.size.y -0.2f), 0f, Vector2.down, 0.2f, groundLayer);
        return raycastHit.collider != null;
    }

    private void Jumping()
    {
        if (canJump)
        {
            if (IsGrounded())
            {

                restJumps = maxJumps; //The 2 variables are equalized when touching the ground, to restart the jump "counter"
            }

            if (Input.GetKeyDown(KeyCode.Space) && restJumps > 0)
            {

                restJumps--;
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);  //When jumping, the character stops its speed in the Y axis. In this way, gravity when falling does not cancel the jump.
                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //ForceMode2D.Impulse, adds an instantaneous impulse to the rigidbody2D depending on the mass of the object.This allows for a jump to occur instantly.
                SoundManager.Instance.PlaySound(jumpSoundFX);
            }
        }
        

        //Jumping Animations
        if(Input.GetKeyDown(KeyCode.Space))
        {

            animator.SetBool("isJumpping", true);
        }
        
        if (!Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {

            animator.SetBool("isJumpping", false);
        }
    }

    //Movement Logic.
    private void Movement()
    {
        if (canMove)
        {

            inputMovement = Input.GetAxis("Horizontal");
            rigidBody.velocity = new Vector2(inputMovement * velocity, rigidBody.velocity.y);
        }
        
        //Movement Animation
        if (inputMovement != 0f)
        {

            animator.SetBool("isRunning", true);
        }
        else
        {

            animator.SetBool("isRunning", false);
        }
    }

    private void FlipCharacterSprite(float inputMovement)
    {
        if((rightOrientation == true && inputMovement < 0) || (rightOrientation == false && inputMovement > 0))
        {

            rightOrientation = !rightOrientation;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    #endregion

    #region APPLY HIT
    public void ApplyHit()
    {
        canMove = false;
        canAttack = false;
        CameraShake.Instance.ShakeCamera(5, 5, 0.5f);

        Vector2 hitDirection;
        if(transform.localScale.x > 0)
        {
            hitDirection = new Vector2(-1, 1);
        }
        else
        {
            hitDirection = new Vector2(1, 1);
        }
        rigidBody.AddForce(hitDirection * hitForceReceived);

        StartCoroutine(WaitAndActivateMovement());
        SoundManager.Instance.PlaySound(damageSoundFX);
    }

    IEnumerator WaitAndActivateMovement()
    {
        // Wait before checking if it is on the ground

        yield return new WaitForSeconds(0.1f);
        while(!IsGrounded()) //If IsGrounded is equal to False...
        {
            // Wait for the next frame.

            yield return null; //I just want it to wait one frame
        }

        //When it hits the ground we make...
        canMove = true;
        canAttack = true;
    }
    #endregion

    #region ATTACK
    public void Attack()
    {
        if(Input.GetButtonDown("Fire1") && IsGrounded() && canAttack == true)
        {

            StartCoroutine(AttackCooldown());
            animator.SetBool("isAttacking", true);
            SoundManager.Instance.PlaySound(attackSoundFX);
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = true;
        
        yield return new WaitForSeconds(0.1f);
        canMove = false;
        canJump = false;
        rigidBody.bodyType = RigidbodyType2D.Static;
        particlesAttack.Play();

        yield return new WaitForSeconds(0.3f);
        canAttack = false;
        SoundManager.Instance.PlaySound(attackSoundFX);
        animator.SetBool("isAttacking", false);

        yield return new WaitForSeconds(0.3f);
        canAttack = true;
        canMove = true;
        canJump = true;
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
    }
    #endregion

    #region CHARACTER DASH
    private void CharacterDash()
    {
        if (Input.GetButtonDown("Fire2") && canDash == true && PauseUI.isPause == false)
        {

            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        float originalGravity = rigidBody.gravityScale;
        canDash = false;
        isDashing = true;
        
        rigidBody.gravityScale = 0f;
        rigidBody.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        trailRenderer.emitting = true;

        SoundManager.Instance.PlaySound(dashingSoundFX);

        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        rigidBody.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    #endregion 

    public void Respawn()
    {

        transform.position = Vector2.zero;
    }

    public void Death()
    {
        if (isDead == true)
        {
            canDash = false;
            canMove = false;
            canAttack = false;
            canJump = false;

            spriteRenderer.color = Color.gray;
            animator.SetBool("isDead", true);
        }
        
    }
}
