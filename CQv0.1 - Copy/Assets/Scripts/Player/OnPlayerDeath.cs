using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class OnPlayerDeath : MonoBehaviour
{
    //Sprite deathScreenshot;
    //public Image deathBackground;
    //Texture2D deathScreenshotTexture;

    private void OnEnable()
    {
        PlayerStatus.OnPlayerDeath += EnableDeathScreen;  //adds function to OnPlayerDamagedEvent ?
    }
    private void OnDisable()
    {
        PlayerStatus.OnPlayerDeath -= EnableDeathScreen;
    }


    void Start()
    {
        //deathScreenshot = deathBackground.sprite;
    }
    private void Update()
    {
        /*
        if (SceneManager.GetActiveScene().name == "YouDied")
        {
            ChangeUIBackground();
        }
        */
    }


    public void EnableDeathScreen()
    {
        ScenesManager.Instance.LoadYouDied();
    }
/*
    public void ChangeUIBackground()
    {
        deathScreenshot = TakeDeathScreenshot();
        GameObject canvas = this.gameObject;
        canvas.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = deathScreenshot;

    }


    public Sprite TakeDeathScreenshot()
    {
        ScreenCapture.CaptureScreenshot("DeathScreenshot.png");
        var directoryPath = Application.dataPath + "/DeathScreenshot.png";

        deathScreenshotTexture = ConvertFileIntoTexture(directoryPath);
        Sprite screenshotSprite = Sprite.Create(deathScreenshotTexture, new Rect(0, 0, deathScreenshotTexture.width, deathScreenshotTexture.height), new Vector2(0.5f, 0.5f));
          deathScreenshot = screenshotSprite;
        return deathScreenshot;

    }


public Texture2D ConvertFileIntoTexture(string FilePath)
    {

        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D screenshotTexture;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            screenshotTexture = new Texture2D(2, 2);           // Create new "empty" texture
            if (screenshotTexture.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return screenshotTexture;                 // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
    }
*/

}
