using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockFormation: MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        movePoint.parent = null;
        InvokeRepeating("moveDown", 0, timeBetweenMoveDowns);
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
        }

    }

    private void moveDown()
    {
        if (isActive)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 1);
        }
    }
}
