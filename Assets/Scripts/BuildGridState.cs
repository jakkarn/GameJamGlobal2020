using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGridState : MonoBehaviour
{
    public int gridHeight = 0;
    public int gridWidth = 0;

    [HideInInspector]
    public bool[,] grid;

    public Vector2 activePosition = new Vector2(0,0);

    // Start is called before the first frame update
    void Start()
    {
        grid = new bool[gridWidth, gridHeight];
        grid[3,3] = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
