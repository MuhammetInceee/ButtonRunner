using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ObstacleInteraction : MonoBehaviour
{
    private int _index;
    private Collider _col;
    private List<GameObject> StackedList => StackManager.Instance.StackedObjList;

    private void Start()
    {
        _col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Button"))
        {
            if (!StackedList.Contains(other.gameObject)) return;
            _col.enabled = false;
            _index = CheckIndex(other.gameObject);

            foreach (var t in StackedList)
            {
                int index = CheckIndex(t);

                if (index >= _index && StackedList[index].layer == 6)
                {
                    StackScatter(StackedList[index]);
                }
            }
        }
    }

    private int CheckIndex(GameObject other)
    {
        for (int i = 0; i < StackedList.Count; i++)
        {
            if (StackedList[i] == other)
            {
                return i;
            }
        }

        return -1;
    }

    private void StackScatter(GameObject targetObj)
    {
        targetObj.transform.parent = null;
        targetObj.GetComponent<Collider>().enabled = false;
        Vector3 pos = targetObj.transform.position;

        targetObj.transform.DOJump(
            new Vector3(pos.x + Random.Range(-2f, 3f), pos.y, pos.z + Random.Range(-1f, 2f)),
            1.5f, 1, 0.5f).OnComplete(() =>
        {
            StackedList.Remove(targetObj);
            Destroy(targetObj, 1f);
            StackManager.Instance.CheckCameraPos();
        });
    }
}
