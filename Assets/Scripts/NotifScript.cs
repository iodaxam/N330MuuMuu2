using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotifScript : MonoBehaviour
{
    public GameObject LineObject;
    public GameObject TopTextObject;
    public GameObject BottomTextObject;

    private float CurrentOpacity;

    private Color DefaultImageColor;
    private Color DefaultTextColor;

    private float timer;

    void Start()
    {
        DefaultImageColor = gameObject.GetComponent<Image>().color;
        DefaultTextColor = LineObject.GetComponent<Image>().color;
    }
    
    public void RestartFadeStats()
    {
        StopCoroutine("NotifFade");

        CurrentOpacity = 0;

        timer = 3f;

        gameObject.GetComponent<Image>().color = new Color(DefaultImageColor.r, DefaultImageColor.g, DefaultImageColor.b, CurrentOpacity);
        LineObject.GetComponent<Image>().color = new Color(DefaultTextColor.r, DefaultTextColor.g, DefaultTextColor.b, CurrentOpacity);
        TopTextObject.GetComponent<TextMeshProUGUI>().color = new Color(DefaultTextColor.r, DefaultTextColor.g, DefaultTextColor.b, CurrentOpacity);
        BottomTextObject.GetComponent<TextMeshProUGUI>().color = new Color(DefaultTextColor.r, DefaultTextColor.g, DefaultTextColor.b, CurrentOpacity);
    }

    public IEnumerator NotifFade(Biome CurrentArea)
    {
        switch(CurrentArea)
        {
            case Biome.Ocean:
                TopTextObject.GetComponent<TextMeshProUGUI>().text = "Ocean";
                BottomTextObject.GetComponent<TextMeshProUGUI>().text = "Reach the beach!";
                break;
            case Biome.Beach:
                TopTextObject.GetComponent<TextMeshProUGUI>().text = "Beach";
                BottomTextObject.GetComponent<TextMeshProUGUI>().text = "Reach the pirate town!";
                break;
            case Biome.PirateTown:
                TopTextObject.GetComponent<TextMeshProUGUI>().text = "Pirate Town";
                BottomTextObject.GetComponent<TextMeshProUGUI>().text = "Reach the Ghost yard!";
                break;
            case Biome.GhostYard:
                TopTextObject.GetComponent<TextMeshProUGUI>().text = "Ghost Yard";
                BottomTextObject.GetComponent<TextMeshProUGUI>().text = "Reach the Lava Land!";
                break;
            case Biome.VolcanoZone:
                TopTextObject.GetComponent<TextMeshProUGUI>().text = "Lava Land";
                BottomTextObject.GetComponent<TextMeshProUGUI>().text = "Go as far as possible!";
                break;
        }

        CurrentOpacity = 0;

        while(CurrentOpacity < 1f)
        {
            CurrentOpacity += Time.deltaTime;

            if(CurrentOpacity <= .5f) {
                gameObject.GetComponent<Image>().color = new Color(DefaultImageColor.r, DefaultImageColor.g, DefaultImageColor.b, CurrentOpacity);
            }
            LineObject.GetComponent<Image>().color = new Color(DefaultTextColor.r, DefaultTextColor.g, DefaultTextColor.b, CurrentOpacity);
            TopTextObject.GetComponent<TextMeshProUGUI>().color = new Color(DefaultTextColor.r, DefaultTextColor.g, DefaultTextColor.b, CurrentOpacity);
            BottomTextObject.GetComponent<TextMeshProUGUI>().color = new Color(DefaultTextColor.r, DefaultTextColor.g, DefaultTextColor.b, CurrentOpacity);
            yield return null; 
        }

        for(timer = 3 ; timer >= 0 ; timer -= Time.deltaTime )
        {
            
            yield return null ;
        }
        //don't use waitforseconds, it gets funky if the player loses in the middle of it
        //yield return new WaitForSeconds(3f);
        
        while(CurrentOpacity > 0)
        {
            CurrentOpacity -= Time.deltaTime;

            gameObject.GetComponent<Image>().color = new Color(DefaultImageColor.r, DefaultImageColor.g, DefaultImageColor.b, (CurrentOpacity - .5f));
            LineObject.GetComponent<Image>().color = new Color(DefaultTextColor.r, DefaultTextColor.g, DefaultTextColor.b, CurrentOpacity);
            TopTextObject.GetComponent<TextMeshProUGUI>().color = new Color(DefaultTextColor.r, DefaultTextColor.g, DefaultTextColor.b, CurrentOpacity);
            BottomTextObject.GetComponent<TextMeshProUGUI>().color = new Color(DefaultTextColor.r, DefaultTextColor.g, DefaultTextColor.b, CurrentOpacity);
            yield return null; 
        }
    }
}
