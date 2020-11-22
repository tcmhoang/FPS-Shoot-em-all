using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private const float DELAY_TIME = 2;

        public AudioSource FootstepSlow, FootstepFast;

        [HideInInspector] public bool LevelEnding;



        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseUnPause();
            }
        }

        public void PauseUnPause()
        {
            var pauseScreen = UIController.Instance.PauseScreen;

            if (pauseScreen.activeInHierarchy)
            {
                pauseScreen.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;

                Cursor.visible = false;

                FootstepFast.Play();
                FootstepSlow.Play();
            }
            else
            {
                pauseScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;

                Cursor.visible = true;

                FootstepFast.Stop();
                FootstepSlow.Stop();
            }
        }

        public void ReloadLevel()
        {
            StartCoroutine(ReloadInvoke());
        }

        public IEnumerator ReloadInvoke()
        {
            yield return new WaitForSeconds(DELAY_TIME);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
}