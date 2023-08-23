using System.Collections;
using UnityEngine;

public class MovementBlock : MonoBehaviour, ISpecialBlock
{
    public GameObject CameraAndLighting;

    public Vector3 startingPoint;
    public Vector3 secondPoint;
    public Quaternion startingRotation;
    public Quaternion secondRotation;

    public float speed = 5.0f;
    public int row1;
    public int col1;
    public int row2;
    public int col2;
    public Player.LockCharacterAxis axis1;
    public Player.LockCharacterAxis axis2;

    public bool rotateBlock;
    public bool moveBlock;

    bool isMoving = false;
    bool isRotating = false;

    Vector3 destination;
    Quaternion destinationRotation;
    bool IsAtStartingPoint = true;
    bool IsStartingRotation = true;


    void LateUpdate()
    {
        if (isMoving)
        {
            MoveBlock();
        }

        if (isRotating)
        {
            isRotating = false;
            StartCoroutine(RotateBlock());
        }
    }

    public void OnInteract()
    {
        GameManager.instance.player.MovePlayerWithBlock(this);
        if (rotateBlock)
        {
            SetDestinationRotation();
        }

        if (moveBlock)
        {
            SetDestionationPoint();
        }
    }

    void SetDestionationPoint()
    {
        if (IsAtStartingPoint)
        {
            destination = secondPoint;
        }
        else
        {
            destination = startingPoint;
        }

        isMoving = true;
    }

    void SetDestinationRotation()
    {
        if (IsStartingRotation)
        {
            destinationRotation = secondRotation;
        }
        else
        {
            destinationRotation = startingRotation;
        }

        // parent camera and lighting onto player
        GameManager.instance.ParentObject(CameraAndLighting, GameManager.instance.player.gameObject);
        GameManager.instance.cameraManager.FollowPlayer(false);
        isRotating = true;
    }

    void MoveBlock()
    {
        float frameMoveDistance = (speed) * Time.deltaTime;

        if (Vector3.Distance(destination, transform.position) > frameMoveDistance)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, destination, frameMoveDistance);
        }
        else
        {
            this.transform.position = destination;
            DoneMoving();
        }
    }

    IEnumerator RotateBlock()
    {
        Transform parentTransform = transform.parent.gameObject.transform;
        Quaternion rotateTowardDestination = Quaternion.Euler(destinationRotation.x, destinationRotation.y, destinationRotation.z);
        while (parentTransform.rotation != rotateTowardDestination)
        {
            float rotationdegrees = (speed * 20) * Time.deltaTime;
            rotateTowardDestination = Quaternion.Euler(destinationRotation.x, destinationRotation.y, destinationRotation.z);
            parentTransform.rotation = Quaternion.RotateTowards(parentTransform.rotation, rotateTowardDestination, rotationdegrees);
            yield return null;
        }

        parentTransform.rotation = Quaternion.Euler(destinationRotation.x, destinationRotation.y, destinationRotation.z);

        yield return null;
        DoneRotating();
    }

    void DoneMoving()
    {
        GameManager.instance.player.MovePlayerWithBlockDone();
        isMoving = false;

        transform.Rotate(new Vector3(0.0f, 180f, 0.0f));

        if (IsAtStartingPoint)
        {
            ChangeCoordinates(row2, col2);
            IsAtStartingPoint = false;
        }
        else
        {
            ChangeCoordinates(row1, col1);
            IsAtStartingPoint = true;
        }
    }

    void DoneRotating()
    {
        GameManager.instance.player.MovePlayerWithBlockDone();
        isRotating = false;
        GameManager.instance.ParentObject(CameraAndLighting, null);
        GameManager.instance.cameraManager.FollowPlayer(true);

        if (IsStartingRotation)
        {
            ChangeCoordinates(row2, col2);
            IsStartingRotation = false;
            GameManager.instance.player.ChangeLockAxis(axis2);
        }
        else
        {
            ChangeCoordinates(row1, col1);
            IsStartingRotation = true;
            GameManager.instance.player.ChangeLockAxis(axis1);
        }
    }

    void ChangeCoordinates(int row, int col)
    {
        Ground ground = this.GetComponent<Ground>(); 
        ground.rowNumber = row;
        ground.columnNumber = col;
        GameManager.instance.player.SetNewPlayerPos(row, col);
    }
}
