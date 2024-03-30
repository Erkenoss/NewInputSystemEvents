using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerInputActions input;
    public Vector2 inputMovement;
    public Vector2 inputLook;
    public float JumpTimer;
    private float mouseScrollY;
    public bool isSprinting;
    
    public float movementSpeed;
    public float sprintSpeed;

    private void Awake() {
        input = new PlayerInputActions();
        input.Movement.Movement.performed += x => inputMovement = x.ReadValue<Vector2>();
        input.Movement.Look.performed += x => inputLook = x.ReadValue<Vector2>();
        input.Player.MouseScrollY.performed += x => mouseScrollY = x.ReadValue<float>();
        input.Player.SprintStart.performed += x => SprintPressed();
        input.Player.SprintFinish.performed += x => SprintRelease();


        input.Actions.Jump.performed += x => Jump();
        input.Actions.SuperJump.performed += x => SuperJump();
    }

    private void Update() {
        JumpingTimer();
        if (mouseScrollY > 0) {
            Debug.Log("Scroll up");
        }
        if (mouseScrollY < 0) {
            Debug.Log("Scroll down");
        }

        if (isSprinting)
        {
            transform.position += transform.forward * sprintSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
        }
    }

    private void SprintPressed() {
        isSprinting = true;
    }

    private void SprintRelease() {
        isSprinting = false;
    }

    private void JumpingTimer() {
        if (JumpTimer >= 0) {
            JumpTimer -= Time.deltaTime;
        }
    }
    private void Jump() {
        if (JumpTimer <= 0) {
            JumpTimer = 0.4f;
            return;
        }
        Debug.Log("I'm jumping");
    }
    private void SuperJump() {
        Debug.Log("I'm SuperJumping");
    }


    #region - Enable/Disable -
    private void OnEnable() {
        input.Enable();
    
    }

    private void OnDisable() {
        input.Disable();
    }
    #endregion
}
