using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    Rigidbody rb;
    public float playerSpeed = 3f;
   // bool isGrounded = true;
    public float jumpForce = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

        void Update()
        {
            //WASD movement, transform.translate
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * playerSpeed * Time.deltaTime);
            }
        }
            private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //  transform.transform.Translate(Vector3.up * playerSpeed * Time.deltaTime);
            //makes player go UP when pressing Space. As of writing this makes you float into the sky.
            rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
        }
    }
}

   


  

