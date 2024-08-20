using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// common (3-4 raffle tickets): wblock 1
// uncommon (2 raffle tickets): wblock 3,4,7
// somewhat rare (1 raffle ticket): everything else

public class BlockSpawner : MonoBehaviour
{

    [SerializeField] public GameObject[] blocks;
    private float spawnDelay = 0.5f;

    public void SpawnBlock() {
        StartCoroutine(SpawnBlockDelay());
    }

    IEnumerator SpawnBlockDelay() {
        yield return new WaitForSeconds(spawnDelay);
        RandomizeBlock();
    }
    
    public void RandomizeBlock() {
        int rand = Random.Range(0, blocks.Length);
        Instantiate(blocks[rand]);
    }
}
