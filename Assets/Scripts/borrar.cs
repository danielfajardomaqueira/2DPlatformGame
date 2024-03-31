using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class borrar : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float timeShoots = 2f;
    public float detectRadius = 5f;

    private Transform character;
    private bool facingRight = true;

    void Start()
    {
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
}
