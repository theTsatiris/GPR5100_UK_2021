using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;
    public float minX, minZ, maxX, maxZ;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 randomSpawnPos = new Vector3(Random.Range(minX, maxX), 1.5f, Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(player.name, randomSpawnPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
