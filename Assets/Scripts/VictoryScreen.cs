using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class VictoryScreen : MonoBehaviour
    {
        public string MainMenuScene;

        private const float TimeDelay = 2f;

        public GameObject Context, ReturnButton;

        public Image BlackImage;
        private const float FadeSpeed = 1f;

        // Start is called before the first frame update
        private void Start()
        {
            StartCoroutine(DelayShow(TimeDelay));

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Update()
        {
            BlackImage.color = new Color(BlackImage.color.r,BlackImage.color.g,BlackImage.color.b,Mathf.MoveTowards(BlackImage.color.a,0,FadeSpeed * Time.deltaTime));
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(MainMenuScene);
        }

        public IEnumerator DelayShow(float delaySecs)
        {
            yield return new WaitForSeconds(delaySecs);
            Context.SetActive(true);
            yield return new WaitForSeconds(delaySecs);
            ReturnButton.SetActive(true);
        }
    }
}