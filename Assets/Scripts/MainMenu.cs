using System.Security;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        public string FirstLevel;


        public GameObject ContinueButton;

        // Start is called before the first frame update
        private void Start()
        {
            if (PlayerPrefs.HasKey("CurrentLevel")) return;
            ContinueButton.SetActive(false);
        }


        public void PlayGame()
        {
            SceneManager.LoadScene(FirstLevel);
            PlayerPrefs.DeleteAll();
        }

        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("Quitting Game");
        }

        public void Continue()
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("CurrentLevel"));
        }
    }
}