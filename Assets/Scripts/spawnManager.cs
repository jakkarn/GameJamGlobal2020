using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject blockFormationPrefab;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private Transform rightBorder;
    [SerializeField]
    private Transform leftBorder;
    [SerializeField]
    private Transform bottomBorder;

    private BlockFormation activeBlockFormation;

    // Start is called before the first frame update
    void Start()
    {
        instantiateNewFormation();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeBlockFormation && !activeBlockFormation.isActive)
        {
            activeBlockFormation = null;
            instantiateNewFormation();
        }
    }

    private void instantiateNewFormation()
    {
        var activeBlockForm = (GameObject)Instantiate(blockFormationPrefab, spawnPoint.position, Quaternion.identity);
        activeBlockFormation = activeBlockForm.GetComponent<BlockFormation>();
        activeBlockFormation.bottomBorder = bottomBorder;
        activeBlockFormation.leftBorder = leftBorder;
        activeBlockFormation.rightBorder = rightBorder;
    }
}
