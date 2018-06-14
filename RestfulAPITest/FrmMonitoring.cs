using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using RestfulAPITest.Model;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System.Globalization;
using Newtonsoft.Json.Linq;


namespace RestfulAPITest
{
    public partial class FrmMonitoring : Form
    {
       //*************VARIABLES*****************
        private const string Ready = "READY";
        private const string Status = "Status";
        private Timer Timer { get; set; }
        private ChartValues<MeasureModel> ChartValues { get; set; }
        private readonly List<ChartValues<MeasureModel>> _replayMonitoring = new List<ChartValues<MeasureModel>>();
        private int CurrentStatus { get; set; } 
        //***************************************

        /// <summary>
        /// Constructor
        /// </summary>
        public FrmMonitoring()
        {
            InitializeComponent();
            InitializeChart();
        }

        private void tbInterval_Scroll(object sender, EventArgs e)
        {
            lblValue.Text = "" + tbInterval.Value + @" second(s)";
        }
     
        /// <summary>
        /// When the form is loading, we passed the default Url which can be edit once loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMonitoring_Load(object sender, EventArgs e)
        {
            tbInterval_Scroll(this, new EventArgs());
            tbUrl.Text = "https://api.test.paysafe.com/accountmanagement/";
        }

        /// <summary>
        /// The same button is used to start and stop the monitoring. 
        /// A timer that raises an event at user-defined intervals is also implemented.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbUrl.Text))
            {
                if (btnStart.Text.Equals("&Start"))
                {
                    int range = 1;
                    if (tbInterval != null)
                        range = tbInterval.Value; //Passing the interval value
                    Timer = new Timer
                    {
                        Interval = range * 1000
                    };
                    Timer.Tick += TimerOnTick;//Timer is registered to the TimeOnTick event.
                    Timer.Start();
                    btnStart.Text = @"&Stop";
                    btnStart.BackColor = Color.Lime;
                    tbUrl.Enabled = false;
                    btnReport.Enabled = false;
                    tslInformation.Text = @"Monitoring Started... Please wait a few seconds";
                }
                else
                {
                    Timer.Stop();
                    tbUrl.Enabled = true;
                    btnStart.BackColor = Color.White;
                    btnStart.Text = @"&Start";
                    btnReport.Enabled = true;
                    tslInformation.Text = @"Monitoring stopped.";
                    tslInformation.BackColor = SystemColors.Control;
                }
            }
            else
            {
                MessageBox.Show(@"Field required!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbUrl.Focus();
            }

        }

        /// <summary>
        /// The initialization of the Chart
        /// More info about LiveCharts <a href="https://github.com/Live-Charts/Live-Charts/blob/master/README.md">Credits</a>.
        /// </summary>
        private void InitializeChart()
        {
            var mapper = Mappers.Xy<MeasureModel>()
                .X(model => (double)model.DateTime.Ticks / TimeSpan.FromHours(1).Ticks)//DateTime.Ticks as X
                .Y(model => model.Value);    //Value property as Y

            //lets save the mapper globally.
            Charting.For<MeasureModel>(mapper);

            //the ChartValues property will store our values array
            ChartValues = new ChartValues<MeasureModel>();
            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title=Status,
                    Values = ChartValues,
                    PointGeometrySize = 18,
                    StrokeThickness = 4
                }
            };
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = Status,
                DisableAnimations = true,
                Labels = new[] { "Offline", "Online" },
                LabelFormatter = value => value.ToString("D1"), 
                Separator = new Separator // force the separator step to 1, so it always display all labels
                {
                    Step = 1,
                    IsEnabled = false //disable it to make lines invisible.
                },
            });

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = @"Launch Time: " + DateTime.Now.ToString(CultureInfo.InvariantCulture),
                LabelFormatter = value => new DateTime((long)(value * TimeSpan.FromHours(1).Ticks)).ToString("HH:mm:ss")
            });
        }

        /// <summary>
        /// A timer that raises an event at user-defined interval.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            Task<string> task = Task.Run(() => ExecuteUrl(tbUrl.Text));//Run a task to look for status of the server to prevent a frozen window.

            if (!string.IsNullOrWhiteSpace(task.Result))
            {
                if (IsValidJson(task.Result))
                {
                    ServerState deserializedResult = JsonConvert.DeserializeObject<ServerState>(task.Result);
                    CurrentStatus = GetServerStatus(deserializedResult.Status);
                }
                else
                {
                    CurrentStatus = 0;
                    tslInformation.BackColor = Color.Red;
                    tslInformation.Text = @"Offline: " + task.Result;
                }
            }
            else
            {
                CurrentStatus = 0;
                tslInformation.BackColor = Color.Red;
                tslInformation.Text = @"Offline";

            }
            ChartValues.Add(new MeasureModel
            {
                DateTime = DateTime.Now,
                Value = CurrentStatus
            });

            //lets only use the last 30 values
            if (ChartValues.Count > 30) ChartValues.RemoveAt(0);
        }

        /// <summary>
        /// This method return 1 if the server is online and 0 otherwise.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int GetServerStatus(string value)
        {
            switch (value.ToUpper())
            {
                case Ready:
                    tslInformation.BackColor = Color.Lime;
                    tslInformation.Text = @"Online";
                    return 1;
                default:
                    tslInformation.BackColor = Color.Red;
                    tslInformation.Text = @"Offline: " + value;
                    return 0;
            }
        }

        /// <summary>
        /// Verify if the response coming from the Paysafe server is indeed a valid Json format.
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        private static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This method is look for the server status in an asynchronous way.
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        private async Task<string> ExecuteUrl(string queryString)
        {
            string result = string.Empty;
            try
            {
                var baseAddress = new Uri(queryString);
                using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                {
                    using (var response = await httpClient.GetAsync("monitor"))
                    {
                        //Few server's responses have been customized. But all the answers are handled.
                        if (response.IsSuccessStatusCode)
                        { 
                            string responseData = await response.Content.ReadAsStringAsync(); 
                            result = responseData;
                        }
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            result = @"No valid API key provided.";
                        }
                        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            result = @"No valid API key provided. (Not found).";
                        }
                        if (response.StatusCode >= System.Net.HttpStatusCode.InternalServerError)
                        {
                            result = @"There is a problem when contacting the server.";
                        }
                    }
                }
            }
            catch (TaskCanceledException taskException)
            {
                if (!taskException.CancellationToken.IsCancellationRequested)
                {
                    result = @"Timeout expired trying to reach the server.";
                }
            }
            catch (HttpRequestException httpException)
            {
                result = @"An error occurred during the transaction: " + httpException.Message;
            }
            catch (Exception ex)
            {
                result = @"An error occurred during the request: " + ex.Message;
            }

            return result;
        }

       

        /// <summary>
        /// Open the report form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReport_Click(object sender, EventArgs e)
        {
            if (ChartValues.Count > 0)
            {
                _replayMonitoring.Clear();
                _replayMonitoring.Add(ChartValues);
                FrmReport frmReport = new FrmReport(_replayMonitoring);
                frmReport.ShowDialog();
            }
            else
            {
                MessageBox.Show(@"No Data", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

       
    }

}
