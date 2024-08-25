using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]  // needs spriteRenderer to work
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer {  get; private set; }
    public Sprite[] sprites;
    public float animation_time = .25f;
    public int animation_frame {  get; private set; }

    public bool loop = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Advance), this.animation_time, this.animation_time);
    }

    private void Advance()
    {
        if (!this.spriteRenderer.enabled) return;

        this.animation_frame++;

        if (this.animation_frame >= this.sprites.Length && this.loop) this.animation_frame = 0;

        if (this.animation_frame >= 0 && this.animation_frame < this.sprites.Length) this.spriteRenderer.sprite = this.sprites[this.animation_frame];
    }

    public void Restart()
    {
        this.animation_frame = -1;

        Advance();
    }
}
