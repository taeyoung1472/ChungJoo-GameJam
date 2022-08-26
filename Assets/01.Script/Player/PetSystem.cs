using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetSystem : MonoBehaviour
{
    [SerializeField] private Pets[] pets;
    [SerializeField] private GameObject smallPet;
    int curIndex;
    void Update()
    {
        transform.Rotate(Vector3.forward * 90 * Time.deltaTime);
        if(curIndex != GameManager.Instance.Data.petLevel)
        {
            curIndex = GameManager.Instance.Data.petLevel;
            foreach (var pet in pets[curIndex].pets)
            {
                pet.gameObject.SetActive(true);
            }
        }
        if (GameManager.Instance.Data.smallPetLevel == 1)
        {
            smallPet.SetActive(true);
        }
    }
    [System.Serializable]
    public class Pets
    {
        public Transform[] pets;
    }
}
