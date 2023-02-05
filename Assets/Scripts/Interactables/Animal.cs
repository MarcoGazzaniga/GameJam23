using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Animal : MonoBehaviour
{
    public UnityEvent OnDeath;

    private void Start() {
        if (OnDeath == null) {
            OnDeath = new UnityEvent();
        }
    }

    void OnPlayerMovement() {
        ExecuteTurn();
    }

    public abstract void ExecuteTurn();

    public abstract void Interact();
}
