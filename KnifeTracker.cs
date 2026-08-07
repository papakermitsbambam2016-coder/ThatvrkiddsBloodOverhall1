using UnityEngine;

namespace ScratchLab
{
    public class KnifeTracker : MonoBehaviour
    {
        private Vector3 lastPosition;
        private float spawnTime;
        private bool initialized;

        public float CurrentSpeed { get; private set; }

        private void Awake()
        {
            lastPosition = transform.position;
            spawnTime = Time.time;
            initialized = true;
        }

        private void OnEnable()
        {
            if (!initialized)
            {
                lastPosition = transform.position;
                spawnTime = Time.time;
                initialized = true;
            }
        }

        private void Update()
        {
            float delta = Time.deltaTime;

            if (delta <= 0f)
                return;

            Vector3 currentPosition = transform.position;

            CurrentSpeed =
                Vector3.Distance(currentPosition, lastPosition) / delta;

            lastPosition = currentPosition;
        }

        public bool CanScratch()
        {
            if (!Config.ModEnabled)
                return false;

            if (Time.time - spawnTime < Config.PickupDelay)
                return false;

            return CurrentSpeed >= Config.MinimumSwipeSpeed &&
                   CurrentSpeed <= Config.MaximumSwipeSpeed;
        }

        public float Strength()
        {
            return Utilities.StrengthFromSpeed(CurrentSpeed);
        }
    }
}
