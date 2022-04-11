using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    const float MAX_X_VELOCITY = 10;
    [Tooltip("Defines the max range to detect ground")]
    [SerializeField] float charHeight;
    [SerializeField] float wallDetectingDistance;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float wallSlidingSpeed;
    [SerializeField] float wallJumpForce;
    [SerializeField] Vector2 wallJumpDirection;
    [SerializeField] LayerMask whatIsGround;
    bool isGrounded { get => CheckGround(); }
    bool isNearWall { get => CheckWall(); }
    bool isWallSliding { get => CheckWallSliding(); }
    bool canWalk;
    [SerializeField]float horizontal;
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {

        body = GetComponent<Rigidbody2D>();
        canWalk = true;
        LevelControl.Instance.OnLevelLoad();
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelControl.Instance.ControlEnabled)
        {
            Move();
            Jump();
        }
    }
    void Move()
    {
        if (!canWalk) return;
        if (isWallSliding)
        {
            if (body.velocity.y < wallSlidingSpeed)
                body.velocity = new Vector2(body.velocity.x, -wallSlidingSpeed);
            Debug.Log("isWallSliding");
        }
        else
        {
            horizontal = Input.GetAxis("Horizontal");
            body.velocity = new Vector2(horizontal * moveSpeed, body.velocity.y);
        }
    }
    void Jump()
    {
        if (Input.GetAxis("Vertical") == 0) return;
        if (isGrounded)
        {
            body.velocity = jumpForce * Vector2.up;
        }
        else if (isWallSliding)
        {
            Vector2 force = new Vector2(wallJumpDirection.x * wallJumpForce * -horizontal, wallJumpDirection.y * wallJumpForce);
            body.velocity = Vector2.zero;
            body.AddForce(force, ForceMode2D.Impulse);
            canWalk = false;
            Invoke("ResetCanWalk", 0.3f);
        }
    }
    void ResetCanWalk()
    {
        canWalk = true;
    }
    bool CheckGround()
    {
        return Physics2D.Raycast(transform.position, -transform.up, charHeight,whatIsGround);
    }
    bool CheckWall()
    {
        return 
            Physics2D.Raycast(transform.position, -transform.right, wallDetectingDistance, whatIsGround) ||
            Physics2D.Raycast(transform.position, transform.right, wallDetectingDistance, whatIsGround);
    }
    bool CheckWallSliding()
    {
        return isNearWall && !isGrounded && horizontal != 0;
    }
}
