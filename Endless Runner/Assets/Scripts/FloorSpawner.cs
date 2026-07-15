using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
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
        if (hit.gameObject.tag == Constants.PlayerTag)
        {
            Transform spawn;

            int pathChoice = Random.Range(0, PathSpawnPoints.Length);
            var path = PathSpawnPoints[pathChoice];
            float offset = Mathf.DeltaAngle(
                previousPath.transform.rotation.eulerAngles.y,
                path.transform.rotation.eulerAngles.y
            );

            if (Mathf.Approximately(offset, 0f))
            {
                Instantiate(Paths[1], path.transform.position, path.transform.rotation);
                GameManager
                    .getManager()
                    .getDirection()
                    .Enqueue(GameManager.turnDirection.Straight);
                Destroy(this);
                return;
            }

            int obsticleSpawn = Random.Range(0, ObsticleSpawnPoints.Length);
            spawn = ObsticleSpawnPoints[obsticleSpawn];
            int element = Random.Range(0, Obsticles.Length);
            Instantiate(Obsticles[element], spawn.transform.position, spawn.transform.rotation);
            int powerSpawn = Random.Range(0, PowerSpawnPoints.Length);
            spawn = PowerSpawnPoints[powerSpawn];
            element = Random.Range(0, PowerUps.Length);
            Instantiate(PowerUps[element], spawn.transform.position, spawn.transform.rotation);

            Instantiate(Paths[0], path.transform.position, path.transform.rotation);

            if (Mathf.Approximately(offset, -90f))
            {
                var border = DangerousBorders[1];
                spawn = BorderSpawnPoints[1];
                Instantiate(border, spawn.position, spawn.rotation);
                GameManager.getManager().getDirection().Enqueue(GameManager.turnDirection.Right);
                Destroy(this);
                return;
            }

            if (Mathf.Approximately(offset, 90f))
            {
                var border = DangerousBorders[0];
                spawn = BorderSpawnPoints[0];
                Instantiate(border, spawn.position, spawn.rotation);
                GameManager.getManager().getDirection().Enqueue(GameManager.turnDirection.Left);
                Destroy(this);
                return;
            }

            Debug.LogWarning("FloorSpawner: unhandled path offset " + offset);
            GameManager.getManager().getDirection().Enqueue(GameManager.turnDirection.Straight);
            Destroy(this);
        }
    }
}
