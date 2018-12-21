using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMove : MonoBehaviour
{
    public Tilemap tilemap;

    public Tile wall;
    public Tile box;

    KeyValuePair<Tile, Vector3> lastAction;
    int lockFlag = 0;

    void Start()
    {
        SwipeDetector.ClearOnSwipeEvent();
        SwipeDetector.OnSwipe += SwipeDetectorOnSwipeHandler;
    }

    void Update()
    {
        UpdateMoveOnPC();
        UpdateMoveOnAndroid();
    }

    void UpdateMoveOnAndroid()
    {
        if ( Input.touchCount > 1 && lockFlag > 0)
        {
            UndoMove(lastAction);
            lockFlag = 0;
        }
    }

    void SwipeDetectorOnSwipeHandler(SwipeData data)
    {
        print("Im HERE");
        switch (data.Direction)
        {
            case SwipeDirection.Up:
                lastAction = Move(Vector3.up);
                lockFlag = 1;
                break;

            case SwipeDirection.Down:
                lastAction = Move(Vector3.down);
                lockFlag = 1;
                break;

            case SwipeDirection.Right:
                lastAction = Move(Vector3.right);
                lockFlag = 1;
                break;

            case SwipeDirection.Left:
                lastAction = Move(Vector3.left);
                lockFlag = 1;
                break;
        }
    }

    void UpdateMoveOnPC()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            lastAction = Move(Vector3.up);
            lockFlag = 1;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            lastAction = Move(Vector3.down);
            lockFlag = 1;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            lastAction = Move(Vector3.right);
            lockFlag = 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lastAction = Move(Vector3.left);
            lockFlag = 1;
        }

        if (Input.GetKeyDown(KeyCode.Z) && lockFlag > 0)
        {
            UndoMove(lastAction);
            lockFlag = 0;
        }
    }

    KeyValuePair<Tile, Vector3> Move(Vector3 direction)
    {
        Tile tile = tilemap.GetTile<Tile>(tilemap.WorldToCell(gameObject.transform.position + direction));

        if (tile == wall)
        {
            //player hit the wall
            return new KeyValuePair<Tile, Vector3>(wall, direction);
        }

        if (tile == box)
        {
            //player is trying to move the box
            Tile nextTile = tilemap.GetTile<Tile>(tilemap.WorldToCell(gameObject.transform.position + 2 * direction));

            if (nextTile == null)
            {
                tilemap.SetTile(tilemap.WorldToCell(gameObject.transform.position + direction), null);
                tilemap.SetTile(tilemap.WorldToCell(gameObject.transform.position + 2 * direction), box);
                gameObject.transform.position += direction;
                return new KeyValuePair<Tile, Vector3>(box, direction);
            }

            return new KeyValuePair<Tile, Vector3>(box, Vector3.zero);
        }

        if (tile == null)
        {
            //Move player
            gameObject.transform.position += direction;
            return new KeyValuePair<Tile, Vector3>(null, direction);
        }

        return new KeyValuePair<Tile, Vector3>(null, Vector3.zero);
    }

    void UndoMove(KeyValuePair<Tile, Vector3> toUndo)
    {
        //Reverse? last Move action
        if (toUndo.Key == wall)
        {
            return;
        }

        if (toUndo.Key == null)
        {
            if (toUndo.Value != Vector3.zero)
                gameObject.transform.position -= toUndo.Value;
        }

        if (toUndo.Key == box && toUndo.Value != Vector3.zero)
        {
            gameObject.transform.position -= toUndo.Value;
            tilemap.SetTile(tilemap.WorldToCell(gameObject.transform.position + toUndo.Value), box);
            tilemap.SetTile(tilemap.WorldToCell(gameObject.transform.position + 2 * toUndo.Value), null);
        }
        return;
    }
}
