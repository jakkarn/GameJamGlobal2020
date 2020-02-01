using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGridView : MonoBehaviour
{
    //[HideInspector]
    public GameObject GridState;
    public GameObject DefaultBlockPrefab;
    public GameObject SelectedBlockPrefab;

    //[SerializeField]
    private GameObject[,] gridblocks;
    private BuildGridState gridState;
    private Vector2 activeGridPosition;
    private float blockSizeX;
    private float blockSizeY;

    // Start is called before the first frame update
    void Start()
    {
        gridState = GridState.GetComponent<BuildGridState>();

        activeGridPosition = gridState.activePosition;

        gridblocks = new GameObject[gridState.gridWidth, gridState.gridWidth];

        blockSizeX = DefaultBlockPrefab.GetComponent<Renderer>().bounds.size.x;
        blockSizeY = DefaultBlockPrefab.GetComponent<Renderer>().bounds.size.y;

        InstantiateGridView();
    }

    // Update is called once per frame
    void Update()
    {
        activeGridPosition = gridState.activePosition;
    }

    private void InstantiateGridView()
    {
        Debug.Log($"x size of default: {DefaultBlockPrefab.GetComponent<Renderer>().bounds.size.x}");
        Debug.Log($"x size of selected: {SelectedBlockPrefab.GetComponent<Renderer>().bounds.size.x}");

        InstantiateGridBlocks();

        UpdateGridView();
    }

    private void InstantiateGridBlocks()
    {
         for (int x = 0; x < gridState.gridWidth; ++x)
        {
            for (int y = 0; y < gridState.gridHeight; ++y)
            {
                if (activeGridPosition.x == x && activeGridPosition.y == y) //is selected
                {
                    gridblocks[x,y] = Instantiate(SelectedBlockPrefab, GridPosToUnityPos(x,y), Quaternion.identity);
                }
                else
                {
                    gridblocks[x,y] = Instantiate(DefaultBlockPrefab, GridPosToUnityPos(x,y), Quaternion.identity);
                }
            }
        }
    }


    private void UpdateGridView()
    {
        for (int x = 0; x < gridState.gridWidth; ++x)
        {
            for (int y = 0; y < gridState.gridHeight; ++y)
            {
                if (gridState.grid[x,y]) //has block
                {

                }
                else
                {
                    
                }
            }
        }
    }

    private Vector3 GridPosToUnityPos(int x, int y)
    {
        return new Vector3(x*blockSizeX, y*blockSizeY, 0);
    }
}
