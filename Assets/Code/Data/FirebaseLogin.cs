using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;
using System;

public class FirebaseLogin : MonoBehaviour {
    [SerializeField] FirebaseManager firebase;
    [SerializeField] GameManager gm;
    [SerializeField] MenuController mc;

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
            string message = "Login Failed!";
            switch (errorCode) {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            loginButton.enabled = false;
            firebase.ui.ClearLoginFields();
            loginText.text = message;
            Invoke(nameof(EnableLoginButton), 3f);
        } else {
            firebase.User = LoginTask.Result.User;
            firebase.usernameField.text = firebase.User.DisplayName;
            loginText.text = "Logged In!";
            firebase.isLoggedIn = true;
            StartCoroutine(LoadUserData());
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    public IEnumerator LoadUserData() {
        //Užkraunami vartotojo duomenys
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
            string l3 = snapshot.Child("highscore1").Value.ToString();
            gm.data.level3 = int.Parse(l3);
            string c = snapshot.Child("coins").Value.ToString();
            gm.data.coins = int.Parse(c);
            string c1 = snapshot.Child("characters1").Value.ToString();
            gm.data.frogBodyOwned = Boolean.Parse(c1);
            string c2 = snapshot.Child("characters2").Value.ToString();
            gm.data.thirdPlayerBodyOwned = Boolean.Parse(c2);

            gm.SaveData();
            firebase.SaveDataButton();
        }
        mc.ClickProfileButton();
        firebase.ui.ClearLoginFields();
    }

    public void ResetProfile() {
        //Atstatomas vartotojo profilis
        gm.data.level1 = 0;
        gm.data.level2 = 0;
        gm.data.level3 = 0;
        gm.data.coins = 0;
        gm.data.frogBodyOwned = false;
        gm.data.thirdPlayerBodyOwned = false;
        firebase.SaveDataButton();
        //player.LoadSettings();
    }

    public void EnableLoginButton() {
        //Įjungiamas prisijungimo mygtukas
        loginButton.enabled = true;
        loginText.text = "";
    }
}