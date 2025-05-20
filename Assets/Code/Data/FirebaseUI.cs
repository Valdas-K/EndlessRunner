using UnityEngine;

public class FirebaseUI : MonoBehaviour {
    [SerializeField] FirebaseManager firebase;

    public void ClearRegisterFields() {
        firebase.register.usernameRegister.text = "";
        firebase.register.emailRegister.text = "";
        firebase.register.passwordRegister.text = "";
        firebase.register.passwordConfirmRegister.text = "";
        firebase.register.registerText.text = "";
    }

    public void ClearLoginFields() {
        firebase.login.emailLogin.text = "";
        firebase.login.passwordLogin.text = "";
        firebase.login.loginText.text = "";
    }
}