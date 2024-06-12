using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public void OnPlayAgainButtonClicked()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // "Play Again" butonuna týklandýðýnda oyunu yeniden baþlat
        SceneManager.LoadScene(currentSceneIndex - 1);
    }

    public void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif

    }
}
