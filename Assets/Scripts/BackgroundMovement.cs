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
        material = GetComponent<SpriteRenderer>().material; //Obtenemos el mismo material que contiene el SpriteRenderer.
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>(); //Busca el gameobject "Character" y obtiene su rigidbody2D
    }

    // Update is called once per frame
    void Update()
    {
                //Quiero solo una decima parte de la velocidad del jugador en el eje X
        offset = (playerRigidbody.velocity.x * 0.1f) * movementVelocity * Time.deltaTime;
        material.mainTextureOffset += offset; //Obtiene la textura principal del material y se suma la variable "Vector2 offset" para su desplazamiento.
    }
}
