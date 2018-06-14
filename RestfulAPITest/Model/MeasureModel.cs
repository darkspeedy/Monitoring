using System;

namespace RestfulAPITest.Model
{
    /// <summary>
    /// This class is referring to the information that will appear in th AxisX and AxisY of the chart.
    /// </summary>
    public class MeasureModel
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
    }
}
