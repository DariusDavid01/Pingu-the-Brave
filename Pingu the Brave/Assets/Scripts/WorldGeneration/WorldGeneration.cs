using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    //gameplay
    private float chunkSpawnZ;
    private Queue<Chunk> activeChunks = new Queue<Chunk>();
    private List<Chunk> chunkPool = new List<Chunk>();

    //configurable fields
    [SerializeField] private int firstChunkSpawnPosition = -10;
    [SerializeField] private int chunkOnScreen = 5;
    [SerializeField] private float despawnDistance = 5.0f;

    [SerializeField] private List<GameObject> chunkPrefab;
    [SerializeField] private Transform cameraTransform;

    private void Awake()
    {
        ResetWorld();
    }

    private void Start()
    {
        //check if we have an empty chunkPrefab list
        if (chunkPrefab.Count == 0)
            return;
        //try to assign the cameraTransform
        if (!cameraTransform)
            cameraTransform = Camera.main.transform;
    }

    public void ScanPosition()
    {
        float cameraZ = cameraTransform.position.z;
        Chunk lastChunk = activeChunks.Peek();

        if (cameraZ >= lastChunk.transform.position.z + lastChunk.chunkLength + despawnDistance)
        { 
            SpawnNewChunk();
            DeleteLastChunk();
        }
    }

    private void SpawnNewChunk()
    {
        //get a random index for which prefab to spawn
        int randomIndex = Random.Range(0, chunkPrefab.Count);

        //does it already exist within our pool
        Chunk chunk = chunkPool.Find( x => !x.gameObject.activeSelf && x.name == (chunkPrefab[randomIndex].name)+"(Clone)");

        //create a chunk, if were not able to find one to reuse
        if (!chunk)
        {
            GameObject go = Instantiate(chunkPrefab[randomIndex], transform);
            chunk = go.GetComponent<Chunk>();
        }

        //place the object, and show it
        chunk.transform.position = new Vector3(0, 0, chunkSpawnZ);
        chunkSpawnZ += chunk.chunkLength;

        //store the value, to reuse in our pool
        activeChunks.Enqueue(chunk);
        chunk.ShowChunk();
    }

    private void DeleteLastChunk()
    {
        Chunk chunk = activeChunks.Dequeue();
        chunk.HideChunk();
        chunkPool.Add(chunk);
    }

    public void ResetWorld()
    {
        //reset the chunkspawn z
        chunkSpawnZ = firstChunkSpawnPosition;

        for (int i = activeChunks.Count; i != 0 ; i--)
        {
            DeleteLastChunk();
        }
        for (int i = 0; i < chunkOnScreen; i++)
        {
            SpawnNewChunk();
        }
    }
}
