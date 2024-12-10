using UnityEngine;

public class LongPlayerMovement : PlayerMovement
{
    //Kiek laiko veikėjas gali būti pašokęs į orą
    [SerializeField] float jumpTime;

    [SerializeField] Animator anim;

    //Sukuriami kintamieji, kurie aprašo veikėjo būseną: ar yra ant žemės, ar yra pašokęs į orą ir kiek laiko yra pašokęs
    private bool isJumping = false;
    private float jumpTimer;

    private void Update() {
        //Jei žaidimas nėra sutabdytas
        if (!PauseGame.gameIsPaused) {
            rb.mass = mass;
            if (isGrounded && Input.GetKeyDown(inputcontrol.JumpKey)) {
                isJumping = true;
                PlayerJump();
            }

            //Jei veikėjas jau yra ore ir toliau yra laikomas pašokimo mygtukas
            if (isJumping && Input.GetKey(inputcontrol.JumpKey)) {
                //Jei veikėjas dar gali šokti į orą
                if (jumpTimer < jumpTime) {
                    //Veikėjas toliau šoka aukštyn 
                    PlayerJump();

                    //Kintamojo reikšmė didėja priklausomai nuo laiko, kurį buvo nuspaustas pašokimo mygtukas
                    jumpTimer += Time.deltaTime;
                } else {
                    PlayerFall();
                }
            }

            //Tikrinama, ar pašokimo mygtukas yra nebenaudojamas
            if (Input.GetKeyUp(inputcontrol.JumpKey)) {
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
            animM.GroundAnimation(anim);
        }
    }

    //Tikrinama, ar Veikėjas yra ore
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            isGrounded = false;
            animM.JumpAnimation(anim);
        }
    }

    protected override void PlayerJump() {
        //Paleidžiamas garso efektas ir veikėjas šoka į viršų
        mc.PlayJumpSound();
        rb.AddForce(Vector2.up * jumpForce);
    }

    protected override void PlayerFall() {
        //Jei negali, veikėjas nebegali toliau šokti į orą
        isJumping = false;
        animM.FallAnimation(anim);
    }
}