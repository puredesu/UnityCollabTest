using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private CharacterController controller;
    private GameManager manager;

    [SerializeField] private Camera mainCamera; // Raycast from mouse to world space

    // Default values
    [SerializeField] private float speed = 10f;
    [SerializeField] private float turnSmoothTime = 0.02f;
    [SerializeField] private float gravity = -9.81f;

    private Vector3 velocity; // Currently only keeps track of gravity of player
    private float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        manager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // Raycast from mouse position to a world space
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            // Get the angle between the mouse and the player
            float targetAngle = Mathf.Atan2(raycastHit.point.x - transform.position.x, raycastHit.point.z - transform.position.z) * Mathf.Rad2Deg;

            // Set how fast the player can rotate (0 means instanly)
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // Rotate accordingly
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }


        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        // Since the camera is pointing 45 degrees to the left of the player,
        // we have to transform WASD directions to 45 degrees to the left
        // such that pressing 'W' will move the player upwards relative to the screen.
        Vector3 direction = Quaternion.AngleAxis(-45, Vector3.up) * (new Vector3(horizontal, 0f, vertical));

        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction.normalized * speed * Time.deltaTime);
        }

        // Gravity: delta Y = 1/2 * gravity * time^2
        if (controller.isGrounded)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y += gravity;
            controller.Move(velocity / 2 * Mathf.Pow(Time.deltaTime, 2));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            SceneManager.LoadScene("SampleScene");
            manager.Score = 0;
        }
    }
}
