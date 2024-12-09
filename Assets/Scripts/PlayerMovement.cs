using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //Pašokimo jėgos kintamasis
    [SerializeField] float jumpForce;

    //Kiek laiko veikėjas gali būti pašokęs į orą
    [SerializeField] float jumpTime;

    //Sukuriami kintamieji, kurie aprašo veikėjo būseną: ar yra ant žemės, ar yra pašokęs į orą ir kiek laiko yra pašokęs
    private bool isGrounded = false;
    private bool isJumping = false;
    private float jumpTimer;

    //Aprašomas klasės kintamasis, iš kurio bus paimami valdymo nustatymai
    public InputControl inputcontrol;

    //Aprašomas veikėjo fizikos komponentas
    private Rigidbody2D rb;

    //Parinkto veikėjo, pašokimų kiekio ir didžiausio pašokimų skaičiaus kintamieji
    public SwitchPlayer pickedPlayer;
    private int jumps = 0;
    public int maxJumps = 2;

    [SerializeField] MusicController mc;

    private void Awake() {
        //Pridedamas veikėjo fizikos komponentas
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        //Jei žaidimas nėra sutabdytas
        if (!PauseGame.gameIsPaused) {
            //Jei veikėjas yra ant žemės ir yra paspaustas pašokimo mygtukas, veikėjas pašoka į orą
            if (isGrounded && Input.GetKeyDown(inputcontrol.JumpKey)) {
                isJumping = true;

                //Vykdomas pašokimas pagal pasirinktą veikėją
                if (pickedPlayer.playerPicked == 0) {
                    rb.AddForce(Vector2.up * jumpForce);
                }
                if (pickedPlayer.playerPicked == 1) {
                    jumps++;
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                }

                //Paleidžiamas pašokimo garso efektas
                mc.PlayJumpSound();
            } else if (Input.GetKeyDown(inputcontrol.JumpKey) &&
                pickedPlayer.playerPicked == 1 && jumps < maxJumps) {
                //Jei pasirinktas antras veikėjas ir yra pašokama jau esant ore,
                //leidžiama pašokti dar kartą
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

                //Paleidžiamas pašokimo garso efektas
                mc.PlayJumpSound();

                jumps++;
            }

            //Tikrinama, ar veikėjas jau yra ore ir toliau yra laikomas pašokimo mygtukas
            if (isJumping && Input.GetKey(inputcontrol.JumpKey)) {
                //Tikrinama, ar veikėjas dar gali šokti į orą
                if (jumpTimer < jumpTime) {
                    //max greitis
                    if (rb.linearVelocityY < 8f) {
                        //Jei taip, veikėjas toliau šoka aukštyn 
                        rb.AddForce(Vector2.up * jumpForce);
                    }

                    //Kintamojo reikšmė didėja priklausomai nuo laiko, kurį buvo nuspaustas pašokimo mygtukas
                    jumpTimer += Time.deltaTime;
                } else {
                    //Jei ne, veikėjas nebegali toliau šokti į orą
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
    }

    //Tikrinama, ar Veikėjas yra ant žemės
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            isGrounded = true;
            jumps = 0;
        }
    }

    //Tikrinama, ar Veikėjas yra pašokęs
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            isGrounded = false;
        }
    }
}