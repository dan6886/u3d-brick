using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2Int originZero = new Vector2Int(-5, -9);


    GameObject[,] all_cell = new GameObject[24, 10];

    void Start()
    {
        Debug.Log(all_cell);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool isDownEmpty(int row, int col)
    {
        if (row <= 0)
        {
            return false;
        }

        return all_cell[row - 1, col] == null;
    }

    public void setLocation(GameObject tile)
    {
        var index = worldPositionToArrayIndex(tile.transform.position.x, tile.transform.position.y);

        if (all_cell[index.x, index.y] == null)
        {
            all_cell[index.x, index.y] = tile;
        }
        else
        {
            throw new ArgumentException("already set tile");
        }
    }

    public Vector2Int worldPositionToArrayIndex(int x, int y)
    {
        return new Vector2Int(y - originZero.y, x - originZero.x);
    }

    public Vector2Int worldPositionToArrayIndex(float x, float y)
    {
        return worldPositionToArrayIndex((int) x, (int) y);
    }

    public bool isCanMoveLeft(GameObject brick)
    {
        var isCanMoveLeft = true;
        foreach (Transform child in brick.transform)
        {
            var worldPosition = brick.transform.TransformPoint(child.localPosition);
            var cellIndex = worldPositionToArrayIndex((int) worldPosition.x, (int) worldPosition.y);

            if (cellIndex.y - 1 < 0)
            {
                isCanMoveLeft = false;
            }
        }

        return isCanMoveLeft;
    }

    public bool isCanMoveRight(GameObject brick)
    {
        var isCanMoveRight = true;
        foreach (Transform child in brick.transform)
        {
            var worldPosition = brick.transform.TransformPoint(child.localPosition);
            var cellIndex = worldPositionToArrayIndex((int) worldPosition.x, (int) worldPosition.y);

            if (cellIndex.y + 1 >= 10)
            {
                isCanMoveRight = false;
            }
        }

        return isCanMoveRight;
    }

    public bool isValidAt(int x, int y)
    {
        var isValid = true;
        var index = worldPositionToArrayIndex(x, y);
        if (index.y < 0 || index.y >= all_cell.GetLength(1))
        {
            return false;
        }

        if (index.x < 0)
        {
            return false;
        }

        return isValid;
    }
}