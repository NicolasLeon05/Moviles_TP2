using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SetGrounded(bool value)
    {
        animator.SetBool("IsGrounded", value);
    }

    public void SetJumping(bool value)
    {
        animator.SetBool("IsJumping", value);
    }

}
