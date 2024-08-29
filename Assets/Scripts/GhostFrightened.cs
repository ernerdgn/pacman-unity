using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFrightened : GhostBehaviour
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer weak_mode_blue;
    public SpriteRenderer weak_mode_flash;

    public bool eaten { get; private set; }

    public override void Enable(float duration)
    {
        base.Enable(duration);

        this.body.enabled = false;
        this.eyes.enabled = false;
        this.weak_mode_blue.enabled = true;
        this.weak_mode_flash.enabled = false;

        Invoke(nameof(Flash), duration / 2.0f);
    }

    public override void Disable()
    {
        base.Disable();
        this.body.enabled = true;
        this.eyes.enabled = true;
        this.weak_mode_blue.enabled = false;
        this.weak_mode_flash.enabled = false;
    }

    private void OnEnable()
    {
        this.ghost.movement.speed_coefficient = .5f;
        this.eaten = false;
    }

    private void OnDisable()
    {
        this.ghost.movement.speed_coefficient = 1.0f;
        this.eaten = false;
    }

    private void Flash()
    {
        if (!this.eaten)
        {
            this.weak_mode_blue.enabled = false;
            this.weak_mode_flash.enabled = true;
            this.weak_mode_flash.GetComponent<AnimatedSprite>().Restart();
        }


    }

    private void Eaten()
    {
        this.eaten = true;

        Vector3 pos = this.ghost.home.home.position;
        pos.z = this.ghost.transform.position.z;

        this.ghost.transform.position = pos;

        this.ghost.home.Enable(this.duration);

        this.body.enabled = false;
        this.eyes.enabled = true;
        this.weak_mode_blue.enabled = false;
        this.weak_mode_flash.enabled = false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.enabled) Eaten();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled)
        {
            Vector2 dir = Vector2.zero;
            float escape_dist = float.MinValue;

            foreach (Vector2 poss_dir in node.possible_directions)
            {
                Vector3 new_position = this.transform.position + new Vector3(poss_dir.x, poss_dir.y, 0f);
                float dist = (this.ghost.chase_target.position - new_position).sqrMagnitude;

                if (dist > escape_dist)
                {
                    dir = poss_dir;
                    escape_dist = dist;
                }
            }

            this.ghost.movement.SetDirection(dir);
        }
    }
}
