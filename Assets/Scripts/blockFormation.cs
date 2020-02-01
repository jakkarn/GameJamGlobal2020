﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFormation: MonoBehaviour
{
    [SerializeField]
    private GameObject DefaultBlockPrefab;
    [SerializeField]
    private GameObject SelectedBlockPrefab;

    [SerializeField]
    private float timeBetweenMoveDowns;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private Transform movePoint;

    public bool isActive;
    
    [SerializeField]
    private BuildGridState buildGridState;
    
    private GameObject[,] gridblocks;
    private float blockSizeX;
    private float blockSizeY;

    public bool[,] startGrid;
    private bool[,] currentGrid;

    private StaticBlockContainer staticBlockContainer;
    // Start is called before the first frame update
    void Start()
    {
        staticBlockContainer = FindObjectOfType<StaticBlockContainer>();

        buildGridState = FindObjectOfType<BuildGridState>();
        isActive = true;
        movePoint.parent = null;
        InvokeRepeating("moveDown", 0, timeBetweenMoveDowns);

        gridblocks = new GameObject[buildGridState.gridWidth, buildGridState.gridWidth];
        blockSizeX = DefaultBlockPrefab.GetComponent<Renderer>().bounds.size.x;
        blockSizeY = DefaultBlockPrefab.GetComponent<Renderer>().bounds.size.y;
        instantiateChildren();
    }

    // Update is called once per frame
    void Update()
    {
        movePoint.position = Vector2.MoveTowards(movePoint.position, transform.position, movementSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePoint.position) <= 0.5f && isActive)
        {
            if (Input.GetAxisRaw("Horizontal") == 1f && transform.position.x < 8)
            {
                transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f);
            }

            if (Input.GetAxisRaw("Horizontal") == -1f && transform.position.x > -8)
            {
                transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f);
            }

            if (Input.GetAxisRaw("Vertical") == -1f)
            {
                transform.position += new Vector3(0f, Input.GetAxisRaw("Vertical"));
            }
        }

        if (!(transform.position.y > -7))
        {
            isActive = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                staticBlockContainer.addStaticBlock(transform.GetChild(i).position);
            }
            Destroy(gameObject);
        }

        if (Input.GetButtonDown("z") == true)
        {
            currentGrid = turnPieceLeft(currentGrid);
            createGrid(currentGrid);
        }

        if (Input.GetButtonDown("x") == true)
        {
            currentGrid = turnPieceRight(currentGrid);
            createGrid(currentGrid);
        }

    }

    private void moveDown()
    {
        if (isActive)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 1);
        }
    }

    private void instantiateChildren()
    {
        var testGrid = new bool[5, 5]
        {
            {true,false,false,false,false },
            {false,true,false,false,false },
            {false,false,true,true,true },
            {false,false,false,false,false },
            {false,false,false,false,false },
        };

        if (startGrid != null)
        {
            currentGrid = flipGrid(startGrid);
        } else
        {
            currentGrid = flipGrid(testGrid);
        }

        createGrid(currentGrid);
    }

    private void createGrid(bool[,] grid)
    {
        for (int x = 0; x < buildGridState.gridWidth; ++x)
        {
            for (int y = 0; y < buildGridState.gridHeight; ++y)
            {
                if (gridblocks[x, y] != null)
                {
                    Destroy(gridblocks[x, y]);
                    gridblocks[x, y] = null;
                }
                
                if (grid[y, x])
                {
                    gridblocks[x, y] = Instantiate(DefaultBlockPrefab, new Vector2(transform.position.x + (x - buildGridState.gridWidth / 2) * blockSizeX, transform.position.y + (y - buildGridState.gridHeight / 2) * blockSizeY), Quaternion.identity, transform);
                }
                
            }
        }
    }

    private bool[,] flipGrid(bool[,] grid)
    {
       
        var counter = 0;
        var newGrid = new bool[grid.GetLength(0), grid.GetLength(1)];
        for (int i = grid.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = 0; j < grid.GetLength(1) - 1; j++)
            {
                newGrid[counter, j] = grid[i, j];
            }
            counter++;
        }
        return newGrid;
    }

    public bool[,] turnPieceRight(bool[,] gridToTurn)
    {
        var newGrid = new bool[gridToTurn.GetLength(0), gridToTurn.GetLength(1)];
        for (int y = 0; y < gridToTurn.GetLength(0); y++)
        {
            for (int x = 0; x < gridToTurn.GetLength(1); x++)
            {
                newGrid[gridToTurn.GetLength(0) - 1 - x, y] = gridToTurn[y, x];
            }
        }
        return newGrid;
    }

    public bool[,] turnPieceLeft(bool[,] gridToTurn)
    {
        var newGrid = new bool[gridToTurn.GetLength(0), gridToTurn.GetLength(1)];
        for (int y = 0; y < gridToTurn.GetLength(0); y++)
        {
            for (int x = 0; x < gridToTurn.GetLength(1); x++)
            {
                newGrid[x, gridToTurn.GetLength(0) - 1 - y] = gridToTurn[y, x];
            }
        }
        return newGrid;
    }

    // test comment
}
