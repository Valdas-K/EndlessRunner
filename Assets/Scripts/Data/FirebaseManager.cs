using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.UI;

public class FirebaseManager : MonoBehaviour {
    [SerializeField] UIManager ui;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject profileMenu;
    [SerializeField] GameObject loginMenu;
    [SerializeField] GameObject registerMenu;
    [SerializeField] TMP_Text profileButtonText;
    [SerializeField] GameManager gm;
    [SerializeField] SwitchPlayer player;
    [SerializeField] Button loginButton;

    private bool isLoggedIn;
    private string ownedCharacters;

    //Firebase variables
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    //Login variables
    [SerializeField] TMP_InputField emailLoginField;
    [SerializeField] TMP_InputField passwordLoginField;
    [SerializeField] TMP_Text loginText;

    //Register variables
    [SerializeField] TMP_InputField usernameRegisterField;
    [SerializeField] TMP_InputField emailRegisterField;
    [SerializeField] TMP_InputField passwordRegisterField;
    [SerializeField] TMP_InputField passwordRegisterVerifyField;
    [SerializeField] TMP_Text registerText;

    //User Data variables
    [SerializeField] TMP_InputField usernameField;
    [SerializeField] TMP_Text highscoreText;
    [SerializeField] TMP_Text allCoinsText;

    private void Awake() {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available) {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            } else {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void Start() {
        gm = GameManager.Instance;
        if (isLoggedIn) {
            gm.data.highscore = int.Parse(highscoreText.text);
            gm.data.coins = int.Parse(allCoinsText.text);
            gm.data.ownedCharacters = ownedCharacters;
        } else {
            player.LoadSettings();
        }
    }

    private void InitializeFirebase() {
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void ClearLoginFields() {
        emailLoginField.text = "";
        passwordLoginField.text = "";
    }

    private void ClearRegisterFields() {
        usernameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordRegisterVerifyField.text = "";
    }

    public void LoginButton() {
        //Function for the login button
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }

    public void RegisterButton() {
        //Function for the register button
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    public void SignOutButton() {
        //Function for the sign out button
        auth.SignOut();
        ui.HideMenus();
        loginMenu.SetActive(true);
        isLoggedIn = false;
        profileButtonText.text = "Profile";
        ClearRegisterFields();
        ClearLoginFields();

        gm.data.highscore = 0;
        gm.data.coins = 0;
        gm.data.ownedCharacters = "";

        player.LoadSettings();
        player.hintText = "Buy";
        player.ChangeHintText();
    }

    public void SaveDataButton() {
        //Function for the save button
        StartCoroutine(UpdateUsernameAuth(usernameField.text));
        StartCoroutine(UpdateUsernameDatabase(usernameField.text));

        StartCoroutine(UpdateHighScore(int.Parse(highscoreText.text)));
        StartCoroutine(UpdateCoinsCollected(int.Parse(allCoinsText.text)));
        StartCoroutine(UpdateCharactersOwned(ownedCharacters));
    }

    private IEnumerator Login(string _email, string _password) {
        //Call the Firebase auth signin function passing the email and password
        Task<AuthResult> LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null) {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
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
            ClearLoginFields();
            loginText.text = message;
            loginButton.enabled = false;
            Invoke(nameof(EnableButton), 3f);
        } else {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result.User;
            loginText.text = "Logged In!";
            isLoggedIn = true;

            StartCoroutine(LoadUserData());

            yield return new WaitForSecondsRealtime(1.5f);

            usernameField.text = User.DisplayName;
            profileButtonText.text = "Welcome, " + User.DisplayName;
            ui.HideMenus();
            profileMenu.SetActive(true);
            loginText.text = "";
            ClearLoginFields();
            ClearRegisterFields();
        }
    }

    private void EnableButton() {
        loginButton.enabled = true;
        loginText.text = "";
    }

    private IEnumerator Register(string _email, string _password, string _username) {
        if (_username == "") {
            //If the username field is blank show a warning
            registerText.text = "Missing Username";
        } else if (passwordRegisterField.text != passwordRegisterVerifyField.text) {
            //If the password does not match show a warning
            registerText.text = "Password Does Not Match!";
        } else {
            //Call the Firebase auth signin function passing the email and password
            Task<AuthResult> RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null) {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
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
            } else {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result.User;

                if (User != null) { 
                    //Create a user profile and set the username
                    UserProfile profile = new() { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    Task ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null) {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        registerText.text = "Username Set Failed!";
                    } else {
                        //Username is now set
                        //Now return to login screen
                        registerText.text = "Registration successful!";
                        ClearRegisterFields();
                        ClearLoginFields();
                        usernameField.text = User.DisplayName;
                        profileButtonText.text = "Welcome, " + User.DisplayName;
                        Invoke(nameof(ShowLogin), 1.5f);
                    }
                }
            }
        }
    }

    private IEnumerator UpdateUsernameAuth(string _username) {
        //Create a user profile and set the username
        UserProfile profile = new() { DisplayName = _username };

        //Call the Firebase auth update user profile function passing the profile with the username
        Task ProfileTask = User.UpdateUserProfileAsync(profile);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        profileButtonText.text = "Welcome, " + User.DisplayName;
        usernameField.text = User.DisplayName;

        if (ProfileTask.Exception != null) {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        } else {

            //Auth username is now updated
        }
    }

    private IEnumerator UpdateUsernameDatabase(string _username) {
        //Set the currently logged in user username in the database
        Task DBTask = DBreference.Child("users").Child(User.UserId).Child("username").SetValueAsync(_username);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        profileButtonText.text = "Welcome, " + User.DisplayName;
        usernameField.text = User.DisplayName;

        if (DBTask.Exception != null) {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        } else {
            //Database username is now updated
        }
    }

    private IEnumerator UpdateHighScore(int _highScore) {
        //Set the currently logged in user xp
        Task DBTask = DBreference.Child("users").Child(User.UserId).Child("highscore").SetValueAsync(_highScore);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null) {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        } else {
            //Xp is now updated
        }
    }

    private IEnumerator UpdateCoinsCollected(int _coins) {
        //Set the currently logged in user xp
        Task DBTask = DBreference.Child("users").Child(User.UserId).Child("coins").SetValueAsync(_coins);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null) {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        } else {
            //Xp is now updated
        }
    }

    private IEnumerator UpdateCharactersOwned(string _characters) {
        //Set the currently logged in user xp
        Task DBTask = DBreference.Child("users").Child(User.UserId).Child("characters").SetValueAsync(_characters);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null) {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        } else {
            //Xp is now updated
        }
    }

    private IEnumerator LoadUserData() {
        //Get the currently logged in user data
        Task<DataSnapshot> DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null) {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        } else if (DBTask.Result.Value == null) {
            SaveGameStats();
        } else {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            highscoreText.text = snapshot.Child("highscore").Value.ToString();
            allCoinsText.text = snapshot.Child("coins").Value.ToString();
            ownedCharacters = snapshot.Child("characters").Value.ToString();
            usernameField.text = User.DisplayName;

            gm.data.highscore = int.Parse(highscoreText.text);
            gm.data.coins = int.Parse(allCoinsText.text);
            gm.data.ownedCharacters = ownedCharacters;
            SaveDataButton();
        }

        if (ownedCharacters == "DJ") {
            player.hintText = "Select";
        } else {
            player.hintText = "Buy";
        }
        player.ChangeHintText();
    }

    public void OpenMenu() {
        if (isLoggedIn) {
            profileMenu.SetActive(true);
            loginMenu.SetActive(false);
            SaveGameStats();
        } else {
            profileMenu.SetActive(false);
            loginMenu.SetActive(true);
        }
    }

    public void SaveGameStats() {
        highscoreText.text = gm.data.highscore.ToString();
        allCoinsText.text = gm.data.coins.ToString();
        ownedCharacters = gm.data.ownedCharacters;
        SaveDataButton();
    }

    public void ShowLogin() {
        ui.HideMenus();
        loginMenu.SetActive(true);
    }

    public void ResetProfile() {
        highscoreText.text = "0";
        allCoinsText.text = "0";
        ownedCharacters = "";
        usernameField.text = "";
        SaveDataButton();
        player.LoadSettings();
    }
}