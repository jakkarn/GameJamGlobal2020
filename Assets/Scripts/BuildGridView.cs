using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGridView : MonoBehaviour
{
    //[HideInspector]
    public GameObject GridState;
    public GameObject blockRenderer;
    public GameObject blockSizePrefab;

    //[SerializeField]
    private Grid<GameObject> gridblocks;
    private BuildGridState gridState;
    private Vector2 activeGridPosition = new Vector2();
    private float blockSizeX;
    private float blockSizeY;
    
    [SerializeField]
    private static float timeLeftDefault = 6.0f;
    private float timeLeft = timeLeftDefault;

    public float offsetGridX = -6.2f;
    public float offsetGridY = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        gridState = GridState.GetComponent<BuildGridState>();

        //Debug.Log($"{blockRenderer.name}");

        gridblocks.Fill(new Vector2(gridState.gridWidth, gridState.gridHeight));

        blockSizeX = blockSizePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        blockSizeY = blockSizePrefab.GetComponent<SpriteRenderer>().bounds.size.y;

        // Debug.Log($"view size: {blockSizeX}, {blockSizeY}");

        InstantiateGridView();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft < 0)
        {
            // Destroy old blocks?
            gridState.reset();
            timeLeft = timeLeftDefault;
        }

        // Update selected block incase change has been made.
        if (activeGridPosition.x != gridState.activePosition.x || gridState.activePosition.y != activeGridPosition.y)
        {
            // Unselect former active block
            gridblocks.GetValue(activeGridPosition).GetComponent<BlockStates>().UnSelect();

            // Select active block
            gridblocks.GetValue(gridState.activePosition).GetComponent<BlockStates>().Select();

            activeGridPosition = gridState.activePosition;
        }

        if (gridState.grid.HasChanged()) 
        {
            UpdateGridView();
        }
    }

    private void InstantiateGridView()
    {
        InstantiateGridBlocks();
    }

    private void InstantiateGridBlocks()
    {
         for (int x = 0; x < gridState.gridWidth; ++x)
        {
            for (int y = 0; y < gridState.gridHeight; ++y)
            {
                gridblocks.Set(new Vector2(x,y), Instantiate(blockRenderer, GridPosToUnityPos(x,y), Quaternion.identity));
            }
        }
    }

    private void UpdateGridView()
    {
        for (int x = 0; x < gridState.gridWidth; ++x)
        {
            for (int y = 0; y < gridState.gridHeight; ++y)
            {
                if (gridState.grid.GetValue(new Vector2(x, y))) //has block
                {
                    gridblocks.GetValue(new Vector2(x, y)).GetComponent<BlockStates>().ActivateBlock();
                }
                else
                {
                    gridblocks.GetValue(new Vector2(x, y)).GetComponent<BlockStates>().DeactivateBlock();
                }
            }
        }
    }

    private Vector3 GridPosToUnityPos(int x, int y)
    {
        return new Vector3(x*blockSizeX+offsetGridX, y*blockSizeY+offsetGridY, 0);
    }
}
