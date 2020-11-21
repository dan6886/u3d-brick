using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2Int originZero = new Vector2Int(-5, -9);
    GameObject[,] all_cell = new GameObject[24, 10];
    [SerializeField] public Spwaner spwaner;
    private ArrayList rol = new ArrayList();
    private List<GameObject> downList = new List<GameObject>();

    void Start()
    {
        spwaner.spwan();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void getNewBrick()
    {
        spwaner.spwan();
    }

    public bool isDownEmpty(int row, int col)
    {
        if (row <= 0)
        {
            return false;
        }

        return all_cell[row - 1, col] == null;
    }

    /**
     * 对象插入到root里面并且返回对应的数组index
    *  
    */
    public Vector2Int setLocation(GameObject tile)
    {
        var index = worldPositionToArrayIndex(tile.transform.position.x, tile.transform.position.y);

        if (all_cell[index.x, index.y] == null)
        {
            all_cell[index.x, index.y] = tile;
            return index;
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

            if (!isValidAtIndex(cellIndex + new Vector2Int(0, -1)))
            {
                isCanMoveLeft = false;
            }

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
            if (!isValidAtIndex(cellIndex + new Vector2Int(0, 1)))
            {
                isCanMoveRight = false;
            }

            if (cellIndex.y + 1 >= 10)
            {
                isCanMoveRight = false;
            }
        }

        return isCanMoveRight;
    }

    public bool isValidAtWolrdPosition(int x, int y)
    {
        var index = worldPositionToArrayIndex(x, y);
        return isValidAtIndex(index);
    }

    public bool isValidAtIndex(Vector2Int index)
    {
        var isValid = true;
        if (index.y < 0 || index.y >= all_cell.GetLength(1))
        {
            return false;
        }

        if (index.x < 0)
        {
            return false;
        }

        if (all_cell[index.x, index.y] != null)
        {
            return false;
        }

        Debug.Log("isValid:" + isValid);
        return isValid;
    }

    public void checkTearDown(List<Vector2Int> needCheck)
    {
        HashSet<Int32> fullRow = new HashSet<int>();
        for (var i = 0; i < needCheck.Count; i++)
        {
            if (isFullRow(needCheck[i].x))
            {
                fullRow.Add(needCheck[i].x);
            }
        }

        Debug.Log(fullRow.Count);
        if (fullRow.Count != 0)
        {
            foreach (var row in fullRow)
            {
                modifyAboveDownStep(row);
            }
        }

        foreach (var o in downList)
        {
            var newPosition = o.transform.position + o.GetComponent<Tile>().downStep * Vector3.down;
            // o.transform.DOLocalMove(newPosition, 1).OnComplete(onDownComplete(o));
        }
    }

    private void onDownComplete(GameObject o)
    {
        
    }

    private void modifyAboveDownStep(int row)
    {
        for (int i = row + 1; i < all_cell.GetLength(0); i++)
        {
            for (int j = 0; j < all_cell.GetLength(1); j++)
            {
                var cell = all_cell[i, j];
                if (cell != null)
                {
                    cell.GetComponent<Tile>().downStep++;
                    if (!downList.Contains(cell))
                    {
                        downList.Add(cell);
                    }
                }
            }
        }
    }

    private bool isFullRow(int row)
    {
        for (int i = 0; i < all_cell.GetLength(1); i++)
        {
            if (all_cell[row, i] == null)
            {
                return false;
            }
        }

        return true;
    }
}