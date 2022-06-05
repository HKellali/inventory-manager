using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;

    public Transform selectedSlot;
    public Transform borders;
    public Transform itemIcon;
    public Text itemText;
    public int inventorySize = 18;
    private bool swapping = false;
    private int swappingIndex;
    Resolution[] resolutions;
    int currentResolutionIndex = 0;
    AudioSource soundEffects;
    public AudioClip pick;
    public AudioClip unpick;
    public AudioClip delete;
    public AudioClip refresh;

    void Awake()
    {
        resolutions = new Resolution[3];
        resolutions[0].width = 1280;
        resolutions[0].height = 720;
        resolutions[1].width = 1920;
        resolutions[1].height = 1080;
        resolutions[2].width = 3860;
        resolutions[2].height = 2160;
        borders = transform.Find("Borders");
        itemText = transform.Find("ItemText").GetComponent<Text>();
        soundEffects = GameObject.Find("SoundEffects").GetComponent<AudioSource>();
        RefreshResolution();
    }

    private void Start()
    {
        GenerateRandomItems();
    }

    void SetResolution(Resolution resolution)
    {
        GameObject.Find("ResolutionText").GetComponent<Text>().text = resolution.width + "x" + resolution.height;
        Screen.SetResolution(resolution.width, resolution.height, true);
    }
    void RefreshResolution()
    {
        if(currentResolutionIndex >= resolutions.Length)
        {
            currentResolutionIndex = 0;
        } else if (currentResolutionIndex < 0)
        {
            currentResolutionIndex = resolutions.Length - 1;
        }
        SetResolution(resolutions[currentResolutionIndex]);
    }

    int GetSelectedIndex()
    {
        int index = -1;
        GameObject currentSlot;
        GameObject targetSlot = EventSystem.current.currentSelectedGameObject; 
        for (int i = 0; i < inventorySize; i++)
        {
            currentSlot = borders.gameObject.transform.GetChild(i).gameObject;
            if (currentSlot == targetSlot)
            {
                index = i;
            }
        }
        return index;
    }

    void Update()
    {
        if (Gamepad.current.startButton.wasPressedThisFrame)
        {
            Application.Quit();
        }

        if (Gamepad.current.rightShoulder.wasPressedThisFrame)
        {
            currentResolutionIndex++;
            RefreshResolution();
        }

        if (Gamepad.current.leftShoulder.wasPressedThisFrame)
        {
            currentResolutionIndex--;
            RefreshResolution();
        }

        if (Gamepad.current.yButton.wasPressedThisFrame && !swapping)
        {
            GenerateRandomItems();
        }

        if (Gamepad.current.xButton.wasPressedThisFrame && swapping)
        {
            inventory.Removetem(swappingIndex);
            swapping = false;
            PlaySoundEffect(delete);
            selectedSlot = borders.gameObject.transform.GetChild(swappingIndex);
            ScaleIcon(1, selectedSlot);
            RefreshInventory();
            itemText.text = "";
        }
        if (Gamepad.current.aButton.wasPressedThisFrame)
        {
            int index = GetSelectedIndex();
            if (!swapping && !(this.inventory.GetItems()[index] is null))
            {
                PlaySoundEffect(pick);
                itemText.text = GetName(inventory.GetItems()[index].itemType.ToString());
                float scalePercent = 1.25f;
                swapping = true;
                swappingIndex = index;
                selectedSlot = EventSystem.current.currentSelectedGameObject.transform;
                ScaleIcon(scalePercent, selectedSlot);
            } else if (swapping)
            {
                PlaySoundEffect(unpick);
                itemText.text = "";
                selectedSlot = borders.gameObject.transform.GetChild(swappingIndex);
                itemIcon = selectedSlot.Find("ItemIcon");
                RectTransform rectTransform = itemIcon.GetComponent<RectTransform>();
                rectTransform.localScale = Vector3.one;
                Swap(swappingIndex, index);
                swapping = false;
            }
        }
    }

    String GetName(String str)
    {
        String name = "";
        int i = 0;
        foreach (char c in str)
        {
            if (char.ToUpper(c) == c && i > 0)
            {
                name += " ";
            }
            i++;
            name += c;
        }
        name.Replace("Cant", "Can't");
        name.Replace("Dont", "Don't");
        return name;
    }
    void PlaySoundEffect(AudioClip clip)
    {
        soundEffects.clip = clip;
        soundEffects.Play();
    }

    void ScaleIcon(float scalePercent, Transform transform)
    {
        //selectedSlot = EventSystem.current.currentSelectedGameObject.transform;
        itemIcon = transform.Find("ItemIcon");
        RectTransform rectTransform = itemIcon.GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.one * scalePercent;
    }

    void Swap(int a, int b)
    {
        inventory.SwapItems(a, b);
        RefreshInventory();
    }

    void GenerateRandomItems()
    {
        PlaySoundEffect(refresh);
        GenerateInventory();
        RandomizeInventory(5);
    }

    void GenerateInventory()
    {
        this.inventory = new Inventory(inventorySize);
    }

    void RandomizeInventory(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            AddRandomItem();
        }
        RefreshInventory();
    }

    void AddRandomItem()
    {
        Item.ItemType itemType;
        int randomEnumNumber = UnityEngine.Random.Range(0, Enum.GetNames(typeof(Item.ItemType)).Length);
        itemType = (Item.ItemType)randomEnumNumber;
        Item item = new Item(); 
        item.itemType = itemType;
        int slotNumber = GenerateRandomSlotNumber();
        this.inventory.AddItem(item, slotNumber);
    }

    int GenerateRandomSlotNumber()
    {
        int randomSlotNumber = UnityEngine.Random.Range(0, inventorySize);
        while (!(this.inventory.GetItems()[randomSlotNumber] is null))
        {
            randomSlotNumber = UnityEngine.Random.Range(0, inventorySize);
        }

        return randomSlotNumber;
    }

    void RefreshInventory()
    {
        int i = 0;
        foreach (Item item in inventory.GetItems())
        {
            selectedSlot = borders.gameObject.transform.GetChild(i);
            itemIcon = selectedSlot.Find("ItemIcon");
            Image image = itemIcon.GetComponent<Image>(); 
            if (this.inventory.GetItems()[i] is null)
            {
                image.enabled = false;
            }
            else
            {
                image.enabled = true;
                image.sprite = item.GetSprite();
            }
            i++;
        }
    }
}
