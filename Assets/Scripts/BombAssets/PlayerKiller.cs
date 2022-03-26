using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerKiller : MonoBehaviour
{
    protected  virtual void Awake()
    {
        gameObject.layer = 6;
    }

    public UnityEvent OnPlayerTouch;
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        LevelControl.Instance.Death();
        OnPlayerTouch?.Invoke();
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        LevelControl.Instance.Death();
        OnPlayerTouch?.Invoke();
    }
}
