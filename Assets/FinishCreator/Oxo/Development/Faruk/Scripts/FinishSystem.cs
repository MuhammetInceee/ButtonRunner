using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishSystem : MonoBehaviour
{
    [Header("OnEnable Spawn Settings")]

    public bool isSpawnOnEnable;
    public int spawnAmount;

    [Header(" ")]
    public GameObject prefab;

    public List<GameObject> finishPiecesList;
    public Vector3 offset;
    public Vector3 rotation;

    public List<Color> materialColors;
    public List<Gradient> gradients;
    private void OnEnable()
    {
        if (isSpawnOnEnable)
        {
            StartCoroutine(Create(spawnAmount));
        }
    }
    public IEnumerator Create(int amount)
    {
        GameObject tmp;
        for (int i = 0; i < amount; i++)
        {
            tmp = Instantiate(prefab, transform);
            tmp.transform.position = transform.position + offset * i;
            tmp.transform.localRotation = Quaternion.Euler(rotation);

            if (gradients.Count > 0)
            {
                float f = (i / (float)spawnAmount);
                tmp.GetComponent<MeshRenderer>().material.color = gradients[0].Evaluate(f);
            }
            tmp.GetComponentInChildren<TextMeshProUGUI>().text = $"{i + 1}X";
            finishPiecesList.Add(tmp);
            yield return new WaitForFixedUpdate();
            //yield return new WaitForSeconds(.3f);
        }
    }

}
