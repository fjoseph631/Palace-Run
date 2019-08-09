using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Adds bonus to score
public class ScoreAdder : MonoBehaviour {
    //Bonus given to player
    public int Bonus=200;
    //Rotation speed
    public float rotateSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * rotateSpeed);
    }
    void OnTriggerEnter(Collider col)
    {
        //Add bounus to player's score
        UIManager.Instance.IncreaseScore(Bonus);
        Destroy(this.gameObject);
        UIManager.Instance.SetStatus("Bonus Earned");
    }
}