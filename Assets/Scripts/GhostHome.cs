using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class GhostHome : GhostBehaviour
{
    public Transform home;
    public Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if (this.gameObject.activeSelf) StartCoroutine(ExitTransition());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(this.enabled + "   ?ENABLED");
        
        if (this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))  // this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")
        {
            Debug.Log(true);
            this.ghost.movement.SetDirection(-this.ghost.movement.direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        this.ghost.movement.SetDirection(Vector2.up, true);
        this.ghost.movement.rigid_body.isKinematic = true;
        this.ghost.movement.enabled = false;

        Vector3 pos = this.transform.position;
        float duration = .5f;
        float elapsed_time = .0f;

        while (elapsed_time < duration)
        {
            //Vector3 new_pos = Vector3.Lerp(pos, this.home.position, elapsed_time / duration);
            //new_pos.z = pos.z;  
            //this.ghost.transform.position = new_pos;
            //elapsed_time += Time.deltaTime;
            //yield return null;

            ghost.SetPosition(Vector3.Lerp(pos, home.position, elapsed_time / duration));
            elapsed_time += Time.deltaTime;
            yield return null;
        }

        elapsed_time = .0f;

        while (elapsed_time < duration)
        {
            //Vector3 new_pos = Vector3.Lerp(this.home.position, this.outside.position, elapsed_time / duration);
            //new_pos.z = pos.z;
            //this.ghost.transform.position = new_pos;
            //elapsed_time += Time.deltaTime;
            //yield return null;

            ghost.SetPosition(Vector3.Lerp(home.position, outside.position, elapsed_time / duration));
            elapsed_time += Time.deltaTime;
            yield return null;
        }

        this.ghost.movement.SetDirection(new Vector2(Random.value < .5f ? -1.0f : 1.0f, .0f), true);
        this.ghost.movement.rigid_body.isKinematic = false;
        this.ghost.movement.enabled = true;
    }
}
