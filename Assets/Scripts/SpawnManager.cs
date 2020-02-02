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

    public GameObject GridState;

    private BuildGridState gridState;

    // Start is called before the first frame update
    void Start()
    {

        instantiateNewFormation(null);

        gridState = GridState.GetComponent<BuildGridState>();
    }

    // Update is called once per frame
    void Update()
    {
        var grid = gridState.GetNewFormation();
        if (grid != null)
        {
            instantiateNewFormation(grid);
        }
    }

    public void instantiateNewFormation(bool [,] grid)
    {
        var testGrid = new bool[5, 5]
       {
            
            {false, false, false, false,false },
            {false, false, false, false,false },
            {false, false, false, false,false },
            {true, false, false, false,false },
            {true, true, true, true,false },
       };

        var activeBlockForm = (GameObject)Instantiate(blockFormationPrefab, spawnPoint.position, Quaternion.identity);
        activeBlockFormation = activeBlockForm.GetComponent<BlockFormation>();
        activeBlockFormation.startGrid = grid != null ? activeBlockFormation.turnPieceRight(grid) : testGrid;
    }
}
