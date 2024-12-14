using UnityEngine;

public class FirebaseUI : MonoBehaviour {
    [SerializeField] UIManager ui;
    [SerializeField] FirebaseManager firebase;

    public void UpdateFields() {
        firebase.usernameField.text = firebase.User.DisplayName;
        firebase.profileButtonText.text = "Welcome, " + firebase.User.DisplayName;
    }

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
            ui.OpenProfileMenu();
            firebase.login.SaveGameStats();
        } else {
            ui.OpenLoginMenu();
            ClearLoginFields();
        }
    }
}