using UnityEngine;

public class FirebaseLogOut : MonoBehaviour {
    [SerializeField] FirebaseManager firebase;
    [SerializeField] GameManager gm;
    [SerializeField] SwitchPlayer player;

    public void SignOut() {
        firebase.auth.SignOut();
        firebase.gameUI.OpenLoginMenu();

        firebase.isLoggedIn = false;
        firebase.profileButtonText.text = "Profile";
        firebase.ui.ClearRegisterFields();
        firebase.ui.ClearLoginFields();

        gm.data.highscore = 0;
        gm.data.coins = 0;
        gm.data.ownedCharacters = "";

        player.LoadSettings();
        player.hintText = "Buy";
        player.ChangeHintText();
    }
}