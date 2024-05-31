using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //----PUBLIC VARIABLES----
    public float projectileSpeed;
    public AudioClip magicSoundFX;


    //----PRIVATE VARIABLES----
    private Transform character;
    private Rigidbody2D rigidBody;

    void Start()
    {
        character = FindObjectOfType<CharacterController>().transform;
        rigidBody = GetComponent<Rigidbody2D>();
        
        LaunchProjectile();
    }

    private void LaunchProjectile()
    {
        Vector2 directionToCharacter = (character.position - transform.position).normalized; //returns vector of magnitude 1
        rigidBody.velocity = directionToCharacter * projectileSpeed;

        StartCoroutine(DestroyProjectile());
    }

    IEnumerator DestroyProjectile()
    {
        SoundManager.Instance.PlaySound(magicSoundFX);

        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.LoseLive();
            other.gameObject.GetComponent<CharacterController>().ApplyHit();
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {

            Destroy(gameObject);
        }
    }
}
