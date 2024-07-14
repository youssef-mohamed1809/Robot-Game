using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Rigidbody2D player_rigid_body;
    [SerializeField] Transform player_transform;
    [Header("Movement")]
    [SerializeField] float player_speed;
    [SerializeField] float jump_force;
    [SerializeField] float dash_force;
    [SerializeField] int dash_cooldown;

    [SerializeField] Transform raycast_ground_checker;
    [SerializeField] LayerMask ground_layer;

    [Header("Attack")]
    [SerializeField] int shoot_damage;
    [SerializeField] int melee_damage;
    [SerializeField] Transform bullet_instantiation_point;
    [SerializeField] GameObject bullet;
    [SerializeField] int bullet_despawn_time;
    [SerializeField] Animator player_animator;


    private double time_dashed;
    private PlayerInput player_input;
    bool isDashed;
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

        player_input.Gameplay.Shoot.performed += OnShoot;
    
    }

    void do_dash()
    {
        if (horizontal_movement > 0)
        {
            Debug.Log("Ana Hena");
            player_rigid_body.AddForce(new Vector2(dash_force, 0), ForceMode2D.Impulse);
        }
        else if (horizontal_movement < 0)
        {
            Debug.Log("Aw Hena");
            player_rigid_body.AddForce(new Vector2(-dash_force, 0), ForceMode2D.Impulse);
        }
    }

    void dash_handler()
    {
        player_animator.SetTrigger("dashed");
        if (horizontal_movement > 0)
        {
            Debug.Log("Ana Hena");
            player_rigid_body.AddForce(new Vector2(dash_force, 0), ForceMode2D.Impulse);
        }
        else if (horizontal_movement < 0)
        {
            Debug.Log("Aw Hena");
            player_rigid_body.AddForce(new Vector2(-dash_force, 0), ForceMode2D.Impulse);
        }
        isDashed = false;
    }
    void FixedUpdate()
    {

        if (isGrounded())
        {
            player_animator.SetBool("isJumping", false);
        }


        if(isDashed)
        {
            dash_handler();

            //player_animator.SetBool("isDashing", false);
        }
        else
        {

            if(Mathf.Abs(horizontal_movement) > 0)
            {
                player_animator.SetBool("isRunning", true);
            }
            else
            {
                player_animator.SetBool("isRunning", false);
            }

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

    private void OnShoot(InputAction.CallbackContext context)
    {
        player_animator.SetTrigger("shoot");
        GameObject obj = Instantiate(bullet, bullet_instantiation_point.position, Quaternion.identity);        
        Bullet bullet_script = obj.GetComponent<Bullet>();
        bullet_script.AddForce(new Vector2(this.transform.localScale.x, 0));
        Destroy(obj, bullet_despawn_time);
        Debug.Log("Shooting...");
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
            player_animator.SetBool("isJumping", true);
            player_rigid_body.AddForce(new Vector2(0, jump_force), ForceMode2D.Impulse);
        }
        Debug.Log("Jump");
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        if ((Time.time - time_dashed) > dash_cooldown)
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
            ground_layer);
    }

    public void playDamageAnimation()
    {
        player_animator.SetTrigger("damage");
    }
}
