using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAnimal : Animal {
    public override void ExecuteTurn() {
        //Debug.Log("Took a turn");
    }

    public override void Interact() {
        Debug.Log("I Died");
        OnDeath.Invoke();
        this.gameObject.SetActive(false);
    }
}
