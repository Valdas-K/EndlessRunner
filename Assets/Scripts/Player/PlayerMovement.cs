using UnityEngine;

public abstract class PlayerMovement : MonoBehaviour {
    //Pašokimo jėgos kintamasis
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float mass;
    [SerializeField] protected AnimationManager animM;

    //Aprašomas klasės kintamasis, iš kurio bus paimami valdymo nustatymai
    [SerializeField] protected InputControl inputcontrol;
    [SerializeField] protected MusicController mc;
    [SerializeField] protected GameObject player;

    protected abstract void PlayerJump();
    protected abstract void PlayerFall();

    protected bool isGrounded;

    //Aprašomas veikėjo fizikos komponentas
    protected Rigidbody2D rb;
    protected void Start() {
        rb = player.GetComponent<Rigidbody2D>();
    }
}