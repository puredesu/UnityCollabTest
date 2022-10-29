using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController controller;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float turnSmoothTime = 0.02f;
    [SerializeField] private float gravity = -9.81f;

    private Vector3 velocity;

    private float turnSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Since the camera is pointing 45 degrees to the left of the player,
        // we have to transform WASD directions to 45 degrees to the left
        // such that pressing 'W' will move the player upwards relative to the screen.
        Vector3 direction = Quaternion.AngleAxis(-45, Vector3.up) * (new Vector3(horizontal, 0f, vertical));

        if (direction.magnitude >= 0.1f)
        {

            // Angle to rotate in
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // Apply smooth while rotating
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            controller.Move(direction.normalized * speed * Time.deltaTime);
        }

        // Gravity: delta Y = 1/2 * gravity * time^2
        velocity.y += gravity;
        controller.Move(velocity/2 * Mathf.Pow(Time.deltaTime, 2));

    }
}
