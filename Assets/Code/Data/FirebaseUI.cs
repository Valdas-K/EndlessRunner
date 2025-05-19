using UnityEngine;

public class FirebaseUI : MonoBehaviour {
    [SerializeField] FirebaseManager firebase;

    public void ClearRegisterFields() {
        firebase.register.usernameRegister.text = "";
        firebase.register.emailRegister.text = "";
        firebase.register.passwordRegister.text = "";
        firebase.register.passwordConfirmRegister.text = "";
    }

    public void ClearLoginFields() {
        firebase.login.emailLogin.text = "";
        firebase.login.passwordLogin.text = "";
    }

    public void OpenMenu() {
        if (firebase.isLoggedIn) {
            //ui.OpenProfileMenu();
            firebase.login.SaveGameStats();
        } else {
            //ui.OpenLoginMenu();
            ClearLoginFields();
        }
    }
}