using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Leaderboards : MonoBehaviour {
    [SerializeField] FirebaseManager firebaseManager;
    public GameObject scoreElement;
    public Transform scoreboardContent;
    public TextMeshProUGUI leaderboardText;

    public IEnumerator LoadScoreboardData() {
        firebaseManager.DBreference.Child("users").OrderByChild("highscore1").LimitToFirst(10).GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted) {
                Debug.LogError("Error loading leaderboard data: " + task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;
            Dictionary<string, object> leaderboardData = (Dictionary<string, object>)snapshot.Value;

            List<KeyValuePair<string, object>> sortedLeaderboard = new List<KeyValuePair<string, object>>(leaderboardData);
            sortedLeaderboard.Sort((x, y) => ((Dictionary<string, object>)y.Value)["highscore1"].ToString().CompareTo(((Dictionary<string, object>)x.Value)["highscore1"].ToString()));

            string leaderboardTextContent = "";
            int rank = 1;
            foreach (KeyValuePair<string, object> entry in sortedLeaderboard) {
                Dictionary<string, object> entryData = (Dictionary<string, object>)entry.Value;
                string name = entryData["username"].ToString();
                string score = entryData["highscore1"].ToString();

                leaderboardTextContent += rank + ". " + name + " - " + score + "\n";
                rank++;
            }

            leaderboardText.text = leaderboardTextContent;
            leaderboardText.text = "";
            leaderboardText.text = leaderboardTextContent;
        });

        yield return new WaitForSecondsRealtime(0f);
    }
}