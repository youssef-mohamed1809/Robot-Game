using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Rigidbody2D player_rigid_body;
    [SerializeField] Transform player_transform;
    [SerializeField] float player_speed;
    [SerializeField] float jump_force;
    [SerializeField] float dash_force;
    [SerializeField] bool isDashed;
    [SerializeField] Transform raycast_ground_checker;
    [SerializeField] LayerMask ground_layer;
    [SerializeField] int dash_cooldown;

    
    private double time_dashed;
    private PlayerInput player_input;
    float horizontal_movement;
    float jump;


    private void Awake()
    {
        horizontal_movement = 0;

        player_input = new PlayerInput();
        player_input.Enable();
        player_input.Gameplay.Move.performed += OnMoveRightLeft;
        player_input.Gameplay.Move.canceled += OnMoveRightLeft;
        player_input.Gameplay.Jump.performed += OnJump;
        player_input.Gameplay.Dash.performed += OnDash;

    
    }

    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void FixedUpdate()
    {                    
        if(isDashed)
        {
            
            if(horizontal_movement > 0)
            {
                player_rigid_body.AddForce(new Vector2(dash_force, 0), ForceMode2D.Impulse);
            }else if(horizontal_movement < 0)
            {
                player_rigid_body.AddForce(new Vector2(-dash_force, 0), ForceMode2D.Impulse);
            }   
            
            
            isDashed = false;
        }
        else
        {
            if((horizontal_movement > 0 && player_transform.localScale.x < 0) || (horizontal_movement < 0 && player_transform.localScale.x > 0))
            {
                player_transform.localScale = new Vector3(
                    player_transform.localScale.x * -1,
                    player_transform.localScale.y,
                    player_transform.localScale.z
                    );
            }
            float velocityY = player_rigid_body.velocity.y;
            player_rigid_body.velocity = new Vector2(horizontal_movement, velocityY);
        }
        
       
    }

    private void OnMoveRightLeft(InputAction.CallbackContext context)
    {
        float move_val = context.ReadValue<float>();
        horizontal_movement = move_val * player_speed;
        Debug.Log("Value: " + move_val);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded())
        {
            player_rigid_body.AddForce(new Vector2(0, jump_force), ForceMode2D.Impulse);
        }
        Debug.Log("Jump");
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        //player_rigid_body.AddForce(new Vector2(dash_force, 0), ForceMode2D.Impulse);
        if((Time.time - time_dashed) > dash_cooldown)
        {
            isDashed = true;
            time_dashed = Time.time;
        }
        Debug.Log("DASH");
    }
    private bool isGrounded() {
        return Physics2D.Raycast(raycast_ground_checker.position,
            -transform.up,
            1,
            ground_layer
            );
    }
}
