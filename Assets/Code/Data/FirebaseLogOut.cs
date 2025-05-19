using UnityEngine;

public class FirebaseLogOut : MonoBehaviour {
    [SerializeField] FirebaseManager firebase;
    [SerializeField] GameManager gm;
    [SerializeField] SwitchPlayer player;

    public void SignOut() {
        if(firebase.isLoggedIn) {
            gm.data.level1 = 0;
            gm.data.level2 = 0;
            gm.data.level3 = 0;
            gm.data.coins = 0;
            gm.data.ownedCharacters = "";
            firebase.ui.ClearRegisterFields();
            firebase.ui.ClearLoginFields();
            firebase.profileButtonText.text = "Profile";
            player.LoadSettings();
            firebase.isLoggedIn = false;
        }
        firebase.auth.SignOut();
        gm.SaveData();
    }
}