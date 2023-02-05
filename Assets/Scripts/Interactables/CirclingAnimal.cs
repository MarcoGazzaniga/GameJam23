using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclingAnimal : Animal {

    Vector2 direction;
    public int defaultCounterValue;
    public int moveSpeed;
    private int counter;
    public bool isMoving;
    public Vector2 targetPosition;

    [SerializeField]
    LayerMask collisionLayer;

    private void Start() {
        direction = Vector2.up;
        counter = defaultCounterValue;

        targetPosition = transform.position;
        isMoving = true;
    }

    private void Update() {
        HandleMovement();
    }

    public override void ExecuteTurn() {
        //transform.position += direction;
        if (!isMoving) {
            counter--;
            if (counter <= 0) {
                counter = defaultCounterValue;
                direction = Quaternion.Euler(0, 0, -90) * direction;
            }
        }
    }

    public override void Interact() {
        Debug.Log("I Died");
        OnDeath.Invoke();
        this.gameObject.SetActive(false);
    }

    private void HandleMovement() {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        Vector2 currentPositionVector2 = transform.position;
        if (!isMoving) {
            Vector2 target = currentPositionVector2 + new Vector2();
            if (targetPosition != target) {
                List<Collider2D> collisions = new List<Collider2D>();

                if (Physics2D.OverlapCircle(target, .1f, collisionLayer)) {

                } else {
                    targetPosition = currentPositionVector2 + direction;
                }
            }
            isMoving = true;
        } else {
            if (currentPositionVector2 == targetPosition) {
                isMoving = false;
            }
        }
    }
}
