using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public Material[] shirtMat;
    public Material[] pantMat;
    public Material[] shoeMat;
    public Material[] hairMat;
    public Material[] upperMat;

    public SkinnedMeshRenderer render;
    public Transform _camera;
    public Transform targetCameraPos;
    public Vector3 cameraRotation;
    public GameObject buyButton;

    public Transform menuPos;
    

  public  int index;

    private void OnEnable()
    {
        GameManager.inst.CoinSetCharSelect();
    }

    private void Start()
    {
        //Debug.Log("Player Store =" + PlayerPrefs.GetInt("PlayerCloth"));
        index = 0;

        buyButton.SetActive(false);
        _camera.position = targetCameraPos.position;
        _camera.localRotation = new Quaternion(0, 0, 0, 0);
    }

    public void ChangeMaterialNext()
    {
        
        
        if(index < shirtMat.Length - 1)
        {
            index++;
            if(index != PlayerPrefs.GetInt("Character" + index))
            {
                buyButton.SetActive(true);
            }
            else
            {
                buyButton.SetActive(false);
            }
            Material[] mat = render.materials;
            mat[0] = pantMat[index];
            mat[1] = hairMat[index];
            mat[3] = upperMat[index];
            mat[4] = shirtMat[index];
            mat[5] = shoeMat[index];
            render.materials = mat;
        }
    }

    public void ChangeMaterialPrevious()
    {
        
        if (index > 0)
        {
            index--;
            if (index != PlayerPrefs.GetInt("Character" + index))
            {
                buyButton.SetActive(true);
            }
            else
            {
                buyButton.SetActive(false);
            }
            Material[] mat = render.materials;
            mat[0] = pantMat[index];
            mat[1] = hairMat[index];
            mat[3] = upperMat[index];
            mat[4] = shirtMat[index];
            mat[5] = shoeMat[index];
            render.materials = mat;
        }
    }

    public void BuyChar()
    {
        if(PlayerPrefs.GetInt("Coin") >= 200)
        {
            PlayerPrefs.SetInt("Character" + index, index);
           
            
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 200);
            GameManager.inst.CoinSetCharSelect();


            // PlayerPrefs.SetInt("PlayerCloth",index);
            buyButton.SetActive(false);
        }
       
    }

    private void OnDisable()
    {
        _camera.SetPositionAndRotation(menuPos.position,menuPos.rotation);
    }

    public void SelectChar()
    {
        PlayerPrefs.SetInt("PlayerCloth", index);
        Debug.Log("Pref For Char" + PlayerPrefs.GetInt("PlayerCloth"));
        GameManager.inst.MainMenu();
    }
}
