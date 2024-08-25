using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8.0f;
    public float speed_coefficient = 1.0f;
    public Vector2 initial_direction;
    public LayerMask obstacle_layer;

    public Rigidbody2D rigid_body {  get; private set; }
    public Vector2 direction {  get; private set; }
    public Vector2 next_direction { get; private set; }
    public Vector3 start_position { get; private set; }

    private void Awake()
    {
        this.rigid_body = GetComponent<Rigidbody2D>();
        this.start_position = this.transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.speed_coefficient = 1.0f;
        this.direction = this.initial_direction;
        this.next_direction = Vector2.zero;
        this.transform.position = this.start_position;
        this.rigid_body.isKinematic = false;
        this.enabled = true;
    }

    private void Update()
    {
        if (this.next_direction != Vector2.zero) SetDirection(this.next_direction);
    }

    private void FixedUpdate()
    {
        Vector2 position = this.rigid_body.position;
        Vector2 translation = this.direction * this.speed * this.speed_coefficient * Time.fixedDeltaTime;
        this.rigid_body.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 dir, bool forced = false)
    {
        if (forced || !Occupied(dir))
        {
            this.direction = dir;
            this.next_direction = Vector2.zero;
        }

        else this.next_direction = dir;
    }

    public bool Occupied(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * .75f, .0f, dir, 1.5f, this.obstacle_layer);
        return hit.collider != null;
    }
}
