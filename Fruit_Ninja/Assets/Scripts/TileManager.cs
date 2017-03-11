using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public GameObject[] tilePrefabs;
    private int lastTilePrefabIndex = 0;

    private Transform playerTransorm;
    private float spawnZ = 100.0f;
    private float tileLength = 100.0f;
    private float safeZone = 15.0f;
    private int animTilesOnTheScreen = 7;

    private List<GameObject> activeTiles = new List<GameObject>();

	// Use this for initialization
	void Start () {
        playerTransorm = GameObject.FindGameObjectWithTag("Player").transform;
        
        for(int i=0; i < animTilesOnTheScreen; i++)
        {
            if (i < 2)
                SpawnTile(0);

            SpawnTile();
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(playerTransorm.position.z - safeZone > spawnZ - animTilesOnTheScreen * tileLength)
        {
            SpawnTile();
            DestroyTile();
        }
	}

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        if(prefabIndex == -1)
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        else
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;

        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }

    private void DestroyTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastTilePrefabIndex;
        while(randomIndex == lastTilePrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastTilePrefabIndex = randomIndex;

        return randomIndex;
    }
}
