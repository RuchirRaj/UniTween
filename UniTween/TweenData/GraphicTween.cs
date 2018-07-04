﻿namespace UniTween.Data
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;
    using UnityEngine.UI;

    [CreateAssetMenu(menuName = "Tween Data/Canvas/Graphic")]
    public class GraphicTween : TweenData
    {
        [Space(15)]
        public GraphicCommand command;
        [ShowIf("IsColor")]
        public Color color;
        [HideIf("IsColor")]
        public float to;

        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<Graphic> graphics = (List<Graphic>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            foreach (var t in graphics)
            {
                tweens.Join(GetTween(t));
            }
            return tweens;
        }

        public Tween GetTween(Graphic graphic)
        {
            switch (command)
            {
                case GraphicCommand.Color:
                    return graphic.DOColor(color, duration);
                case GraphicCommand.Fade:
                    return graphic.DOFade(to, duration);
                case GraphicCommand.BlendableColor:
                    return graphic.DOBlendableColor(color, duration);
                default:
                    return null;
            }
        }

        private bool IsColor()
        {
            return command == GraphicCommand.Color || command == GraphicCommand.BlendableColor;
        }

        public enum GraphicCommand
        {
            Color,
            Fade,
            BlendableColor
        }
    }
}