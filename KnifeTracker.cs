using UnityEngine;

namespace ScratchLab
{
    public class KnifeTracker : MonoBehaviour
    {
        private Vector3 lastPosition;
        private float spawnTime;

        public float CurrentSpeed { get; private set; }

        private void Start()
        {
            lastPosition = transform.position;
            spawnTime = Time.time;
        }

        private void Update()
        {
            float delta = Time.deltaTime;

            if (delta <= 0f)
                return;

            CurrentSpeed =
                Vector3.Distance(transform.position, lastPosition) / delta;

            lastPosition = transform.position;
        }

        public bool CanScratch()
        {
            if (!Config.ModEnabled)
                return false;

            if (Time.time - spawnTime < Config.PickupDelay)
                return false;

            return CurrentSpeed >= Config.MinimumSwipeSpeed;
        }

        public float Strength()
        {
            return Utilities.StrengthFromSpeed(CurrentSpeed);
        }
    }
}
