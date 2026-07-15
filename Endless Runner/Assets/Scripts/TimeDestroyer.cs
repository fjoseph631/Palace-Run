using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroyer : MonoBehaviour
{
    public float lifeTime = 5f;

    void Start()
    {
        Invoke("DestroyObject", lifeTime);
    }

    void DestroyObject()
    {
        if (
            GameManager.getManager().getState() != State.Dead
            && GameManager.getManager().getState() != State.Start
        )
        {
            Destroy(gameObject);
        }
    }
}
