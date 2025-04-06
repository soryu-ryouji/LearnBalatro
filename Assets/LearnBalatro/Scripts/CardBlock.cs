using UnityEngine;

namespace LearnBalatro
{
    public class CardBlock : MonoBehaviour
    {
        public float rotationAngle = 10f;

        private void Update()
        {
            Idle();
        }

        private void Idle()
        {
            float sine = Mathf.Sin(Time.time);
            float cosine = Mathf.Cos(Time.time);

            transform.rotation = Quaternion.Euler(sine * rotationAngle, cosine * rotationAngle, 0);
        }
    }
}
