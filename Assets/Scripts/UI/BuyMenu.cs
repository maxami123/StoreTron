using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuyMenu : MonoBehaviour, IInteractable
{
    public GameObject menu;
    public GameObject storetron;
    public GameObject location;
    public List<GameObject> spawnableCommerce;


    [SerializeField] private AudioSource buySound;
    [SerializeField] private AudioSource openSound;
    private List<Transform> locations;

    void Start()
    {
        var locationsArr = location.GetComponentsInChildren<Transform>();
        locations = locationsArr.ToList();
        locations.Remove(location.transform);
    }

    public void Interact()
    {
        EnableMenu();
    }

    public void EnableMenu()
    {
        openSound.Play();
        menu.SetActive(true);
        storetron.GetComponent<Movement>().inMenu = true;
    }
    public void CloseMenu()
    {
        openSound.Play();
        menu.SetActive(false);
        storetron.GetComponent<Movement>().inMenu = false;
    }

    public void BuyObject(int index)
    {
        Collider2D hitColliders;
        foreach (var tran in locations)
        {
            hitColliders = Physics2D.OverlapCircle(tran.position, 0.2f);
            if (hitColliders == null)
            {
                buySound.Play();// Play Notif sound for buying object
                GameObject product = Instantiate(spawnableCommerce[index]);
                product.transform.position = tran.position;
                product.transform.rotation = Quaternion.identity;
                return;
            }
        }
    }
}
