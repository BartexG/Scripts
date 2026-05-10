using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement instance;
    Vector3 newPosition;
  
    [Header("Keyboard Movement")]
    [SerializeField] float fastSpeed = 0.05f;
    [SerializeField] float normalSpeed = 0.01f;
    [SerializeField] float movementSensitivity = 1f;
    float movementSpeed;
 
    [Header("Bounds")]
    [SerializeField] float minHeight = 10;
    [SerializeField] float maxHeight = 70;


    private void Start()
    {
        instance = this;
 
        newPosition = transform.position;
 
        movementSpeed = normalSpeed;
    }
 
    private void Update()
    {
        HandleCameraMovement();
    }

    void HandleCameraMovement()
    {

        if (Input.GetKey(KeyCode.C))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }
 
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            MoveUp();
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            MoveDown();
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.E))
        {
            Zoom();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            UnZoom();
        }
    
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementSensitivity);
    }

    bool CheckIfNewPosIsInBoundsY(Vector3 modVector)
    {
        Vector3 afterMove = transform.position + modVector;

        if(afterMove.y < minHeight && modVector.y < 0) return false;
        if(afterMove.y > maxHeight && modVector.y > 0) return false;

        return true;
    }

    void MoveUp()
    {
        Vector3 modVector = (transform.forward + transform.up) * movementSpeed;
        newPosition += modVector;
    }

    void MoveDown()
    {
        Vector3 modVector = (transform.forward + transform.up) * -movementSpeed;
        newPosition += modVector;
    }

    void MoveLeft()
    {
        Vector3 modVector = transform.right * -movementSpeed;
        newPosition += modVector;
    }

    void MoveRight()
    {
        Vector3 modVector = transform.right * movementSpeed;
        newPosition += modVector;
    }

    void Zoom()
    {
        Vector3 modVector = transform.forward * movementSpeed * 2;
        if(CheckIfNewPosIsInBoundsY(modVector)) newPosition += modVector;
    }

    void UnZoom()
    {
        Vector3 modVector = transform.forward * -movementSpeed * 2;
        if(CheckIfNewPosIsInBoundsY(modVector)) newPosition += modVector;
    }
}
