using UnityEngine;

public class AnimationManager : MonoBehaviour {
    public void GroundAnimation(Animator anim) {
        anim.SetBool("isGrounded", true);
        anim.SetBool("isFalling", false);
        anim.SetBool("isJumping", false);
        anim.SetBool("isDoubleJumping", false);
    }

    public void JumpAnimation(Animator anim) {
        anim.SetBool("isGrounded", false);
        anim.SetBool("isFalling", false);
        anim.SetBool("isJumping", true);
        anim.SetBool("isDoubleJumping", false);
    }

    public void FallAnimation(Animator anim) {
        anim.SetBool("isGrounded", false);
        anim.SetBool("isFalling", true);
        anim.SetBool("isJumping", false);
        anim.SetBool("isDoubleJumping", false);
    }

    public void DoubleJumpAnimation(Animator anim) {
        anim.SetBool("isGrounded", false);
        anim.SetBool("isFalling", false);
        anim.SetBool("isJumping", false);
        anim.SetBool("isDoubleJumping", true);
    }
}