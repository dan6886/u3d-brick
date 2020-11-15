using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class Brick : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveDuration = 0f;
    public float stepDuration = 1f;

    void Start()
    {
        // transform.localEulerAngles = new Vector3(0, 0, 90);
        // transform.DORotate(new Vector3(0, 0, 90), 0.5f);

        Invoke(nameof(tryMoveDown), 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            tryMoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            tryMoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            tryChange();
        }
    }

    private void doMoveDown()
    {
        Debug.Log("moveDown");
        var oldPosition = transform.position;
        var newPosition = oldPosition + Vector3.down;
        transform.DOLocalMove(newPosition, moveDuration).OnComplete(onCompleteMoveDown);
    }

    private void tryMoveDown()
    {
        if (isCanMoveDown())
        {
            doMoveDown();
        }
        else
        {
            //降落結束,开始拆分到数组   
            Transform parent = GameObject.Find("root").transform;
            var manager = GetComponentInParent<GameManager>();
            while (transform.childCount != 0)
            {
                var child = transform.GetChild(0);
                child.parent = parent;
                manager.setLocation(child.gameObject);
            }

            Destroy(gameObject);
            
            manager.getNewBrick();
        }
    }

    private bool isCanMoveDown()
    {
        var manager = GetComponentInParent<GameManager>();
        var isCan = true;
        foreach (Transform child in transform)
        {
            var worldPosition = transform.TransformPoint(child.localPosition);
            var cellIndex = manager.worldPositionToArrayIndex((int) worldPosition.x, (int) worldPosition.y);
            if (!manager.isDownEmpty((int) cellIndex.x, (int) cellIndex.y))
            {
                isCan = false;
                return isCan;
            }
        }

        return isCan;
    }

    private void onCompleteMoveDown()
    {
        Invoke(nameof(tryMoveDown), stepDuration);
    }

    private void tryMoveLeft()
    {
        var manager = GetComponentInParent<GameManager>();

        if (manager.isCanMoveLeft(gameObject))
        {
            var oldPosition = transform.position;
            var newPosition = oldPosition + Vector3.left;
            transform.DOLocalMove(newPosition, 0f);
        }
    }

    private void tryMoveRight()
    {
        var manager = GetComponentInParent<GameManager>();

        if (manager.isCanMoveRight(gameObject))
        {
            var oldPosition = transform.position;
            var newPosition = oldPosition + Vector3.right;
            transform.DOLocalMove(newPosition, 0f);
        }
    }

    private void tryChange()
    {
        var manager = GetComponentInParent<GameManager>();
        Debug.Log("try change");
        var formTi = GetComponent<BaseChangeForm>();
        State state = formTi.nextState();
        var child1 = gameObject.transform.Find("1").transform;
        var child2 = gameObject.transform.Find("2").transform;
        var child3 = gameObject.transform.Find("3").transform;
        var child4 = gameObject.transform.Find("4").transform;
        var c1 = child1.position + state.v1;
        var c2 = child2.position + state.v2;
        var c3 = child3.position + state.v3;
        var c4 = child4.position + state.v4;
        if (manager.isValidAtWolrdPosition((int) c1.x, (int) c1.y)
            && manager.isValidAtWolrdPosition((int) c2.x, (int) c2.y)
            && manager.isValidAtWolrdPosition((int) c3.x, (int) c3.y)
            && manager.isValidAtWolrdPosition((int) c4.x, (int) c4.y))
        {
            child1.position += state.v1;
            child2.position += state.v2;
            child3.position += state.v3;
            child4.position += state.v4;
        }
        else
        {
            formTi.rollBackState();
        }
    }

    public struct State
    {
        public Vector3 v1;
        public Vector3 v2;
        public Vector3 v3;
        public Vector3 v4;
    }
}