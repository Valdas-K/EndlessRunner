using Firebase.Database;
using UnityEngine;

public class FirebaseLogout : MonoBehaviour {
    [SerializeField] FirebaseManager firebase;
    [SerializeField] GameManager gm;
    [SerializeField] SettingsController settings;
    [SerializeField] MenuController menu;
    public void DeleteAccount() {
        //Šalinama vartotojo paskyra iš autentifikavimo
        Firebase.Auth.FirebaseUser user = firebase.auth.CurrentUser;
        if (user != null) {
            user.DeleteAsync().ContinueWith(task => {
                if (task.IsCanceled || task.IsFaulted) {
                    Debug.LogError("DeleteAsync encountered an error: " + task.Exception);
                    return;
                }
            });

            //Šalinamas vartotojas iš duomenų bazės, ištrinami visi duomenys
            FirebaseDatabase.DefaultInstance.GetReference("users").Child(firebase.User.UserId).RemoveValueAsync().ContinueWith(task => {
                if (task.IsFaulted || task.IsCanceled) {
                    Debug.LogError("Failed to delete user data: " + task.Exception);
                } else {
                    Debug.Log("User data deleted from Realtime Database.");
                }
            });

            LogOutAccount();
        }
    }

    public void LogOutAccount() {
        //Vykdomas profilio atsijungimas
        if (firebase.isLoggedIn) {
            gm.data.level1 = 0;
            gm.data.level2 = 0;
            gm.data.level3 = 0;
            gm.data.coins = 0;
            gm.data.frogBodyOwned = false;
            gm.data.thirdPlayerBodyOwned = false;
            firebase.ui.ClearRegisterFields();
            firebase.ui.ClearLoginFields();
            firebase.isLoggedIn = false;
            firebase.auth.SignOut();
        }
        settings.LoadSettings();
        gm.SaveData();
        menu.ClickMainButton();
    }
}