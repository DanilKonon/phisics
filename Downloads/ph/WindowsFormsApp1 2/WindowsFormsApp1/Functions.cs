﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using AForge.Math;
using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;
using OxyPlot;


namespace WindowsFormsApp1
{
    class Functions
    {
        public static double trans_func_gauss(double w, double sigma)
        {
            return Math.Exp(-w * w * sigma * sigma / 2.0) * Math.Sqrt(sigma) / Math.Sqrt(Math.PI * 2);
        }
        public static double func_gauss(double x, double sigma, double offset = 0)
        {
            double new_x = x - offset;
            return Math.Exp(-new_x * new_x / (sigma * sigma * 2.0)); // / (Math.Sqrt(2.0 * Math.PI) * sigma);
        }
        public static double func_rect(double x, double start, double end)
        {
            // Returns rect that is non-zero from -5 to 5.
            if (x < start || x > end)
            {
                return 0;
            }
            else
            {
                return 5;
            }
        }
        public static double trans_func_rect(double x, double start, double end)
        {
            return Math.Abs((end - start) * 5 *  Trig.Sinc(x * ((end - start) / 2.0) / Math.PI));
        }
        // приводим к нормальному виду
        public static void FlipFlop<T>(T[] f)
        {
            if (f.Length % 2 == 0)
            {
                for (int i = 0, j = f.Length / 2; j < f.Length; ++i, ++j)
                {
                    T buf = f[i];
                    f[i] = f[j];
                    f[j] = buf;
                }
            }
            else
            {
                T mem = f[f.Length / 2];
                for (int i = 0, j = f.Length / 2 + 1; j < f.Length; ++i, ++j)
                {
                    T buf = f[i];
                    f[i] = f[j];
                    f[j] = buf;
                }
                for (int i = f.Length / 2; i < f.Length - 1; ++i)
                {
                    f[i] = f[i + 1];
                }
                f[f.Length - 1] = mem;
            }
        }
        public static void FastDFT(Complex[] x, int mode = 1)
        {
            var dir = FourierTransform.Direction.Forward;
            if (mode == -1)
            {
                dir = FourierTransform.Direction.Backward;
            }
            FourierTransform.FFT(x, dir);
        }
        public static void complex_magnitude_paint(object obj, double[] x, Complex[] y, double koef = 1.0)
        {
            var paint_obj = obj as System.Windows.Forms.DataVisualization.Charting.Chart;
            var ser = paint_obj.Series.Add("New plot");
            for (int i = 0; i < x.Length; i++)
            {
                ser.Points.AddXY(x[i], y[i].Magnitude / koef);
            }
        }
        public static void complex_re_paint(object obj, double[] x, Complex[] y, double koef = 1.0, double sigma = 10,double offset = 0, string name = "New plot!")
        {
            //int st = 0, end = 0;
            //for(int i = 0; i < y.Length; ++i)
            //{
            //    if(Math.Abs(y[i].Re) > 0.001)
            //    {
            //        st = i;
            //        break;
            //    }
            //}
            //for (int i = y.Length - 1; i >= 0; --i)
            //{
            //    if (Math.Abs(y[i].Re) > 0.001)
            //    {
            //        end = i;
            //        break;
            //    }
            //}
            var paint_obj = obj as System.Windows.Forms.DataVisualization.Charting.Chart;
            var ser = paint_obj.Series.Add(name);
            paint_obj.ChartAreas[0].AxisX.Maximum = 4000;
            paint_obj.ChartAreas[0].AxisX.Minimum = 400;
            ser.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline ;
            for (int i = 0; i < x.Length; i++)
            {
                    ser.Points.AddXY(x[i], y[i].Re / koef);
            }
        }
        public static void complex_re_paint_for_ch2(object obj, double[] x, Complex[] y, double koef = 1.0, double sigma = 10, double offset = 0, string name = "New plot!")
        {
            //int st = 0, end = 0;
            //for (int i = 0; i < y.Length; ++i)
            //{
            //    if (Math.Abs(y[i].Re) < 0.999)
            //    {
            //        st = i;
            //        break;
            //    }
            //}
            //for (int i = y.Length - 1; i >= 0; --i)
            //{
            //    if (Math.Abs(y[i].Re) < 0.999)
            //    {
            //        end = i;
            //        break;
            //    }
            //}
            var paint_obj = obj as System.Windows.Forms.DataVisualization.Charting.Chart;
            var ser = paint_obj.Series.Add(name);
            paint_obj.ChartAreas[0].AxisX.Maximum = 4000;
            paint_obj.ChartAreas[0].AxisX.Minimum = 400;
            ser.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            for (int i = 0; i < x.Length; i++)
            {
                ser.Points.AddXY(x[i], y[i].Re / koef);
            }
        }
        public static void complex_im_paint(object obj, double[] x, Complex[] y, double koef = 1.0)
        {
            var paint_obj = obj as System.Windows.Forms.DataVisualization.Charting.Chart;
            var ser = paint_obj.Series.Add("New plot");
            for (int i = 0; i < x.Length; i++)
            {
                ser.Points.AddXY(x[i], y[i].Im / koef);
            }

        }
        public static void complex_re_paint_chart(object obj, double[] x, Complex[] y, double koef = 1.0)
        {
            var paint_obj = obj as System.Windows.Forms.DataVisualization.Charting.Chart;
            var ser = paint_obj.Series.Add("New plot");
            ser.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            for (int i = 0; i < x.Length; i++)
            {
                ser.Points.AddXY(x[i], y[i].Re / koef);
            }
        }
        public static void double_paint(object obj, double[] x, double[] y, double koef = 1.0)
        {
            LiveCharts.WinForms.CartesianChart paint_obj = obj as LiveCharts.WinForms.CartesianChart;
            LiveCharts.Defaults.ObservablePoint[] mas = new LiveCharts.Defaults.ObservablePoint[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                mas[i] = (new LiveCharts.Defaults.ObservablePoint
                {
                    X = x[i],
                    Y = y[i] / koef
                });
            }
            var ListPoints = new ChartValues<LiveCharts.Defaults.ObservablePoint>();
            ListPoints.AddRange(mas);
            paint_obj.Series.Add(new LineSeries
            { 
                Values = ListPoints,
                PointGeometrySize = 0
            });

        }
        public static void double_paint_with_func(object obj, double[] x, double[] y, Func<double, double> f, double koef = 1.0)
        {
            LiveCharts.WinForms.CartesianChart paint_obj = obj as LiveCharts.WinForms.CartesianChart;
            LiveCharts.Defaults.ObservablePoint[] mas = new LiveCharts.Defaults.ObservablePoint[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                mas[i] = (new LiveCharts.Defaults.ObservablePoint
                {
                    X = x[i],
                    Y = f(y[i]) / koef
                });
            }
            var ListPoints = new ChartValues<LiveCharts.Defaults.ObservablePoint>();
            ListPoints.AddRange(mas);
            paint_obj.Series.Add(new LineSeries
            {
                Values = ListPoints,
                PointGeometrySize = 0
            });

        }
        public static void complex_magnitude_paint_with_func(object obj, double[] x, Complex[] y, Func<double, double> f, double koef = 1.0)
        {
            LiveCharts.WinForms.CartesianChart paint_obj = obj as LiveCharts.WinForms.CartesianChart;
            LiveCharts.Defaults.ObservablePoint[] mas = new LiveCharts.Defaults.ObservablePoint[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                mas[i] = (new LiveCharts.Defaults.ObservablePoint
                {
                    X = x[i],
                    Y = f(y[i].Magnitude) / koef
                });
            }
            var ListPoints = new ChartValues<LiveCharts.Defaults.ObservablePoint>();
            ListPoints.AddRange(mas);
            paint_obj.Series.Add(new LineSeries
            {
                Values = ListPoints,
                PointGeometrySize = 0
            });

        }
        public static void complex_re_paint_with_func(object obj, double[] x, Complex[] y, Func<double, double> f, double koef = 1.0)
        {
            LiveCharts.Wpf.CartesianChart paint_obj = obj as LiveCharts.Wpf.CartesianChart;
            LiveCharts.Defaults.ObservablePoint[] mas = new LiveCharts.Defaults.ObservablePoint[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                mas[i] = (new LiveCharts.Defaults.ObservablePoint
                {
                    X = x[i],
                    Y = f(y[i].Re) / koef
                });
            }
            var ListPoints = new ChartValues<LiveCharts.Defaults.ObservablePoint>();
            ListPoints.AddRange(mas);
            paint_obj.Series.Add(new LineSeries
            {
                Values = ListPoints,
                PointGeometrySize = 0
            });

        }
    }
}
