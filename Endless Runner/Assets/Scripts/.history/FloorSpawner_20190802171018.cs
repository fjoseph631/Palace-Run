using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FloorSpawner : MonoBehaviour {
     public float positionY = 0.81f;
    public Transform[] PathSpawnPoints;
    public GameObject []Paths;
    public GameObject PreviousPath;
    public GameObject []DangerousBorders;
    public Transform[] BorderSpawnPoints;
    void OnTriggerEnter(Collider hit)
    {
        //player has hit the collider
        if (hit.gameObject.tag == Constants.PlayerTag)
        {
            //find whether the next path will be straight, left or right
            int pathChoice= Random.Range(1,Paths.Length);
            var path = Paths[pathChoice-1];
            //Get offset between new path and old path
            int offset = (int)PreviousPath.Transform.y - (int)path.Transform.y;
            //Create Path
            Instantiate(path,path.Transform.position,path.Transform.rotation);
            //Straight
            if(offset==0)
            {
                //Add to queue
                GameManager.getManager().getDirection().Enqueue(GameManager.turnDirection.Straight);
                return;
            }
            Instantiate(path,path.Transform.position,path.Transform.rotation);
            
        }
    }
}