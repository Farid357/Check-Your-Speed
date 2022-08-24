﻿using UnityEngine;
using System;
using CheckYourSpeed.Shop.Model;
using CheckYourSpeed.Shop.Visualization;

namespace CheckYourSpeed.Shop
{
    public sealed class SelectableGoodVisualization : MonoBehaviour, ISelectableGood
    {
        [SerializeField] private GoodAlreadyBoughtVisualization _alreadyBoughtVisualization;
        private IGoodVisualization _goodView;
        private bool _hasVisualizedBuying;

        public IGood Good { get; private set; }

        public void Init(IGoodVisualization goodView, IGood good)
        {
            _goodView = goodView ?? throw new ArgumentNullException(nameof(goodView));
            Good = good ?? throw new ArgumentNullException(nameof(good));
        }

        public void Select()
        {
            if (_hasVisualizedBuying)
                return;
            _goodView.Select();
        }

        public void Unselect()
        {
            if (_hasVisualizedBuying)
                return;
            _goodView.Unselect();
        }

        public void VisualizeBuying()
        {
            _alreadyBoughtVisualization.Visualize();
            _hasVisualizedBuying = true;
        }
    }
}