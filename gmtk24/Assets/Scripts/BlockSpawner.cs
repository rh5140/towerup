using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// common (3-4 raffle tickets): wblock 1
// uncommon (2 raffle tickets): wblock 3,4,7
// somewhat rare (1 raffle ticket): everything else

public class BlockSpawner : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] public GameObject[] blocks;
    //[SerializeField] public List<GameObject> blocks;
    [SerializeField] public int[] weights;
    private float spawnDelay = 0.5f;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
    }

    public void SpawnBlock() {
        StartCoroutine(SpawnBlockDelay());
    }

    IEnumerator SpawnBlockDelay() {
        yield return new WaitForSeconds(spawnDelay);
        if (gm.IsRandomWeighted()) RandomizeBlockWeighted();
        else RandomizeBlock();
    }
    
    public void RandomizeBlock() {
        //int rand = Random.Range(0, blocks.Length);
        int rand = Random.Range(0, blocks.Length);
        Instantiate(blocks[rand]);
    }

    public void RandomizeBlockWeighted() { // Weighted random based on the weights list
        if (blocks.Length != weights.Length)
        {
            RandomizeBlock(); // Error in weights, just use pure random instead
            return;
        }

        int totalWeight =  0;
        int totalBlocks = blocks.Length;
        foreach (var w in weights)
        {
            totalWeight += w; // Find the total weight
        }
        int rand = Random.Range(1, totalWeight + 1); // Store a random weighted int from 1 to totalWeight
        int blockIndex = 0;
        foreach (var w in weights)
        {
            rand -= w; // Subtract the weight from the random number
            if(rand <= 0) // This is the correct weighting!
            {
                Instantiate(blocks[blockIndex]);
                return;
            }
            blockIndex++; // Move onto the next index
        }
    }
}
