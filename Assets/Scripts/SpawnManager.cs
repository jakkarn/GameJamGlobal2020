using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public float timeBetweenMoveDowns = 1f;

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
    public bool player1T = true;
    public bool player2T = false;

    public GameObject GridState;

    public GameObject Player;
    private BuilderController playerScript;

    private BuildGridState gridState;

    private int counter = 1;

    // Start is called before the first frame update
    void Start()
    {

        instantiateNewFormation(null);

        gridState = GridState.GetComponent<BuildGridState>();
        playerScript = Player.GetComponent<BuilderController>();
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
            {true, true, false, false,false },
       };
        if (counter % 5 == 0)
        {
            // timeBetweenMoveDowns -= 0.1f;
        }
        if (counter % 5 == 0)
        {
            SwitchPlayer();
        }
        // grid = null;
        var activeBlockForm = (GameObject)Instantiate(blockFormationPrefab, spawnPoint.position, Quaternion.identity);
        activeBlockFormation = activeBlockForm.GetComponent<BlockFormation>();
        activeBlockFormation.startGrid = grid != null ? activeBlockFormation.turnPieceRight(grid) : testGrid;
        
        counter++;
        
    }

    private void SwitchPlayer()
    {
        player1T = !player1T;
        player2T = !player2T;
    }
}
