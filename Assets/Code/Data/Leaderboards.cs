using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;

public class Leaderboards : MonoBehaviour {
    [SerializeField] FirebaseManager firebaseManager;
    public TMP_Text leaderboard1;
    public TMP_Text leaderboard2;
    public TMP_Text leaderboard3;
    private SynchronizationContext context;

    private void Awake() {
        context = SynchronizationContext.Current;
    }

    public IEnumerator LoadScoreboardData() {
        //Atnaujinami geriausi visų lygių rezultatai
        firebaseManager.DBreference.Child("users").OrderByChild("highscore1").LimitToLast(10).GetValueAsync().ContinueWith(task => {
            if (task.IsCompleted) {
                //Neapdoroti duomenys
                Dictionary<string, object> unsortedLeaderboard = (Dictionary<string, object>)task.Result.Value;

                //Apdorojami ir rūšiuojami duomenys
                List<KeyValuePair<string, object>> unsortedLeaderboardList = new(unsortedLeaderboard);
                List<KeyValuePair<string, object>> sortedLeaderboard = unsortedLeaderboardList.OrderBy((x) => 
                    ((Dictionary<string, object>)x.Value)["highscore1"].ToString()).ToList();

                //Informacijos atvaizdavimas
                int rank = 1;
                string leaderboard = "";
                foreach (KeyValuePair<string, object> result in sortedLeaderboard) {
                    Dictionary<string, object> resultData = (Dictionary<string, object>)result.Value;
                    leaderboard += rank + ". " + resultData["username"].ToString() + " - " + resultData["highscore1"].ToString() + "\n";
                    rank++;
                }

                context.Post(_ => {
                    //Teksto atnaujinimas perkeliamas į pagrindinę giją
                    leaderboard1.text = leaderboard;
                }, null);
            }
        });

        firebaseManager.DBreference.Child("users").OrderByChild("highscore2").LimitToLast(10).GetValueAsync().ContinueWith(task => {
            if (task.IsCompleted) {
                //Neapdoroti duomenys
                Dictionary<string, object> unsortedLeaderboard = (Dictionary<string, object>)task.Result.Value;

                //Apdorojami ir rūšiuojami duomenys
                List<KeyValuePair<string, object>> unsortedLeaderboardList = new(unsortedLeaderboard);
                List<KeyValuePair<string, object>> sortedLeaderboard = unsortedLeaderboardList.OrderBy((x) =>
                    ((Dictionary<string, object>)x.Value)["highscore2"].ToString()).ToList();

                //Informacijos atvaizdavimas
                int rank = 1;
                string leaderboard = "";
                foreach (KeyValuePair<string, object> result in sortedLeaderboard) {
                    Dictionary<string, object> resultData = (Dictionary<string, object>)result.Value;
                    leaderboard += rank + ". " + resultData["username"].ToString() + " - " + resultData["highscore2"].ToString() + "\n";
                    rank++;
                }

                context.Post(_ => {
                    //Teksto atnaujinimas perkeliamas į pagrindinę giją
                    leaderboard2.text = leaderboard;
                }, null);
            }
        });

        firebaseManager.DBreference.Child("users").OrderByChild("highscore3").LimitToLast(10).GetValueAsync().ContinueWith(task => {
            if (task.IsCompleted) {
                //Neapdoroti duomenys
                Dictionary<string, object> unsortedLeaderboard = (Dictionary<string, object>)task.Result.Value;

                //Apdorojami ir rūšiuojami duomenys
                List<KeyValuePair<string, object>> unsortedLeaderboardList = new(unsortedLeaderboard);
                List<KeyValuePair<string, object>> sortedLeaderboard = unsortedLeaderboardList.OrderBy((x) =>
                    ((Dictionary<string, object>)x.Value)["highscore3"].ToString()).ToList();

                //Informacijos atvaizdavimas
                int rank = 1;
                string leaderboard = "";
                foreach (KeyValuePair<string, object> result in sortedLeaderboard) {
                    Dictionary<string, object> resultData = (Dictionary<string, object>)result.Value;
                    leaderboard += rank + ". " + resultData["username"].ToString() + " - " + resultData["highscore3"].ToString() + "\n";
                    rank++;
                }

                context.Post(_ => {
                    //Teksto atnaujinimas perkeliamas į pagrindinę giją
                    leaderboard3.text = leaderboard;
                }, null);
            }
        });

        yield return null;
    }
}