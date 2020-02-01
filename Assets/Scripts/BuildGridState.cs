using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Grid<T> {
    private T[,] grid;

    private bool changed;

    public void Fill(Vector2 vector) {
        grid = new T[(int)vector.x, (int)vector.y];
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

    public bool Exists(Vector2 vector) {
        return this.GetMaxX() < vector.x && vector.x >= 0 &&
            this.GetMaxY() > vector.y && vector.y >= 0;
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
    private float timeBetweenActivePositionChecks;

    // Position has true == block is build
    [HideInInspector]
    public Grid<bool> grid;

    public Vector2 activePosition = new Vector2(0,0);

    public Direction direction = Direction.none;

    public Direction storedDirection = Direction.none;

    // Start is called before the first frame update
    void Start()
    {
        grid.Fill(new Vector2(gridWidth, gridHeight));
        grid.Set(new Vector2(3, 3), true);

        InvokeRepeating("UpdateActivePosition", 0, timeBetweenActivePositionChecks);
    }

    // Update is called once per frame
    void Update()
    {
        // Listen to moement input

        direction = Direction.none;

        if (Input.GetAxisRaw("Vertical") == 1f)
        {
            setDirection(Direction.up);
        }
        if (Input.GetAxisRaw("Vertical") == -1f)
        {
            setDirection(Direction.down);
        }
        if (Input.GetAxisRaw("Horizontal") == 1f)
        {
            setDirection(Direction.right);
        }
        if (Input.GetAxisRaw("Horizontal") == -1f)
        {
            setDirection(Direction.left);
        }

        // Listen to action input
        if (/*Input.GetButtonDown("A-Button") ||*/ Input.GetKeyDown("a"))
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
                // if (storedDirection != Direction.none) {
                //     direction = storedDirection;
                //     storedDirection = Direction.none;
                //     UpdateActivePosition();
                // }
                break;
        }

        
    }
}
