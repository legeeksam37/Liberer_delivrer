using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class JoystickControls : MonoBehaviour
{
    PlayerInput playerInput;
    private Rigidbody2D playerRgbd;
    private Vector3 playerVelocity;
    [SerializeField, Range(.1f, 5f)]
    private float playerSpeed = .5f;

    public Image image;

    private Animator playerAnimator;
    private float lastXJoystick = 0;
    private float lastYJoystick = 0;


    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRgbd = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector2 move = new Vector3(input.x, input.y, 0);

        playerRgbd.MovePosition(playerRgbd.position + playerSpeed * Time.deltaTime * move);
        if (image != null)
        {
            if (input.x > 0.1f || input.x < -0.1f || input.y > 0.1f || input.y < -0.1f)
            {
                image.CrossFadeAlpha(255, 1.0f, false);
                //Save the last pos of the player before reaching null point
                lastXJoystick = input.x;
                lastYJoystick = input.y;

                if (playerAnimator)
                {
                    playerAnimator.SetFloat("MoveX", input.x);
                    playerAnimator.SetFloat("MoveY", input.y);
                }
            }
            else
            {
                image.CrossFadeAlpha(0, 0.0f, false);
                if (playerAnimator)
                {
                    playerAnimator.SetFloat("MoveX", 0);
                    playerAnimator.SetFloat("MoveY", 0);
                }
                //Save the value 
                if (playerAnimator)
                {
                    playerAnimator.SetFloat("LastMoveX", lastXJoystick);
                    playerAnimator.SetFloat("LastMoveY", lastYJoystick);
                }
            }


        }
    }
}
