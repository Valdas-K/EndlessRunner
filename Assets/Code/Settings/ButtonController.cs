using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {
    //Foninės nuotraukos
    private GameObject layer0_0;
    private GameObject layer0_1;
    private GameObject layer1_0;
    private GameObject layer1_1;
    private GameObject layer2_0;
    private GameObject layer2_1;
    private GameObject layer3_0;
    private GameObject layer3_1;
    private GameObject layer4_0;
    private GameObject layer4_1;

    //Lygių nuotraukų masyvai
    [SerializeField] GameObject[] sunnyDessert;
    [SerializeField] GameObject[] spookyForrest;
    [SerializeField] GameObject[] pixelMountain;

    //Lygių konteineriai
    [SerializeField] GameObject level1;
    [SerializeField] GameObject level2;
    [SerializeField] GameObject level3;

    //Fono judėjimo greitis, vienos nuotraukos plotis (Nuotraukų pločiai vienodi) ir y aukštis
    [SerializeField] float[] scrollSpeed;
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
        if (layer0_0.transform.position.x < -imageSize) {
            ResetPicture(layer0_0, 0);
        } else if (layer0_1.transform.position.x < -imageSize) {
            ResetPicture(layer0_1, 0);
        }
        if (layer1_0.transform.position.x < -imageSize) {
            ResetPicture(layer1_0, 1);
        } else if (layer1_1.transform.position.x < -imageSize) {
            ResetPicture(layer1_1, 1);
        }
        if (layer2_0.transform.position.x < -imageSize) {
            ResetPicture(layer2_0, 2);
        } else if (layer2_1.transform.position.x < -imageSize) {
            ResetPicture(layer2_1, 2);
        } 
        if (layer3_0.transform.position.x < -imageSize) {
            ResetPicture(layer3_0, 3);
        } else if (layer3_1.transform.position.x < -imageSize) {
            ResetPicture(layer3_1, 3);
        }
        if (layer4_0.transform.position.x < -imageSize) {
            ResetPicture(layer4_0, 4);
        } else if (layer4_1.transform.position.x < -imageSize) {
            ResetPicture(layer4_1, 4);
        }
    }

    private void ResetPicture(GameObject bg, int layer) {
        //Perkeliama reikiama nuotrauka
        float imageX = 0f;
        if (layer == 0) {
            imageX = ((bg == layer0_0) ? layer0_1 : layer0_0).transform.localPosition.x + imageSize;
        }
        if (layer == 1) {
            imageX = ((bg == layer1_0) ? layer1_1 : layer1_0).transform.localPosition.x + imageSize;
        }
        if (layer == 2) {
            imageX = ((bg == layer2_0) ? layer2_1 : layer2_0).transform.localPosition.x + imageSize;
        }
        if (layer == 3) {
            imageX = ((bg == layer3_0) ? layer3_1 : layer3_0).transform.localPosition.x + imageSize;
        }
        if (layer == 4) {
            imageX = ((bg == layer4_0) ? layer4_1 : layer4_0).transform.localPosition.x + imageSize;
        }
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
                layer0_0 = sunnyDessert[0];
                layer0_1 = sunnyDessert[1];
                layer1_0 = sunnyDessert[2];
                layer1_1 = sunnyDessert[3];
                layer2_0 = sunnyDessert[4];
                layer2_1 = sunnyDessert[5];
                layer3_0 = sunnyDessert[6];
                layer3_1 = sunnyDessert[7];
                layer4_0 = sunnyDessert[8];
                layer4_1 = sunnyDessert[9];
                break;
            case 1:
                level2.SetActive(true);
                layer0_0 = spookyForrest[0];
                layer0_1 = spookyForrest[1];
                layer1_0 = spookyForrest[2];
                layer1_1 = spookyForrest[3];
                layer2_0 = spookyForrest[4];
                layer2_1 = spookyForrest[5];
                layer3_0 = spookyForrest[6];
                layer3_1 = spookyForrest[7];
                layer4_0 = spookyForrest[8];
                layer4_1 = spookyForrest[9];
                break;
            case 2:
                level3.SetActive(true);
                layer0_0 = pixelMountain[0];
                layer0_1 = pixelMountain[1];
                layer1_0 = pixelMountain[2];
                layer1_1 = pixelMountain[3];
                layer2_0 = pixelMountain[4];
                layer2_1 = pixelMountain[5];
                layer3_0 = pixelMountain[6];
                layer3_1 = pixelMountain[7];
                layer4_0 = pixelMountain[8];
                layer4_1 = pixelMountain[9];
                break;
            default:
                level2.SetActive(true);
                layer0_0 = sunnyDessert[0];
                layer0_1 = sunnyDessert[1];
                layer1_0 = sunnyDessert[2];
                layer1_1 = sunnyDessert[3];
                layer2_0 = sunnyDessert[4];
                layer2_1 = sunnyDessert[5];
                layer3_0 = sunnyDessert[6];
                layer3_1 = sunnyDessert[7];
                layer4_0 = sunnyDessert[8];
                layer4_1 = sunnyDessert[9];
                break;
        }

        //Fono nuotraukoms suteikiamas greitis
        layer0_0.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed[0], 0);
        layer0_1.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed[0], 0);
        layer1_0.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed[1], 0);
        layer1_1.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed[1], 0);
        layer2_0.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed[2], 0);
        layer2_1.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed[2], 0);
        layer3_0.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed[3], 0);
        layer3_1.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed[3], 0);
        layer4_0.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed[4], 0);
        layer4_1.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed[4], 0);
    }
}