namespace PeakDetectorAnalyzer
{
    partial class DiffComparerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTop = new DevExpress.XtraEditors.LabelControl();
            this.gcTop = new DevExpress.XtraEditors.GroupControl();
            this.lbUTMissing = new DevExpress.XtraEditors.LabelControl();
            this.lblGTMissing = new DevExpress.XtraEditors.LabelControl();
            this.lueUTMissing = new DevExpress.XtraEditors.LookUpEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btsiShowChartControl = new DevExpress.XtraBars.BarToggleSwitchItem();
            this.btsiShowChartLegend = new DevExpress.XtraBars.BarToggleSwitchItem();
            this.btsiChartFullScreen = new DevExpress.XtraBars.BarToggleSwitchItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.lueGTMissing = new DevExpress.XtraEditors.LookUpEdit();
            this.sbtnForceRedraw = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.seTimeOffset = new DevExpress.XtraEditors.SpinEdit();
            this.ceTimeAxis = new DevExpress.XtraEditors.CheckEdit();
            this.sbtnUTPerv = new DevExpress.XtraEditors.SimpleButton();
            this.UTMissing = new DevExpress.XtraEditors.LabelControl();
            this.sbtnUTNext = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnGTPerv = new DevExpress.XtraEditors.SimpleButton();
            this.GTMissing = new DevExpress.XtraEditors.LabelControl();
            this.sbtnGTNext = new DevExpress.XtraEditors.SimpleButton();
            this.clbchannels = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.ceShowUnFiltered = new DevExpress.XtraEditors.CheckEdit();
            this.ceShowFiltered = new DevExpress.XtraEditors.CheckEdit();
            this.lblTolerance = new DevExpress.XtraEditors.LabelControl();
            this.seTolerance = new DevExpress.XtraEditors.SpinEdit();
            this.lblType = new DevExpress.XtraEditors.LabelControl();
            this.cbTypes = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gcDocument = new DevExpress.XtraEditors.GroupControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ceShowAllData = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTop)).BeginInit();
            this.gcTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueUTMissing.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueGTMissing.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seTimeOffset.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceTimeAxis.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clbchannels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceShowUnFiltered.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceShowFiltered.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seTolerance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTypes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDocument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceShowAllData.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTop
            // 
            this.lblTop.AutoEllipsis = true;
            this.lblTop.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTop.Location = new System.Drawing.Point(2, 28);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(1742, 32);
            this.lblTop.TabIndex = 0;
            this.lblTop.Text = "N/A";
            // 
            // gcTop
            // 
            this.gcTop.Controls.Add(this.ceShowAllData);
            this.gcTop.Controls.Add(this.lbUTMissing);
            this.gcTop.Controls.Add(this.lblGTMissing);
            this.gcTop.Controls.Add(this.lueUTMissing);
            this.gcTop.Controls.Add(this.lueGTMissing);
            this.gcTop.Controls.Add(this.sbtnForceRedraw);
            this.gcTop.Controls.Add(this.labelControl1);
            this.gcTop.Controls.Add(this.seTimeOffset);
            this.gcTop.Controls.Add(this.ceTimeAxis);
            this.gcTop.Controls.Add(this.sbtnUTPerv);
            this.gcTop.Controls.Add(this.UTMissing);
            this.gcTop.Controls.Add(this.sbtnUTNext);
            this.gcTop.Controls.Add(this.sbtnGTPerv);
            this.gcTop.Controls.Add(this.GTMissing);
            this.gcTop.Controls.Add(this.sbtnGTNext);
            this.gcTop.Controls.Add(this.clbchannels);
            this.gcTop.Controls.Add(this.ceShowUnFiltered);
            this.gcTop.Controls.Add(this.ceShowFiltered);
            this.gcTop.Controls.Add(this.lblTolerance);
            this.gcTop.Controls.Add(this.seTolerance);
            this.gcTop.Controls.Add(this.lblType);
            this.gcTop.Controls.Add(this.cbTypes);
            this.gcTop.Controls.Add(this.lblTop);
            this.gcTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTop.Location = new System.Drawing.Point(0, 0);
            this.gcTop.Name = "gcTop";
            this.gcTop.Size = new System.Drawing.Size(1746, 176);
            this.gcTop.TabIndex = 1;
            // 
            // lbUTMissing
            // 
            this.lbUTMissing.Location = new System.Drawing.Point(363, 130);
            this.lbUTMissing.Name = "lbUTMissing";
            this.lbUTMissing.Size = new System.Drawing.Size(21, 16);
            this.lbUTMissing.TabIndex = 57;
            this.lbUTMissing.Text = "UT:";
            // 
            // lblGTMissing
            // 
            this.lblGTMissing.Location = new System.Drawing.Point(363, 103);
            this.lblGTMissing.Name = "lblGTMissing";
            this.lblGTMissing.Size = new System.Drawing.Size(21, 16);
            this.lblGTMissing.TabIndex = 56;
            this.lblGTMissing.Text = "GT:";
            // 
            // lueUTMissing
            // 
            this.lueUTMissing.Location = new System.Drawing.Point(407, 127);
            this.lueUTMissing.MenuManager = this.barManager1;
            this.lueUTMissing.Name = "lueUTMissing";
            this.lueUTMissing.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueUTMissing.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueUTMissing.Size = new System.Drawing.Size(244, 22);
            this.lueUTMissing.TabIndex = 55;
            this.lueUTMissing.EditValueChanged += new System.EventHandler(this.lueUTMissing_EditValueChanged);
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.AllowShowToolbarsPopup = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btsiShowChartControl,
            this.btsiShowChartLegend,
            this.btsiChartFullScreen});
            this.barManager1.MaxItemId = 3;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btsiShowChartControl),
            new DevExpress.XtraBars.LinkPersistInfo(this.btsiShowChartLegend),
            new DevExpress.XtraBars.LinkPersistInfo(this.btsiChartFullScreen)});
            this.bar1.Text = "Tools";
            // 
            // btsiShowChartControl
            // 
            this.btsiShowChartControl.Caption = "Show Chart Controls";
            this.btsiShowChartControl.Id = 0;
            this.btsiShowChartControl.Name = "btsiShowChartControl";
            this.btsiShowChartControl.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.btsiShowChartControl_CheckedChanged);
            // 
            // btsiShowChartLegend
            // 
            this.btsiShowChartLegend.Caption = "Show Chart Legend";
            this.btsiShowChartLegend.Id = 1;
            this.btsiShowChartLegend.Name = "btsiShowChartLegend";
            this.btsiShowChartLegend.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.btsiShowChartLegend_CheckedChanged);
            // 
            // btsiChartFullScreen
            // 
            this.btsiChartFullScreen.Caption = "Chart Full Screen";
            this.btsiChartFullScreen.Id = 2;
            this.btsiChartFullScreen.Name = "btsiChartFullScreen";
            this.btsiChartFullScreen.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.btsiChartFullScreen_CheckedChanged);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1746, 25);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 563);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1746, 20);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 25);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 538);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1746, 25);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 538);
            // 
            // lueGTMissing
            // 
            this.lueGTMissing.Location = new System.Drawing.Point(407, 100);
            this.lueGTMissing.MenuManager = this.barManager1;
            this.lueGTMissing.Name = "lueGTMissing";
            this.lueGTMissing.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueGTMissing.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueGTMissing.Size = new System.Drawing.Size(244, 22);
            this.lueGTMissing.TabIndex = 54;
            this.lueGTMissing.EditValueChanged += new System.EventHandler(this.lueGTMissing_EditValueChanged);
            // 
            // sbtnForceRedraw
            // 
            this.sbtnForceRedraw.Location = new System.Drawing.Point(1366, 66);
            this.sbtnForceRedraw.Name = "sbtnForceRedraw";
            this.sbtnForceRedraw.Size = new System.Drawing.Size(120, 29);
            this.sbtnForceRedraw.TabIndex = 53;
            this.sbtnForceRedraw.Text = "Force Redraw";
            this.sbtnForceRedraw.Click += new System.EventHandler(this.sbtnForceRedraw_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(1172, 73);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 16);
            this.labelControl1.TabIndex = 52;
            this.labelControl1.Text = "Time Offset:";
            // 
            // seTimeOffset
            // 
            this.seTimeOffset.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.seTimeOffset.Location = new System.Drawing.Point(1267, 69);
            this.seTimeOffset.Name = "seTimeOffset";
            this.seTimeOffset.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seTimeOffset.Properties.MaskSettings.Set("mask", "n0");
            this.seTimeOffset.Properties.MaxValue = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.seTimeOffset.Properties.MinValue = new decimal(new int[] {
            23,
            0,
            0,
            -2147483648});
            this.seTimeOffset.Size = new System.Drawing.Size(93, 24);
            this.seTimeOffset.TabIndex = 51;
            this.seTimeOffset.EditValueChanged += new System.EventHandler(this.seTimeOffset_EditValueChanged);
            // 
            // ceTimeAxis
            // 
            this.ceTimeAxis.EditValue = true;
            this.ceTimeAxis.Location = new System.Drawing.Point(1024, 68);
            this.ceTimeAxis.Name = "ceTimeAxis";
            this.ceTimeAxis.Properties.Caption = "Show Time in Axis";
            this.ceTimeAxis.Size = new System.Drawing.Size(146, 24);
            this.ceTimeAxis.TabIndex = 50;
            // 
            // sbtnUTPerv
            // 
            this.sbtnUTPerv.Location = new System.Drawing.Point(657, 128);
            this.sbtnUTPerv.Name = "sbtnUTPerv";
            this.sbtnUTPerv.Size = new System.Drawing.Size(24, 24);
            this.sbtnUTPerv.TabIndex = 49;
            this.sbtnUTPerv.Click += new System.EventHandler(this.sbtnUTPerv_Click);
            // 
            // UTMissing
            // 
            this.UTMissing.Location = new System.Drawing.Point(719, 133);
            this.UTMissing.Name = "UTMissing";
            this.UTMissing.Size = new System.Drawing.Size(33, 16);
            this.UTMissing.TabIndex = 47;
            this.UTMissing.Text = "0/200";
            // 
            // sbtnUTNext
            // 
            this.sbtnUTNext.Enabled = false;
            this.sbtnUTNext.Location = new System.Drawing.Point(687, 128);
            this.sbtnUTNext.Name = "sbtnUTNext";
            this.sbtnUTNext.Size = new System.Drawing.Size(24, 24);
            this.sbtnUTNext.TabIndex = 46;
            this.sbtnUTNext.Click += new System.EventHandler(this.sbtnUTNext_Click);
            // 
            // sbtnGTPerv
            // 
            this.sbtnGTPerv.Location = new System.Drawing.Point(657, 98);
            this.sbtnGTPerv.Name = "sbtnGTPerv";
            this.sbtnGTPerv.Size = new System.Drawing.Size(24, 24);
            this.sbtnGTPerv.TabIndex = 45;
            this.sbtnGTPerv.Click += new System.EventHandler(this.sbtnGTPerv_Click);
            // 
            // GTMissing
            // 
            this.GTMissing.Location = new System.Drawing.Point(719, 103);
            this.GTMissing.Name = "GTMissing";
            this.GTMissing.Size = new System.Drawing.Size(33, 16);
            this.GTMissing.TabIndex = 43;
            this.GTMissing.Text = "0/200";
            // 
            // sbtnGTNext
            // 
            this.sbtnGTNext.Enabled = false;
            this.sbtnGTNext.Location = new System.Drawing.Point(687, 98);
            this.sbtnGTNext.Name = "sbtnGTNext";
            this.sbtnGTNext.Size = new System.Drawing.Size(24, 24);
            this.sbtnGTNext.TabIndex = 42;
            this.sbtnGTNext.Click += new System.EventHandler(this.sbtnGTNext_Click);
            // 
            // clbchannels
            // 
            this.clbchannels.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Lead I", System.Windows.Forms.CheckState.Checked),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Lead II", System.Windows.Forms.CheckState.Checked),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Lead III"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "AVR"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "AVF"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "V1"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "V2"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "V3"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "V4"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "V5"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "V6"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Channel 12")});
            this.clbchannels.Location = new System.Drawing.Point(12, 66);
            this.clbchannels.Name = "clbchannels";
            this.clbchannels.Size = new System.Drawing.Size(132, 95);
            this.clbchannels.TabIndex = 41;
            // 
            // ceShowUnFiltered
            // 
            this.ceShowUnFiltered.Location = new System.Drawing.Point(151, 94);
            this.ceShowUnFiltered.Name = "ceShowUnFiltered";
            this.ceShowUnFiltered.Properties.Caption = "Show UnFiltered Data";
            this.ceShowUnFiltered.Size = new System.Drawing.Size(172, 24);
            this.ceShowUnFiltered.TabIndex = 40;
            // 
            // ceShowFiltered
            // 
            this.ceShowFiltered.EditValue = true;
            this.ceShowFiltered.Location = new System.Drawing.Point(151, 66);
            this.ceShowFiltered.Name = "ceShowFiltered";
            this.ceShowFiltered.Properties.Caption = "Show Filtered Data";
            this.ceShowFiltered.Size = new System.Drawing.Size(138, 24);
            this.ceShowFiltered.TabIndex = 39;
            // 
            // lblTolerance
            // 
            this.lblTolerance.Location = new System.Drawing.Point(661, 72);
            this.lblTolerance.Name = "lblTolerance";
            this.lblTolerance.Size = new System.Drawing.Size(188, 16);
            this.lblTolerance.TabIndex = 15;
            this.lblTolerance.Text = "Data before and after the on/off:";
            // 
            // seTolerance
            // 
            this.seTolerance.EditValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.seTolerance.Location = new System.Drawing.Point(856, 67);
            this.seTolerance.Name = "seTolerance";
            this.seTolerance.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seTolerance.Properties.MaskSettings.Set("mask", "n0");
            this.seTolerance.Properties.MaxValue = new decimal(new int[] {
            658067456,
            1164,
            0,
            0});
            this.seTolerance.Size = new System.Drawing.Size(146, 24);
            this.seTolerance.TabIndex = 14;
            this.seTolerance.EditValueChanged += new System.EventHandler(this.seTolerance_EditValueChanged);
            // 
            // lblType
            // 
            this.lblType.Location = new System.Drawing.Point(363, 71);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(33, 16);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Type:";
            // 
            // cbTypes
            // 
            this.cbTypes.Location = new System.Drawing.Point(407, 68);
            this.cbTypes.Name = "cbTypes";
            this.cbTypes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbTypes.Properties.Items.AddRange(new object[] {
            "P",
            "Q",
            "T",
            "V",
            "A",
            "N",
            "X",
            "P Over PQ"});
            this.cbTypes.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbTypes.Size = new System.Drawing.Size(244, 22);
            this.cbTypes.TabIndex = 1;
            this.cbTypes.SelectedIndexChanged += new System.EventHandler(this.cbTypes_SelectedIndexChanged);
            // 
            // gcDocument
            // 
            this.gcDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDocument.Location = new System.Drawing.Point(0, 0);
            this.gcDocument.Name = "gcDocument";
            this.gcDocument.Size = new System.Drawing.Size(1746, 350);
            this.gcDocument.TabIndex = 11;
            this.gcDocument.Text = "Result";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 25);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.gcTop);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.gcDocument);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1746, 538);
            this.splitContainerControl1.SplitterPosition = 176;
            this.splitContainerControl1.TabIndex = 39;
            // 
            // ceShowAllData
            // 
            this.ceShowAllData.EditValue = true;
            this.ceShowAllData.Location = new System.Drawing.Point(856, 99);
            this.ceShowAllData.Name = "ceShowAllData";
            this.ceShowAllData.Properties.Caption = "Show All types of Data in the interval";
            this.ceShowAllData.Size = new System.Drawing.Size(314, 24);
            this.ceShowAllData.TabIndex = 58;
            this.ceShowAllData.EditValueChanged += new System.EventHandler(this.ceShowAllData_EditValueChanged);
            // 
            // DiffComparerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1746, 583);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "DiffComparerForm";
            this.Text = "Data Diff Plotter";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DiffComparerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcTop)).EndInit();
            this.gcTop.ResumeLayout(false);
            this.gcTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueUTMissing.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueGTMissing.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seTimeOffset.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceTimeAxis.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clbchannels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceShowUnFiltered.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceShowFiltered.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seTolerance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTypes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDocument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceShowAllData.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTop;
        private DevExpress.XtraEditors.GroupControl gcTop;
        private DevExpress.XtraEditors.GroupControl gcDocument;
        private DevExpress.XtraEditors.LabelControl lblType;
        private DevExpress.XtraEditors.ComboBoxEdit cbTypes;
        private DevExpress.XtraEditors.LabelControl lblTolerance;
        private DevExpress.XtraEditors.SpinEdit seTolerance;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.CheckEdit ceShowUnFiltered;
        private DevExpress.XtraEditors.CheckEdit ceShowFiltered;
        private DevExpress.XtraEditors.CheckedListBoxControl clbchannels;
        private DevExpress.XtraEditors.SimpleButton sbtnGTNext;
        private DevExpress.XtraEditors.LabelControl GTMissing;
        private DevExpress.XtraEditors.SimpleButton sbtnUTPerv;
        private DevExpress.XtraEditors.LabelControl UTMissing;
        private DevExpress.XtraEditors.SimpleButton sbtnUTNext;
        private DevExpress.XtraEditors.SimpleButton sbtnGTPerv;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarToggleSwitchItem btsiShowChartControl;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.CheckEdit ceTimeAxis;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SpinEdit seTimeOffset;
        private DevExpress.XtraEditors.SimpleButton sbtnForceRedraw;
        private DevExpress.XtraBars.BarToggleSwitchItem btsiShowChartLegend;
        private DevExpress.XtraBars.BarToggleSwitchItem btsiChartFullScreen;
        private DevExpress.XtraEditors.LookUpEdit lueUTMissing;
        private DevExpress.XtraEditors.LookUpEdit lueGTMissing;
        private DevExpress.XtraEditors.LabelControl lbUTMissing;
        private DevExpress.XtraEditors.LabelControl lblGTMissing;
        private DevExpress.XtraEditors.CheckEdit ceShowAllData;
    }
}