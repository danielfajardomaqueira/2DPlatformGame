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
    //public SoundManager soundManager;

    [Header("Particles")]
    public ParticleSystem particlesPowerUp;
    public ParticleSystem particlesAttack;
    public ParticleSystem particlesHealth;


    //----PRIVATES VARIABLES----
    private bool rightOrientation = true;
    private bool isDashing;
    //[SerializeField] private bool imDamaged = false;
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
        if (isDashing) // Comprueba que se ejecute una vez y no haga mas comprobaciones dentro del metodo Update()
        {

            return;
        }

        if (isDead == true) // Comprueba que se ejecute una vez y no haga mas comprobaciones dentro del metodo Update()
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

        if (isDead == true) // Comprueba que se ejecute una vez y no haga mas comprobaciones dentro del metodo Update()
        {

            return;
        }
    }


    #region CHARACTER MOVEMENT
    private bool IsGrounded()
    {                                          //Limite del colider su centro, limites de tamaño del collider, angulo del box(0f, nada de angulo), direccion del box hacia el suelo(down), distancia del box(0.2f), mascara de capa con la cual colisionar
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center -Vector3.up * 0.2f , new Vector2(boxCollider.bounds.size.x -0.2f, boxCollider.bounds.size.y -0.2f), 0f, Vector2.down, 0.2f, groundLayer);
        return raycastHit.collider != null;
    }

    private void Jumping()
    {
        if (canJump)
        {
            if (IsGrounded())
            {

                restJumps = maxJumps; //Se igualan las 2 variables al tocar el suelo, para reiniciar el "contador" de saltos.
            }

            if (Input.GetKeyDown(KeyCode.Space) && restJumps > 0)
            {

                restJumps--;
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);  // Al dar un salto, el personaje para su velocidad en ejeY. De esta manera, la gravedad al caer no anula el salto.
                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // ForceMode2D.Impulse, añade un impulso instantaneo al rigidbody2D dependiendo de la masa del objeto. Esto permite un salto que ocurre al instante.
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
        /*
        if(canMove == false)
        {
            return;
        }
        
        if(imDamaged == true)
        {
            return;
        }
        */
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
        //imDamaged = true;
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

        //Damaged Animation
       /* if (imDamaged == true)
        {
            animator.SetBool("isDamaged", true);
        }
        else
        {
            animator.SetBool("isDamaged", false);
        }*/
    }

    IEnumerator WaitAndActivateMovement()
    {
        // Wait before checking if it is on the ground

        yield return new WaitForSeconds(0.1f);
        while(!IsGrounded()) // Si IsGrounded es igual a False...
        {
            // Wait for the next frame.

            yield return null; //Solo quiero que espere un frame
        }

        // Cuando toca suelo hacemos que...
        canMove = true;
        canAttack = true;
        //imDamaged = false;
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
            //Debug.Log("Attacking");
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
        if (Input.GetButtonDown("Fire2") && canDash == true)
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
