﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Field : MonoBehaviour {

    public Transform dirtPrefab;
    public Sprite[] terrainSprites;
    public int rows;
    public int columns;
    private int size = 1;
    private Transform[,] squares;
    public Transform[] characterPrefabs;

    void Start() {
        squares = new Transform[columns, rows];
        for (int y = -rows / 2; y < rows - rows / 2; y++) {
            for (int x = -columns / 2; x < columns - columns / 2; ++x) {
                Transform dirt = Instantiate(dirtPrefab);
                dirt.parent = transform;
                dirt.position = new Vector3(x * size, y, -1);
                squares[x + columns / 2, y + rows / 2] = dirt;
                int result = UnityEngine.Random.Range(0, terrainSprites.Length);
                dirt.GetComponent<SpriteRenderer>().sprite = terrainSprites[result];
            }
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !GameMaster.started) {
            int x = (int)Math.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
            int y = (int)Math.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            if(GameMaster.spaceIsClear(x, y)) {
                Transform character = Instantiate(characterPrefabs[GameMaster.currentType]);
                character.position = new Vector3(x, y + 0.1f, -2);
                GameMaster.addCharacter(character.gameObject.GetComponent<Character>());
                //print(x + "-> X: " + character.GetComponent<Character>().getX() + ", " + (int)(y + 0.1f) + " -> Y: " + character.GetComponent<Character>().getY());
            }
        }
    }

}
