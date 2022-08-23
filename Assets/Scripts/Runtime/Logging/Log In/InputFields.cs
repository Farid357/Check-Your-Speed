using CheckYourSpeed.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace CheckYourSpeed.Loging
{
    public sealed class InputFields : MonoBehaviour
    {
        [SerializeField, RequireInterface(typeof(IPasswordField))] private MonoBehaviour _password;
        [SerializeField, RequireInterface(typeof(INameField))] private MonoBehaviour _name;

        public IPasswordField Password => _password as IPasswordField;

        public INameField Name => _name as INameField;

        public IReadOnlyList<IInputField> All => new List<IInputField>() { Password, Name };

    }
}