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
            int pathChoice= Random.range(1.Paths.Length);
            
            
        }
    }
}