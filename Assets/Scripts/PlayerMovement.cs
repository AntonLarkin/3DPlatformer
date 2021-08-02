using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject playerModel;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed;

    [Header("Jump")]
    [SerializeField] private float jumpHeight;

    [Header("Rotation")]
    [SerializeField] private float rotateSpeed;
    private float xRot;
    private float yRot;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string moveSpeedName;
    [SerializeField] private string jumpTriggerName;

    [Header("Gravity")]
    [SerializeField] private float gravity;
    [SerializeField] private float currentGravity;
    [SerializeField] private float maxGravity;
    [SerializeField] private float constantGravity;
    [SerializeField] private float gravityScale;
    private Vector3 gravityDirection;
    private Vector3 gravityMovement;
    private Vector3 playerVelocity;

    private Rigidbody rb;
    private Player player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        characterController = GetComponent<CharacterController>();

        gravityDirection = Vector3.down;
    }

    private void OnEnable()
    {
        UiManager.OnRestartButton += UiManager_OnRestartButton;
    }

    private void OnDisable()
    {
        UiManager.OnRestartButton -= UiManager_OnRestartButton;
    }

    private void Update()
    {
        if (player.IsPaused||player.IsBusy)
        {
            return;
        }

        if (!player.IsPlayerDead)
        {
            CalculateGravity();
            Move();
            Rotate();
        }
        else if (player.IsPlayerDead)
        {
            Die();
        }
    }

    private void Move()
    {
        float moveVertical = Input.GetAxis("Vertical");

        var moveDirection = transform.forward * moveVertical;

        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        if (moveDirection.magnitude > 0)
        {
            animator.SetFloat(moveSpeedName, speed);
        }
        else
        {
            animator.SetFloat(moveSpeedName, 0f);
        }

        var moveDelta = moveDirection * (speed * Time.deltaTime);

        if (characterController.isGrounded)
        {
            currentGravity = constantGravity;

            if (Input.GetButtonDown("Jump"))
            {
                currentGravity = jumpHeight;
                animator.SetTrigger(jumpTriggerName);
                Jump();
            }
        }
        else
        {
            currentGravity += Physics.gravity.y * gravityScale * Time.deltaTime;
        }
        moveDelta.y = currentGravity;
        characterController.Move(moveDelta);
    }

    private void Rotate()
    {
        xRot += Input.GetAxis("Mouse X") * rotateSpeed;

        transform.rotation = Quaternion.Euler(0f, xRot, 0f);
    }

    private void Die()
    {
        playerModel.SetActive(false);
    }

    private void Jump()
    {
        playerVelocity.y += Mathf.Sqrt(speed * gravity);

        playerVelocity.y += gravity;
        characterController.Move(playerVelocity * jumpHeight * Time.deltaTime);
    }

    private void CalculateGravity()
    {
        if (IsGrounded())
        {
            currentGravity = constantGravity;
        }
        else
        {
            if (currentGravity > maxGravity)
            {
                currentGravity -= gravity * Time.deltaTime;
            }
        }

        gravityMovement = gravityDirection * -currentGravity * 10f * Time.deltaTime;
    }

    private bool IsGrounded()
    {
        playerVelocity.y = 0;
        return characterController.isGrounded;
    }

    private IEnumerator OnJump()
    {
        yield return new WaitForSeconds(0.2f);
        Jump();
    }

    private void UiManager_OnRestartButton()
    {
        playerModel.SetActive(true);
    }
}
