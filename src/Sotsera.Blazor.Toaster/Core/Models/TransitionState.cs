﻿// Copyright (c) Alessandro Ghidini. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Globalization;

namespace Sotsera.Blazor.Toaster.Core.Models
{
    public class TransitionState
    {
        private int Duration { get; set; }
        private double MaxOpacity { get; set; }
        private double Ratio { get; set; }

        public int RemainingMilliseconds { get; private set; }
        public string Opacity => (Ratio*MaxOpacity).ToString("0.##", CultureInfo.InvariantCulture);
        public string ProgressPercentage => (Ratio*100).ToString("0");

        public static TransitionState ForRequiredInteraction(int maximumOpacity) => new TransitionState
        {
            Duration = 0,
            MaxOpacity = Convert.ToDouble(maximumOpacity) / 100,
            RemainingMilliseconds = 0,
            Ratio = 1
        };

        private TransitionState() { }

        public TransitionState(TransitionTimer timer, int maximumOpacity)
        {
            Duration = timer.Duration;
            MaxOpacity = Convert.ToDouble(maximumOpacity)/100;
            RemainingMilliseconds = Convert.ToInt32(timer.RemainingMilliseconds);

            if (Duration == 0 || RemainingMilliseconds <= 0)
                Ratio = 0;
            else if (timer.RemainingMilliseconds > Duration)
                Ratio = 1;
            else
                Ratio = timer.RemainingMilliseconds / Duration;
        }
    }
}