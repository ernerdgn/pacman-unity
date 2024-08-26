using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : Pellet
{
    public float duration = 8.0f;

    protected override void Eat()
    {
        //this.gameObject.SetActive(false);  // called in the gameManager
        FindObjectOfType<GameManager>().PowerPelletEaten(this);
    }
}
