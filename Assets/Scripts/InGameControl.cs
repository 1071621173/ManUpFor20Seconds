using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameControl : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidBody;

    public void Move(InputAction.CallbackContext context) {
        var movement = context.ReadValue<Vector2>();
        rigidBody.velocity = movement * speed;
    }
}
