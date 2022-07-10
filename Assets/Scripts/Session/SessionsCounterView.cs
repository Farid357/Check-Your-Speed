using DG.Tweening;
using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace CheckYourSpeed.Loging
{
    public sealed class SessionsCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _duration;

        private ISessionsCounter _sessionsCounter;
        private readonly CompositeDisposable _disposables = new();

        public void Init(ISessionsCounter sessionsCounter)
        {
            _sessionsCounter = sessionsCounter ?? throw new ArgumentNullException(nameof(sessionsCounter));
            _sessionsCounter.Count.Subscribe(count => Display(count)).AddTo(_disposables);
        }

        private void OnDestroy() => _disposables.Clear();

        private void Display(int count)
        {
            _text.text = count.ToString();
            _text.DOText(count.ToString(), _duration, scrambleMode: ScrambleMode.Numerals);
        }
    }
}