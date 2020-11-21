using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Elements
{
    public class CheckpointController : MonoBehaviour
    {
        public string Name;

        // Start is called before the first frame update
        private void Start()
        {
            if (PlayerPrefs.HasKey($"{SceneManager.GetActiveScene().name}_cp") &&
                PlayerPrefs.GetString($"{SceneManager.GetActiveScene().name}_cp") == Name)
            {
                PlayerController.Instance.transform.position = transform.position;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKey(KeyCode.L))
                PlayerPrefs.DeleteKey($"{SceneManager.GetActiveScene().name}_cp");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "Player") return;
            PlayerPrefs.SetString($"{SceneManager.GetActiveScene().name}_cp", Name);
            AudioManager.Instance.PlaySfx(SoundIndex.Checkpoint);
        }
    }
}