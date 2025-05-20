using System.Collections;
using UnityEngine;
using Firebase.Auth;
using System.Threading.Tasks;

public class FirebaseUpdate : MonoBehaviour {
    [SerializeField] FirebaseManager firebase;
    [SerializeField] GameManager gm;

    public IEnumerator UpdateUsername() {
        UserProfile profile = new() { DisplayName = firebase.usernameField.text };
        Task ProfileTask = firebase.User.UpdateUserProfileAsync(profile);
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        Task DBTask = firebase.DBreference.Child("users").Child(firebase.User.UserId).Child("username").SetValueAsync(firebase.usernameField.text); 
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        firebase.usernameField.text = firebase.User.DisplayName;
    }

    public IEnumerator UpdateUserStats() {
        Task DBTask = firebase.DBreference.Child("users").Child(firebase.User.UserId).Child("highscore1").SetValueAsync(gm.data.level1);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        DBTask = firebase.DBreference.Child("users").Child(firebase.User.UserId).Child("highscore2").SetValueAsync(gm.data.level2);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        DBTask = firebase.DBreference.Child("users").Child(firebase.User.UserId).Child("highscore3").SetValueAsync(gm.data.level3);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        DBTask = firebase.DBreference.Child("users").Child(firebase.User.UserId).Child("coins").SetValueAsync(gm.data.coins);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        DBTask = firebase.DBreference.Child("users").Child(firebase.User.UserId).Child("characters1").SetValueAsync(gm.data.frogBodyOwned);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        DBTask = firebase.DBreference.Child("users").Child(firebase.User.UserId).Child("characters2").SetValueAsync(gm.data.thirdPlayerBodyOwned);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        UpdateProfileUI();
    }

    public void UpdateProfileUI() {
        firebase.usernameField.text = firebase.User.DisplayName;
        firebase.highscore1Text.text = gm.data.level1.ToString();
        firebase.highscore2Text.text = gm.data.level2.ToString();
        firebase.highscore3Text.text = gm.data.level3.ToString();
        firebase.allCoinsText.text = gm.data.coins.ToString();
    }
}