namespace Game.Controller
{
    using Game.Combat;
    using Game.Model;
    using Game.View;
    using UnityEngine;

    public class LaserController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LaserView _view;

        private LaserDamageModel _damageModel;
        private UnitModel _currentTarget;

        private void Awake()
        {
            _damageModel = new LaserDamageModel();
        }

        private void Update()
        {
            HandleInput();
            _damageModel.Tick(Time.deltaTime, _currentTarget);
        }

        private void HandleInput()
        {
            if (!Input.GetMouseButton(0))
            {
                _currentTarget = null;
                _damageModel.ResetTimer();
                _view.Hide();
                return;
            }

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _view.Show(hit.point);

                var unitView = hit.collider.GetComponent<UnitView>();
                if (unitView != null && unitView.Model.IsAlive)
                {
                    if (_currentTarget != unitView.Model)
                        _damageModel.ResetTimer();

                    _currentTarget = unitView.Model;
                    return;
                }
            }

            _currentTarget = null;
            _damageModel.ResetTimer();
        }
    }
}
