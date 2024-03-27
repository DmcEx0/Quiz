using UnityEngine;

namespace Quiz.Tool
{
    public class CameraCentrer : MonoBehaviour
    {
        private const float AdditionalOffset = 0.5f;

        [SerializeField] private float _cameraOffcet = 2f;

        public void CenteringCamera(int width, int height)
        {
            Camera.main.transform.position = new Vector3((width / _cameraOffcet) + AdditionalOffset, -((height / _cameraOffcet) + AdditionalOffset), -10);
        }
    }
}