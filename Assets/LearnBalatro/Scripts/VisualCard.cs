using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LearnBalatro
{
    public class VisualCard : MonoBehaviour
    {
        public CardBlock cardBlock;
        private Transform mShakeParent;
        private Canvas mShadowCanvas;
        private Transform mTiltParent;

        private bool mScaleAnimations = true;
        private float mScaleOnHover = 1.15f;
        private float mScaleTransition = 0.15f;
        private float mHoverTransition = 0.15f;
        private float mHoverPunchAngle = 5;
        private float mManualTiltAmount = 1;
        private float mAutoTiltAmount = 15;
        private float mTiltSpeed = 40;
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
            TiltCard();
        }

        private void TiltCard()
        {
            if (cardBlock.isHovering)
            {
                HoverTilt();
            }
            else
            {
                NormalTilt();
            }
        }

        private void HoverTilt()
        {
            // NormalTilt();
            // 计算基于时间的正弦/余弦波动（悬停时幅度减小）
            float sine = Mathf.Sin(Time.time) * (cardBlock.isHovering ? 0.5f : 1);
            float cosine = Mathf.Cos(Time.time) * (cardBlock.isHovering ? 0.5f : 1);
            float speed = mTiltSpeed * Time.deltaTime;

            Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float tiltX = offset.y * -1 * mManualTiltAmount;
            float tiltY = offset.x * mManualTiltAmount;
            float tiltZ = 0;

            // 使用 LerpAngle 平滑过渡角度（避免360°跳变）
            float lerpX = Mathf.LerpAngle(mTiltParent.eulerAngles.x, tiltX + (sine * mAutoTiltAmount), speed);
            float lerpY = Mathf.LerpAngle(mTiltParent.eulerAngles.y, tiltY + (cosine * mAutoTiltAmount), speed);
            float lerpZ = Mathf.LerpAngle(mTiltParent.eulerAngles.z, tiltZ, speed * 0.5f);

            mTiltParent.eulerAngles = new Vector3(lerpX, lerpY, lerpZ);
        }

        private void NormalTilt()
        {
            // 计算基于时间的正弦/余弦波动（悬停时幅度减小）
            float sine = Mathf.Sin(Time.time) * (cardBlock.isHovering ? 0.5f : 1);
            float cosine = Mathf.Cos(Time.time) * (cardBlock.isHovering ? 0.5f : 1);
            float speed = mTiltSpeed * Time.deltaTime;

            // 使用 LerpAngle 平滑过渡角度（避免360°跳变）
            float lerpX = Mathf.LerpAngle(mTiltParent.eulerAngles.x, sine * mAutoTiltAmount, speed);
            float lerpY = Mathf.LerpAngle(mTiltParent.eulerAngles.y, cosine * mAutoTiltAmount, speed);
            float lerpZ = Mathf.LerpAngle(mTiltParent.eulerAngles.z, 0, speed * 0.5f);

            mTiltParent.eulerAngles = new Vector3(lerpX, lerpY, lerpZ);
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
    }
}