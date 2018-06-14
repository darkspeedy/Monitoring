using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using RestfulAPITest.Model;
using Brushes = System.Windows.Media.Brushes;

namespace RestfulAPITest
{
    public partial class FrmReport : Form
    {
      
        /// <summary>
        /// A List of all the values of the Chart has been passed.
        /// The foreach loop assign all the values to the chart.
        /// </summary>
        /// <param name="reportList"></param>
        public FrmReport(List<ChartValues<MeasureModel>> reportList)
        {
            InitializeComponent();
            var reportList1 = reportList;

            if (reportList1.Count > 0)
            {
                cartesianChartReport.Series.Clear();
                foreach (var chartValues in reportList1)
                {
                    cartesianChartReport.Series.Add(new LineSeries
                    {
                        Title = "Status",
                        Values = chartValues,
                        LineSmoothness = 0, //straight lines, 1 really smooth lines
                        PointGeometry = DefaultGeometries.Square,
                        PointGeometrySize = 10,
                        PointForeground = Brushes.White,
                        Stroke = Brushes.LimeGreen
                    });
                    //Retrieve the start and end Date for monitoring period.
                    mStartDate.Text = chartValues[0].DateTime.ToString(CultureInfo.InvariantCulture);
                    mEndDate.Text = chartValues[chartValues.Count - 1].DateTime.ToString(CultureInfo.InvariantCulture);
                }
            }

            cartesianChartReport.AxisY.Add(new Axis
            {
                Title = "Status",
                DisableAnimations = true,
                Labels = new[] { "Offline", "Online" },
                LabelFormatter = value => value.ToString("D1"), 
                Separator = new Separator 
                {
                    Step = 1,
                    IsEnabled = false 
                },
            });

            cartesianChartReport.AxisX.Add(new Axis
            {
                //Format the showing date
                LabelFormatter = value => new DateTime((long)(value * TimeSpan.FromHours(1).Ticks)).ToString("yy-MMM-dd ddd HH:mm:ss")
            });
            //The legend of the chart
            cartesianChartReport.LegendLocation = LegendLocation.Right;

        }
    }
}
