using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DogamManager : MonoSingleTon<DogamManager>
{
    [SerializeField] private Dogam[] dogam;

    public void ActiveDogam(int dogamNum)
    {
        dogam[dogamNum].explaneButton.interactable = true;
        dogam[dogamNum].iconImage.sprite = dogam[dogamNum].data.enemyImage;
        dogam[dogamNum].passiveDesc.text = dogam[dogamNum].data.passiveText;
    }

    [System.Serializable]
    public class Dogam
    {
        public DogamDataSO data;
        public Button explaneButton;
        public Image iconImage;
        public TextMeshProUGUI passiveDesc;
    }
}
