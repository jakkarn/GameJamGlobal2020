using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Grid<T> {
    private T[,] grid;

    private bool changed;

    public bool IsEmpty() {
        return grid == null;
    }

    public void Fill(Vector2 vector) {
        grid = new T[(int)vector.x, (int)vector.y];

        changed = true;
    }

    public void Set(T[,] newGrid)
    {
        grid = newGrid;

        changed = true;
    }

    public void Set(Vector2 vector, T status) {
        grid[(int)vector.x, (int)vector.y] = status;

        changed = true;
    }

    public bool HasChanged() {
        var hasChanged = changed;
        changed = false;

        return hasChanged;
    }

    public T GetValue(Vector2 vector) {
        if (grid == null) {
            return default(T);
        }
        return grid[(int)vector.x, (int)vector.y];
    }

    public int GetMaxX() {
        return grid.GetLength(0);
    }

    public int GetMaxY() {
        return grid.GetLength(1);
    }

    public T[,] GetInnerGrid() {
        return grid;
    }

    public bool Exists(Vector2 vector) {
        return this.GetMaxX() < vector.x && vector.x >= 0 &&
            this.GetMaxY() > vector.y && vector.y >= 0;
    }

    public void Clear() {
        grid = null;
    }

    public int CountSetValues()
    {
        var setValueCount = 0;

        for (int x = 0; x < GetMaxY(); ++x)
        {
            for (int y = 0; y < GetMaxX(); ++y)
            {
                if (GetValue(new Vector2(x, y)).Equals(default(T)))
                {
                    setValueCount++;
                }
            }
        }
        return setValueCount;
    }
}

public enum Direction {
    none,
    up,
    down,
    left,
    right
}

public class BuildGridState : MonoBehaviour
{
    public int gridHeight = 0;
    public int gridWidth = 0;

    [SerializeField]
    private float moveDelay;
    
    [SerializeField]
    private float buildDelay;
    
    private bool moveIsLocked;
    private bool buildIsLocked;
    private float moveTimer;
    private float buildTimer;

    // Position has true == block is build
    [HideInInspector]
    public Grid<bool> grid;

    public Vector2 activePosition = new Vector2(0,0);

    public Direction direction = Direction.none;

    public Direction storedDirection = Direction.none;

    private Grid<bool> buildBlockFormation;

    private BlockCounter blockCounter = null;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log($"Start {nameof(BuildGridState)}");

        grid.Fill(new Vector2(gridWidth, gridHeight));
        blockCounter = FindObjectOfType<BlockCounter>();

        grid.GetMaxX();
        grid.GetMaxY();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CanMoveDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.up:
                return (activePosition.y + 1) < grid.GetMaxY();
            case Direction.down:
                return activePosition.y > 0;
            case Direction.right:
                return (activePosition.x + 1) < grid.GetMaxX();
            case Direction.left:
                return activePosition.x > 0;
        }

        return false;
    }

    public void reset()
    {
        this.NewFormation();
        grid.Fill(new Vector2(gridWidth, gridHeight));
    }

    public void ToggleBuildBlock()
    {
        moveIsLocked = true;
        grid.Set(activePosition, !grid.GetValue(activePosition));
    }

    public void Move(Direction dir)
    {
        if (!CanMoveDirection(dir))
        {
            return;
        }

        if (dir != Direction.none)
        {
            //Debug.Log($"INPUT: {dir}");
        }

        switch (dir)
        {
            case Direction.up:
                activePosition.Set(activePosition.x, activePosition.y + 1);
                // Debug.Log($"Update active postion: x {activePosition.x}, y {activePosition.y}");
                break;
            case Direction.down:
                activePosition.Set(activePosition.x, activePosition.y - 1);
                // Debug.Log($"Update active postion: x {activePosition.x}, y {activePosition.y}");
                break;
            case Direction.right:
                activePosition.Set(activePosition.x + 1, activePosition.y);
                // Debug.Log($"Update active postion: x {activePosition.x}, y {activePosition.y}");
                break;
            case Direction.left:
                activePosition.Set(activePosition.x - 1, activePosition.y);
                // Debug.Log($"Update active postion: x {activePosition.x}, y {activePosition.y}");
                break;
            // If none, fallback to stored value incase one was inputed between checks and then dropped
            default:
                break;
        }

        moveIsLocked = true;
    }

    public bool[,] GetNewFormation()
    {
        var newFormation = buildBlockFormation.GetInnerGrid();
        buildBlockFormation.Clear();

        return newFormation;
    }

     public void NewFormation()
    {
        buildBlockFormation = grid;
<<<<<<< HEAD
        //BlockCounter.RemoveCreatedFormationBlocks(buildBlockFormation.CountSetValues());
=======
        blockCounter.RemoveCreatedFormationBlocks(buildBlockFormation.CountSetValues());
>>>>>>> 0d28d3484e80b7de2dec01a2f47243801e794fbc
    }
}
