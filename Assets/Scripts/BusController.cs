using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BusController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider FL_Collider;
    public WheelCollider FR_Collider;
    public WheelCollider RL_Collider;
    public WheelCollider RR_Collider;

    [Header("Wheel Meshes (Visuals)")]
    public Transform FL_Mesh;
    public Transform FR_Mesh;
    public Transform RL_Mesh;
    public Transform RR_Mesh;

    [Header("Bus Settings")]
    public float motorTorque = 10000f;
    public float maxSteerAngle = 28f;
    public float brakeTorque = 6000f;
    public float idleBrakeTorque = 1500f;

    private float moveInput;
    private float steerInput;
    public bool isBraking;

    private void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
        isBraking = Input.GetKey(KeyCode.Space);
        UpdateAllWheelsVisuals();
    }

    private void FixedUpdate()
    {
        HandleSteering();
        HandleMotorAndBrakes();
    }

    private void HandleSteering()
    {      
        float currentSteerAngle = steerInput * maxSteerAngle;
        FL_Collider.steerAngle = currentSteerAngle;
        FR_Collider.steerAngle = currentSteerAngle;
    }

    private void HandleMotorAndBrakes()
    {
        float appliedMotorTorque = moveInput * motorTorque;
        float appliedBrakeTorque = 0f;
        if (isBraking)
        {
            appliedBrakeTorque = brakeTorque;
            appliedMotorTorque = 0f;
        }
        else if (Mathf.Approximately(moveInput, 0f))
        {
            appliedBrakeTorque = idleBrakeTorque;
        }

        RL_Collider.motorTorque = appliedMotorTorque;
        RR_Collider.motorTorque = appliedMotorTorque;
        FL_Collider.motorTorque = appliedMotorTorque;
        FR_Collider.motorTorque = appliedMotorTorque;


        FL_Collider.brakeTorque = appliedBrakeTorque;
        FR_Collider.brakeTorque = appliedBrakeTorque;
        RL_Collider.brakeTorque = appliedBrakeTorque;
        RR_Collider.brakeTorque = appliedBrakeTorque;
    }

    private void UpdateAllWheelsVisuals()
    {
        UpdateSingleWheelVisual(FL_Collider, FL_Mesh);
        UpdateSingleWheelVisual(FR_Collider, FR_Mesh);
        UpdateSingleWheelVisual(RL_Collider, RL_Mesh);
        UpdateSingleWheelVisual(RR_Collider, RR_Mesh);
    }

    private void UpdateSingleWheelVisual(WheelCollider collider, Transform wheelMesh)
    {
        if (collider == null || wheelMesh == null) return;

        // Получаем текущие мировые координаты и вращение колеса от WheelCollider:
        // сюда уже входят поворот (steerAngle), вращение качения и ход подвески
        collider.GetWorldPose(out Vector3 worldPosition, out Quaternion worldRotation);

        wheelMesh.position = worldPosition;
        wheelMesh.rotation = worldRotation;
    }
}