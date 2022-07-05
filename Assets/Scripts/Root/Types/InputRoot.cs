using CheckYourSpeed.GameLogic;
using UnityEngine;

namespace CheckYourSpeed.Root
{
    public sealed class InputRoot : CompositeRoot
    {
        private IUpdatable _updatable;

        public override void Compose() => _updatable = new PointerInput(Camera.main);

        private void Update() => _updatable.Update(Time.deltaTime);

    }
}