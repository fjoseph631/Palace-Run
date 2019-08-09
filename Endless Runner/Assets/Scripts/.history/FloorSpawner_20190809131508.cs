using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FloorSpawner : MonoBehaviour {
     //Path Spawn Points and the paths to be placed
    public Transform[] PathSpawnPoints;
    public GameObject[] Paths;
    //The previous path - used to determine rotation
    public GameObject previousPath;
    //Border Spawn Points and the paths to be placed
    public GameObject[] DangerousBorders;
    public Transform[] BorderSpawnPoints;
    //Obsticle Spawn Points and the obsticles to be placed
    public GameObject[] PowerUps;
    public Transform[] PowerSpawnPoints;
    //Power Up Spawn Points and the rewards to be placed
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
            var path = PathSpawnPoints[pathChoice];
            //Get offset between new path and old path
            int offset = (int)previousPath.transform.rotation.eulerAngles.y - (int)path.transform.rotation.eulerAngles.y;
            //Reduce offset to acceptable range
            while(offset>360)
            {
                offset-=360;
            }
            //Straight
            if(offset==0)
            {
                //Debug.Log("Trigger Hit");
                //Create Path
                Instantiate(Paths[1],path.transform.position,(path.transform.rotation));
                //Add to queue
                GameManager.getManager().getDirection().Enqueue(GameManager.turnDirection.Straight);
                return;
            }

            //Generate obsticle point and obsticle
            int obsticleSpawn = Random.Range(0, ObsticleSpawnPoints.Length);
            spawn= ObsticleSpawnPoints[obsticleSpawn];
            int element= Random.Range(0,Obsticles.Length);
            Instantiate(Obsticles[element],spawn.transform.position,(spawn.transform.rotation));
            //Generate Power Ups
            int powerSpawn = Random.Range(0, PowerSpawnPoints.Length);
            spawn= PowerSpawnPoints[powerSpawn];
            element= Random.Range(0,PowerUps.Length);
            Instantiate(PowerUps[element],spawn.transform.position,(spawn.transform.rotation));
            Instantiate(spawn,spawn.transform.position,(spawn.transform.rotation));
            
            //Create Path
            Instantiate(Paths[0],path.transform.position,(path.transform.rotation));
            
            //Generate left border for right turn
            if((int)offset==-90||(int)offset==270)
            {
                //Get border and placement
                var border= DangerousBorders[1];
                spawn = BorderSpawnPoints[1];
                //Create border
                Instantiate(border,spawn.position,spawn.rotation);
                //Add to queue
                GameManager.getManager().getDirection().Enqueue(GameManager.turnDirection.Right);
                // Destroy Object    
                Destroy(this);
                return;
            }
            //Generate right border for left turn
            if(offset==90||offset==-270)
            {
                //Get border
                var border= DangerousBorders[0];
                spawn = BorderSpawnPoints[0];
                //Create border
                Instantiate(border,spawn.position,spawn.rotation);
                //Add to direction queue
                GameManager.getManager().getDirection().Enqueue(GameManager.turnDirection.Left);
                //Destroy Object
                 Destroy(this);
                return;
            }
        }
    }
}