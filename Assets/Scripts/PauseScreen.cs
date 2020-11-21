using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class PauseScreen : MonoBehaviour
    {
        public string MainMenuName;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void Resume()
        {
            GameManager.Instance.PauseUnPause();
        }

        public void MainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(MainMenuName);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}