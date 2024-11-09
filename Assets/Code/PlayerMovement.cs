using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Pašokimo jėgos kintamasis
    [SerializeField] private float jumpForce;

    //Kiek laiko veikėjas gali būti pašokęs į orą
    [SerializeField] private float jumpTime;

    //Sukuriami kintamieji, kurie aprašo veikėjo būseną: ar yra ant žemės, ar yra pašokęs į orą ir kiek laiko yra pašokęs
    private bool isGrounded = false;
    private bool isJumping = false;
    private float jumpTimer;

    //Aprašomas veikėjo fizikos komponentas
    private Rigidbody2D rb;
    private void Awake()
    {
        //Pridedamas veikėjo fizikos komponentas
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Jei veikėjas yra ant žemės ir yra paspaustas pašokimo mygtukas, veikėjas pašoka į orą
        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            rb.AddForce(Vector2.up * jumpForce);
        }

        //Tikrinama, ar veikėjas jau yra ore ir toliau yra laikomas pašokimo mygtukas
        if(isJumping && Input.GetButton("Jump"))
        {
            //Tikrinama, ar veikėjas dar gali šokti į orą
            if(jumpTimer < jumpTime)
            {
                //Jei taip, veikėjas toliau šoka aukštyn 
                rb.AddForce(Vector2.up * jumpForce);

                //Kintamojo reikšmė didėja priklausomai nuo laiko, kurį buvo nuspaustas pašokimo mygtukas
                jumpTimer += Time.deltaTime;
            } else
            {
                //Jei ne, veikėjas nebegali toliau šokti į orą
                isJumping = false;
            }
        }

        //Tikrinama, ar pašokimo mygtukas yra nebenaudojamas
        if (Input.GetButtonUp("Jump"))
        {
            //Jei taip, veikėjas nebegali toliau šokti į orą
            isJumping = false;

            //Pašokimo laiko reikšmė atnaujinama
            jumpTimer = 0;
        }

    }

    //Tikrinama, ar Veikėjas yra ant žemės
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    //Tikrinama, ar Veikėjas yra pašokęs
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}