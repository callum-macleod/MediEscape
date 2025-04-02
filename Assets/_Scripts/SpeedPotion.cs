using UnityEngine;

[CreateAssetMenu(fileName = "SpeedPotion", menuName = "Scriptable Objects/SpeedPotion")]
public class SpeedPotion : ItemInfo
{
    public float timerLimit;

    public void Use(GameObject player)
    {
        AudioMgr.Instance.PlaySpeedSound(player.transform);
    }

}
