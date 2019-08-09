using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRandomMaterial : MonoBehaviour {

    // Use this for initialization
    void Awake()
    {
        GetComponent<Renderer>().material = GetMaterial();
        if (GetComponent<Renderer>().material == null)
            Debug.Log("Material not found");
    }

    public Material GetMaterial()
    {
        //Choose wall material
        int x = Random.Range(0, 2);
        
        if (x == 0)
        {
            //Tile 1            
            if (Resources.Load("Materials/Tile 1") as Material != null)
            {
                return Resources.Load("Materials/Tile 1") as Material;
            }
        }
        else if (x == 1)
        {
            //Tile 2
            if (Resources.Load("Materials/Tile 2") as Material != null)
            {
                Debug.Log("Found 1");
                return Resources.Load("Materials/Tile 2") as Material;
            }
        }
        else
        {
            //Tile 3
            if (Resources.Load("Materials/Materials/tile3") as Material != null)
            {
                Debug.Log("Found 3");
                return Resources.Load("Materials/tile3") as Material;
            }
        }
        return null as Material;
    }

}

