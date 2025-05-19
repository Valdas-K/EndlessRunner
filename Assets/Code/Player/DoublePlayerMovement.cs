using UnityEngine;

public class DoublePlayerMovement : PlayerMovement {
    //Pašokimo jėgos kintamasis
    [SerializeField] float doubleJumpForce;
    [SerializeField] Animator anim;
    //Didžiausias pašokimų kiekis
    [SerializeField] int maxJumps;

    //Parinkto veikėjo, pašokimų kiekio ir didžiausio pašokimų skaičiaus kintamieji
    private int jumps = 0;

    private void Update() {
        //Jei žaidimas nėra sutabdytas
        if (!PauseGame.gameIsPaused) {
            rb.mass = mass;
            if (isGrounded && Input.GetKeyDown(input.jumpKey)) {
                PlayerJump();
            }
            if (!isGrounded && jumps < maxJumps && Input.GetKeyDown(input.jumpKey)) {
                PlayerDoubleJump();
            }
            if (!isGrounded && Input.GetKeyUp(input.jumpKey)) {
                PlayerFall();
            }
        }
    }

    //Tikrinama, ar Veikėjas yra ant žemės
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            isGrounded = true;
            playerAnimation.PlayCharacterAnim(anim, "ground");
            jumps = 0;
        }
    }

    //Tikrinama, ar Veikėjas yra ore
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            isGrounded = false;
            playerAnimation.PlayCharacterAnim(anim, "jump");
        }
    }

    protected override void PlayerJump() {
        //Paleidžiamas garso efektas ir veikėjas šoka į viršų, pridedama pašokimo reikšmė
        mc.PlayJumpSound();
        jumps++;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void PlayerDoubleJump() {
        //Jei pridėta pašokimo reikšmė yra mažesnė nei pašokimų limitas, veikėjas dar kartą pašoka
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, doubleJumpForce);
        playerAnimation.PlayCharacterAnim(anim, "dJump");
        mc.PlayJumpSound();
        jumps++;
    }

    protected override void PlayerFall() {
        //Veikėjas krenta žemyn
        playerAnimation.PlayCharacterAnim(anim, "fall");
    }
}