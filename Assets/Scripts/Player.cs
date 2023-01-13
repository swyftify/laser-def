using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 rawInput;

    [SerializeField]
    float moveSpeed = 5f;

    Vector2 minBounds,
        maxBounds;

    [SerializeField]
    float paddingLeft,
        paddingRight,
        paddingTop,
        paddingBottom;

    Shooter shooter;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        InitBounds();
    }

    void Update()
    {
        MovePlayer();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;

        // Translate viewport coordinates into world coordinates to set the boundaries for the player
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void MovePlayer()
    {
        // Add raw input value from key press to the position of the object thus offsetting it.
        // RawInput = (0.0, 0.0). When move is invoked -> W -> (1.0, 0.0) and that is continuously applied to the position.
        // Time.deltaTime -> makes things framerate independent
        Vector2 deltaPosition = rawInput * moveSpeed * Time.deltaTime;

        Vector2 newPosition = new Vector2();
        newPosition.x = Mathf.Clamp(
            transform.position.x + deltaPosition.x,
            minBounds.x + paddingLeft,
            maxBounds.x - paddingRight
        );
        newPosition.y = Mathf.Clamp(
            transform.position.y + deltaPosition.y,
            minBounds.y + paddingBottom,
            maxBounds.y - paddingTop
        );

        transform.position = newPosition;
    }

    void OnMove(InputValue input)
    {
        rawInput = input.Get<Vector2>();
    }

    void OnFire(InputValue input)
    {
        if (shooter != null)
        {
            shooter.isShooting = input.isPressed;
        }
    }
}
