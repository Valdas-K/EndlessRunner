using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //Pašokimo jėgos kintamasis
    [SerializeField] float jumpForce;

    //Kiek laiko veikėjas gali būti pašokęs į orą
    [SerializeField] float jumpTime;

    //Didžiausias pašokimų kiekis
    public int maxJumps = 2;

    //Aprašomas klasės kintamasis, iš kurio bus paimami valdymo nustatymai
    [SerializeField] InputControl inputcontrol;
    [SerializeField] MusicController mc;

    //Sukuriami kintamieji, kurie aprašo veikėjo būseną: ar yra ant žemės, ar yra pašokęs į orą ir kiek laiko yra pašokęs
    private bool isGrounded = true;
    private bool isJumping = false;
    private float jumpTimer;

    //Aprašomas veikėjo fizikos komponentas
    private Rigidbody2D rb;

    //Parinkto veikėjo, pašokimų kiekio ir didžiausio pašokimų skaičiaus kintamieji
    public SwitchPlayer pickedPlayer;
    private int jumps = 0;


    private void Awake() {
        //Pridedamas veikėjo fizikos komponentas
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        //Jei žaidimas nėra sutabdytas
        if (!PauseGame.gameIsPaused) {
            //Tikrinama, kuris veikėjas pasirinktas
            if (pickedPlayer.playerPicked == 0) {
                rb.mass = 0.5f;
                if (isGrounded && Input.GetKeyDown(inputcontrol.JumpKey)) {
                    //Paleidžiamas garso efektas ir veikėjas šoka į viršų
                    isJumping = true;
                    mc.PlayJumpSound();
                    rb.AddForce(Vector2.up * jumpForce);
                }

                //Jei veikėjas jau yra ore ir toliau yra laikomas pašokimo mygtukas
                if (isJumping && Input.GetKey(inputcontrol.JumpKey)) {
                    //Jei veikėjas dar gali šokti į orą
                    if (jumpTimer < jumpTime) {

                        //Veikėjas toliau šoka aukštyn 
                        rb.AddForce(Vector2.up * jumpForce);
    
                        //Kintamojo reikšmė didėja priklausomai nuo laiko, kurį buvo nuspaustas pašokimo mygtukas
                        jumpTimer += Time.deltaTime;
                    } else {
                        //Jei negali, veikėjas nebegali toliau šokti į orą
                        isJumping = false;
                    }
                }

                //Tikrinama, ar pašokimo mygtukas yra nebenaudojamas
                if (Input.GetKeyUp(inputcontrol.JumpKey)) {
                    //Jei taip, veikėjas nebegali toliau šokti į orą
                    isJumping = false;

                    //Pašokimo laiko reikšmė atnaujinama
                    jumpTimer = 0;
                }
            }

            //Tikrinama, kuris veikėjas pasirinktas
            if (pickedPlayer.playerPicked == 1 && Input.GetKeyDown(inputcontrol.JumpKey)) {
                rb.mass = 5f;
                if (isGrounded) {
                    //Paleidžiamas garso efektas ir veikėjas šoka į viršų, pridedama pašokimo reikšmė
                    mc.PlayJumpSound();
                    jumps++;
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                } else if (jumps < maxJumps) {
                    //Jei pridėta pašokimo reikšmė yra mažesnė nei pašokimų limitas, veikėjas dar kartą pašoka
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                    mc.PlayJumpSound();
                    jumps++;
                }
            }
        }
    }

    //Tikrinama, ar Veikėjas yra ant žemės
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            isGrounded = true;
            jumps = 0;
        }
    }

    //Tikrinama, ar Veikėjas yra ore
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            isGrounded = false;
        }
    }
}