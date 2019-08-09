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
        Debug.Log("Trigger Hit");
        //player has hit the collider
        if (hit.gameObject.tag == Constants.PlayerTag)
        {
            //find whether the next path will be straight, left or right
            int pathChoice= Random.Range(1,Paths.Length);
            var path = PathSpawnPoints[pathChoice-1];
            //Get offset between new path and old path
            int offset = (int)PreviousPath.transform.rotation.y - (int)path.transform.rotation.eulerAngles.y;
            Debug.Log(path.transform.rotation.eulerAngles.y);
            //REduce offset to acceptable range
            while(offset>360)
            {
                offset-=360;
            }
            //Straight
            if(offset==0)
            {
                //Create Path
                Instantiate(Paths[1],path.transform.position,(path.transform.rotation));
                //Add to queue
                GameManager.getManager().getDirection().Enqueue(GameManager.turnDirection.Straight);
                return;
            }
            //Create Path
            Instantiate(Paths[0],path.transform.position,(path.transform.rotation));
            //Generate left border
            if(offset==-90||offset==270)
            {
                var border= BorderSpawnPoints[0];
                Instantiate(border,border.position,border.rotation);
                GameManager.getManager().getDirection().Enqueue(GameManager.turnDirection.Left);
                return;
            }
            //Generate right border
            if(offset==90)
            {
                var border= BorderSpawnPoints[1];
                Instantiate(border,border.position,border.rotation);
                //Add to direction queue
                GameManager.getManager().getDirection().Enqueue(GameManager.turnDirection.Right);
                return;
            }
        }
    }
}