using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LearnBalatro
{
    public class VisualCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Transform mShakeParent;
        private Canvas mShadowCanvas;
        private Transform mTiltParent;

        private bool mScaleAnimations = true;
        private float mScaleOnHover = 1.15f;
        private float mScaleTransition = 0.15f;
        private float mHoverTransition = 0.15f;
        private float mHoverPunchAngle = 5;
        private Ease mScaleEase = Ease.OutBack;

        private void Init()
        {
            mShakeParent = transform.Find("ShakeParent");
            mShadowCanvas = mShakeParent.Find("ShadowCanvas").GetComponent<Canvas>();
            mTiltParent = mShakeParent.Find("TiltParent");
        }

        private void Awake()
        {
            Init();
        }

        private void Update()
        {
        }

        private void TiltCard()
        {
        }

        public void BeginDrag(CardBlock card)
        {
        }

        public void EndDrag(CardBlock card)
        {

        }

        public void EnterPointer(CardBlock card = null)
        {
            if (mScaleAnimations)
            {
                transform.DOScale(mScaleOnHover, mScaleTransition).SetEase(mScaleEase);
            }

            DOTween.Kill(2, true);
            mShakeParent.DOPunchRotation(Vector3.forward * mHoverPunchAngle, mHoverTransition, 20, 1).SetId(2);
        }

        public void ExitPointer(CardBlock card = null)
        {
            transform.DOScale(1, mScaleTransition).SetEase(mScaleEase);
        }

        public void UpPointer(CardBlock card)
        {
        }

        public void DownPointer(CardBlock card)
        {
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            EnterPointer();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ExitPointer();
        }
    }
}