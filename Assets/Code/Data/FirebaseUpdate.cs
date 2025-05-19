using System.Collections;
using UnityEngine;
using Firebase.Auth;
using System.Threading.Tasks;

public class FirebaseUpdate : MonoBehaviour {
    [SerializeField] FirebaseManager firebase;

    public IEnumerator UpdateUsername() {
        UserProfile profile = new() { DisplayName = firebase.usernameField.text };
        Task ProfileTask = firebase.User.UpdateUserProfileAsync(profile);
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        Task DBTask = firebase.DBreference.Child("users").Child(firebase.User.UserId).Child("username").SetValueAsync(firebase.usernameField.text); 
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        firebase.usernameField.text = firebase.User.DisplayName;
    }

    public IEnumerator UpdateUserStats() {
        Task DBTask = firebase.DBreference.Child("users").Child(firebase.User.UserId).Child("highscore").SetValueAsync(firebase.highscoreText.text);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        DBTask = firebase.DBreference.Child("users").Child(firebase.User.UserId).Child("coins").SetValueAsync(firebase.allCoinsText.text);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        DBTask = firebase.DBreference.Child("users").Child(firebase.User.UserId).Child("characters").SetValueAsync(firebase.ownedCharacters);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
    }
}
