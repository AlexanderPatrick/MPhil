using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Move : MonoBehaviour {
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public Vector3 gravityCore;

    private Vector3 moveDirection = Vector3.zero;
    void Update() {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection *= speed;
            if (Input.GetButton("Jump")) {
                moveDirection.y = jumpSpeed;
            }
            moveDirection = transform.TransformDirection(moveDirection);
        } else {
            Debug.Log("Not Grounded");
        }
        //moveDirection.y -= gravity * Time.deltaTime;
        moveDirection -= transform.up * gravity * Time.deltaTime;
        //moveDirection = transform.TransformDirection(moveDirection);
        controller.Move(moveDirection * Time.deltaTime);
        //transform.rotation = Quaternion.LookRotation(transform.forward, (transform.position - gravityCore).normalized);
        transform.up = (transform.position - gravityCore).normalized;
    }
}
