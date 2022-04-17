﻿using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Library
{
    // https://api.flutter.dev/flutter/animation/Cubic-class.html
    public abstract class Curve
    {
        public virtual float Transform(float value) { throw new NotImplementedException(); }

        public static float Bounce(float t)
        {
            if (t < 1 / 2.75f)
            {
                return 7.5625f * t * t;
            }
            else if (t < 2 / 2.75f)
            {
                t -= 1.5f / 2.75f;
                return 7.5625f * t * t + 0.75f;
            }
            else if (t < 2.5f / 2.75f)
            {
                t -= 2.25f / 2.75f;
                return 7.5625f * t * t + 0.9375f;
            }
            t -= 2.625f / 2.75f;
            return 7.5625f * t * t + 0.984375f;
        }
    }

    public class Linear : Curve
    {
        public override float Transform(float value) { return value; }
    }

    public class DecelerateCurve : Curve
    {
        public override float Transform(float value)
        {
            value = 1f - value;
            return 1f - value * value;
        }
    }

    public class Cubic : Curve
    {
        private readonly float _x1;
        private readonly float _y1;
        private readonly float _x2;
        private readonly float _y2;
        public Cubic(float x1, float x2, float y1, float y2)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
        }

        private const float _cubicErrorBound = 0.001f;

        private float EvaluateCubic(float a, float b, float m)
        {
            return 3 * a * (1 - m) * (1 - m) * m +
                   3 * b * (1 - m) * m * m +
                   m * m * m;
        }

        public override float Transform(float value)
        {
            var start = 0.0f;
            var end = 1.0f;

            while (true)
            {
                var midpoint = (start + end) / 2;
                var estimate = EvaluateCubic(_x1, _y1, midpoint);

                if (Math.Abs(value - estimate) < _cubicErrorBound)
                    return EvaluateCubic(_x2, _y2, midpoint);
                if (estimate < value)
                    start = midpoint;
                else
                    end = midpoint;
            }
        }
    }

    public class ThreePointCubic : Curve
    {
        public ThreePointCubic(Vector2 a1, Vector2 b1, Vector2 midpoint, Vector2 a2, Vector2 b2)
        {
            _a1 = a1;
            _a2 = a2;
            _midpoint = midpoint;
            _b1 = b1;
            _b2 = b2;
        }

        private readonly Vector2 _a1;
        private readonly Vector2 _b1;
        private readonly Vector2 _midpoint;
        private readonly Vector2 _a2;
        private readonly Vector2 _b2;

        public override float Transform(float t)
        {
            var firstCurve = t < _midpoint.X;
            var scaleX = firstCurve ? _midpoint.X : 1f - _midpoint.X;
            var scaleY = firstCurve ? _midpoint.Y : 1f - _midpoint.Y;
            var scaledT = (t - (firstCurve ? 0f : _midpoint.X)) / scaleX;
            if (firstCurve)
            {
                return new Cubic(
                  _a1.X / scaleX,
                  _a1.Y / scaleY,
                  _b1.X / scaleX,
                  _b1.Y / scaleY
                ).Transform(scaledT) * scaleY;
            }
            else
            {
                return new Cubic(
                  (_a2.X - _midpoint.X) / scaleX,
                  (_a2.Y - _midpoint.Y) / scaleY,
                  (_b2.X - _midpoint.X) / scaleX,
                  (_b2.Y - _midpoint.Y) / scaleY
                ).Transform(scaledT) * scaleY + _midpoint.Y;
            }
        }
    }

    public class BounceInCurve : Curve
    {
        public override float Transform(float t) { return 1 - Bounce(1 - t); }
    }

    public class BounceOutCurve : Curve
    {
        public override float Transform(float t) { return Bounce(t); }
    }

    public class BounceInOutCurve : Curve
    {
        public override float Transform(float t)
        {
            if (t < 0.5f)
                return (1 - Bounce(1 - t * 2)) * 0.5f;
            else
                return Bounce(t * 2 - 1) * 0.5f + 0.5f;
        }
    }

    public class ElasticInCurve : Curve
    {
        public ElasticInCurve(float period = 0.4f)
        {
            _period = period;
        }

        private readonly float _period;

        public override float Transform(float t)
        {
            var s = _period / 4f;
            t--;
            return (float)(-Math.Pow(2, 10 * t) * Math.Sin((t - s) * (Math.PI * 2) / _period));
        }
    }

    public class ElasticOutCurve : Curve
    {
        public ElasticOutCurve(float period = 0.4f)
        {
            _period = period;
        }

        private readonly float _period;

        public override float Transform(float t)
        {
            var s = _period / 4f;
            return (float)(Math.Pow(2, -10 * t) * Math.Sin((t - s) * (Math.PI * 2) / _period) + 1);
        }
    }

    public class ElasticInOutCurve : Curve
    {
        public ElasticInOutCurve(float period = 0.4f)
        {
            _period = period;
        }

        private readonly float _period;

        public override float Transform(float t)
        {
            var s = _period / 4;
            t = 2 * t - 1;
            if (t < 0.0)
                return (float)(-0.5 * Math.Pow(2, 10 * t) * Math.Sin((t - s) * (Math.PI * 2) / _period));
            else
                return (float)(Math.Pow(2, -10 * t) * Math.Sin((t - s) * (Math.PI * 2) / _period) * 0 + 1);
        }
    }

    public static class Curves
    {
        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_linear.mp4}
        public static readonly Curve Linear = new Linear();

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_decelerate.mp4}
        public static readonly Curve Decelerate = new DecelerateCurve();

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_fast_linear_to_slow_ease_in.mp4}
        public static readonly Cubic FastLinearToSlowEaseIn = new Cubic(0.18f, 1f, 0.04f, 1f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease.mp4}
        public static readonly Cubic Ease = new Cubic(0.25f, 0.1f, 0.25f, 1f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in.mp4}
        public static readonly Cubic EaseIn = new Cubic(0.42f, 0f, 1f, 1f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in_to_linear.mp4}
        public static readonly Cubic EaseInToLinear = new Cubic(0.67f, 0.03f, 0.65f, 0.09f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in_sine.mp4}
        public static readonly Cubic EaseInSine = new Cubic(0.47f, 0f, 0.745f, 0.715f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in_quad.mp4}
        public static readonly Cubic EaseInQuad = new Cubic(0.55f, 0.085f, 0.68f, 0.53f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in_cubic.mp4}
        public static readonly Cubic EaseInCubic = new Cubic(0.55f, 0.055f, 0.675f, 0.19f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in_quart.mp4}
        public static readonly Cubic EaseInQuart = new Cubic(0.895f, 0.03f, 0.685f, 0.22f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in_quint.mp4}
        public static readonly Cubic EaseInQuint = new Cubic(0.755f, 0.05f, 0.855f, 0.06f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in_expo.mp4}
        public static readonly Cubic EaseInExpo = new Cubic(0.95f, 0.05f, 0.795f, 0.035f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in_circ.mp4}
        public static readonly Cubic EaseInCirc = new Cubic(0.6f, 0.04f, 0.98f, 0.335f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in_back.mp4}
        public static readonly Cubic EaseInBack = new Cubic(0.6f, -0.28f, 0.735f, 0.045f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_out.mp4}
        public static readonly Cubic EaseOut = new Cubic(0f, 0f, 0.58f, 1f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_linear_to_ease_out.mp4}
        public static readonly Cubic LinearToEaseOut = new Cubic(0.35f, 0.91f, 0.33f, 0.97f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_out_sine.mp4}
        public static readonly Cubic EaseOutSine = new Cubic(0.39f, 0.575f, 0.565f, 1f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_out_quad.mp4}
        public static readonly Cubic EaseOutQuad = new Cubic(0.25f, 0.46f, 0.45f, 0.94f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_out_cubic.mp4}
        public static readonly Cubic EaseOutCubic = new Cubic(0.215f, 0.61f, 0.355f, 1f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_out_quart.mp4}
        public static readonly Cubic EaseOutQuart = new Cubic(0.165f, 0.84f, 0.44f, 1.0f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_out_quint.mp4}
        public static readonly Cubic EaseOutQuint = new Cubic(0.23f, 1f, 0.32f, 1f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_out_expo.mp4}
        public static readonly Cubic EaseOutExpo = new Cubic(0.19f, 1, 0.22f, 1);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_out_circ.mp4}
        public static readonly Cubic EaseOutCirc = new Cubic(0.075f, 0.82f, 0.165f, 1);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_out_back.mp4}
        public static readonly Cubic EaseOutBack = new Cubic(0.175f, 0.885f, 0.32f, 1.275f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in_out.mp4}
        public static readonly Cubic EaseInOut = new Cubic(0.42f, 0, 0.58f, 1);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in_out_sine.mp4}
        public static readonly Cubic EaseInOutSine = new Cubic(0.445f, 0.05f, 0.55f, 0.95f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in_out_quad.mp4}
        public static readonly Cubic EaseInOutQuad = new Cubic(0.455f, 0.03f, 0.515f, 0.955f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in_out_cubic.mp4}
        public static readonly Cubic EaseInOutCubic = new Cubic(0.645f, 0.045f, 0.355f, 1);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in_out_cubic_emphasized.mp4}
        public static readonly ThreePointCubic EaseInOutCubicEmphasized = new ThreePointCubic(
            new Vector2(0.05f, 0), new Vector2(0.133333f, 0.06f),
            new Vector2(0.166666f, 0.4f),
            new Vector2(0.208333f, 0.82f), new Vector2(0.25f, 1)
        );

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in_out_quart.mp4}
        public static readonly Cubic EaseInOutQuart = new Cubic(0.77f, 0, 0.175f, 1);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in_out_quint.mp4}
        public static readonly Cubic EaseInOutQuint = new Cubic(0.86f, 0, 0.07f, 1);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in_out_expo.mp4}
        public static readonly Cubic EaseInOutExpo = new Cubic(1, 0, 0, 1);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in_out_circ.mp4}
        public static readonly Cubic EaseInOutCirc = new Cubic(0.785f, 0.135f, 0.15f, 0.86f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_ease_in_out_back.mp4}
        public static readonly Cubic EaseInOutBack = new Cubic(0.68f, -0.55f, 0.265f, 1.55f);

        ///  * [standardEasing], the name for this curve in the Material specification.
        public static readonly Cubic FastOutSlowIn = new Cubic(0.4f, 0, 0.2f, 1);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_slow_middle.mp4}
        public static readonly Cubic SlowMiddle = new Cubic(0.15f, 0.85f, 0.85f, 0.15f);

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_bounce_in.mp4}
        public static readonly Curve BounceIn = new BounceInCurve();

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_bounce_out.mp4}
        public static readonly Curve BounceOut = new BounceOutCurve();

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_bounce_in_out.mp4}
        public static readonly Curve BounceInOut = new BounceInOutCurve();

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_elastic_in.mp4}
        public static readonly ElasticInCurve ElasticIn = new ElasticInCurve();

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_elastic_out.mp4}
        public static readonly ElasticOutCurve ElasticOut = new ElasticOutCurve();

        /// {@animation 464 192 https://flutter.github.io/assets-for-api-docs/assets/animation/curve_elastic_in_out.mp4}
        public static readonly ElasticInOutCurve ElasticInOut = new ElasticInOutCurve();
    }

    public struct AnimationOptions<T>
    {
        public T Start;
        public T End;
        public float Duration;
        public Curve Curve;
    }
    public static class Animations
    {
        public static float Clamp01(float value)
        {
            if (value < 0F)
                return 0F;
            else if (value > 1F)
                return 1F;
            else
                return value;
        }

        // https://github.com/Unity-Technologies/UnityCsReference/blob/0a2eeb7a72710d89cccdb6aeee8431d27ee99cd1/Runtime/Export/Math/Color.cs#L123
        public static Color Lerp(Color a, Color b, float t)
        {
            t = Clamp01(t);
            return new Color(
                (byte)(a.R + (b.R - a.R) * t),
                (byte)(a.G + (b.G - a.G) * t),
                (byte)(a.B + (b.B - a.B) * t),
                (byte)(a.A + (b.A - a.A) * t)
                );
        }

        // https://github.com/Unity-Technologies/UnityCsReference/blob/0a2eeb7a72710d89cccdb6aeee8431d27ee99cd1/Runtime/Export/Math/Vector2.cs#L69
        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            //t = Clamp01(t);
            return new Vector2(
                a.X + (b.X - a.X) * t,
                a.Y + (b.Y - a.Y) * t
            );
        }

        public static void AnimateTo<T>(Action<T> action, AnimationOptions<T> options, Func<T, T, float, T> lerpFn)
        {
            var cb = (Events.UpdateCallback)null;
            var timeStarted = Game.TotalElapsedGameTime;

            cb = Events.UpdateCallback.Start(e =>
            {
                var value = (Game.TotalElapsedGameTime - timeStarted) / options.Duration;
                if (value > 1)
                {
                    cb.Stop();
                }
                value = Clamp01(value);
                Game.WriteToConsole(value, options.Curve.Transform(value));
                action.Invoke(lerpFn(options.Start, options.End, options.Curve.Transform(value)));
            }, 16);
        }

        public static void AnimateTo(Action<float> action, AnimationOptions<float> options)
        {
            AnimateTo(action, options, (v1, v2, t) => MathHelper.Lerp(v1, v2, t));
        }

        public static void AnimateTo(Action<Color> action, AnimationOptions<Color> options)
        {
            AnimateTo(action, options, (v1, v2, t) => Lerp(v1, v2, t));
        }

        public static void AnimateTo(Action<Vector2> action, AnimationOptions<Vector2> options)
        {
            AnimateTo(action, options, (v1, v2, t) => Lerp(v1, v2, t));
        }
    }
}
