using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    Vector2 targetPosition;
    PlayerActionScript actions;

    bool isMoving;
    // Start is called before the first frame update
    void Start() {
        actions = new PlayerActionScript();
        actions.Player.Enable();

        targetPosition = transform.position;
        isMoving = true;
    }

    // Update is called once per frame
    void FixedUpdate() {
        HandleMovement();
    }

    private void HandleMovement() {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        Vector2 currentPositionVector2 = transform.position;
        if (!isMoving) {
            Vector2 offset = actions.Player.Movement.ReadValue<Vector2>();
            if (offset.x != 0 && offset.y != 0) {
                offset.x = 0;
            }
            if (targetPosition != currentPositionVector2 + offset) {
                targetPosition = currentPositionVector2 + offset;
                BroadcastMessage("OnMovement", offset);
            }
            isMoving = true;
        } else {
            if (currentPositionVector2 == targetPosition) {
                isMoving = false;
            }
        }
    }
}
