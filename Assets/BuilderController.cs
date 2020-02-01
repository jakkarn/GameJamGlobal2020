﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderController : MonoBehaviour
{
    public GameObject gridStateGameObject;
    private BuildGridState buildGridState;

    private Dictionary<Direction, bool> moveIsLocked = new Dictionary<Direction, bool>();
    private Dictionary<Direction, bool> moveIsInitialLocked = new Dictionary<Direction, bool>();
    private Dictionary<Direction, bool> moveKeyIsDepressed = new Dictionary<Direction, bool>();
    private Dictionary<Direction, bool> moveIsRepeating = new Dictionary<Direction, bool>();
   
    private Dictionary<Direction, float> moveTimers = new Dictionary<Direction, float>();
    private Dictionary<Direction, float> moveInitialTimers = new Dictionary<Direction, float>();

    [SerializeField]
    private float moveDelay;

    [SerializeField]
    private float moveInitialDelay;

    // Start is called before the first frame update
    void Start()
    {
        buildGridState = gridStateGameObject.GetComponent<BuildGridState>();
        
        InitializeLocks();
    }

    // Update is called once per frame
    void Update()
    {
        var dir = GetMovementInput();

        UpdateMoveLocks();

        Move(dir);

        // Listen to action input
        if (Input.GetKeyDown("q"))
        {
            buildGridState.ToggleBuildBlock();
        }
    }

    private void Move(Direction dir)
    {
        if (dir == Direction.none)
        {
            var dirs = new[] 
            { 
                Direction.up, 
                Direction.down, 
                Direction.right, 
                Direction.left
            };

            foreach (var tempDir in dirs)
            {
                moveKeyIsDepressed[tempDir] = false;
            }

            return;
        }

        moveKeyIsDepressed[dir] = true;

        if (moveIsRepeating[dir])
        {
            if (!moveIsLocked[dir])
            {
                // Debug.Log($"repeat move: {dir}");
                
                buildGridState.Move(dir); 
                moveIsLocked[dir] = true;
            }

            return;
        }

        if (!moveIsInitialLocked[dir])
        {
            // Debug.Log($"initial move: {dir}");
            
            buildGridState.Move(dir); 
            moveIsInitialLocked[dir] = true;

            return;
        }
    }

    private void UpdateMoveLocks()
    {
        var dirs = new[] 
        { 
            Direction.up, 
            Direction.down, 
            Direction.right, 
            Direction.left
        };

        foreach (var dir in dirs)
        {
            UpdateTimer(dir);
        }
    }

    private void UpdateTimer(Direction dir)
    {
        if (!moveKeyIsDepressed[dir])
        {
            moveInitialTimers[dir] = 0;
            moveIsInitialLocked[dir] = false;
            moveTimers[dir] = 0;
            moveIsLocked[dir] = false;
            moveIsRepeating[dir] = false;
            
            return;
        }

        if (moveIsRepeating[dir])
        {
            if (moveIsLocked[dir])
            {
                moveTimers[dir] += Time.deltaTime;

                if (moveTimers[dir] > moveDelay)
                {
                    // Debug.Log($"Reset repeat timer: {dir}");

                    moveIsLocked[dir] = false;
                    moveTimers[dir] = 0;
                }
                
                return;
            }
        }

        if (moveIsInitialLocked[dir])
        {
            if (moveInitialTimers[dir] > moveInitialDelay)
            {
                // Debug.Log($"Finished initial move: {dir}");
                
                moveIsInitialLocked[dir] = false;
                moveInitialTimers[dir] = 0;

                moveIsRepeating[dir] = true;
            }
            else
            {
                moveInitialTimers[dir] += Time.deltaTime;
            }

            return;
        }
    }

    private void InitializeLocks()
    {
        var dirs = new[] 
        { 
            Direction.up, 
            Direction.down, 
            Direction.right, 
            Direction.left
        };

        foreach (var dir in dirs)
        {
            moveIsLocked[dir] = false;
            moveIsInitialLocked[dir] = false;
            moveTimers[dir] = 0;
            moveInitialTimers[dir] = 0;
            moveKeyIsDepressed[dir] = false;
        }
    }

    private Direction GetMovementInput()
    {
        Direction dir;

        var u = Input.GetAxisRaw("Vertical") == 1f;
        var d = Input.GetAxisRaw("Vertical") == -1f;
        var r = Input.GetAxisRaw("Horizontal") == 1f;
        var l = Input.GetAxisRaw("Horizontal") == -1f;

        if (r && l || d && u)
        {
            dir = Direction.none;
        }
        else if (u && !d)
        {
            dir = Direction.up;
        }
        else if (d && !u)
        {
            dir = Direction.down;
        }
        else if (r && !l)
        {
            dir = Direction.right;
        }
        else if (l && !r)
        {
            dir = Direction.left;
        }
        else
        {
            dir = Direction.none;
        }

        return dir;
    }
}