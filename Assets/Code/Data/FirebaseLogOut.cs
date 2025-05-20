using UnityEngine;

public class FirebaseLogOut : MonoBehaviour {
    [SerializeField] FirebaseManager firebase;
    [SerializeField] GameManager gm;
    [SerializeField] SwitchPlayer player;

    public void SignOut() {
        gm.SaveData();
        if (firebase.isLoggedIn) {
            gm.data.level1 = 0;
            gm.data.level2 = 0;
            gm.data.level3 = 0;
            gm.data.coins = 0;
            gm.data.frogBodyOwned = false;
            gm.data.thirdPlayerBodyOwned = false;
            firebase.ui.ClearRegisterFields();
            firebase.ui.ClearLoginFields();
            player.LoadSettings();
            firebase.isLoggedIn = false;
        }
        firebase.auth.SignOut();
        gm.SaveData();
    }
}