using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;

namespace PeakDetectorAnalyzer
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

        private UserSettingsManager Settings => UserSettingsManager.Instance;

        private List<DetectionPeak> GoldPeaks { get; set; }
        private List<DetectionPeak> ManualPeaks { get; set; }
        private ECGData ECG { get; set; }
        private PeakDetectionResultTable Result { get; set; }
        private PlottingUC Plotter { get; set; }
        private PeakDetectionVerifierTypeResult SelectedResult { get; set; }
        private List<DetectionPeakWithProperties> SelectedMissingGT { get; set; }

        private int SelectedMissingGTIndex
        {
            get => _selectedMissingGtIndex;
            set
            {
                _selectedMissingGtIndex = value;
                GTMissing.Text = $"{SelectedMissingGTIndex + 1}/{SelectedMissingGT.Count}";

            }
        }

        private List<DetectionPeakWithProperties> SelectedMissingUT { get; set; }

        private int SelectedMissingUTIndex
        {
            get => _selectedMissingUtIndex;
            set
            {
                _selectedMissingUtIndex = value;
                UTMissing.Text = $"{SelectedMissingUTIndex + 1}/{SelectedMissingUT.Count}";

            }
        }

        private Dictionary<DetectionPeakType, Color> PeakTypeColors { get; }
        private DashStyle GTLine => DashStyle.Solid;
        private DashStyle UTLine => DashStyle.Dot;

        private DetectionPeakWithProperties SelectedPoint
        {
            get => _selectedPoint;
            set
            {
                if (value != _selectedPoint && value != null)
                {
                    _selectedPoint = value;
                    LoadDataToChart(false);
                }
            }
        }

        private AnalogyPlottingPointXAxisDataType AxisType { get; set; }
        private List<string> ChannelNames { get; }
        private int TimeOffset { get; set; }

        public DiffComparerForm()
        {
            ChannelNames = new List<string>()
                { "Lead I", "Lead II", "Lead III", "AVR", "AVL", "AVF", "V1", "V2", "V3", "V4", "V5", "V6" };
            PeakTypeColors = new Dictionary<DetectionPeakType, Color>()
            {
                { DetectionPeakType.P, Color.Blue },
                { DetectionPeakType.Q, Color.Green },
                { DetectionPeakType.T, Color.DarkOrange },
                { DetectionPeakType.V, Color.Chocolate },
                { DetectionPeakType.A, Color.Gray },
                { DetectionPeakType.N, Color.SkyBlue },
                { DetectionPeakType.X, Color.Purple },
            };
            InitializeComponent();


        }

        public DiffComparerForm(List<DetectionPeak> goldPeaks, List<DetectionPeak> manualPeaks, ECGData ecg, int timeOffset, PeakDetectionResultTable result) : this()
        {
            GoldPeaks = goldPeaks;
            ManualPeaks = manualPeaks;
            ECG = ecg;
            Result = result;
            TimeOffset = timeOffset;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DateTime GetTime(long ts) => DateTimeOffset.FromUnixTimeMilliseconds(ts).UtcDateTime.AddHours(TimeOffset);

        private void DiffComparerForm_Load(object sender, EventArgs e)
        {
            cbTypes.SelectedIndex = 0;
            seTimeOffset.Value = Settings.HourOffset;
            seTimeOffset.EditValue = TimeOffset;
            lblTop.Text = $"Missing Detections: P: {Result.PResults.MissingDetectionInGT}(GT)/{Result.PResults.MissingDetectionInUT}(UT). Q: {Result.QResults.MissingDetectionInGT}(GT)/{Result.QResults.MissingDetectionInUT}(UT). T: {Result.TResults.MissingDetectionInGT}(GT)/{Result.TResults.MissingDetectionInUT}(UT). V: {Result.VResults.MissingDetectionInGT}(GT)/{Result.VResults.MissingDetectionInUT}(UT). A: {Result.AResults.MissingDetectionInGT}(GT)/{Result.AResults.MissingDetectionInUT}(UT). N: {Result.NResults.MissingDetectionInGT}(GT)/{Result.NResults.MissingDetectionInUT}(UT). X: {Result.XResults.MissingDetectionInGT}(GT)/{Result.XResults.MissingDetectionInUT}(UT. P over PQ: {Result.PPQResults.MissingDetectionInGT}(GT)/{Result.PPQResults.MissingDetectionInUT}(UT))";
        }

        public void GeneratePlotter()
        {
            if (Plotter != null)
            {
                gcDocument.Controls.Clear();
                Plotter.Stop();
                Settings.ChartState = Plotter.PlotState;
                Plotter.LegendItemChecked -= Plotter_LegendItemChecked;
            }

            AxisType = ceTimeAxis.Checked ? AnalogyPlottingPointXAxisDataType.DateTime
                : AnalogyPlottingPointXAxisDataType.Numerical;
            Plotter = new PlottingUC(this, new AnalogyPlottingInteractor(AxisType, 500000));
            gcDocument.Controls.Add(Plotter);
            Plotter.Dock = DockStyle.Fill;
            Plotter.SetState(Settings.ChartState, false);
            Plotter.SetECGScaling();
            Plotter.SetDarkTheme(true);
            Plotter.SetDataColor(Color.Yellow);
            Plotter.Start();
            Plotter.LegendItemChecked += Plotter_LegendItemChecked;
        }

        private void Plotter_LegendItemChecked(object sender, (string name, bool isChecked) e)
        {
            Settings.ChartState.AddCheckedState(e.name, e.isChecked);
        }

        private void cbTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbTypes.SelectedIndex)
            {
                case 0: //p
                    SelectedResult = Result.PResults;
                    break;
                case 1: //q
                    SelectedResult = Result.QResults;
                    break;
                case 2: //t
                    SelectedResult = Result.TResults;
                    break;
                case 3: //v
                    SelectedResult = Result.VResults;
                    break;
                case 4: //a
                    SelectedResult = Result.AResults;
                    break;
                case 5: //n
                    SelectedResult = Result.NResults;
                    break;
                case 6: //x
                    SelectedResult = Result.XResults;
                    break;
                case 7: //P Over PQ
                    SelectedResult = Result.PPQResults;
                    break;
            }

            SelectedMissingGT = SelectedResult.GetNonMatchedGoldStandardPeaks()
                .Select(p => new DetectionPeakWithProperties(p, "GT")).ToList();

            SelectedMissingUT = (SelectedResult.GetNonMatchedManualPeaks()
            .Select(p => new DetectionPeakWithProperties(p, "UT"))).ToList();

            UpdateSelection();
        }

        private void UpdateSelection()
        {
            SelectedMissingGTIndex = -1;
            SelectedMissingUTIndex = -1;

            sbtnGTPerv.Enabled = SelectedMissingGT.Any();
            sbtnUTPerv.Enabled = SelectedMissingUT.Any();
            sbtnGTNext.Enabled = SelectedMissingGT.Any();
            sbtnUTNext.Enabled = SelectedMissingUT.Any();
            lueGTMissing.Properties.DataSource = SelectedMissingGT;
            lueUTMissing.Properties.DataSource = SelectedMissingUT;

        }
        public IEnumerable<(string SeriesName, AnalogyPlottingSeriesType SeriesViewType)> GetChartSeries()
        {
            for (int i = 0; i < 12; i++)
            {
                if (clbchannels.GetItemChecked(i))
                {
                    if (ceShowFiltered.Checked)
                    {
                        yield return ($"{ChannelNames[i]} (Filtered)", AnalogyPlottingSeriesType.Line);
                    }

                    if (ceShowUnFiltered.Checked)
                    {
                        yield return ($"{ChannelNames[i]} (Non Filtered)", AnalogyPlottingSeriesType.Line);

                    }
                }
            }
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
            if (SelectedPoint == null)
            {
                return;
            }

            if (regenerate || Plotter == null)
            {
                GeneratePlotter();
            }
            else
            {
                Plotter?.ClearSeriesDataAndRemoveConstantLines();
            }
            void Plot(int index)
            {
                var ts = ECG.Timestamps[index, 0];
                if (ceShowFiltered.Checked)
                {
                    for (int i = 0; i < ECG.FilteredSignal.GetLength(1); i++)
                    {
                        if (clbchannels.GetItemChecked(i))
                        {
                            if (AxisType == AnalogyPlottingPointXAxisDataType.Numerical)
                            {
                                OnNewPointData?.Invoke(this,
                                    new AnalogyPlottingPointData($"{ChannelNames[i]} (Filtered)",
                                        ECG.FilteredSignal[index, i], ts));
                            }
                            else
                            {
                                OnNewPointData?.Invoke(this,
                                    new AnalogyPlottingPointData($"{ChannelNames[i]} (Filtered)",
                                        ECG.FilteredSignal[index, i], GetTime(ts)));
                            }
                        }
                    }
                }

                if (ceShowUnFiltered.Checked)
                {
                    for (int i = 0; i < ECG.UnfilteredSignal.GetLength(1); i++)
                    {
                        if (clbchannels.GetItemChecked(i))
                        {
                            if (AxisType == AnalogyPlottingPointXAxisDataType.Numerical)
                            {
                                OnNewPointData?.Invoke(this, new AnalogyPlottingPointData($"{ChannelNames[i]} (Non Filtered)", ECG.UnfilteredSignal[index, i],
                                     ts));
                            }
                            else
                            {
                                var time = GetTime(ts);
                                OnNewPointData?.Invoke(this, new AnalogyPlottingPointData($"{ChannelNames[i]} (Non Filtered)", ECG.UnfilteredSignal[index, i], time));

                            }
                        }
                    }
                }

            }

            int offset = decimal.ToInt32((decimal)seTolerance.EditValue);
            for (int i = 0; i < ECG.Timestamps.GetLength(0); i++)
            {
                var timestamp = ECG.Timestamps[i, 0];
                if (timestamp >= SelectedPoint.On - offset && timestamp <= SelectedPoint.Off + offset)
                {
                    Plot(i);
                }

            }

            if (ceShowAllData.Checked)
            {
                GenerateVerticals(Result.TResults, offset, "T");
                GenerateVerticals(Result.VResults, offset, "V");
                GenerateVerticals(Result.AResults, offset, "A");
                GenerateVerticals(Result.NResults, offset, "N");
                GenerateVerticals(Result.XResults, offset, "X");
                GenerateVerticals(Result.PResults, offset, "P");
                GenerateVerticals(Result.QResults, offset, "Q");
                GenerateVerticals(Result.PPQResults, offset, "P over PQ");
            }
            else
            {
                switch (SelectedPoint.Type)
                {
                    case DetectionPeakType.P:
                        GenerateVerticals(Result.PResults, offset, "P");

                        break;
                    case DetectionPeakType.Q:
                        GenerateVerticals(Result.QResults, offset, "Q");
                        break;
                    case DetectionPeakType.T:
                        GenerateVerticals(Result.TResults, offset, "T");
                        break;
                    case DetectionPeakType.V:
                        GenerateVerticals(Result.VResults, offset, "V");
                        break;
                    case DetectionPeakType.A:
                        GenerateVerticals(Result.AResults, offset, "A");
                        break;
                    case DetectionPeakType.N:
                        GenerateVerticals(Result.NResults, offset, "N");
                        break;
                    case DetectionPeakType.X:
                        GenerateVerticals(Result.XResults, offset, "X");
                        break;
                    case DetectionPeakType.START:
                    case DetectionPeakType.END:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void GenerateVerticals(PeakDetectionVerifierTypeResult res, int offset, string name)
        {
            foreach (PeakDetectionVerifierMatchedPeaks p in res.GetMatched())
            {
                if (p.GoldStandardPeak.On > SelectedPoint.On - offset && p.GoldStandardPeak.On < SelectedPoint.On + offset)
                {
                    string legendName = $"GT on ({name})";
                    if (AxisType == AnalogyPlottingPointXAxisDataType.Numerical)
                    {
                        Plotter.AddConstantVerticalLine(legendName, p.GoldStandardPeak.On, PeakTypeColors[p.GoldStandardPeak.Type], GTLine, Settings.ChartState.GetChecked(legendName));
                    }
                    else
                    {
                        Plotter.AddConstantVerticalLine(legendName, GetTime(p.GoldStandardPeak.On), PeakTypeColors[p.GoldStandardPeak.Type], GTLine, Settings.ChartState.GetChecked(legendName));

                    }
                }
                if (p.GoldStandardPeak.Off > SelectedPoint.Off - offset && p.GoldStandardPeak.Off < SelectedPoint.Off + offset)
                {
                    string legendName = $"GT off ({name})";
                    if (AxisType == AnalogyPlottingPointXAxisDataType.Numerical)
                    {
                        Plotter.AddConstantVerticalLine(legendName, p.GoldStandardPeak.Off, PeakTypeColors[p.GoldStandardPeak.Type], GTLine, Settings.ChartState.GetChecked(legendName));
                    }
                    else
                    {
                        Plotter.AddConstantVerticalLine(legendName, GetTime(p.GoldStandardPeak.Off), PeakTypeColors[p.GoldStandardPeak.Type], GTLine, Settings.ChartState.GetChecked(legendName));
                    }
                }
            }

            foreach (var p in res.GetNonMatchedGoldStandardPeaks())
            {
                if (p.On > SelectedPoint.On - offset && p.On < SelectedPoint.On + offset)
                {
                    string legendName = $"GT on ({name})";
                    if (AxisType == AnalogyPlottingPointXAxisDataType.Numerical)
                    {
                        Plotter.AddConstantVerticalLine(legendName, p.On, PeakTypeColors[p.Type], GTLine, Settings.ChartState.GetChecked(legendName));
                    }
                    else
                    {
                        Plotter.AddConstantVerticalLine(legendName, GetTime(p.On), PeakTypeColors[p.Type], GTLine, Settings.ChartState.GetChecked(legendName));

                    }
                }
                if (p.Off > SelectedPoint.Off - offset && p.Off < SelectedPoint.Off + offset)
                {
                    string legendName = $"GT off ({name})";
                    if (AxisType == AnalogyPlottingPointXAxisDataType.Numerical)
                    {
                        Plotter.AddConstantVerticalLine(legendName, p.Off, PeakTypeColors[p.Type], GTLine, Settings.ChartState.GetChecked(legendName));
                    }
                    else
                    {
                        Plotter.AddConstantVerticalLine(legendName, GetTime(p.Off), PeakTypeColors[p.Type], GTLine, Settings.ChartState.GetChecked(legendName));

                    }
                }
            }

            foreach (var p in res.GetNonMatchedManualPeaks())
            {
                if (p.On > SelectedPoint.On - offset && p.On < SelectedPoint.On + offset)
                {
                    string legendName = $"UT on ({name})";
                    if (AxisType == AnalogyPlottingPointXAxisDataType.Numerical)
                    {
                        Plotter.AddConstantVerticalLine(legendName, p.On, PeakTypeColors[p.Type], UTLine, Settings.ChartState.GetChecked(legendName));
                    }
                    else
                    {
                        Plotter.AddConstantVerticalLine(legendName, GetTime(p.On), PeakTypeColors[p.Type], UTLine, Settings.ChartState.GetChecked(legendName));

                    }
                }
                if (p.Off > SelectedPoint.Off - offset && p.Off < SelectedPoint.Off + offset)
                {
                    string legendName = $"UT off ({name})";
                    if (AxisType == AnalogyPlottingPointXAxisDataType.Numerical)
                    {
                        Plotter.AddConstantVerticalLine(legendName, p.Off, PeakTypeColors[p.Type], UTLine, Settings.ChartState.GetChecked(legendName));
                    }
                    else
                    {
                        Plotter.AddConstantVerticalLine(legendName, GetTime(p.Off), PeakTypeColors[p.Type], UTLine, Settings.ChartState.GetChecked(legendName));

                    }
                }
            }

        }
        private void sbtnGTNext_Click(object sender, EventArgs e)
        {
            if (SelectedMissingGT.Count > SelectedMissingGTIndex)
            {
                SelectedMissingGTIndex++;
                SelectedPoint = SelectedMissingGT[SelectedMissingGTIndex];
                lueGTMissing.EditValue = SelectedPoint;
            }
        }

        private void sbtnGTPerv_Click(object sender, EventArgs e)
        {
            if (SelectedMissingGTIndex >= 0)
            {
                SelectedMissingGTIndex--;
                SelectedPoint = SelectedMissingGT[SelectedMissingGTIndex];
                lueGTMissing.EditValue = SelectedPoint;
            }
        }

        private void sbtnUTNext_Click(object sender, EventArgs e)
        {
            if (SelectedMissingUT.Count > SelectedMissingUTIndex)
            {
                SelectedMissingUTIndex++;
                SelectedPoint = SelectedMissingUT[SelectedMissingUTIndex];
                lueUTMissing.EditValue = SelectedPoint;
            }
        }

        private void sbtnUTPerv_Click(object sender, EventArgs e)
        {
            if (SelectedMissingUTIndex >= 0)
            {
                SelectedMissingUTIndex--;
                SelectedPoint = SelectedMissingUT[SelectedMissingUTIndex];
                lueUTMissing.EditValue = SelectedPoint;
            }
        }

        private void seTolerance_EditValueChanged(object sender, EventArgs e)
        {
            LoadDataToChart(false);
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

        private void lueUTMissing_EditValueChanged(object sender, EventArgs e)
        {
            if (lueUTMissing.EditValue is DetectionPeakWithProperties val)
            {
                SelectedMissingUTIndex = lueUTMissing.ItemIndex;
                SelectedPoint = val;
            }
        }

        private void lueGTMissing_EditValueChanged(object sender, EventArgs e)
        {
            if (lueGTMissing.EditValue is DetectionPeakWithProperties val)
            {
                SelectedMissingGTIndex = lueGTMissing.ItemIndex;
                SelectedPoint = val;
            }
        }

        private void seTimeOffset_EditValueChanged(object sender, EventArgs e)
        {
            Settings.HourOffset = decimal.ToInt32((decimal)seTimeOffset.EditValue);
        }

        private void ceShowAllData_EditValueChanged(object sender, EventArgs e)
        {
            LoadDataToChart(false);
        }
    }
}
