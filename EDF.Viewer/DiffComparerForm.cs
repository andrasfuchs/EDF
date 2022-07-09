using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Analogy.CommonControls.DataTypes;
using Analogy.CommonControls.Plotting;
using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;
using DevExpress.XtraEditors;
using EDFCSharp;

namespace EDF.Viewer
{
    public partial class DiffComparerForm : XtraForm, IAnalogyPlotting
    {
        private int _selectedMissingGtIndex;
        private int _selectedMissingUtIndex;
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid FactoryId { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = "ECG Plotter";
        public event EventHandler<AnalogyPlottingPointData> OnNewPointData;
        public event EventHandler<List<AnalogyPlottingPointData>> OnNewPointsData;
        private PlottingUC Plotter { get; set; }
        private List<(string SeriesName, AnalogyPlottingSeriesType SeriesViewType)> Series { get; set; }
        private AnalogyPlottingPointXAxisDataType AxisType { get; set; }
        private int TimeOffset { get; set; }
        private EDFSignal[] Signals { get; set; }
        public DiffComparerForm()
        {
            InitializeComponent();
            Series = new List<(string SeriesName, AnalogyPlottingSeriesType SeriesViewType)>();
        }

        private void DiffComparerForm_Load(object sender, EventArgs e)
        {
            seTimeOffset.EditValue = TimeOffset;
            if (!DesignMode)
            {
                sbtnBrowse.Click += (s, arg) =>
                {
                    using (var openFileDialog1 = new OpenFileDialog
                    {
                        Multiselect = false,
                        Filter = "EDF files (*.edf)|*.edf|All files (*.*)|*.*"
                    })
                    {
                        DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
                        if (result == DialogResult.OK) // Test result.
                        {
                            teH5.Text = openFileDialog1.FileName;
                            LoadEDFFile(teH5.Text);

                        }
                    }
                };
                sbtnLoad.Click += (s, arg) => LoadEDFFile(teH5.Text);
            }
        }

        public void LoadEDFFile(string filename)
        {
            using (var edf = new EDFFile(filename))
            {
                Series = edf.Signals.Select(s => (s.Label.Value, AnalogyPlottingSeriesType.Line)).ToList();
                Signals = edf.Signals;
            }
            LoadDataToChart(true);
        }
        public void GeneratePlotter()
        {
            if (Plotter != null)
            {
                gcDocument.Controls.Clear();
                Plotter.Stop();
                //Settings.ChartState = Plotter.PlotState;
                Plotter.LegendItemChecked -= Plotter_LegendItemChecked;
            }

            AxisType = ceTimeAxis.Checked ? AnalogyPlottingPointXAxisDataType.DateTime
                : AnalogyPlottingPointXAxisDataType.Numerical;
            Plotter = new PlottingUC(this, new AnalogyPlottingInteractor(AxisType, 500000000));
            gcDocument.Controls.Add(Plotter);
            Plotter.Dock = DockStyle.Fill;
            //Plotter.SetState(Settings.ChartState, false);
            Plotter.SetECGScaling();
            Plotter.SetDarkTheme(true);
            Plotter.SetDataColor(Color.Yellow);
            Plotter.Start();
            Plotter.LegendItemChecked += Plotter_LegendItemChecked;
        }

        private void Plotter_LegendItemChecked(object sender, (string name, bool isChecked) e)
        {
            //Settings.ChartState.AddCheckedState(e.name, e.isChecked);
        }


        public IEnumerable<(string SeriesName, AnalogyPlottingSeriesType SeriesViewType)> GetChartSeries()
        {
            return Series.AsEnumerable();
        }


        public Task InitializePlottingAsync(IAnalogyPlottingInteractor uiInteractor, IAnalogyLogger logger)
        {
            return Task.CompletedTask;
        }

        public Task StartPlotting()
        {
            return Task.CompletedTask;
        }

        public Task StopPlotting()
        {
            return Task.CompletedTask;
        }

        private void LoadDataToChart(bool regenerate)
        {
            if (regenerate || Plotter == null)
            {
                GeneratePlotter();
            }
            else
            {
                Plotter?.ClearSeriesDataAndRemoveConstantLines();
            }

            foreach (EDFSignal signal in Signals)
            {
                var data = new List<AnalogyPlottingPointData>();
                for (int i = 0; i < signal.SamplesCount; i++)
                {
                    data.Add(new AnalogyPlottingPointData(signal.Label.Value, signal.Samples[i], signal.Times[i].DateTime, signal.Timestamps[i], AxisType));
                }
                OnNewPointsData?.Invoke(this, data);
            }
        }


        private void btsiShowChartControl_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Plotter?.ShowHidePlotControls(btsiShowChartControl.Checked);
        }

        private void sbtnForceRedraw_Click(object sender, EventArgs e)
        {
            TimeOffset = decimal.ToInt32((decimal)seTimeOffset.EditValue);
            LoadDataToChart(true);
        }

        private void btsiShowChartLegend_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Plotter?.ShowHideLegend(btsiShowChartLegend.Checked);
        }

        private void btsiChartFullScreen_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splitContainerControl1.PanelVisibility =
                btsiChartFullScreen.Checked ? SplitPanelVisibility.Panel2 : SplitPanelVisibility.Both;
        }

        private void seTimeOffset_EditValueChanged(object sender, EventArgs e)
        {
            // Settings.HourOffset = decimal.ToInt32((decimal)seTimeOffset.EditValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DateTime GetTime(long ts) => DateTimeOffset.FromUnixTimeMilliseconds(ts).UtcDateTime.AddHours(TimeOffset);

    }
}
