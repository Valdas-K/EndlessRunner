using TMPro;
using UnityEngine;

public class FirebaseUI : MonoBehaviour {
    [SerializeField] FirebaseManager firebase;
    [SerializeField] GameManager gm;

    //Profilio lange esantys teksto elementai
    [SerializeField] TMP_Text allCoinsText;
    [SerializeField] TMP_Text highscore1Text;
    [SerializeField] TMP_Text highscore2Text;
    [SerializeField] TMP_Text highscore3Text;

    public void ClearRegisterFields() {
        //Išvalomi registracijos laukai
        firebase.register.usernameRegister.text = "";
        firebase.register.emailRegister.text = "";
        firebase.register.passwordRegister.text = "";
        firebase.register.passwordConfirmRegister.text = "";
    }

    public void ClearLoginFields() {
        //Išvalomi prisijungimo laukai
        firebase.login.emailLogin.text = "";
        firebase.login.passwordLogin.text = "";
    }

    public void UpdateProfileUI() {
        //Atnaujinami profilio lango elementai
        firebase.usernameField.text = firebase.User.DisplayName;
        highscore1Text.text = gm.data.level1.ToString();
        highscore2Text.text = gm.data.level2.ToString();
        highscore3Text.text = gm.data.level3.ToString();
        allCoinsText.text = gm.data.coins.ToString();
    }
}