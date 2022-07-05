using DG.Tweening;

namespace CheckYourSpeed.Root
{
    public sealed class DOTweenRoot : CompositeRoot
    {
        public override void Compose() => DOTween.Init();

    }
}
