using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement movement {  get; private set; }
    public Transform chase_target;
    // public new Rigidbody2D rigidbody {  get; private set; }
    public GhostBehaviour initial_behaviour;
    public GhostHome home { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFrightened frightened { get; private set; }
    public GhostScatter scatter { get; private set; }
    public int points = 200;

    private void Awake()
    {
        //this.initial_behaviour = GetComponent<GhostBehaviour>();
        this.home = GetComponent<GhostHome>();
        this.chase = GetComponent<GhostChase>();
        this.frightened = GetComponent<GhostFrightened>();
        this.scatter = GetComponent<GhostScatter>();
        this.movement = GetComponent<Movement>();
        // this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();
        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable();

        if (this.home != this.initial_behaviour) this.home.Disable();

        if (this.initial_behaviour != null) this.initial_behaviour.Enable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.frightened.enabled) FindObjectOfType<GameManager>().GhostEaten(this);
            else FindObjectOfType<GameManager>().PacmanEaten();
        }
    }

    public void SetPosition(Vector3 position)
    {
        position.z = transform.position.z;
        transform.position = position;
    }
}
