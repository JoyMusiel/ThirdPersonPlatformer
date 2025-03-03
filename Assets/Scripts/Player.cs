using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxVelocity;
    [SerializeField] private LayerMask ground;
    private float raycastDistance = 1f;
    private Rigidbody rb;
    private bool isGrounded;
    private int maxJumps = 2;
    private int jumpCount;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        inputManager.OnMove.AddListener(MovePlayer);
        inputManager.OnJumpPressed.AddListener(Jump);
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, raycastDistance, ground);
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();
        transform.forward = cameraForward;
    }

    private void MovePlayer(Vector2 direction)
    {
        Vector3 moveDirection = new Vector3(direction.x, 0f, direction.y);
        moveDirection = cameraTransform.TransformDirection(moveDirection);
        moveDirection.y = 0f;
        moveDirection.Normalize();

        if (moveDirection.magnitude > 0)
        {
            Vector3 velocity = moveDirection * moveSpeed;
            if (!isGrounded)
            {
                velocity.x *= 0.5f;
                velocity.z *= 0.5f;
            }
            rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        }
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxVelocity);
    }

    private void Jump()
    {
        if (isGrounded || jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
            isGrounded = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        jumpCount = 0;
    }
}
