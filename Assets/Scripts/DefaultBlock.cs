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

    private void OnDestroy()
    {
    }
}
