using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {
    //Foninės nuotraukos
    private GameObject bg1;
    private GameObject bg2;

    //Lygių nuotraukų masyvai
    [SerializeField] GameObject[] sunnyDessert;
    [SerializeField] GameObject[] spookyForrest;
    [SerializeField] GameObject[] pixelCity;

    //Lygių konteineriai
    [SerializeField] GameObject level1;
    [SerializeField] GameObject level2;
    [SerializeField] GameObject level3;

    //Fono judėjimo greitis, vienos nuotraukos plotis (Nuotraukų pločiai vienodi) ir y aukštis
    [SerializeField] float scrollSpeed;
    [SerializeField] float imageSize;
    [SerializeField] float imageY;

    //Lygių konteinerio, paspaudimo garso, visų mygtukų masyvo ir lygių keitimo kintamieji
    [SerializeField] Transform levelContainer;
    [SerializeField] AudioSource clickSound;
    [SerializeField] Button[] allButtons;
    public int levelId;

    private void Start() {
        //Visiems mygtukams pridedami lygių perjungimai ir paspaudimo garsai
        UpdateButtonSound();
        LoadLevelButtons();
        ChangeGameLevel(levelId);
    }

    private void Update() {
        //Suteikiamas besikartojančio fono efektas: pasibaigusi nuotrauka perkeliama už sekančios
        if (bg1.transform.position.x < -imageSize) {
            ResetPicture(bg1);
        } else if (bg2.transform.position.x < -imageSize) {
            ResetPicture(bg2);
        }
    }

    private void ResetPicture(GameObject bg) {
        //Perkeliama reikiama nuotrauka
        float imageX = ((bg == bg1) ? bg2 : bg1).transform.localPosition.x + imageSize;
        bg.transform.localPosition = new Vector3(imageX, imageY, 0f);
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
            button.onClick.AddListener(() => ChangeGameLevel(currentIndex));
        }
    }

    public void ChangeGameLevel(int index) {
        //Perjungiamas reikiamas lygis
        levelId = index;

        //Išjungiami nereikalingi komponentai
        level2.SetActive(false);
        level3.SetActive(false);
        level1.SetActive(false);

        //Parenkamas lygis pagal lygio id reikšmę ir įjungiami komponentai
        switch (index) {
            case 0:
                level1.SetActive(true);
                bg1 = sunnyDessert[0];
                bg2 = sunnyDessert[1];
                break;
            case 1:
                level2.SetActive(true);
                bg1 = spookyForrest[0];
                bg2 = spookyForrest[1];
                break;
            case 2:
                level3.SetActive(true);
                bg1 = pixelCity[0];
                bg2 = pixelCity[1];
                break;
            default:
                level2.SetActive(true);
                bg1 = spookyForrest[0];
                bg2 = spookyForrest[1];
                break;
        }

        //Fono nuotraukoms suteikiamas greitis
        bg1.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed, 0);
        bg2.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed, 0);
    }
}