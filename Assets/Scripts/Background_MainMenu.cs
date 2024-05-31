using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_MainMenu : MonoBehaviour
{
    [SerializeField] private Vector2 moveVelocity;
    private Vector2 offset;
    private Material material;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        offset = moveVelocity * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
