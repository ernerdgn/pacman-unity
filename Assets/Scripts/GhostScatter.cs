using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScatter : GhostBehaviour
{
    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !ghost.frightened.enabled)
        {
            int index = Random.Range(0, node.possible_directions.Count);

            if (node.possible_directions[index] == -this.ghost.movement.direction) // && node.posdir.count > 1
            {
                index++;
                if (index >= node.possible_directions.Count) index = 0;
            }

            this.ghost.movement.SetDirection(node.possible_directions[index]);
        }
    }


}
