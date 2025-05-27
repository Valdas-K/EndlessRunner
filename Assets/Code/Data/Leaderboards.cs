using System;
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
        firebaseManager.DBreference.Child("users").OrderByChild("highscore1").LimitToFirst(10).GetValueAsync().ContinueWith(task => {
            if (task.IsCompleted) {
                //Neapdoroti duomenys
                Dictionary<string, object> unsortedLeaderboard = (Dictionary<string, object>)task.Result.Value;

                //Gaunami atskiri vartotojų vardų ir jų rezultatų duomenys
                string[] names = unsortedLeaderboard.Values.Select(value => (Dictionary<string, object>)value).
                Select(d => d["username"].ToString()).ToArray();
                int[] scores = unsortedLeaderboard.Values.Select(value => (Dictionary<string, object>)value).
                Select(d => Convert.ToInt32(d["highscore1"])).ToArray();

                //Gauti duomenys sujungiami į Tuple tipą
                Tuple<string, int>[] userScores = new Tuple<string, int>[scores.Length];
                for (int i = 0; i < scores.Length; i++) {
                    userScores[i] = new(names[i], scores[i]);
                }

                //Tuple tipas surūšiuoja pagal rezultatus
                Tuple<string, int>[] sortedScores = userScores.OrderByDescending(tuple => tuple.Item2).ToArray();
                string leaderboard = "";
                for (int i = 0; i < sortedScores.Length; i++) {
                    //Rezultatai atvaizduojami
                    leaderboard += (i+1) + ". " + sortedScores[i].Item1 + " - " + sortedScores[i].Item2 + "\n";
                }

                context.Post(_ => {
                    //Teksto atnaujinimas perkeliamas į pagrindinę giją, vartotojas mato rezultatus
                    leaderboard1.text = leaderboard;
                }, null);
            }
        });

        firebaseManager.DBreference.Child("users").OrderByChild("highscore2").LimitToLast(10).GetValueAsync().ContinueWith(task => {
            if (task.IsCompleted) {
                //Neapdoroti duomenys
                Dictionary<string, object> unsortedLeaderboard = (Dictionary<string, object>)task.Result.Value;

                //Gaunami atskiri vartotojų vardų ir jų rezultatų duomenys
                string[] names = unsortedLeaderboard.Values.Select(value => (Dictionary<string, object>)value).
                Select(d => d["username"].ToString()).ToArray();
                int[] scores = unsortedLeaderboard.Values.Select(value => (Dictionary<string, object>)value).
                Select(d => Convert.ToInt32(d["highscore2"])).ToArray();

                //Gauti duomenys sujungiami į Tuple tipą
                Tuple<string, int>[] userScores = new Tuple<string, int>[scores.Length];
                for (int i = 0; i < scores.Length; i++) {
                    userScores[i] = new(names[i], scores[i]);
                }

                //Tuple tipas surūšiuoja pagal rezultatus
                Tuple<string, int>[] sortedScores = userScores.OrderByDescending(tuple => tuple.Item2).ToArray();
                string leaderboard = "";
                for (int i = 0; i < sortedScores.Length; i++) {
                    //Rezultatai atvaizduojami
                    leaderboard += (i + 1) + ". " + sortedScores[i].Item1 + " - " + sortedScores[i].Item2 + "\n";
                }

                context.Post(_ => {
                    //Teksto atnaujinimas perkeliamas į pagrindinę giją, vartotojas mato rezultatus
                    leaderboard2.text = leaderboard;
                }, null);
            }
        });

        firebaseManager.DBreference.Child("users").OrderByChild("highscore3").LimitToLast(10).GetValueAsync().ContinueWith(task => {
            if (task.IsCompleted) {
                //Neapdoroti duomenys
                Dictionary<string, object> unsortedLeaderboard = (Dictionary<string, object>)task.Result.Value;

                //Gaunami atskiri vartotojų vardų ir jų rezultatų duomenys
                string[] names = unsortedLeaderboard.Values.Select(value => (Dictionary<string, object>)value).
                Select(d => d["username"].ToString()).ToArray();
                int[] scores = unsortedLeaderboard.Values.Select(value => (Dictionary<string, object>)value).
                Select(d => Convert.ToInt32(d["highscore3"])).ToArray();

                //Gauti duomenys sujungiami į Tuple tipą
                Tuple<string, int>[] userScores = new Tuple<string, int>[scores.Length];
                for (int i = 0; i < scores.Length; i++) {
                    userScores[i] = new(names[i], scores[i]);
                }

                //Tuple tipas surūšiuoja pagal rezultatus
                Tuple<string, int>[] sortedScores = userScores.OrderByDescending(tuple => tuple.Item2).ToArray();
                string leaderboard = "";
                for (int i = 0; i < sortedScores.Length; i++) {
                    //Rezultatai atvaizduojami
                    leaderboard += (i + 1) + ". " + sortedScores[i].Item1 + " - " + sortedScores[i].Item2 + "\n";
                }

                context.Post(_ => {
                    //Teksto atnaujinimas perkeliamas į pagrindinę giją, vartotojas mato rezultatus
                    leaderboard3.text = leaderboard;
                }, null);
            }
        });

        yield return null;
    }
}