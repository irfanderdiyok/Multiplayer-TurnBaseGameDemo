using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    //! FPS Limitler ekran kartını yormamak adına yapılması gerekiyor.
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}
