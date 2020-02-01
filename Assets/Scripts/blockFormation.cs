using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFormation: MonoBehaviour
{
    [SerializeField]
    private GameObject gridState;
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

    [HideInInspector]
    public Transform leftBorder;
    [HideInInspector]
    public Transform rightBorder;
    [HideInInspector]
    public Transform bottomBorder;

    public bool isActive;

    private BuildGridState buildGridState;
    private GameObject[,] gridblocks;
    private float blockSizeX;
    private float blockSizeY;

    private StaticBlockContainer staticBlockContainer;

    // Start is called before the first frame update
    void Start()
    {
        staticBlockContainer = FindObjectOfType<StaticBlockContainer>();

        buildGridState = gridState.GetComponent<BuildGridState>();
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
            if (Input.GetAxisRaw("Horizontal") == 1f && transform.position.x < rightBorder.position.x)
            {
                transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f);
            }

            if (Input.GetAxisRaw("Horizontal") == -1f && transform.position.x > leftBorder.position.x)
            {
                transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f);
            }

            if (Input.GetAxisRaw("Vertical") == -1f)
            {
                transform.position += new Vector3(0f, Input.GetAxisRaw("Vertical"));
            }
        }

        if (!(transform.position.y > bottomBorder.position.y))
        {
            isActive = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                staticBlockContainer.addStaticBlock(transform.GetChild(i).position);
            }
            Destroy(gameObject);
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
        for (int x = 0; x < buildGridState.gridWidth; ++x)
        {
            for (int y = 0; y < buildGridState.gridHeight; ++y)
            {
                gridblocks[x, y] = Instantiate(DefaultBlockPrefab, new Vector2(transform.position.x + (x - buildGridState.gridWidth/2) * blockSizeX, transform.position.y + (y - buildGridState.gridHeight/2) * blockSizeY), Quaternion.identity, transform);
            }
        }
    }

    // test comment
}
