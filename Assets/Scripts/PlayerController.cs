using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private GameObject winTextObject;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        LockRotation();
        SetCountText();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
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

    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 5)
        {
            winTextObject.SetActive(true);
        }
    }
}
