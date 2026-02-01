namespace Game.View
{
    using UnityEngine;

    public class LaserView : MonoBehaviour
    {
        [SerializeField] private LineRenderer _line;
        [SerializeField] private float _height = 30f;

        public void Show(Vector3 worldPoint)
        {
            _line.enabled = true;

            Vector3 start = worldPoint + Vector3.up * _height;
            _line.SetPosition(0, start);
            _line.SetPosition(1, worldPoint);
        }

        public void Hide()
        {
            _line.enabled = false;
        }
    }
}
