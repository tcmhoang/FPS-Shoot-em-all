using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private const float DELAY_TIME = 2;

        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        private void Update()
        {
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