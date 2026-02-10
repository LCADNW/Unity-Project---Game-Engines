using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Camera playerCamera;

    public float runAcceleration = 0.2f;
    public float runSpeed = 4f;

    private PlayerLocomotionInput m_playerLocomotionInput;

    private void Awake()
    {
        m_playerLocomotionInput = GetComponent<PlayerLocomotionInput>();   
    }

    private void Update()
    {
        Vector3 cameraFowardXZ = new Vector3(playerCamera.transform.forward.x, 0f, playerCamera.transform.forward.z).normalized;
        Vector3 cameraRightXZ = new Vector3(playerCamera.transform.right.x, 0f, playerCamera.transform.right.z).normalized;

    }
}
