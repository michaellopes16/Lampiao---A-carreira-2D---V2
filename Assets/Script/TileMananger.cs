using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TileMananger : MonoBehaviour {

    public GameObject [] tilePrefabs;
    public Transform playerTransform;
    private float spawnZ = -1.72f;
    private float tileLength = 16.09f;
    private int amnTilesIntheScreen = 3;
    private float saveZone = 7.0f;

    private int lastPrefabIndex = 0;
    public float speed;
    private float x;

    private List<GameObject> actviTiles;
	// Use this for initialization
	private void Start () {

        actviTiles = new List<GameObject>();

		//playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i=0;  i<  amnTilesIntheScreen;i++)
        {
            if (i<2)
            {
                SpawTite(0);
            }
            else
            {
                SpawTite();
            }
            
            
        }
    }
	
	// Update is called once per frame
	private void Update () {

        if (playerTransform.position.x - saveZone > (spawnZ- amnTilesIntheScreen*tileLength))
        {
            SpawTite();
            DeleteTile();

        }
	}

    private void SpawTite(int prefabsIndex = -1)
    {
        GameObject go;
        
            if (prefabsIndex == -1)
                go = Instantiate(tilePrefabs[RamdomPrefabIndex()]) as GameObject;
            else
                go = Instantiate(tilePrefabs[prefabsIndex]) as GameObject;



            if (actviTiles.Count != 0)
            {
          //  Debug.Log("X: " + actviTiles[actviTiles.Count - 1].gameObject.transform.position.x + tileLength + "Count" + actviTiles.Count);
            if (transform.position.x < -2)
                {
                    go.transform.position = new Vector3(actviTiles[actviTiles.Count-1].gameObject.transform.position.x + tileLength, transform.position.y, transform.position.z);
                }
        
          }
        actviTiles.Add(go);
       // Debug.Log("X: " + go.transform.position.x + tileLength);
            // x += speed * Time.deltaTime;
            // go.transform.position = new Vector3(x, transform.position.y, transform.position.z);
            lastPrefabIndex++;
            // go.transform.SetParent(transform);
            // go.transform.position = Vector3.forward * spawnZ;
            //spawnZ += tileLength;

            
        
    }

    private void DeleteTile()
    {
        Destroy(actviTiles[0]);
        actviTiles.RemoveAt(0);
        lastPrefabIndex--;
    }

    private int RamdomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;
        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
