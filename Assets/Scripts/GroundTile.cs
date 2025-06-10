using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    private GroundSpawner groundspawner; // Corretto il nome

    public GameObject coinPrefab;

    public GameObject[] obstaclePrefabs;
    public Transform[] spawnpoints;

    public Vector3 vectObs;

    private void Awake()
    {
        groundspawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        SpawnObs();
        SpawnCoin();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            groundspawner.spawnTile();

            Destroy(gameObject, 5f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnObs()
    {
        int SpawnPrefab = Random.Range(0, obstaclePrefabs.Length);
        if (SpawnPrefab == 2)
        {
            Instantiate(obstaclePrefabs[SpawnPrefab], spawnpoints[0].transform.position, Quaternion.Euler(0, 90, 0), transform);
        }
        else
        {
            int ChooseSpawnPoint = Random.Range(0, spawnpoints.Length);
            vectObs = spawnpoints[ChooseSpawnPoint].transform.position;
            Instantiate(obstaclePrefabs[SpawnPrefab], vectObs, Quaternion.Euler(0, 90, 0), transform);
        }
    }

    public void SpawnCoin()
    {
        int randomColumn = Random.Range(0, spawnpoints.Length); //sceglie una colonna casuale tra 1-3

        
            Vector3 coinPosition = spawnpoints[randomColumn].position;
            coinPosition.y = 0; // Altezza del coin

        if (coinPosition != vectObs) //non voglio che il coin spawni nella stessa posizione dell'ostacolo
        {
            coinPosition.y = 1; // Altezza del coin
            GameObject tempCoin = Instantiate(coinPrefab);
            tempCoin.transform.position = coinPosition; //coin posizionato nella posizione desiderata
            tempCoin.transform.SetParent(transform); //il coin diventa figlio di GroundTile
        }
        
        
       
        
    }

   
}
