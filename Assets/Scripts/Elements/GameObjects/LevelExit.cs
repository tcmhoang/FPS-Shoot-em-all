using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Elements.GameObjects
{
    public class LevelExit : MonoBehaviour
    {
        public string NextLevel;

        private float _delayToEnd = 3.75f;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player") return;
            GameManager.Instance.LevelEnding = true;
            StartCoroutine(Delay(_delayToEnd));

            AudioManager.Instance.PlayLevelVictory();
            PlayerPrefs.SetString("CurrentLevel",NextLevel);
        }

        private IEnumerator Delay(float delay)
        {
            PlayerPrefs.DeleteKey($"{NextLevel}_cp");
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(NextLevel);
        }
    }
}
