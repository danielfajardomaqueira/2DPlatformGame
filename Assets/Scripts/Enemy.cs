using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //----PUBLIC VARIABLES----
    [Header("Variables")]
    public float cooldownAttack;
    public float velocity;

    [Header("Sounds")]
    public AudioClip coinSoundFX;
    public AudioClip dieSoundFX;

    //----PRIVATES VARIABLES----
    private bool canAttack = true;
    private bool canMove = true;
    private SpriteRenderer spriteRenderer;
    private int value = 1;
    [SerializeField]private Transform groundController;
    [SerializeField] private float distance;
    [SerializeField] private bool rightDirection;
    private Rigidbody2D rigidBody;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        EnemyMove();
    }

    #region ENEMY MOVE
    private void EnemyMove()
    {
        if (canMove == true)
        {
            RaycastHit2D groundCheck = Physics2D.Raycast(groundController.position, Vector2.down, distance);
            rigidBody.velocity = new Vector2(velocity, rigidBody.velocity.y);

            if (groundCheck == false)
            {
                FlipEnemy();
            }
        }
        
    }

    private void FlipEnemy()
    {
        rightDirection = !rightDirection;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocity = velocity * -1;
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundController.transform.position, groundController.transform.position + Vector3.down);
    }*/
    #endregion

    #region COLLISIONS
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(canAttack == false)
            {
                return;
            }
            canAttack = false;

            // Cambia el Canal Alfa del sprite cuando el jugador colisiona.
            Color color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;

            GameManager.Instance.LoseLive();

            other.gameObject.GetComponent<CharacterController>().ApplyHit();
            Invoke("ReactivateAttack", cooldownAttack);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {

            StartCoroutine(KillEnemy());
        }
    }
    #endregion

    #region REACTIVATE ATTACK
    public void ReactivateAttack()
    {
        canAttack = true;

        //Cambia el Canal Alfa cuando pasan los "segundos" del valor de la Variable cooldownAttack
        Color colour = spriteRenderer.color;
        colour.a = 1f;
        spriteRenderer.color = colour;
    }

    IEnumerator KillEnemy()
    {
        canMove = false;
        if (rigidBody != null)
        {
            rigidBody.velocity = Vector2.zero;
            rigidBody.bodyType = RigidbodyType2D.Static; // Este comportamiento impide que reaccione a la gravedad o fuerzas aplicadas.
        }

        yield return new WaitForSeconds(0.1f);
        GetComponent<Collider2D>().enabled = false;
        spriteRenderer.color = Color.gray;

        yield return new WaitForSeconds(0.1f);
        SoundManager.Instance.PlaySound(dieSoundFX);

        yield return new WaitForSeconds(0.3f);
        GameManager.Instance.ScorePoints(value);
        SoundManager.Instance.PlaySound(coinSoundFX);

        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
    #endregion 
}
