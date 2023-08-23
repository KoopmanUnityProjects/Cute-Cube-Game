using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum LockCharacterAxis
    {
        X = 0,
        Y,
        Z,
    }

    LockCharacterAxis lockaxis;

    public int playerRow;
    public int playerCol;

    public bool isMoving;
    bool isOnMovingBlock = false;

    public float speed = 5.0f;
    Vector3 destination;
    Ground destinationBlock;

    // Start is called before the first frame update
    void Start()
    {
        lockaxis = LockCharacterAxis.Y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            PlayerMove();
        }
    }

    public void TryToMoveToPoint(Ground point)
    {
        if (isMoving || isOnMovingBlock)
            return;

        if (IsMoveValid(point.rowNumber, point.columnNumber))
        {
            destinationBlock = point;
            playerRow = point.rowNumber;
            playerCol = point.columnNumber;
            StartMoving(point.transform.position);
        }
    }

    bool IsMoveValid(int row, int col)
    {
        if (playerRow == row && playerCol == col + 1 ||
            playerRow == row && playerCol == col - 1 ||
            playerRow == row + 1 && playerCol == col ||
            playerRow == row - 1 && playerCol == col)
        {
            return true;
        }
        return false;
    }

    void StartMoving(Vector3 moveToPoint)
    {
        isMoving = true;

        // TODO change this to work with 3 diminsions. when moving up and down, this breaks'


        destination = GetDestination(moveToPoint);
        lookAtDestinationPoint();

        // TODO: Rotate character towards walking direction
        // TODO: trigger walking animation
    }

    void lookAtDestinationPoint()
    {
        transform.LookAt(destination, transform.up);
    }

    Vector3 GetDestination(Vector3 moveToPoint)
    {
        float xpoint = moveToPoint.x;
        float ypoint = moveToPoint.y;
        float zpoint = moveToPoint.z;

        if (lockaxis == LockCharacterAxis.X)
        {
            xpoint = transform.position.y; ;
        }
        else if (lockaxis == LockCharacterAxis.Y) 
        {
            ypoint = transform.position.y;
        }
        else if (lockaxis == LockCharacterAxis.Z)
        {
            zpoint = transform.position.z;
        }

        return new Vector3(xpoint, ypoint, zpoint);
    }

    void PlayerMove()
    {
        float frameMoveDistance = (speed) * Time.deltaTime;

        if (Vector3.Distance(destination, transform.position) > frameMoveDistance)
        {
            transform.Translate(Vector3.forward * frameMoveDistance);
        }
        else
        {
            this.transform.position = destination;
            DoneMoving();
        }

    }

    void DoneMoving()
    {
        isMoving = false;
        destinationBlock.StepOn();
        // transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.time * 1.0f);
        // TODO: Rotate character back.
        // TODO: Stop walking animation
    }

    public void MovePlayerWithBlock(MovementBlock block)
    {
        isOnMovingBlock = true;
        GameManager.instance.ParentObject(this.gameObject, block.gameObject);
    }

    public void MovePlayerWithBlockDone()
    {
        isOnMovingBlock = false;
        GameManager.instance.ParentObject(this.gameObject, null);
    }

    public void SetNewPlayerPos(int row, int col)
    {
        playerRow = row;
        playerCol = col;
    }

    public void ChangeLockAxis(LockCharacterAxis axisToLock)
    {
        lockaxis = axisToLock;
    }
}
