using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    //----PRIVATE VARIABLES----
    [SerializeField] private Vector2 movementVelocity;
    private Vector2 offset;
    private Material material;
    private Rigidbody2D playerRigidbody;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material; //We get the same material that the SpriteRenderer contains.
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>(); //Find the gameobject "Character" and get its rigidbody2D
    }

    void Update()
    {
        //I want only one tenth of the player's speed on the X axis
        offset = (playerRigidbody.velocity.x * 0.1f) * movementVelocity * Time.deltaTime;
        material.mainTextureOffset += offset; //Gets the main texture of the material and adds the "Vector2 offset" variable for its offset.
    }
}
