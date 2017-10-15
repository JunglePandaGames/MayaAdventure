using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour {

	public static FadeManager Instance { set; get;}

	public Image fadeImage;
    public Sprite[] images;
    private bool isInTransition;
	private float transition;
	private bool isShowing;
	private float duration;

	private void Awake(){
		Instance = this;

	}

	public void Fade(bool showing, float duration){
		isShowing = showing;
		isInTransition = true;
		this.duration = duration;
		transition = (isShowing) ? 0 : 1;
	}
	private void Update(){
        if (!isInTransition) {
            fadeImage.color = Color.Lerp(new Color(0, 0, 0, 0), Color.white, 1);
            return;
        }
		transition += (isShowing) ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
		fadeImage.color = Color.Lerp (new Color (0, 0, 0, 0), Color.black, transition);

		if (transition > 1 || transition < 0)
			isInTransition = false;
	}

    public void updateLifeValue(int life)
    {
        switch (life)
        {
            case 5:
                fadeImage.sprite = images[0];
                break;
            case 4:
                fadeImage.sprite = images[1];
                break;
            case 3:
                fadeImage.sprite = images[2];
                break;
            case 2:
                fadeImage.sprite = images[3];
                break;
            case 1:
                fadeImage.sprite = images[4];
                break;
        }
        Debug.Log("Image Name: " + fadeImage.sprite.name);
    }

}
