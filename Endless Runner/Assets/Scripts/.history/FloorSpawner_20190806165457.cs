using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FloorSpawner : MonoBehaviour {
     //Path Spawn Points and the paths to be placed
    public Transform[] PathSpawnPoints;
    public GameObject[] Paths;
    //The previous path - used to determine rotation
    public GameObject PreviousPath;
         //Border Spawn  Points and the paths to be placed
    public GameObject[] DangerousBorders;
    public Transform[] BorderSpawnPoints;
    public GameObject[] PowerUps;
    public Transform[] PowerSpawnPoints;
    public Transform[] ObsticleSpawnPoints;
    public GameObject[] Obsticles;
    
    void OnTriggerEnter(Collider hit)
    {
        //player has hit the collider
        if (hit.gameObject.tag == Constants.PlayerTag)
        {
            Transform spawn;
            //find whether the next path will be straight, left or right
            int pathChoice= Random.Range(0,PathSpawnPoints.Length);
            //Debug.Log(pathChoice);
            var path = PathSpawnPoints[pathChoice];
            //Get offset between new path and old path
            int offset = (int)PreviousPath.transform.rotation.eulerAngles.y - (int)path.transform.rotation.eulerAngles.y;
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
                // Debug.Log( GameManager.getManager().getDirection().Count);
                // Debug.Break();
                return;
            }
            //Generate obsticle point
            int obsticleSpawn = Random.Range(0, ObsticleSpawnPoints.Length);
            Debug.Log(obsticleSpawn);
            spawn= ObsticleSpawnPoints[obsticleSpawn];
            Instantiate(spawn,spawn.transform.position,(spawn.transform.rotation));
            //Generate Power Ups
            int powerSpawn = Random.Range(0, PowerSpawnPoints.Length);
            spawn= PowerSpawnPoints[powerSpawn];
            Debug.Log(powerSpawn);
            Instantiate(spawn,spawn.transform.position,(spawn.transform.rotation));
            //Create Path
            Instantiate(Paths[0],path.transform.position,(path.transform.rotation));
            //Generate left border
  //          Debug.Log(offset);
            
            if((int)offset==-90||(int)offset==270)
            {
            //  Debug.Log("Right"+offset);
                var border= DangerousBorders[1];
                spawn = BorderSpawnPoints[1];
                Instantiate(border,spawn.position,spawn.rotation);
                GameManager.getManager().getDirection().Enqueue(GameManager.turnDirection.Right);
                // Debug.Log( GameManager.getManager().getDirection().Count);
                // Debug.Break();
                return;
            }
            //Generate right border for left turn
            if(offset==90||offset==-270)
            {
                
                var border= DangerousBorders[0];
                spawn = BorderSpawnPoints[0];
                Instantiate(border,spawn.position,spawn.rotation);
                //Add to direction queue
                GameManager.getManager().getDirection().Enqueue(GameManager.turnDirection.Left);
                // Debug.Log( GameManager.getManager().getDirection().Count);
                // Debug.Break();
                return;
            }
        }
    }
}