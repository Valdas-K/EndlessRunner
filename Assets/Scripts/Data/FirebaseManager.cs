using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;

public class FirebaseManager : MonoBehaviour {
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public DatabaseReference DBreference;
    public FirebaseUser User;

    [SerializeField] public UIManager gameUI;

    public FirebaseLogin login;
    public FirebaseUpdate update;
    public FirebaseRegister register;
    public FirebaseLogOut signOut;
    public FirebaseUI ui;

    public TMP_InputField usernameField;
    public TMP_Text highscoreText;
    public TMP_Text allCoinsText;
    public TMP_Text profileButtonText;
    public string ownedCharacters;

    public bool isLoggedIn;

    private void Awake() {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available) {
                auth = FirebaseAuth.DefaultInstance;
                DBreference = FirebaseDatabase.DefaultInstance.RootReference;
            }
        });
    }

    public void LoginButton() {
        StartCoroutine(login.Login());
    }

    public void RegisterButton() {
        StartCoroutine(register.Register());
    }

    public void SignOutButton() {
        signOut.SignOut();
    }

    public void SaveDataButton() {
        StartCoroutine(update.UpdateUsername());
        StartCoroutine(update.UpdateUserStats());
    }

    public void ResetButton() {
        login.ResetProfile();
    }
}