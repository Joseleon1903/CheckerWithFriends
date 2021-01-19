using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ProfileUI
{
    [RequireComponent(typeof(Image))]
    public class MaskCooldownBehavour : MonoBehaviour
    {

        private Image _image;

        [SerializeField] private float cooldownTime;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _image.fillAmount = 0;
        }

        private void OnEnable()
        {
            _image.fillAmount = 0;
        }

        void Update()
        {
            if (_image.fillAmount <= 1) {

                _image.fillAmount += 1 / cooldownTime * Time.deltaTime;

            }
        }

      
    }
}