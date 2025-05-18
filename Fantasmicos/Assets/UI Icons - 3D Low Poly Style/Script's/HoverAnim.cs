using UnityEngine;
using UnityEngine.EventSystems;

namespace FWC
{
    public class HoverAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
    {
        [SerializeField] float scaleChange = 1.1f;
        [SerializeField] AudioClip hoverClip; // clip asociado al botón
        private Vector3 originalScale;

        private AudioSource sfxSource;

        void Start()
        {
            originalScale = transform.localScale;

            // Buscar el AudioSource global de efectos en el MenuManagerPropio
            var manager = FindObjectOfType<MenuManagerPropio>();
            if (manager != null)
            {
                sfxSource = manager.sfxSource;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = originalScale * scaleChange;

            if (hoverClip != null && sfxSource != null)
            {
                sfxSource.PlayOneShot(hoverClip);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = originalScale;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.localScale = originalScale;
        }
    }
}

