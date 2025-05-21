using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {
    //Lygių konteinerio, paspaudimo garso, visų mygtukų masyvo ir lygių keitimo kintamieji
    [SerializeField] Transform levelContainer;
    [SerializeField] AudioSource clickSound;
    [SerializeField] ChangeLevel level;
    [SerializeField] Button[] allButtons;

    private void Start() {
        //Visiems mygtukams pridedami lygių perjungimai ir paspaudimo garsai
        UpdateButtonSound();
        LoadLevelButtons();
    }

    private void UpdateButtonSound() {
        //Kiekvienam mygtukui yra pridedamas paspaudimo įvykis, kuris paleis garsą
        for (int i = 0; i < allButtons.Length; i++) {
            Button button = allButtons[i];
            button.onClick.AddListener(() => ClickButton());
        }
    }

    public void ClickButton() {
        //Mygtukų paspaudimo garso paleidimas
        clickSound.Play();
    }

    private void LoadLevelButtons() {
        //Kiekvienam lygio mygtukui yra pridedamas paspaudimo įvykis, kuris perjungs reikiamą lygį
        for (int i = 0; i < levelContainer.childCount; i++) {
            Transform level = levelContainer.GetChild(i);
            int currentIndex = level.GetSiblingIndex();
            Button button = level.GetComponent<Button>();
            button.onClick.AddListener(() => ClickLoadLevelButton(currentIndex));
        }
    }

    private void ClickLoadLevelButton(int index) {
        //Perjungiamas lygis ir išsaugomas pasirinkimas
        level.ChangeGameLevel(index);
        PlayerPrefs.SetInt("lastLevel", index);
        GameManager.Instance.chosenLevel = index;
    }
}