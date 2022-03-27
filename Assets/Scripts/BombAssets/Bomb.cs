using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : PlayerKiller
{
    [SerializeField] private ParticleSystem particles;
    private SpriteRenderer renderer;
    private bool exploding = false;
    protected override void Awake()
    {
        base.Awake();
        particles = GetComponent<ParticleSystem>();
        renderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        Explodir();
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        Explodir();
    }


    private void Explodir()
    {
        if (exploding) return;
        StartCoroutine(ExplodirCoroutine());
    }
    IEnumerator ExplodirCoroutine()
    {
        exploding = true;
        renderer.enabled = false;
        particles.Play();
        yield return new WaitUntil(() => !particles.isPlaying);
        Destroy(gameObject);
    }
}
