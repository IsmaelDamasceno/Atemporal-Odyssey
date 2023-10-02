using CrystalBot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoiTata
{
    public class DamageMember : MonoBehaviour, IDamage
    {
        [SerializeField] private float flashTime;

        public void ApplyDamage(Transform hitTransform, Vector2 impact, int amount)
        {
            if (TryGetComponent<FlashWhite>(out var component))
            {
                component.EndComponent();
            }

            gameObject.AddComponent<FlashWhite>().Init(flashTime, flashTime, GetComponent<SpriteRenderer>());
        }
    }

}