using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;

public class FirebaseLogin : MonoBehaviour {

    [SerializeField] FirebaseManager firebase;
    [SerializeField] GameManager gm;
    [SerializeField] protected SwitchPlayer player;

    public TMP_InputField emailLogin;
    public TMP_InputField passwordLogin;
    public TMP_Text loginText;
    public Button loginButton;

    public IEnumerator Login() {
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
            firebase.ui.ClearLoginFields();
            loginText.text = message;
            loginButton.enabled = false;
            Invoke(nameof(EnableLoginButton), 3f);
        } else {
            firebase.User = LoginTask.Result.User;
            loginText.text = "Logged In!";
            firebase.isLoggedIn = true;
            StartCoroutine(LoadUserData());
            yield return new WaitForSecondsRealtime(1.5f);

            firebase.ui.UpdateFields();
            firebase.gameUI.OpenProfileMenu();

            loginText.text = "";
            firebase.ui.ClearLoginFields();
        }
    }

    public IEnumerator LoadUserData() {
        Task<DataSnapshot> DBTask = firebase.DBreference.Child("users").Child(firebase.User.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
        if (DBTask.Result.Value == null) {
            SaveGameStats();
        } else {
            DataSnapshot snapshot = DBTask.Result;
            firebase.highscoreText.text = snapshot.Child("highscore").Value.ToString();
            firebase.allCoinsText.text = snapshot.Child("coins").Value.ToString();
            firebase.ownedCharacters = snapshot.Child("characters").Value.ToString();
            firebase.usernameField.text = firebase.User.DisplayName;

            gm.data.highscore = int.Parse(firebase.highscoreText.text);
            gm.data.coins = int.Parse(firebase.allCoinsText.text);
            gm.data.ownedCharacters = firebase.ownedCharacters;
            firebase.SaveDataButton();
        }

        player.LoadSettings();
        if (firebase.ownedCharacters == "DJ") {
            player.hintText = "Select";
        } else {
            player.hintText = "Buy";
        }
        player.ChangeHintText();
    }

    private void EnableLoginButton() {
        loginButton.enabled = true;
        loginText.text = "";
    }

    public void SaveGameStats() {
        firebase.highscoreText.text = gm.data.highscore.ToString();
        firebase.allCoinsText.text = gm.data.coins.ToString();
        firebase.ownedCharacters = gm.data.ownedCharacters;
        firebase.SaveDataButton();
    }

    public void ResetProfile() {
        firebase.highscoreText.text = "0";
        firebase.allCoinsText.text = "0";
        firebase.ownedCharacters = "";
        firebase.usernameField.text = "";
        firebase.SaveDataButton();
        player.LoadSettings();
    }
}