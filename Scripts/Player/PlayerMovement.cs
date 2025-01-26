using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {

        private float _rotation;
        private Vector3 _velocity;

        void Update()
        {
            if (GameManager.activeGameState <= 0) return;
            // Resetting Velocity
            if (PlayerManager.PlayerController.isGrounded)
            {
                _velocity.y = -2f;  
            }

            // Any movement
            if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
            {
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");

                Vector3 move = PlayerManager.PlayerGameObj.transform.right * horizontal +
                               PlayerManager.PlayerGameObj.transform.forward * vertical;
            
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    // Running
                    move *= PlayerManager.RunningSpeed;
                }
                else
                {
                    // Walking
                    move *= PlayerManager.WalkingSpeed;
                }
            
                PlayerManager.PlayerController.Move(move * Time.deltaTime);

            }
        
            // Jumping
            if (PlayerManager.PlayerController.isGrounded && Input.GetKey(KeyCode.Space))
            {
                _velocity.y = Mathf.Sqrt(PlayerManager.JumpForce * -2 * PlayerManager.Gravity);
            }
        
            // Gravity
            _velocity.y += PlayerManager.Gravity * Time.deltaTime;
            PlayerManager.PlayerController.Move(_velocity * Time.deltaTime);
        
        
            // Camera Movement
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                float mouseY = Input.GetAxis("Mouse Y");
                float mouseX = Input.GetAxis("Mouse X");
                _rotation += -mouseY * 5;
                _rotation = Mathf.Clamp(_rotation, -30, 40);

                PlayerManager.PlayerCamera.transform.localRotation = Quaternion.Euler(_rotation, 0, 0);
                PlayerManager.PlayerGameObj.transform.rotation *= Quaternion.Euler(0, mouseX * 5, 0);

            }
        
        }
    

    }
}
