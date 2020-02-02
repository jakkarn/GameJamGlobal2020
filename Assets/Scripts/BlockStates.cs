using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStates : MonoBehaviour
{
    private SpriteRenderer blockHolderRenderer;
    private SpriteRenderer selectionOutlineRenderer;
    private SpriteRenderer blockRenderer;

    public Sprite activeBlock;
    
    public Sprite unActiveBlock;

    private bool isSelected;

    private bool blockActive;

    private Color selectedColor = Color.blue;

    // Start is called before the first frame update
    void Start()
    {
        LoadRenderers();
        blockHolderRenderer.enabled = true;
    }

    public void Select()
    {
        isSelected = true;

        // Renders border
        selectionOutlineRenderer.enabled = true;
    }

    public void UnSelect()
    {
        isSelected = false;

        // Removes border
        selectionOutlineRenderer.enabled = false;
    }

    public void ActivateBlock()
    {
        //blockActive = true;
        blockRenderer.enabled = true;
    }

    public void DeactivateBlock()
    {
        //blockActive = false; 
        blockRenderer.enabled = false;
    }

    private void LoadRenderers()
    {
        if (blockRenderer is null || blockHolderRenderer is null || selectionOutlineRenderer is null)
        {
            var renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();

            blockHolderRenderer = renderers[0];
            //Debug.Log($"{blockHolderRenderer.name}"); 

            selectionOutlineRenderer = renderers[1];
            //Debug.Log($"{selectionOutlineRenderer.name}");

            blockRenderer = renderers[2];
            //Debug.Log($"{blockRenderer.name}");

        }
    }
}
