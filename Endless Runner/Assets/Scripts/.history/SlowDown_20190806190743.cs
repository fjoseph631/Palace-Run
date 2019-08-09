using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class SlowDown : MonoBehaviour {
    // Update is called once per frame
    private GameObject player;
    public float rotateSpeed =6f;
    void start()
    {
        player = GameObject.FindWithTag(Constants.PlayerTag);
    }

    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * rotateSpeed);
    }
    //Slow Player By 10%;
    void OnTriggerEnter(Collider col)
    {
        CharacterInput.Speed*=.90f;
        Destroy(this.gameObject);
        UIManager.Instance.SetStatus("Slowed Down");
        Debug.Log("Slow Down");
    }
}