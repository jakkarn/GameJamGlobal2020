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

    // public int CountSetValues()
    // {
    //     var setValueCount = 0;

    //     for (int x = 0; x < GetMaxY(); ++x)
    //     {
    //         for (int y = 0; y < GetMaxX(); ++y)
    //         {
    //             if (GetValue(new Vector2(x, y)).Equals(default(T)))
    //             {
    //                 setValueCount++;
    //             }
    //         }
    //     }
    //     return setValueCount;
    // }
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
    private float timeBetweenActivePositionChecks;

    // Position has true == block is build
    [HideInInspector]
    public Grid<bool> grid;

    public Vector2 activePosition = new Vector2(0,0);

    public Direction direction = Direction.none;

    public Direction storedDirection = Direction.none;

    private Grid<bool> buildBlockFormation;

    // public GameObject BlockCounterObject;
    // private BlockCounter BlockCounter;

    // Start is called before the first frame update
    void Start()
    {
        grid.Fill(new Vector2(gridWidth, gridHeight));
        // BlockCounter = BlockCounterObject.GetComponent<BlockCounter>();

        InvokeRepeating("UpdateActivePosition", 0, timeBetweenActivePositionChecks);
    }

    // Update is called once per frame
    void Update()
    {
        // Listen to moement input

        direction = Direction.none;

        if (Input.GetAxisRaw("Vertical") == 1f)
        {
            Debug.Log($"Up");
            setDirection(Direction.up);
        }
        if (Input.GetAxisRaw("Vertical") == -1f)
        {
            setDirection(Direction.down);
            Debug.Log($"Down");
        }
        if (Input.GetAxisRaw("Horizontal") == 1f)
        {
            setDirection(Direction.right);
            Debug.Log($"Right");
        }
        if (Input.GetAxisRaw("Horizontal") == -1f)
        {
            setDirection(Direction.left);
            Debug.Log($"Left");
        }


        // Listen to action input
        if (/*Input.GetButtonDown("A-Button") ||*/ Input.GetKeyDown("q"))
        {
            grid.Set(activePosition, !grid.GetValue(activePosition));
        }
    }

    public void setDirection(Direction d)
    {
        direction = d;
        
        if (direction != Direction.none) {
            storedDirection = direction;
        }

        // Debug.Log($"SetDirection: {direction}, {storedDirection}");
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

    public void UpdateActivePosition()
    {
        switch (direction)
        {
            case Direction.up:
                if (!CanMoveDirection(Direction.up))
                    return;
                activePosition.Set(activePosition.x, activePosition.y + 1);
                Debug.Log($"Update active postion: x {activePosition.x}, y {activePosition.y}");
                break;
            case Direction.down:
                if (!CanMoveDirection(Direction.down))
                    return;
                activePosition.Set(activePosition.x, activePosition.y - 1);
                Debug.Log($"Update active postion: x {activePosition.x}, y {activePosition.y}");
                break;
            case Direction.right:
                if (!CanMoveDirection(Direction.right))
                    return;
                activePosition.Set(activePosition.x + 1, activePosition.y);
                Debug.Log($"Update active postion: x {activePosition.x}, y {activePosition.y}");
                break;
            case Direction.left:
                if (!CanMoveDirection(Direction.left))
                    return;
                activePosition.Set(activePosition.x - 1, activePosition.y);
                Debug.Log($"Update active postion: x {activePosition.x}, y {activePosition.y}");
                break;
            // If none, fallback to stored value incase one was inputed between checks and then dropped
            case Direction.none:
                if (storedDirection != Direction.none) {
                    direction = storedDirection;
                    storedDirection = Direction.none;
                    UpdateActivePosition();
                }
                break;
        }
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
        // BlockCounter.RemoveCreatedFormationBlocks();
    }
}
