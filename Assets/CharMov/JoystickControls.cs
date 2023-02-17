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
    [SerializeField,Range(.1f,5f)]
    private float playerSpeed=.5f;

    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<Rigidbody2D>();       
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
            if (input.x > 0.2f || input.x < -0.2f || input.y > 0.2f || input.y < -0.2f)
            {

                image.CrossFadeAlpha(255, 1.0f, false);
            }
            else
            {
                //image.CrossFadeAlpha(0, 0.0f, false);

                if (playerAnimator)
                {
                    playerAnimator.SetFloat("MoveX",0);
                    playerAnimator.SetFloat("MoveY",0);
                }
                //Save the value 
                if (playerAnimator)
                {
                    playerAnimator.SetFloat("LastMoveX",lastXJoystick);
                    playerAnimator.SetFloat("LastMoveY",lastYJoystick);
                }
            }
        }
       
        
    }
}
