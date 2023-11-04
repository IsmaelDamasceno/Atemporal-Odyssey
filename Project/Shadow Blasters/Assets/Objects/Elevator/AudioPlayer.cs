using UnityEngine;

namespace Elevator
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip engineClip;

        private AudioSource source;

        void Start()
        {
            source = GetComponent<AudioSource>();
            source.loop = true;
        }

        public void PlayEngine()
        {
            StopEngine();
            source.clip = engineClip;
            source.Play();
        }

        public void StopEngine()
        {
            source.Stop();
        }
    }

}