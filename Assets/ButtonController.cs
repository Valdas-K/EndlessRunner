using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {
    [SerializeField] Transform levelContainer;
    [SerializeField] AudioSource clickSound;
    [SerializeField] Button[] allButtons;

    private void Start() {
        UpdateButtonSound();
        LoadLevelButtons();
    }

    private void UpdateButtonSound() {
        //Kiekvienam mygtukui yra pridedamas paspaudimo įvykis
        for (int i = 0; i < allButtons.Length; i++) {
            Button button = allButtons[i];
            button.onClick.AddListener(() => ClickButton());
        }
    }

    //Paleidžiamas mygtuko paspaudimo efektas
    public void ClickButton() {
        clickSound.Play();
    }

    private void LoadLevelButtons() {
        //Kiekvienam lygio mygtukui yra pridedamas paspaudimo įvykis
        for (int i = 0; i < levelContainer.childCount; i++) {
            Transform level = levelContainer.GetChild(i);
            int currentIndex = level.GetSiblingIndex();
            Button button = level.GetComponent<Button>();
            button.onClick.AddListener(() => ClickLoadLevelButton(currentIndex));
        }
    }

    private void ClickLoadLevelButton(int index) {
        //Lygio perjungimo mygtukas
        Debug.Log("level index " + index);
    }
}