using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemy;
    public float maxSpawnInterval;
    public Transform[] spawningPoints;

    float timeBetweenSpawns;
    
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenSpawns = maxSpawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if((PhotonNetwork.IsMasterClient) && (PhotonNetwork.CurrentRoom.PlayerCount > 1))
        { 
            if(timeBetweenSpawns <= 0.0f)
            {
                int randIndx = Random.Range(0, spawningPoints.Length);
                Vector3 randomSpawnPos = spawningPoints[randIndx].position;
                PhotonNetwork.Instantiate(enemy.name, randomSpawnPos, Quaternion.identity);
                timeBetweenSpawns = maxSpawnInterval;
            }
            else
            {
                timeBetweenSpawns -= Time.deltaTime;
            }
        }
    }
}
