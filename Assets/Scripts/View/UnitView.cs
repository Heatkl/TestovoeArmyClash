namespace Game.View
{
    using UnityEngine;
    using Game.Model;
    using System;

    public class UnitView : MonoBehaviour
    {
        public UnitModel Model { get; private set; }

        [SerializeField] private Renderer _renderer;
        [SerializeField] private Collider _collider;

        private Transform _target;
        private float colliderRadiusMultiplier = 1.5f;
        public float Radius => _collider.bounds.extents.x * colliderRadiusMultiplier;

        public void Bind(UnitModel model)
        {
            Model = model;
        }

        public void ApplyVisual(Color color, Vector3 scale)
        {
            if (_renderer != null)
                _renderer.material.color = color;

            transform.localScale = scale;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public float DistanceTo(UnitView other)
        {
            return Vector3.Distance(transform.position, other.transform.position);
        }

        public void MoveTowards(Vector3 position, float speed)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
        }

        public void AddOffset(Vector3 offset)
        {
            transform.position += offset;
        }

        public void LookAtTarget()
        {
            if (_target == null) return;

            Vector3 dir = _target.position - transform.position;
            dir.y = 0;

            if (dir.sqrMagnitude > 0.001f)
                transform.rotation = Quaternion.LookRotation(dir);
        }

        public void PlayDeath()
        {
            Destroy(gameObject);
        }

    }
}
