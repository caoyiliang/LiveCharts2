﻿// The MIT License(MIT)
//
// Copyright(c) 2021 Alberto Rodriguez Orozco & LiveCharts Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Collections.Generic;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel.Sketches;

namespace LiveChartsCore;

/// <summary>
/// Defines the stacked column series class.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TVisual">The type of the visual.</typeparam>
/// <typeparam name="TLabel">The type of the label.</typeparam>
/// <typeparam name="TErrorGeometry">The type of the error geometry.</typeparam>
/// <seealso cref="CoreColumnSeries{TModel, TVisual, TLabel, TErrorGeometry}" />
/// <remarks>
/// Initializes a new instance of the <see cref="CoreStackedColumnSeries{TModel, TVisual, TLabel, TErrorGeometry}"/> class.
/// </remarks>
/// <param name="values">The values.</param>
public abstract class CoreStackedColumnSeries<TModel, TVisual, TLabel, TErrorGeometry>(IReadOnlyCollection<TModel>? values)
    : CoreColumnSeries<TModel, TVisual, TLabel, TErrorGeometry>(values, true), IStackedBarSeries
        where TVisual : BoundedDrawnGeometry, new()
        where TLabel : BaseLabelGeometry, new()
        where TErrorGeometry : BaseLineGeometry, new()
{
    private int _stackGroup = 0;

    /// <inheritdoc cref="IStackedBarSeries.StackGroup"/>
    public int StackGroup { get => _stackGroup; set { _stackGroup = value; OnPropertyChanged(); } }

    /// <summary>
    /// Gets the stack group.
    /// </summary>
    /// <returns></returns>
    /// <inheritdoc />
    public override int GetStackGroup() => _stackGroup;
}
