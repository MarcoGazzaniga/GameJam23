using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {
    public bool isAttacking;
    public bool canInput;
    public bool mudMovement;
    public int attackDistance;
    private int attackCounter;
    public float moveSpeed;
    public float attackSpeedMultiplier;
    Vector2 targetPosition;
    private Vector2 cachedOffset;
    private Vector2 offset;
    PlayerActionScript actions;
    [SerializeField]
    UnityEvent OnTurn;

    [SerializeField]
    Animal[] animalList;

    [SerializeField]
    LayerMask collisionLayer;

    [SerializeField]
    LayerMask interactableLayer;

    [SerializeField]
    LayerMask mudLayer;

    bool isMoving;
    // Start is called before the first frame update
    void Start() {
        actions = new PlayerActionScript();
        actions.Player.Enable();

        if (OnTurn == null) {
            OnTurn = new UnityEvent();
        }

        foreach (Animal a in animalList) {
            OnTurn.AddListener(a.ExecuteTurn);
        }

        attackCounter = 0;

        targetPosition = transform.position;
        cachedOffset = actions.Player.Movement.ReadValue<Vector2>();

        isMoving = true;
        canInput = true;

        actions.Player.AtackModifier.performed += context => isAttacking = true;
        actions.Player.Movement.performed += context => {
            if (Physics2D.OverlapCircle(gameObject.transform.position, .1f, mudLayer)) {
                if (mudMovement) {
                    offset = actions.Player.Movement.ReadValue<Vector2>();
                    canInput = false;
                }
                mudMovement = !mudMovement;
            } else {
                offset = actions.Player.Movement.ReadValue<Vector2>();
                canInput = false;
            }
        };
        actions.Player.Movement.canceled += context => {
            offset = Vector2.zero;
            canInput = true;
        };

        //actions.Player.AtackModifier.canceled += context => isAttacking = (attackCounter<attackDistance);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (isAttacking) {
            HandleAttack();
        } else {
            HandleMovement();
        }
    }

    private void HandleMovement() {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        Vector2 currentPositionVector2 = transform.position;
        if (!isMoving) {
            if (offset.x != 0 && offset.y != 0) {
                offset.x = 0;
            }
            Vector2 target = currentPositionVector2 + offset;
            if (targetPosition != target) {
                RaycastHit2D[] hits;
                if (Physics2D.OverlapCircle(target, .1f, collisionLayer) || Physics2D.OverlapCircle(target, .1f, interactableLayer)) {

                } else {
                    isMoving = true;
                    targetPosition = currentPositionVector2 + offset;
                    BroadcastMessage("OnMovement", offset);
                }
                OnTurn.Invoke();
            }
        } else {
            if (currentPositionVector2 == targetPosition) {
                isMoving = false;
            }
        }
    }

    private void HandleAttackMovement() {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        Vector2 currentPositionVector2 = transform.position;
        if (!isMoving) {
            Vector2 offset = cachedOffset;
            if (offset.x != 0 && offset.y != 0) {
                offset.x = 0;
            }
            Vector2 target = currentPositionVector2 + offset;
            if (targetPosition != target) {
                RaycastHit2D[] hits;

                if (Physics2D.OverlapCircle(target, .1f, collisionLayer)) {
                    attackCounter = 0;
                } else {
                    isMoving = true;

                    if (Physics2D.OverlapCircle(target, .1f, collisionLayer)) {
                        attackCounter = 0;
                    }

                    hits = Physics2D.RaycastAll(transform.position, offset.normalized, attackDistance, interactableLayer);

                    if (hits.Length > 0) {
                        hits[0].transform.gameObject.GetComponent<Animal>().Interact();
                    }

                    targetPosition = currentPositionVector2 + offset;
                    BroadcastMessage("OnMovement", offset);
                }
            }
        } else {
            if (currentPositionVector2 == targetPosition) {
                isMoving = false;
                attackCounter++;
            }
        }
    }

    private void HandleAttack() {
        if (offset != Vector2.zero && attackCounter==0) {
            cachedOffset = offset;
            BroadcastMessage("OnAttack", offset);
        }
        if (cachedOffset != Vector2.zero && attackCounter < attackDistance) {
            HandleAttackMovement();
        } else {
            if (attackCounter >= attackDistance) {
                cachedOffset = Vector2.zero;
                attackCounter = 0;
                isAttacking = false;
            }
        }
    }
}
