using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;

    private Rigidbody rb;
    private float movementX;
    private float movementY;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        LockRotation();
    }

    private void FixedUpdate()
    {     
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        if (movement != Vector3.zero) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(targetRotation);
        }
        rb.linearVelocity = movement * speed;
    }

    private void LockRotation()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;  
    }
        
    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
}
