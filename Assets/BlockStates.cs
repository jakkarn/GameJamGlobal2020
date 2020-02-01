using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStates : MonoBehaviour
{
    private SpriteRenderer childSpriteRenderer;
    private SpriteRenderer spriteRenderer;

    public Sprite activeBlock;
    
    public Sprite unActiveBlock;

    private bool isSelected;

    private bool blockActive;

    private Color selectedColor = Color.blue;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        childSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        childSpriteRenderer.enabled = true;
    }

    public void Select()
    {
        isSelected = true;

        // Renders border
        spriteRenderer.enabled = true;
    }

    public void UnSelect()
    {
        isSelected = false;

        // Removes border
        spriteRenderer.enabled = false;
    }

     public void ActivateBlock()
    {
        blockActive = true;
        childSpriteRenderer.sprite = activeBlock;
    }

    public void DeactivateBlock()
    {
        blockActive = true;
        childSpriteRenderer.sprite = unActiveBlock;
    }
}
