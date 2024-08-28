using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehaviour
{
    private void OnDisable()
    {
        this.ghost.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled && !ghost.frightened.enabled)
        {
            Vector2 dir = Vector2.zero;
            float min_dist = float.MaxValue;

            foreach (Vector2 poss_dir in node.possible_directions)
            {
                Vector3 new_position = this.transform.position + new Vector3(poss_dir.x, poss_dir.y, 0f);
                float dist = (this.ghost.chase_target.position - new_position).sqrMagnitude;

                if (dist < min_dist)
                {
                    dir = poss_dir;
                    min_dist = dist;
                }
            }

            this.ghost.movement.SetDirection(dir);
        }
    }
}
