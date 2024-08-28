using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask obstacle_layer;
    public List<Vector2> possible_directions {  get; private set; }

    private void Start()
    {
        this.possible_directions = new List<Vector2>();
        CheckPossibleDirection(Vector2.up);
        CheckPossibleDirection(Vector2.down);
        CheckPossibleDirection(Vector2.right);
        CheckPossibleDirection(Vector2.left);
    }

    private void CheckPossibleDirection(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * .5f, .0f, dir, 1.0f, this.obstacle_layer);
    
        if (hit.collider == null) this.possible_directions.Add(dir);
    }
}
