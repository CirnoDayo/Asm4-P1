using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Akino_PlayerMovement : MonoBehaviour
{
    [Header("Player Properties")]
    Rigidbody2D rb;
    Vector2 input;
    public int movementSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Debug.Log(input);
        Vector3 move = new Vector2(input.x, input.y) * Time.deltaTime * movementSpeed;
        transform.position += move;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
}
