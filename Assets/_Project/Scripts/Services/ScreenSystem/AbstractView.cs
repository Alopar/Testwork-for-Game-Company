using UnityEngine;

namespace Services.ScreenSystem
{
    public abstract class AbstractView : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] protected ScreenType _type;

        [Space(10)]
        [SerializeField] protected Canvas _canvas;
        [SerializeField] protected GameObject _content;
        #endregion

        #region PROPERTIES
        public ScreenType ScreenType => _type;
        #endregion

        #region METHODS PUBLIC
        public virtual void ShowScreen()
        {
            _content.SetActive(true);
        }

        public virtual void HideScreen()
        {
            _content.SetActive(false);
        }
        #endregion
    }
}
