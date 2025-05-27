using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Localization.Settings;

public class FirebaseLogin : MonoBehaviour {
    [SerializeField] FirebaseManager firebase;
    [SerializeField] GameManager gm;
    [SerializeField] MenuController mc;
    [SerializeField] SwitchPlayer player;

    //Prisijungimo meniu laukai
    public TMP_InputField emailLogin;
    public TMP_InputField passwordLogin;
    public TMP_Text loginText;
    public Button loginButton;

    public IEnumerator Login() {
        //Atliekamas vartotojo prisijungimas
        Task<AuthResult> LoginTask = firebase.auth.SignInWithEmailAndPasswordAsync(emailLogin.text, passwordLogin.text);
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);
        if (LoginTask.Exception != null) {
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
            switch (errorCode) {
                case AuthError.MissingEmail:
                    UpdateLoginText(1);
                    break;
                case AuthError.MissingPassword:
                    UpdateLoginText(2);
                    break;
                case AuthError.WrongPassword:
                    UpdateLoginText(3);
                    break;
                case AuthError.InvalidEmail:
                    UpdateLoginText(4);
                    break;
                case AuthError.UserNotFound:
                    UpdateLoginText(5);
                    break;
            }
            firebase.ui.ClearLoginFields();
            loginButton.enabled = false;
            Invoke("EnableLoginButton", 3f);
        } else {
            firebase.User = LoginTask.Result.User;
            firebase.usernameField.text = firebase.User.DisplayName;
            firebase.isLoggedIn = true;
            UpdateLoginText(6);
            StartCoroutine(LoadUserData());
        }
    }

    public IEnumerator LoadUserData() {
        //Užkraunami vartotojo duomenys
        yield return new WaitForSecondsRealtime(0.4f);
        Task<DataSnapshot> DBTask = firebase.DBreference.Child("users").Child(firebase.User.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
        if (DBTask.Result.Value == null) {
            firebase.SaveDataButton();
        } else {
            firebase.usernameField.text = firebase.User.DisplayName;
            DataSnapshot snapshot = DBTask.Result;
            string l1 = snapshot.Child("highscore1").Value.ToString();
            gm.data.level1 = int.Parse(l1);
            string l2 = snapshot.Child("highscore2").Value.ToString();
            gm.data.level2 = int.Parse(l2);
            string l3 = snapshot.Child("highscore3").Value.ToString();
            gm.data.level3 = int.Parse(l3);
            string c = snapshot.Child("coins").Value.ToString();
            gm.data.coins = int.Parse(c);
            string c1 = snapshot.Child("characters1").Value.ToString();
            gm.data.frogBodyOwned = Boolean.Parse(c1);
            string c2 = snapshot.Child("characters2").Value.ToString();
            gm.data.thirdPlayerBodyOwned = Boolean.Parse(c2);

            player.ChangePlayer(0);
            gm.SaveData();
            firebase.SaveDataButton();

        }
        mc.ClickProfileButton();
        firebase.ui.ClearLoginFields();
    }

    public void EnableLoginButton() {
        loginButton.enabled = true;
        loginText.text = "";
    }

    public void UpdateLoginText(int code) {
        if (LocalizationSettings.SelectedLocale.ToString() == "Lithuanian (lt)") {
            loginText.text = code switch {
                1 => "Trūksta el. pašto!",
                2 => "Trūksta slaptažodžio!",
                3 => "Neteisingas slaptažodis!",
                4 => "Neteisingas el. paštas!",
                5 => "Paskyra neegzistuoja!",
                6 => "Prisijungta sėkmingai!",
                _ => "Prisijungti nepavyko!",
            };
        } else {
            loginText.text = code switch {
                1 => "Missing Email!",
                2 => "Missing Password!",
                3 => "Wrong Password!",
                4 => "Invalid Email!",
                5 => "Account does not exist!",
                6 => "Logged In!",
                _ => "Login Failed!",
            };
        }
    }
}