using UnityEngine;

public class ChangeLevel : MonoBehaviour {
    //Foninės nuotraukos komponentai
    GameObject bg1;
    GameObject bg2;

    [SerializeField] GameObject[] sunnyDessert;
    [SerializeField] GameObject[] spookyForrest;
    [SerializeField] GameObject[] pixelCity;

    [SerializeField] GameObject level1;
    [SerializeField] GameObject level2;
    [SerializeField] GameObject level3;
    
    //Fono judėjimo greitis
    [SerializeField] float scrollSpeed;

    //Vienos nuotraukos plotis (Nuotraukų pločiai turi būti vienodi)
    [SerializeField] float imageSize;

    //Nuotraukos y aukštis
    [SerializeField] float imageY;

    [SerializeField] MusicController music;

    private void Start() {
        int id = PlayerPrefs.GetInt("lastLevel");
        if(id == 0 || id == 1 || id == 2)
        {
            ChangeGameLevel(id);
        } else
            ChangeGameLevel(0);


        //Fonui suteikiamas judėjimo greitis:
        //x reikšmei suteikiamas minusinis "scrollSpeed" kintamasis (tada fonas juda link žaidėjo),
        //y - 0, nes y ašyje fonas nejuda
        bg1 = spookyForrest[0];
        bg2 = spookyForrest[1];

        bg1.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed, 0);
        bg2.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed, 0);
    }

    private void Update() {
        //Suteikiamas besikartojančio fono efektas
        //Pasibaigusi nuotrauka perkeliama už sekančios
        if (bg1.transform.position.x < -imageSize) {
            ResetPicture(bg1);
        } else if (bg2.transform.position.x < -imageSize) {
            ResetPicture(bg2);
        }
    }

    private void ResetPicture(GameObject bg) {
        float imageX = ((bg == bg1) ? bg2 : bg1).transform.localPosition.x + imageSize;
        bg.transform.localPosition = new Vector3(imageX, imageY, 0f);
    }

    public void ChangeGameLevel(int levelID) {
        if (levelID == 0) {
            music.ChangeGameMusic(levelID);
            spookyForrest[0].SetActive(false);
            spookyForrest[1].SetActive(false);
            pixelCity[0].SetActive(false);
            pixelCity[1].SetActive(false);
            sunnyDessert[0].SetActive(true);
            sunnyDessert[1].SetActive(true);
            level2.SetActive(false);
            level3.SetActive(false);
            level1.SetActive(true);
            bg1 = sunnyDessert[0];
            bg2 = sunnyDessert[1];
        } else if (levelID == 1) {
            music.ChangeGameMusic(levelID);
            sunnyDessert[0].SetActive(false);
            sunnyDessert[1].SetActive(false);
            pixelCity[0].SetActive(false);
            pixelCity[1].SetActive(false);
            spookyForrest[0].SetActive(true);
            spookyForrest[1].SetActive(true);
            level1.SetActive(false);
            level3.SetActive(false);
            level2.SetActive(true);
            bg1 = spookyForrest[0];
            bg2 = spookyForrest[1];
        } else if (levelID == 2) {
            music.ChangeGameMusic(levelID);
            spookyForrest[0].SetActive(false);
            spookyForrest[1].SetActive(false);
            sunnyDessert[0].SetActive(false);
            sunnyDessert[1].SetActive(false);
            pixelCity[0].SetActive(true);
            pixelCity[1].SetActive(true);
            level2.SetActive(false);
            level1.SetActive(false);
            level3.SetActive(true);
            bg1 = pixelCity[0];
            bg2 = pixelCity[1];
        } else {
            music.ChangeGameMusic(levelID);
            sunnyDessert[0].SetActive(false);
            sunnyDessert[1].SetActive(false);
            pixelCity[0].SetActive(false);
            pixelCity[1].SetActive(false);
            spookyForrest[0].SetActive(true);
            spookyForrest[1].SetActive(true);
            level1.SetActive(false);
            level3.SetActive(false);
            level2.SetActive(true);
            bg1 = spookyForrest[0];
            bg2 = spookyForrest[1];
        }

        bg1.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed, 0);
        bg2.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed, 0);
    }
}