using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MuhammetInce.HelperUtils
{
    public static class HelperUtils
    {
        public static IEnumerator AnimationToggle(Animator animator ,int id, float duration)
        {
            animator.SetBool(id, true);
            yield return new WaitForSeconds(duration);
            animator.SetBool(id, false);
            if (!animator.GetBool(id))
            {
                animator.transform.parent.transform.position = new Vector3(50, 50, 100);
            }
        }
        
        public static void ListFucker<T>(this IEnumerable<T> numerable)
        {

            for (int i = 0; i < numerable.Count(); i++)
            {
                if (numerable.ElementAt(i) as GameObject)
                {
                    object mono = numerable.ElementAt(i);

                    GameManager.instance.DestroyObj(mono);
                }
            }

            List<T> list = numerable as List<T>;
            list.Clear();
        }
        
        public static void ShuffleList<T>(this IList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T temp = list[i];

                int randomIndex = Random.Range(0, list.Count);

                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }
    }
}