using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject CubePrefab;
    [SerializeField] private GameObject RectPrefab;
    [SerializeField] private GameObject CylPrefab;
    private Vector3 startPosition = new Vector3(0, 0.55f, 0);
    private Quaternion startRotation = Quaternion.identity;
    private float spawnDelay = 0.5f;
    private int numBlocks = 3;

    public enum Block {Cube, RectPrism, Cylinder};

    public void SpawnBlock() {
        StartCoroutine(SpawnBlockDelay());
    }

    
    IEnumerator SpawnBlockDelay() {
        yield return new WaitForSeconds(spawnDelay);
        RandomizeBlock();
    }

    public void RandomizeBlock() {
        Block block = (Block) Random.Range(0,numBlocks);
        switch(block) {
            case Block.Cube: 
                Instantiate(CubePrefab, startPosition, startRotation);
                break;
            case Block.RectPrism: 
                Instantiate(RectPrefab, startPosition, startRotation);
                break;
            case Block.Cylinder:
                Instantiate(CylPrefab, startPosition, startRotation);
                break;
            default:
                Instantiate(CubePrefab, startPosition, startRotation); // NEEDS TO STILL INSTANTIATE SOMETHING OTHERWISE IT CAN SOFTLOCK
                break;
        }
    }
}
