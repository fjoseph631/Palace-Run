using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class SlowDown : MonoBehaviour
{
    public float rotateSpeed = 65f;

    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * rotateSpeed);
    }

    void OnTriggerEnter(Collider col)
    {
        CharacterInput.speed *= .90f;
        Destroy(this.gameObject);
        UIManager.Instance.SetStatus(Constants.StatusSlowDown);
    }
}
