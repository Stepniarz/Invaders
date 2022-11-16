using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;
    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;
    Shooter shooter;

    void Awake() 
    {
        shooter = GetComponent<Shooter>();
    }
    void Start() 
    {
        InitializationBounds();
    }
    void Update()
    {
        Move();
    }

    void InitializationBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0)); //bottom left
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1)); //top right
    }

    void Move()
    {
        Vector2 playerPosition = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPosition = new Vector2();
        newPosition.x = Mathf.Clamp(transform.position.x + playerPosition.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPosition.y = Mathf.Clamp(transform.position.y + playerPosition.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPosition;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if(shooter != null)
        {
            shooter.isShooting = value.isPressed;
        }
    }
}
