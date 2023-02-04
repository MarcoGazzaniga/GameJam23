using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    Vector2 targetPosition;
    PlayerActionScript actions;

    bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        actions = new PlayerActionScript();
        actions.Player.Enable();

        isMoving = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        Vector2 currentPositionVector2 = transform.position;
        Vector2 offset = actions.Player.Movement.ReadValue<Vector2>();
        if (!isMoving) {
            if (offset.x != 0 && offset.y != 0) {
                offset.Set(0, offset.y);
                offset = offset.normalized;
            }
            targetPosition = currentPositionVector2 + offset;
            isMoving = true;
        } else {
            if (currentPositionVector2 == targetPosition) {
                isMoving = false;
            }
        }
    }
}