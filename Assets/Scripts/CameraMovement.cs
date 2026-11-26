using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;// The target the camera will follow
    public Vector3 offset;// The offset from the target position

    void Update()
    {
        //Quaternion rotation = Quaternion.Euler(21.47f, target.eulerAngles.y, 0f);
        transform.position = target.position + offset;
        //transform.rotation = rotation;
    }
}