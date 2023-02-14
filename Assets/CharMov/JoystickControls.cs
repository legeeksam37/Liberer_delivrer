using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class JoystickControls : MonoBehaviour
{
    PlayerInput playerInput;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private float playerSpeed = 10.0f;

    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, input.y, 0);

        controller.Move(move * Time.deltaTime * playerSpeed);
        if (input.x > 0.2f || input.x < -0.2f || input.y > 0.2f || input.y < -0.2f)
        {
            image.CrossFadeAlpha(255, 1.0f, false);
        }
        else
        {
            image.CrossFadeAlpha(0, 0.0f, false);
        }
        
    }
}
