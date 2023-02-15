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

    private Animator playerAnimator;
    private float lastXJoystick = 0;
    private float lastYJoystick = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, input.y, 0);

        controller.Move(move * Time.deltaTime * playerSpeed);
        if (image != null)
        {
            if (input.x > 0.1f || input.x < -0.1f || input.y > 0.1f || input.y < -0.1f)
            {
                Debug.LogError(input);
                image.CrossFadeAlpha(255, 1.0f, false);
                //Save the last pos of the player before reaching null point
                lastXJoystick = input.x;
                lastYJoystick = input.y;
                
            }
            else
            {
                image.CrossFadeAlpha(0, 0.0f, false);
                //Save the value 
                if (playerAnimator)
                {
                    //playerAnimator.SetFloat("MoveX",lastXJoystick);
                    //playerAnimator.SetFloat("MoveY",lastYJoystick);
                }
            }
            
            if (playerAnimator)
            {
                playerAnimator.SetFloat("MoveX",input.x);
                playerAnimator.SetFloat("MoveY",input.y);
            }
        }
    }
}
