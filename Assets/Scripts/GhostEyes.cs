using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    public SpriteRenderer sprite_renderer { get; private set; }
    public Movement movement { get; private set; }
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    private void Awake()
    {
        this.sprite_renderer = GetComponent<SpriteRenderer>();
        this.movement = GetComponentInParent<Movement>();
    }

    private void Update()
    {
        if (this.movement.direction == Vector2.up) this.sprite_renderer.sprite = this.up;
        else if (this.movement.direction == Vector2.down) this.sprite_renderer.sprite = this.down;
        else if (this.movement.direction == Vector2.left) this.sprite_renderer.sprite = this.left;
        else if (this.movement.direction == Vector2.right) this.sprite_renderer.sprite = this.right;
    }
}
