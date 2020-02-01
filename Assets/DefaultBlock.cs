using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBlock : MonoBehaviour
{
    private BlockFormation parent;

    [HideInInspector]
    public bool toFarLeft;
    [HideInInspector]
    public bool toFarRight;
    [HideInInspector]
    public bool HitBottom;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.GetComponent<BlockFormation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "leftWall")
    //    {
    //        toFarLeft = true;
    //    }

    //    if (collision.tag == "rightWall")
    //    {
    //        toFarRight = true;
    //    }

    //    if (collision.tag =="bottomWall")
    //    {
    //        HitBottom = true;
    //    }

    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "leftWall")
    //    {
    //        toFarLeft = false;
    //    }

    //    if (collision.tag == "rightWall")
    //    {
    //        toFarRight = false;
    //    }

    //}
}
