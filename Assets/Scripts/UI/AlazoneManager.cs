using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlazoneManager : MonoBehaviour
{
    public enum DeliveryType
    {
        NONE,
        RELAIS,
        DOMICILE
    };

    public DeliveryType deliveryType = DeliveryType.NONE;

    public MarketplaceItem marketplaceItem;

    [Header("All Alazone Panels")]
    [SerializeField] GameObject HomePanel;
    [SerializeField] GameObject ProductPanel;
    [SerializeField] GameObject DeliveryPanel;
    [SerializeField] GameObject RecapPanel;

    [Header("Home Panel Object")]
    [SerializeField] Image Product01;
    [SerializeField] TMP_Text ProductTitle01;

    [SerializeField] Image UselessProduct01;
    [SerializeField] TMP_Text UselessProductTitle01;

    [SerializeField] Image UselessProduct02;
    [SerializeField] TMP_Text UselessProductTitle02;


    [Header("Product Panel Object")]
    [SerializeField] Image Product02;
    [SerializeField] TMP_Text ProductTitle02;
    [SerializeField] TMP_Text ProductDescription;
    [SerializeField] TMP_Text ProductPrice;

    [Header("Delivery Panel Object")]
    [SerializeField] Image Product03;
    [SerializeField] TMP_Text ProductTitle03;
    [SerializeField] Color colorOn, colorOff;
    [SerializeField] Button buttonRelais, buttonDomi;
    [SerializeField] Image imageRelais, imageDomi;
    [SerializeField] GameObject Warning;

    [Header("Recap Panel Object")]
    [SerializeField] Image Product04;
    [SerializeField] TMP_Text ProductTitle04;

    public void LaunchAlazone()
    {
        HomePanel.SetActive(true);
        Product01.sprite = marketplaceItem.itemSprite;
        Product02.sprite = marketplaceItem.itemSprite;
        Product03.sprite = marketplaceItem.itemSprite;
        Product04.sprite = marketplaceItem.itemSprite;

        ProductTitle01.text = marketplaceItem.itemName + " - " + marketplaceItem.itemPrice + "€";

        ProductTitle02.text = marketplaceItem.itemName;
        ProductDescription.text = marketplaceItem.itemDescription;
        ProductPrice.text = marketplaceItem.itemPrice + "€ - Livraison en moins de 24H";

        ProductTitle03.text = marketplaceItem.itemName + " - " + marketplaceItem.itemPrice + "€";

        ProductTitle04.text = marketplaceItem.itemName + " - " + marketplaceItem.itemPrice + "€";
    }

    public void SelectProduct()
    {
        HomePanel.SetActive(false);
        ProductPanel.SetActive(true);
    }

    public void Buy()
    {
        ProductPanel.SetActive(false);
        DeliveryPanel.SetActive(true);
    }

    public void Confirm()
    {
        switch (deliveryType)
        {
            case DeliveryType.NONE:
                StartCoroutine(WarningText());
                break;
            case DeliveryType.RELAIS:
                DeliveryPanel.SetActive(false);
                RecapPanel.SetActive(true);
                break;
            case DeliveryType.DOMICILE:
                DeliveryPanel.SetActive(false);
                RecapPanel.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void SelectRelais()
    {
        buttonRelais.interactable = false;
        buttonDomi.interactable = true;
        imageRelais.color = colorOff;
        imageDomi.color = colorOn;
        deliveryType = DeliveryType.RELAIS;
    }

    public void SelectDomi()
    {
        buttonRelais.interactable = true;
        buttonDomi.interactable = false;
        imageRelais.color = colorOn;
        imageDomi.color = colorOff;
        deliveryType = DeliveryType.DOMICILE;
    }

    IEnumerator WarningText()
    {
        Warning.SetActive(true);
        int child = Warning.transform.childCount - 1;

        Warning.GetComponent<TMP_Text>().color = new Color(1, 0, 0, 1);
        Warning.transform.GetChild(child).GetComponent<Image>().color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(.5f);

        float a = 1;
        while(Warning.GetComponent<TMP_Text>().color.a > 0)
        {
            a -= Time.deltaTime * 2;
            Warning.GetComponent<TMP_Text>().color = new Color(1, 0, 0, a);
            child = Warning.transform.childCount - 1;
            Warning.transform.GetChild(child).GetComponent<Image>().color = new Color(1, 1, 1, a);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        Warning.SetActive(false);
    }

    public void Quit()
    {
        HomePanel.SetActive(false);
        ProductPanel.SetActive(false);
        DeliveryPanel.SetActive(false);
        RecapPanel.SetActive(false);
    }

    public void Back()
    {
        deliveryType = DeliveryType.NONE;
        buttonRelais.interactable = true;
        buttonDomi.interactable = true;
        imageRelais.color = colorOn;
        imageDomi.color = colorOn;
        if (ProductPanel.activeSelf)
        {
            ProductPanel.SetActive(false);
            HomePanel.SetActive(true);
        }
        else if (DeliveryPanel.activeSelf)
        {
            DeliveryPanel.SetActive(false);
            ProductPanel.SetActive(true);
        }
    }

    private void OnEnable()
    {
        LaunchAlazone();
    }
}
