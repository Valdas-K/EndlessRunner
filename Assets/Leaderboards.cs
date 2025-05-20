using Firebase.Database;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Leaderboards : MonoBehaviour
{
    [SerializeField] FirebaseManager firebaseManager;
    public GameObject scoreElement;
    public Transform scoreboardContent;

    public IEnumerator LoadScoreboardData()
    {

        //Get all the users data ordered by kills amount
        Task<DataSnapshot> DBTask = firebaseManager.DBreference.Child("users").OrderByChild("highscore1").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);


        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Destroy any existing scoreboard elements
            foreach (Transform child in scoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int score1 = int.Parse(childSnapshot.Child("highscore1").Value.ToString());


                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, scoreboardContent);
                //scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, score1);
            }
        }
    }
}
