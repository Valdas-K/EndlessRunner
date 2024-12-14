using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;
using TMPro;

public class FirebaseRegister : MonoBehaviour {

    public TMP_InputField emailRegister;
    public TMP_InputField passwordRegister;
    public TMP_InputField passwordConfirmRegister;
    public TMP_InputField usernameRegister;
    public TMP_Text registerText;
    [SerializeField] FirebaseManager firebase;

    public IEnumerator Register() {
        if (usernameRegister.text == "") {
            registerText.text = "Missing Username";
        } else if (passwordRegister.text != passwordConfirmRegister.text) {
            registerText.text = "Password Does Not Match!";
        } else {
            Task<AuthResult> RegisterTask = firebase.auth.CreateUserWithEmailAndPasswordAsync(emailRegister.text, passwordRegister.text);
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);
            if (RegisterTask.Exception != null) {
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                string message = "Register Failed!";
                switch (errorCode) {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                registerText.text = message;
                Invoke(nameof(ShowText), 3f);
            }
            else {
                firebase.User = RegisterTask.Result.User;
                if (firebase.User != null) {
                    UserProfile profile = new() { DisplayName = usernameRegister.text };
                    Task ProfileTask = firebase.User.UpdateUserProfileAsync(profile);
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);
                    if (ProfileTask.Exception != null) {
                        registerText.text = "Username Set Failed!";
                    } else {
                        registerText.text = "Registration successful!";
                        firebase.usernameField.text = firebase.User.DisplayName;
                        StartCoroutine(ShowLogin());
                    }
                }
            }
        }
    }

    IEnumerator ShowLogin() {
        yield return new WaitForSecondsRealtime(1.5f);
        firebase.ui.ClearRegisterFields();
        firebase.gameUI.OpenLoginMenu();
    }

    public void ShowText() {
        registerText.text = "";
    }
}