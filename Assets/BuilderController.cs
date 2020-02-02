using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderController : MonoBehaviour
{
    public GameObject gridStateGameObject;

    public int controller;

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

    private SpawnManager spawnManagerScript;
    public GameObject spawnManager;

    private bool player2 = true;
    private bool player1 = false;

    // Start is called before the first frame update
    void Start()
    {
        buildGridState = gridStateGameObject.GetComponent<BuildGridState>();
        spawnManagerScript = spawnManager.GetComponent<SpawnManager>();
        InitializeLocks();
    }

    // Update is called once per frame
    void Update()
    {
        //DebugJoystick();

        Direction dir;

        if (controller == 0)
        {
            dir = GetKeyMoveInput();
        }
        else
        {
             dir = GetControllerMoveInput();
        }

        UpdateMoveLocks();

        Move(dir);

        //Debug.Log($"Horizontal axis: {Input.GetAxis("Horizontal")}");

        // Listen to action input
        if (controller == 0 && Input.GetKeyDown("q"))
        {
            buildGridState.ToggleBuildBlock();
        }
        else if (PlaceBlockPressed())
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

    private Direction GetKeyMoveInput()
    {
        Direction dir;

        // var u = Input.GetAxisRaw("Vertical") == 1f;
        // var d = Input.GetAxisRaw("Vertical") == -1f;
        // var r = Input.GetAxisRaw("Horizontal") == 1f;
        // var l = Input.GetAxisRaw("Horizontal") == -1f;

        var u = Input.GetKey("w");
        var d = Input.GetKey("s");
        var l = Input.GetKey("a");
        var r = Input.GetKey("d");

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

    private Direction GetControllerMoveInput()
    {
        Direction dir;

        if(controller != 0) 
        {
            var xAxis = (!spawnManagerScript.player1T ? Input.GetAxis("Horizontal1") : 0) + (!spawnManagerScript.player2T ? Input.GetAxis("Horizontal2") : 0);
            var yAxis = (!spawnManagerScript.player1T ? Input.GetAxis("Vertical1" ) : 0) + (!spawnManagerScript.player2T ? Input.GetAxis("Vertical2") : 0);
            
            //Debug.Log($"controller1: {xAxis}, {yAxis}");
            
            var r = xAxis > 0;
            var l = xAxis < 0;
            var u = yAxis > 0;
            var d = yAxis < 0;

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
        }
        else
        {
            dir = Direction.none;
        }

        return dir;
    }

    private bool PlaceBlockPressed()
    {
        if (controller != 0) 
        {

            if (!spawnManagerScript.player1T ? Input.GetButtonDown("Placeblock1") : false || !spawnManagerScript.player2T ? Input.GetButtonDown("Placeblock2") : false) 
            {
                //Debug.Log($"controller1: {Input.GetButtonDown("Placeblock" + controller)}");
                return true;
            }
        }

        return false;
    }

    private void DebugJoystick()
    {
        for (int i = 1; i <= 5; ++i)
        {
            if(Input.GetButtonDown("Placeblock" + i))
            {
                //Debug.Log("Button" + i + "pressed");
            }
        }
    }
}
