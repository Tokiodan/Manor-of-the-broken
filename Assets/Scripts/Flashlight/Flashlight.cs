using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    public float batteryLife = 100f;
    public Light flashlight;
    public float tickSpeed = 0.3f; // Battery drain interval

    public Slider batterySlider; // Reference to the UI Slider

    private bool isOn = false;
    private Coroutine drainCoroutine;
    private Coroutine flickerCoroutine;

    void Start()
    {
        flashlight.enabled = false;

        if (batterySlider != null)
        {
            batterySlider.maxValue = 100f;
            batterySlider.value = batteryLife;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && batteryLife > 0)
        {
            ToggleFlashlight();
        }

        if (batteryLife <= 0 && isOn)
        {
            flashlight.enabled = false;
            isOn = false;
            if (drainCoroutine != null) StopCoroutine(drainCoroutine);
            if (flickerCoroutine != null) StopCoroutine(flickerCoroutine);
        }

        // Update slider value every frame
        if (batterySlider != null)
        {
            batterySlider.value = batteryLife;
        }

        // Hide the battery slider when battery life is 0
        if (batteryLife == 0)
        {
            batterySlider.gameObject.SetActive(false);
        }
        else
        {
            batterySlider.gameObject.SetActive(true);
        }
    }

    void ToggleFlashlight()
    {
        isOn = !isOn;
        flashlight.enabled = isOn;

        if (isOn)
        {
            drainCoroutine = StartCoroutine(DrainBattery());
            flickerCoroutine = StartCoroutine(Flickering());
        }
        else
        {
            if (drainCoroutine != null) StopCoroutine(drainCoroutine);
            if (flickerCoroutine != null) StopCoroutine(flickerCoroutine);
        }
    }

    IEnumerator DrainBattery()
    {
        while (batteryLife > 0 && isOn)
        {
            yield return new WaitForSeconds(tickSpeed);
            batteryLife -= 1f;
            batteryLife = Mathf.Max(0, batteryLife); // Clamp to avoid negative values
        }
    }

    IEnumerator Flickering()
    {
        while (isOn && batteryLife > 0)
        {
            yield return new WaitForSeconds(Random.Range(2f, 6f));

            float flickerChance = Mathf.Clamp01(1f - (batteryLife / 100f));

            if (Random.value < flickerChance)
            {
                int flickerCount = Random.Range(1, 4);
                for (int i = 0; i < flickerCount; i++)
                {
                    flashlight.enabled = false;
                    yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
                    flashlight.enabled = true;
                    yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
                }

                if (batteryLife < 10f && Random.value < 0.3f)
                {
                    flashlight.enabled = false;
                    yield return new WaitForSeconds(0.5f);
                    flashlight.enabled = true;
                }
            }
        }
    }
}
