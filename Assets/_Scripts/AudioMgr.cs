using System.Security.Cryptography;
using UnityEngine;

public class AudioMgr : MonoBehaviour
{
    public static AudioMgr Instance;

    [Header("Combat")]
    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip dieSound;

    [Header("Items/Menu")]
    [SerializeField] AudioClip swapItemSound;
    [SerializeField] AudioClip pickupItemSound;
    [SerializeField] AudioClip defaultUseItemSound;
    [SerializeField] AudioClip useSpeedSound;
    [SerializeField] AudioClip useStealthSound;
    [SerializeField] AudioClip useHealthSound;

    [Header("Enemy Interactions")]
    [SerializeField] AudioClip bribeSound;
    [SerializeField] AudioClip aggroSound;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    // ATTACKING
    public void PlayAttackSound(Transform transform)
    {
        if (attackSound == null) return;
        AudioSource.PlayClipAtPoint(attackSound, transform.position);
    }

    public void PlayHitSound(Transform transform)
    {
        if (hitSound == null) return;
        AudioSource.PlayClipAtPoint(hitSound, transform.position);
    }


    public void PlayDieSound(Transform transform)
    {
        if (dieSound == null) return;
        AudioSource.PlayClipAtPoint(dieSound, transform.position);
    }

    // ITEMS/MENU
    public void PlaySwapItemSound(Transform transform)
    {
        if (swapItemSound == null) return;
        AudioSource.PlayClipAtPoint(swapItemSound, transform.position);
    }

    public void PLayPickupItemSound(Transform transform)
    {
        if (pickupItemSound == null) return;
        AudioSource.PlayClipAtPoint(pickupItemSound, transform.position);
    }

    public void PlayDefaultUseItemSound(Transform transform)
    {
        if (defaultUseItemSound == null) return;
        AudioSource.PlayClipAtPoint(defaultUseItemSound, transform.position);
    }

    public void PlaySpeedSound(Transform transform)
    {
        if (useSpeedSound == null) return;
        AudioSource.PlayClipAtPoint(useSpeedSound, transform.position);
    }

    public void PlayStealthSound(Transform transform)
    {
        if (useStealthSound == null) return;
        AudioSource.PlayClipAtPoint(useStealthSound, transform.position);
    }

    public void PlayHealthSound(Transform transform)
    {
        if (useHealthSound == null) return;
        AudioSource.PlayClipAtPoint(useHealthSound, transform.position);
    }

    // ENEMY INTERACTIONS

    public void PlayBribeSound(Transform transform)
    {
        if (bribeSound == null) return;
        AudioSource.PlayClipAtPoint(bribeSound, transform.position);
    }

    public void PlayAggroSound(Transform transform)
    {
        if (aggroSound == null) return;
        AudioSource.PlayClipAtPoint(aggroSound, transform.position);
    }
}
