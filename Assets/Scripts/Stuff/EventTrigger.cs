using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    [SerializeField]
    UnityEvent trigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        trigger?.Invoke();
    }
}
