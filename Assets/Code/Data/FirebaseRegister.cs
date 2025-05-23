using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class FirebaseRegister : MonoBehaviour {
    [SerializeField] FirebaseManager firebase;
    [SerializeField] MenuController menu;

    //Registracijos ekrano laukai
    public TMP_InputField emailRegister;
    public TMP_InputField passwordRegister;
    public TMP_InputField passwordConfirmRegister;
    public TMP_InputField usernameRegister;
    public TMP_Text registerText;
    public Button registerButton;

    public IEnumerator Register() {
        //Atliekama vartotojo registracija
        if (usernameRegister.text == "") {
            UpdateRegisterText(1);
        } else if (passwordRegister.text != passwordConfirmRegister.text) {
            UpdateRegisterText(2);
        } else if (passwordRegister.text.Length < 8 || passwordConfirmRegister.text.Length < 8) {
            UpdateRegisterText(3);
        } else if (!ContainsNumber(passwordRegister.text)) {
            UpdateRegisterText(4);
        } else if (!ContainsUpperLetter(passwordRegister.text)) {
            UpdateRegisterText(5);
        } else if (!ContainsLowerLetter(passwordRegister.text)) {
            UpdateRegisterText(6);
        } else {
            Task<AuthResult> RegisterTask = firebase.auth.CreateUserWithEmailAndPasswordAsync(emailRegister.text, passwordRegister.text);
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);
            if (RegisterTask.Exception != null) {
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                switch (errorCode) {
                    case AuthError.MissingEmail:
                        UpdateRegisterText(7);
                        break;
                    case AuthError.MissingPassword:
                        UpdateRegisterText(8);
                        break;
                    case AuthError.WeakPassword:
                        UpdateRegisterText(9);
                        break;
                    case AuthError.EmailAlreadyInUse:
                        UpdateRegisterText(10);
                        break;
                }
                firebase.ui.ClearRegisterFields();
                registerButton.enabled = false;
                Invoke("EnableRegisterButton", 3f);
            } else {
                firebase.User = RegisterTask.Result.User;
                if (firebase.User != null) {
                    UserProfile profile = new() { DisplayName = usernameRegister.text };
                    Task ProfileTask = firebase.User.UpdateUserProfileAsync(profile);
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);
                    if (ProfileTask.Exception == null) {
                        UpdateRegisterText(11);
                        firebase.usernameField.text = firebase.User.DisplayName;
                        StartCoroutine(ShowLogin());
                    }
                }
            }
        }
    }

    private bool ContainsNumber(string input) {
        //Tikrinama, ar yra skaičius
        return Regex.IsMatch(input, @"\d");
    }

    private bool ContainsUpperLetter(string input) {
        //Tikrinama, ar yra didžioji raidė
        return Regex.IsMatch(input, @"[A-Z]");
    }

    private bool ContainsLowerLetter(string input) {
        //Tikrinama, ar yra mažoji raidė
        return Regex.IsMatch(input, @"[a-z]");
    }

    public void EnableRegisterButton() {
        registerText.text = "";
        registerButton.enabled = true;
    }

    IEnumerator ShowLogin() {
        //Pereinama į prisijungimo ekraną
        yield return new WaitForSecondsRealtime(1f);
        firebase.ui.ClearRegisterFields();
        menu.OpenLoginMenu();
    }

    public void UpdateRegisterText(int code) {
        if (LocalizationSettings.SelectedLocale.ToString() == "Lithuanian (lt)") {
            registerText.text = code switch {
                1 => "Trūksta vartotojo vardo!",
                2 => "Slaptažodžiai nesutampa!",
                3 => "Slaptažodis turi būti bent 8 simbolių ilgio!",
                4 => "Slaptažodis turi turėti bent vieną skaičių!",
                5 => "Slaptažodis turi turėti bent vieną didžiąją raidę!",
                6 => "Slaptažodis turi turėti bent vieną mažąją raidę!",
                7 => "Trūksta el. pašto!",
                8 => "Trūksta slaptažodžio!",
                9 => "Silpnas slaptažodis!",
                10 => "El. paštas jau yra naudojamas!",
                11 => "Registracija sėkminga!",
                _ => "Registracija nesėkminga!",
            };
        } else {
            registerText.text = code switch {
                1 => "Missing Username!",
                2 => "Password Does Not Match!",
                3 => "Password Must Be At Least 8 Symbols Long!",
                4 => "Password Must Have At Least 1 Number!",
                5 => "Password Must Have At Least 1 Upper Case Letter!",
                6 => "Password Must Have At Least 1 Lower Case Letter!",
                7 => "Missing Email!",
                8 => "Missing Password!",
                9 => "Weak Password!",
                10 => "Email Already In Use!",
                11 => "Registration successful!",
                _ => "Register Failed!",
            };
        }
    }
}