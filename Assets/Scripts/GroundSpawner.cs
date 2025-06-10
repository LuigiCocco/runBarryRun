using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    
    public GameObject groundTilePrefab;
    Vector3 nextspawnPoint;

    public void spawnTile()
    {
        GameObject tempGround = Instantiate(groundTilePrefab, nextspawnPoint, Quaternion.identity);
        nextspawnPoint = tempGround.transform.GetChild(1).transform.position;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            spawnTile();
        }
        
    }


}
