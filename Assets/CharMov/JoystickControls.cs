using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class JoystickControls : MonoBehaviour
{
    PlayerInput playerInput;
    private Rigidbody2D controller;
    private Vector3 playerVelocity;
    [SerializeField,Range(.1f,5f)]
    private float playerSpeed=.5f;

    private Animator _animator;

    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector2 move = new Vector3(input.x, input.y, 0);
        
        controller.MovePosition(controller.position + playerSpeed * Time.deltaTime * move);

        if (input.x != 0f)
        {
            _animator.SetFloat("LastMoveX", input.x);
        }
        
        if (input.y != 0f)
        {
            _animator.SetFloat("LastMoveY", input.y);
        }
        _animator.SetFloat("MoveX", input.x);
        _animator.SetFloat("MoveY", input.y);
        
        _animator.SetFloat("Speed", move.magnitude);
        
        if (image != null)
        {
            if (input.x > 0.2f || input.x < -0.2f || input.y > 0.2f || input.y < -0.2f)
            {

                image.CrossFadeAlpha(255, 1.0f, false);
            }
            else
            {
                //image.CrossFadeAlpha(0, 0.0f, false);
            }
        }
       
        
    }
}
