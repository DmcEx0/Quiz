using UnityEngine;

namespace Quiz.Tool
{
    public class CameraCentrer
    {
        private const float Offset = 1.3f;

        public void CenteringCamera(int width, int height)
        {
            Camera.main.transform.position = new Vector3(((float)width / 2) * Offset, ((float)height / 2) * Offset, -10);
        }
    }
}