﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject blockFormationPrefab;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private Transform rightBorder;
    [SerializeField]
    private Transform leftBorder;
    [SerializeField]
    private Transform bottomBorder;

    private BlockFormation activeBlockFormation;

    // Start is called before the first frame update
    void Start()
    {
        var testGrid = new bool[5, 5]
        {
            { (Random.value > 0.5f), (Random.value > 0.5f), (Random.value > 0.5f), (Random.value > 0.5f), (Random.value > 0.5f) },
            { (Random.value > 0.5f), (Random.value > 0.5f), (Random.value > 0.5f), (Random.value > 0.5f), (Random.value > 0.5f) },
            { (Random.value > 0.5f), (Random.value > 0.5f), (Random.value > 0.5f), (Random.value > 0.5f), (Random.value > 0.5f) },
            { (Random.value > 0.5f), (Random.value > 0.5f), (Random.value > 0.5f), (Random.value > 0.5f), (Random.value > 0.5f) },
            { (Random.value > 0.5f), (Random.value > 0.5f), (Random.value > 0.5f), (Random.value > 0.5f), (Random.value > 0.5f) },
        };
        instantiateNewFormation(testGrid);
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public void instantiateNewFormation(bool[,] grid)
    {
        var activeBlockForm = (GameObject)Instantiate(blockFormationPrefab, spawnPoint.position, Quaternion.identity);
        activeBlockFormation = activeBlockForm.GetComponent<BlockFormation>();
        activeBlockFormation.startGrid = grid;
    }
}