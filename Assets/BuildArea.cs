using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildArea : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnPoints;

    [SerializeField]
    private GameObject visualBlockPrefab;

    [SerializeField]
    private Transform visualBlockContainer;

    [SerializeField]
    private int totalNumberOfBlocks = 30;

    [SerializeField]
    private GameObject destroyParticle;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(initSpawnRoutine());
    }

    IEnumerator initSpawnRoutine()
    {
        for (int i = 0; i < totalNumberOfBlocks; i++)
        {
            var randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            var block = Instantiate(visualBlockPrefab, randomSpawnPoint.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            block.transform.parent = visualBlockContainer;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void removeVisualBlocks(int numberOfBlocks)
    {
        StartCoroutine(removeBlocks(numberOfBlocks));
    }

    IEnumerator removeBlocks(int numberOfBlocks)
    {
        for (int i = 0; i < numberOfBlocks; i++)
        {
            var radnomNumb = Random.Range(0, visualBlockContainer.childCount - 1);
            Instantiate(destroyParticle, visualBlockContainer.GetChild(radnomNumb).position, Quaternion.identity);
            Destroy(visualBlockContainer.GetChild(radnomNumb).gameObject);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void spawnVisualBlocks(int numberOfBlocks)
    {
        StartCoroutine(spawnBlocks(numberOfBlocks));
    }

    IEnumerator spawnBlocks(int numberOfBlocks)
    {
        for (int i = 0; i < numberOfBlocks; i++)
        {
            var randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            var block = Instantiate(visualBlockPrefab, randomSpawnPoint.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            block.transform.parent = visualBlockContainer;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
