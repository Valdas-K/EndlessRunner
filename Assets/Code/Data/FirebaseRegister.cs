using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;
using TMPro;
using System.Text.RegularExpressions;

public class FirebaseRegister : MonoBehaviour {

    public TMP_InputField emailRegister;
    public TMP_InputField passwordRegister;
    public TMP_InputField passwordConfirmRegister;
    public TMP_InputField usernameRegister;
    public TMP_Text registerText;
    [SerializeField] FirebaseManager firebase;
    [SerializeField] MenuController menu;

    public IEnumerator Register() {
        if (usernameRegister.text == "") {
            registerText.text = "Missing Username";
        } else if (passwordRegister.text != passwordConfirmRegister.text) {
            registerText.text = "Password Does Not Match!";
        } else if (passwordRegister.text.Length < 8 || passwordConfirmRegister.text.Length < 8) {
            registerText.text = "Password Must Be At Least 8 Symbols Long!";
        } else if (!ContainsNumber(passwordRegister.text)) {
            registerText.text = "Password Must Have At Least 1 Number!";
        } else if (!ContainsUpperLetter(passwordRegister.text)) {
            registerText.text = "Password Must Have At Least 1 Upper Case Letter!";
        } else if (!ContainsLowerLetter(passwordRegister.text)) {
            registerText.text = "Password Must Have At Least 1 Lower Case Letter!";
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

    private bool ContainsNumber(string input) {
        return Regex.IsMatch(input, @"\d");
    }

    private bool ContainsUpperLetter(string input) {
        return Regex.IsMatch(input, @"[A-Z]");
    }

    private bool ContainsLowerLetter(string input) {
        return Regex.IsMatch(input, @"[a-z]");
    }

    IEnumerator ShowLogin() {
        yield return new WaitForSecondsRealtime(1f);
        firebase.ui.ClearRegisterFields();
        menu.OpenLoginMenu();
    }
}