using UnityEngine;

public class LongPlayerMovement : PlayerMovement {
    //Kiek laiko veikėjas gali būti pašokęs į orą
    public float jumpTime;

    [SerializeField] Animator anim;

    //Sukuriami kintamieji, kurie aprašo veikėjo būseną: ar yra ant žemės, ar yra pašokęs į orą ir kiek laiko yra pašokęs
    private bool isJumping = false;
    private float jumpTimer;

    private void Update() {
        //Jei žaidimas nėra sutabdytas
        if (!PauseGame.gameIsPaused) {
            rb.mass = mass;
            if (isGrounded && Input.GetKeyDown(input.jumpKey)) {
                isJumping = true;
                mc.PlayJumpSound();
                PlayerJump();
            }

            //Jei veikėjas jau yra ore ir toliau yra laikomas pašokimo mygtukas
            if (isJumping && Input.GetKey(input.jumpKey)) {
                //Jei veikėjas dar gali šokti į orą
                if (jumpTimer < jumpTime) {
                    //Veikėjas toliau šoka aukštyn 
                    PlayerJump();

                    //Kintamojo reikšmė didėja priklausomai nuo laiko, kurį buvo nuspaustas pašokimo mygtukas
                    jumpTimer += Time.smoothDeltaTime;
                } else {
                    PlayerFall();
                }
            }

            //Tikrinama, ar pašokimo mygtukas yra nebenaudojamas
            if (Input.GetKeyUp(input.jumpKey)) {
                //Jei taip, veikėjas nebegali toliau šokti į orą
                PlayerFall();

                //Pašokimo laiko reikšmė atnaujinama
                jumpTimer = 0;
            }
        }
    }

    //Tikrinama, ar Veikėjas yra ant žemės
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            isGrounded = true;
            playerAnimation.PlayCharacterAnim(anim, "ground");
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
        //Paleidžiamas garso efektas ir veikėjas šoka į viršų
        rb.AddForce(new Vector2(0f, jumpForce));
    }

    protected override void PlayerFall() {
        //Jei negali, veikėjas nebegali toliau šokti į orą
        isJumping = false;
        playerAnimation.PlayCharacterAnim(anim, "fall");
    }
}