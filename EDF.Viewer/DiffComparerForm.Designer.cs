namespace EDF.Viewer
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
            this.gcTop = new DevExpress.XtraEditors.GroupControl();
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
            this.sbtnForceRedraw = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.seTimeOffset = new DevExpress.XtraEditors.SpinEdit();
            this.ceTimeAxis = new DevExpress.XtraEditors.CheckEdit();
            this.gcDocument = new DevExpress.XtraEditors.GroupControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.lblProgressText = new DevExpress.XtraEditors.LabelControl();
            this.pbH5 = new DevExpress.XtraEditors.ProgressBarControl();
            this.sbtnLoad = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.teH5 = new DevExpress.XtraEditors.TextEdit();
            this.lblH5File = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcTop)).BeginInit();
            this.gcTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seTimeOffset.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceTimeAxis.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDocument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbH5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teH5.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcTop
            // 
            this.gcTop.Controls.Add(this.lblProgressText);
            this.gcTop.Controls.Add(this.pbH5);
            this.gcTop.Controls.Add(this.sbtnLoad);
            this.gcTop.Controls.Add(this.sbtnBrowse);
            this.gcTop.Controls.Add(this.teH5);
            this.gcTop.Controls.Add(this.lblH5File);
            this.gcTop.Controls.Add(this.sbtnForceRedraw);
            this.gcTop.Controls.Add(this.labelControl1);
            this.gcTop.Controls.Add(this.seTimeOffset);
            this.gcTop.Controls.Add(this.ceTimeAxis);
            this.gcTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTop.Location = new System.Drawing.Point(0, 0);
            this.gcTop.Name = "gcTop";
            this.gcTop.Size = new System.Drawing.Size(1746, 176);
            this.gcTop.TabIndex = 1;
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
            // sbtnForceRedraw
            // 
            this.sbtnForceRedraw.Location = new System.Drawing.Point(1454, 144);
            this.sbtnForceRedraw.Name = "sbtnForceRedraw";
            this.sbtnForceRedraw.Size = new System.Drawing.Size(120, 29);
            this.sbtnForceRedraw.TabIndex = 53;
            this.sbtnForceRedraw.Text = "Force Redraw";
            this.sbtnForceRedraw.Click += new System.EventHandler(this.sbtnForceRedraw_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(1260, 151);
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
            this.seTimeOffset.Location = new System.Drawing.Point(1355, 147);
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
            this.ceTimeAxis.Location = new System.Drawing.Point(1112, 146);
            this.ceTimeAxis.Name = "ceTimeAxis";
            this.ceTimeAxis.Properties.Caption = "Show Time in Axis";
            this.ceTimeAxis.Size = new System.Drawing.Size(146, 24);
            this.ceTimeAxis.TabIndex = 50;
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
            // lblProgressText
            // 
            this.lblProgressText.AutoEllipsis = true;
            this.lblProgressText.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblProgressText.Location = new System.Drawing.Point(12, 75);
            this.lblProgressText.Name = "lblProgressText";
            this.lblProgressText.Size = new System.Drawing.Size(88, 22);
            this.lblProgressText.TabIndex = 59;
            this.lblProgressText.Text = "Progress:";
            // 
            // pbH5
            // 
            this.pbH5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbH5.Location = new System.Drawing.Point(106, 61);
            this.pbH5.Name = "pbH5";
            this.pbH5.Properties.ShowTitle = true;
            this.pbH5.ShowProgressInTaskBar = true;
            this.pbH5.Size = new System.Drawing.Size(1637, 24);
            this.pbH5.TabIndex = 58;
            // 
            // sbtnLoad
            // 
            this.sbtnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtnLoad.Location = new System.Drawing.Point(1663, 28);
            this.sbtnLoad.Name = "sbtnLoad";
            this.sbtnLoad.Size = new System.Drawing.Size(80, 27);
            this.sbtnLoad.TabIndex = 57;
            this.sbtnLoad.Text = "Load";
            // 
            // sbtnBrowse
            // 
            this.sbtnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtnBrowse.Location = new System.Drawing.Point(1614, 28);
            this.sbtnBrowse.Name = "sbtnBrowse";
            this.sbtnBrowse.Size = new System.Drawing.Size(43, 27);
            this.sbtnBrowse.TabIndex = 56;
            this.sbtnBrowse.Text = "...";
            // 
            // teH5
            // 
            this.teH5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teH5.Location = new System.Drawing.Point(39, 31);
            this.teH5.Name = "teH5";
            this.teH5.Size = new System.Drawing.Size(1569, 22);
            this.teH5.TabIndex = 55;
            // 
            // lblH5File
            // 
            this.lblH5File.Location = new System.Drawing.Point(12, 34);
            this.lblH5File.Name = "lblH5File";
            this.lblH5File.Size = new System.Drawing.Size(25, 16);
            this.lblH5File.TabIndex = 54;
            this.lblH5File.Text = "File:";
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
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seTimeOffset.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceTimeAxis.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDocument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbH5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teH5.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl gcTop;
        private DevExpress.XtraEditors.GroupControl gcDocument;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
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
        private DevExpress.XtraEditors.LabelControl lblProgressText;
        private DevExpress.XtraEditors.ProgressBarControl pbH5;
        private DevExpress.XtraEditors.SimpleButton sbtnLoad;
        private DevExpress.XtraEditors.SimpleButton sbtnBrowse;
        private DevExpress.XtraEditors.TextEdit teH5;
        private DevExpress.XtraEditors.LabelControl lblH5File;
    }
}