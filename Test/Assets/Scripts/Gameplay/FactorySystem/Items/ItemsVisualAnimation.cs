using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.FactorySystem.Items
{
    public class ItemsVisualAnimation : MonoBehaviour
    {
        [SerializeField] private float animationSpeed;

        public void MakeTransitionAnimation(ItemView item, Transform startTransform,Transform endTransform, bool deactivateAfterAction)
        {
            item.transform.position = startTransform.position;
            item.transform.localRotation = Quaternion.identity;
            item.gameObject.SetActive(true);
            StartCoroutine(TransitionAnimationTracker(item, endTransform, deactivateAfterAction));
        }

        public void InstantiateItemOnPoint(ItemView item, Transform point)
        {
            item.transform.position = point.position;
            item.transform.localRotation = Quaternion.identity;
            item.gameObject.SetActive(true);
        }

        private IEnumerator TransitionAnimationTracker(ItemView item, Transform endTransform, bool deactivateAfterAction)
        {
            while (Vector3.Distance(item.transform.position, endTransform.position) > 1f)
            {
                item.transform.position = Vector3.Lerp(item.transform.position, endTransform.position, animationSpeed * Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }

            item.transform.position = endTransform.position;
            if (deactivateAfterAction)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}
