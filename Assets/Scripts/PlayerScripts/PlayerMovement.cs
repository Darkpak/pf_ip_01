using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        private float speed = 5f;
        private float sprintMultiplier = 2f;
        private float jumpForce = 250f;
        private float mouseSensitivity = 100f;
        public AudioSource jumpSound;
        public AudioSource walkSound;
        public AudioSource runSound;

        private Rigidbody rb;
        private bool grounded;
        private float currentSpeed;
        private Transform cameraTransform;
        private float xRotation = 0f;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            cameraTransform = Camera.main.transform;
            cameraTransform.SetParent(transform); 
            cameraTransform.localPosition = new Vector3(0, 0.316f, 0.286f); // Adjust the camera position to the player's head
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            HandleMovement();
            HandleJumping();
            HandleCameraMovement();
        }

        void HandleMovement()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            currentSpeed = Input.GetKey(KeyCode.LeftShift) ? speed * sprintMultiplier : speed;

            Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical).normalized * currentSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + transform.TransformDirection(movement));

            if (grounded && (moveHorizontal != 0 || moveVertical != 0))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (!runSound.isPlaying) runSound.Play();
                    walkSound.Stop();
                }
                else
                {
                    if (!walkSound.isPlaying) walkSound.Play();
                    runSound.Stop();
                }
            }
            else
            {
                walkSound.Stop();
                runSound.Stop();
            }
        }

        void HandleJumping()
        {
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                rb.AddForce(Vector3.up * jumpForce);
                jumpSound?.Play();
                walkSound?.Stop();
                runSound?.Stop();
            }
        }

        void HandleCameraMovement()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Rotate the player horizontally
            transform.Rotate(Vector3.up * mouseX);

            // Rotate the camera vertically
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                grounded = true;
            }
        }

        void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                grounded = false;
            }
        }
    }
