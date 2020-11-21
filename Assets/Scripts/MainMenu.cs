using System.Security;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        public string FirstLevel;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void PlayGame()
        {
            SceneManager.LoadScene(FirstLevel);
        }

        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("Quitting Game");
        }
    }
}
