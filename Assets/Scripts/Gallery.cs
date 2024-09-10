using UnityEngine;
using UnityEngine.SceneManagement;

public class GalleryButton : MonoBehaviour
{
    public void Galleryload()
    {
        SceneManager.LoadScene($"GalleryScene_{GameManager.Instance.Name}");
    }
}