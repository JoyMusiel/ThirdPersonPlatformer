using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask ground;
    private Rigidbody rb;
    private bool isOnGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputManager.OnMove.AddListener(MovePlayer);
        inputManager.OnJumpPressed.AddListener(Jump);
    }

    void Update()
    {
        isOnGround = Physics.Raycast(transform.position, Vector3.down, 1.1f, ground);
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
        rb.AddForce(speed * moveDirection);
    }

    private void Jump()
    {
        if (isOnGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
    }
}
