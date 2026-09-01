using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TankController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float rotationSpeed = 100f;

    [Header("Acceleration")]
    [SerializeField] private float acceleration = 3f;
    [SerializeField] private float deceleration = 5f;
    [SerializeField] private float turnAcceleration = 6f;

    [Header("Turret")]
    [SerializeField] private Transform turretPivot;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float turretRotationSpeed = 12f;

    private Rigidbody tankRigidbody;

    private float targetMoveInput;
    private float targetTurnInput;
    private float currentMoveInput;
    private float currentTurnInput;

    private void Awake()
    {
        tankRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        targetMoveInput = Input.GetAxisRaw("Vertical");
        targetTurnInput = Input.GetAxisRaw("Horizontal");

        AimTurret();
    }

    private void FixedUpdate()
    {
        SmoothInput();
        MoveTank();
    }

    private void SmoothInput()
    {
        float moveChangeSpeed =
            targetMoveInput == 0f ? deceleration : acceleration;

        currentMoveInput = Mathf.MoveTowards(
            currentMoveInput,
            targetMoveInput,
            moveChangeSpeed * Time.fixedDeltaTime
        );

        currentTurnInput = Mathf.MoveTowards(
            currentTurnInput,
            targetTurnInput,
            turnAcceleration * Time.fixedDeltaTime
        );
    }

    private void MoveTank()
    {
        float turn =
            currentTurnInput *
            rotationSpeed *
            Time.fixedDeltaTime;

        Quaternion rotationDelta =
            Quaternion.Euler(0f, turn, 0f);

        Quaternion nextRotation =
            tankRigidbody.rotation * rotationDelta;

        Vector3 moveDirection =
            nextRotation * Vector3.forward;

        Vector3 nextPosition =
            tankRigidbody.position +
            moveDirection *
            currentMoveInput *
            moveSpeed *
            Time.fixedDeltaTime;

        tankRigidbody.MoveRotation(nextRotation);
        tankRigidbody.MovePosition(nextPosition);
    }

    private void AimTurret()
    {
        if (turretPivot == null || Camera.main == null)
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 200f, groundMask))
        {
            Vector3 direction = hit.point - turretPivot.position;
            direction.y = 0f;
            if (direction.sqrMagnitude < 0.01f)
                return;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            turretPivot.rotation = Quaternion.Slerp(
                turretPivot.rotation,
                targetRotation,
                turretRotationSpeed * Time.deltaTime
            );
        }
    }
}