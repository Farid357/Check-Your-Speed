using CheckYourSpeed.Model;
using CheckYourSpeed.SaveSystem;
using CheckYourSpeed.Shop.Model;
using CheckYourSpeed.Utils;
using UnityEngine;

namespace CheckYourSpeed.Root
{
    public sealed class WalletRoot : MonoBehaviour
    {
        [SerializeField, RequireInterface(typeof(IVisualization<int>))] private MonoBehaviour _moneyVisualization;

        public IWallet Compose()
        {
           return new Wallet((IVisualization<int>)_moneyVisualization, new BinaryStorage());
        }
    }
}
