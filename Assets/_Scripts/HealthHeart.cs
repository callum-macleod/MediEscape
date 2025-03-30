using UnityEngine;
using UnityEngine.UI;

public class HealthHeart : MonoBehaviour
{
    public Sprite fullHeart, halfHeart, emptyHeart;
    Image HeartImage;

    private void Awake()
    {
        HeartImage = GetComponent<Image>();
    }

    public void setHeartImage(HeartStatus status)
    {
        switch(status)
        {
            case HeartStatus.Empty:
                HeartImage.sprite = emptyHeart;
                break;
            case HeartStatus.Half:
                HeartImage.sprite = halfHeart;
                break;
            case HeartStatus.Full:
                HeartImage.sprite = fullHeart;
                break;
        }
    }
}
public enum HeartStatus
{
    Empty = 0,
    Half = 1,
    Full = 2
}
