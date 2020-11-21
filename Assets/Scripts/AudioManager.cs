using UnityEngine;

namespace Assets.Scripts
{
    public enum SoundIndex
    {
        BoundPad,
        Checkpoint,
        Explosion,
        Ammo,
        Gun,
        Health,
        Dead,
        Hurt,
        Jump
    }

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        public AudioSource bgm;

        public AudioSource[] SoundEffects;

        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public void StopBgm()
        {
            bgm.Stop();
        }

        public void PlaySfx(SoundIndex index)
        {
            StopSfx(index);
            SoundEffects[(int) index].Play();
        }

        public void StopSfx(SoundIndex index)
        {
            SoundEffects[(int) index].Stop();
        }
    }
}