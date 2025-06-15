using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    AudioMenager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioMenager>();
    }

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    private PlayerCombat playerCombat;

    private float stepTimer = 0.2f;
    public float stepInterval = 0.4f;

    void Start()
    {
        playerCombat = GetComponent<PlayerCombat>();
    }

    void Update()
    {
        HandleFlip();

        if (playerCombat != null && playerCombat.isAttacking)
        {
            horizontalMove = 0f;
            animator.SetFloat("Speed", 0f);
            return;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            audioManager.PlaySFX(audioManager.Run);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (Mathf.Abs(horizontalMove) > 0.1f && controller.IsGrounded && !crouch)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                audioManager.PlaySFX(audioManager.Run);
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void HandleFlip()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
        if (isCrouching)
        {
            animator.SetBool("IsJumping", false);
        }
    }

    void FixedUpdate()
    {
        if (playerCombat != null && playerCombat.isAttacking)
        {
            controller.Move(0f, false, false);
            return;
        }

        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
