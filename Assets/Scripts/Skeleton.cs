using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    //----PUBLIC VARIABLES----
    [Header("Variables")]
    public float timeShoots = 3f;
    public float detectRadius = 5f;
    public GameObject projectilePrefab;
    

    [Header("Sounds")]
    public AudioClip coinSoundFX;
    public AudioClip dieSoundFX;
    //public AudioClip magicSoundFX;

    //----PRIVATES VARIABLES----
    private int value = 1;
    private Transform character;
    private bool facingRight = true;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        character = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Shoot());
    }

    void Update()
    {
        bool characterRight = transform.position.x < character.transform.position.x;
        Flip(characterRight);
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, character.position) <= detectRadius)
            {
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                //SoundManager.Instance.PlaySound(magicSoundFX);
            }
            yield return new WaitForSeconds(timeShoots);
        }
    }

    private void Flip(bool characterRight)
    {
        if ((facingRight && !characterRight) || (!facingRight && characterRight))
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {

            StartCoroutine(KillEnemy());
        }
    }

    IEnumerator KillEnemy()
    {
        StopCoroutine(Shoot());
        if (rigidBody != null)
        {

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
}
