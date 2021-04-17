﻿// The MIT License(MIT)

// Copyright(c) 2021 Alberto Rodriguez Orozco & LiveCharts Contributors

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using LiveChartsCore.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiveChartsCore.Kernel
{
    /// <summary>
    /// Defiens the points states dictionary class.
    /// </summary>
    /// <typeparam name="TDrawingContext">The type of the drawing context.</typeparam>
    public class PointStatesDictionary<TDrawingContext>
        where TDrawingContext : DrawingContext
    {
        private Dictionary<string, StrokeAndFillDrawable<TDrawingContext>> states = new();

        /// <summary>
        /// Gets or sets the stroke and fill for the specified state name.
        /// </summary>
        /// <value>
        /// The stroke and fill.
        /// </value>
        /// <param name="stateName">Name of the state.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">$"A null instance is not valid at this point, to delete a key please use the {nameof(DeleteState)}() method.</exception>
        public StrokeAndFillDrawable<TDrawingContext>? this[string stateName]
        {
            get
            {
                if (!states.TryGetValue(stateName, out var state)) return null;
                return state;
            }
            set
            {
                if (value == null)
                    throw new InvalidOperationException(
                        $"A null instance is not valid at this point, to delete a key please use the {nameof(DeleteState)}() method.");

                if (states.ContainsKey(stateName)) RemoveState(states[stateName]);

                states[stateName] = value;

                if (Chart == null) return;

                if (value.Fill != null) Chart.Canvas.AddDrawableTask(value.Fill);
                if (value.Stroke != null) Chart.Canvas.AddDrawableTask(value.Stroke);
            }
        }

        /// <summary>
        /// Gets the chart.
        /// </summary>
        /// <value>
        /// The chart.
        /// </value>
        public Chart<TDrawingContext>? Chart { get; internal set; }

        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <returns></returns>
        public StrokeAndFillDrawable<TDrawingContext>[] GetStates() => states.Values.ToArray();

        /// <summary>
        /// Deletes the state.
        /// </summary>
        /// <param name="stateName">Name of the state.</param>
        /// <returns></returns>
        public void DeleteState(string stateName)
        {
            RemoveState(states[stateName]);
            states.Remove(stateName);
        }

        /// <summary>
        /// Removes the state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        private void RemoveState(StrokeAndFillDrawable<TDrawingContext> state)
        {
            if (Chart == null) return;

            if (state.Fill != null) Chart.Canvas.RemovePaintTask(state.Fill);
            if (state.Stroke != null) Chart.Canvas.RemovePaintTask(state.Stroke);
        }
    }
}
