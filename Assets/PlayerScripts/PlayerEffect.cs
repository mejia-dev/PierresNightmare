using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEffects : MonoBehaviour
{
    public AudioClip damageSound;
    public Camera playerCamera;
    public float shakeDuration = 0.4f;
    public float shakeAmount = 0.3f;
    public Image damageImage; // Assign this in the inspector
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public float flashSpeed = 5f;

    private AudioSource audioSource;

    private bool isDamaged;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the GameObject");
        }
    }

    void Update()
    {
        if (isDamaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        isDamaged = false;
    }

    public void TakeDamage()
    {
        Debug.Log("TakeDamage in PlayerEffect called");
        isDamaged = true;
        StartCoroutine(CameraShake(shakeDuration, shakeAmount));

        if (damageSound == null)
        {
          Debug.LogError("Damage sound is not assigned!");
          return;
        }
        audioSource.PlayOneShot(damageSound);
    }

    IEnumerator CameraShake(float duration, float amount)
    {
        Vector3 originalPos = playerCamera.transform.localPosition;
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            float x = Random.Range(-1f, 1f) * amount;
            float y = Random.Range(-1f, 1f) * amount;
            playerCamera.transform.localPosition = new Vector3(x, y, originalPos.z);

            yield return null; // This is necessary for the coroutine to yield execution until the next frame
        }

        playerCamera.transform.localPosition = originalPos;
    }
}