﻿using UnityEngine;


namespace StatsenkoAA
{
    public abstract class Environment : BaseObjectScene, ISetDamage
    {
        [SerializeField] private BulletProjector _projector;

        public virtual void SetDamage(InfoCollision info)
        {
            if (_projector == null) return;
            var projectorRotation = Quaternion.FromToRotation(-Vector3.forward, info.Contact.normal);
            var obj = Instantiate(_projector, info.Contact.point + info.Contact.normal * 0.25f,
                projectorRotation, info.ObjectCollision);
            obj.transform.rotation = Quaternion.Euler(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y,
                Random.Range(0, 360));
        }
    }
}
