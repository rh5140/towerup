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

    enum Block {Cube, RectPrism, Cylinder};

    public void SpawnBlock() {
        StartCoroutine(SpawnBlockDelay());
    }

    
    IEnumerator SpawnBlockDelay() {
        yield return new WaitForSeconds(spawnDelay);
        RandomizeBlock();
    }

    void RandomizeBlock() {
        int block = Random.Range(0,3);
        switch(block) {
            case 0: 
                Instantiate(CubePrefab, startPosition, startRotation);
                break;
            case 1: 
                Instantiate(RectPrefab, startPosition, startRotation);
                break;
            case 2:
                Instantiate(CylPrefab, startPosition, startRotation);
                break;
            default:
                break;
        }
    }
}
