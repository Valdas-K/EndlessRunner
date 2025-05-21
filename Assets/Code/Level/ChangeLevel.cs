using UnityEngine;

public class ChangeLevel : MonoBehaviour {
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

    private void Start() {
        //Parenkamas paskutinis parinktas lygis, jei tokio kintamojo nėra, tada užkraunamas antras lygis (id = 1)
        int id = PlayerPrefs.GetInt("lastLevel");
        if (id == 0 || id == 1 || id == 2) {
            ChangeGameLevel(id);
        } else {
            ChangeGameLevel(1);
        }

        //Fonui suteikiamas judėjimo greitis: y - 0, nes y ašyje fonas nejuda,
        //x reikšmei suteikiamas minusinis "scrollSpeed" kintamasis, kad fonas judėtų link žaidėjo
        bg1.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed, 0);
        bg2.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed, 0);
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

    public void ChangeGameLevel(int levelID) {
        //Išjungiami nereikalingi komponentai
        level2.SetActive(false);
        level3.SetActive(false);
        level1.SetActive(false);

        //Parenkamas lygis pagal lygio id reikšmę ir įjungiami komponentai
        switch (levelID) {
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