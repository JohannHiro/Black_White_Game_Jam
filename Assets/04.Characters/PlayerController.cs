using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask layerMask;
    private Vector2 velocity;
    [SerializeField] private float moveSpeed;
    private bool jump = false;
    [SerializeField] private float jumpForce;
    private bool isOnGround = false;
    private bool canDoubleJump = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        GetMovement();
        CheckIsOnGround();
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity;
        if (jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jump = false;
        }
    }

    private void GetMovement()
    {
        velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isOnGround)
            {
                jump = true;
            }
            else if (canDoubleJump && PlayerManager.power == PlayerManager.Power.DOUBLE_JUMP)
            {
                jump = true;
                canDoubleJump = false;
            }
        }
    }

    private void CheckIsOnGround()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size * 0.8f, 0f, Vector2.down, 0.3f, layerMask);
        Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down * (boxCollider2D.bounds.extents.y + 0.3f) * 0.8f, Color.green);
        isOnGround = raycastHit.collider != null;
        if (isOnGround)
            canDoubleJump = true;
    }
}
