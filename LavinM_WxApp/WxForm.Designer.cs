
namespace LavinM_WxApp
{
    partial class WxForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textRequest = new System.Windows.Forms.TextBox();
            this.labelStation = new System.Windows.Forms.Label();
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.ButtonGeolocate = new System.Windows.Forms.Button();
            this.ButtonRetrieve = new System.Windows.Forms.Button();
            this.tipStation = new System.Windows.Forms.ToolTip(this.components);
            this.BoxForecast = new System.Windows.Forms.GroupBox();
            this.tableForecast = new System.Windows.Forms.TableLayoutPanel();
            this.tipGeolocate = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxInput.SuspendLayout();
            this.BoxForecast.SuspendLayout();
            this.SuspendLayout();
            // 
            // textRequest
            // 
            this.textRequest.Location = new System.Drawing.Point(59, 16);
            this.textRequest.Name = "textRequest";
            this.textRequest.Size = new System.Drawing.Size(103, 23);
            this.textRequest.TabIndex = 0;
            this.tipStation.SetToolTip(this.textRequest, "Four-letter code, e.g. KEEN for Keene\r\nand KWAL for Wallops Island.");
            // 
            // labelStation
            // 
            this.labelStation.AutoSize = true;
            this.labelStation.Location = new System.Drawing.Point(6, 19);
            this.labelStation.Name = "labelStation";
            this.labelStation.Size = new System.Drawing.Size(47, 15);
            this.labelStation.TabIndex = 1;
            this.labelStation.Text = "Station:";
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.AutoSize = true;
            this.groupBoxInput.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxInput.Controls.Add(this.ButtonGeolocate);
            this.groupBoxInput.Controls.Add(this.ButtonRetrieve);
            this.groupBoxInput.Controls.Add(this.labelStation);
            this.groupBoxInput.Controls.Add(this.textRequest);
            this.groupBoxInput.Location = new System.Drawing.Point(12, 12);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Size = new System.Drawing.Size(168, 90);
            this.groupBoxInput.TabIndex = 2;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "Input";
            // 
            // ButtonGeolocate
            // 
            this.ButtonGeolocate.Location = new System.Drawing.Point(6, 45);
            this.ButtonGeolocate.Name = "ButtonGeolocate";
            this.ButtonGeolocate.Size = new System.Drawing.Size(75, 23);
            this.ButtonGeolocate.TabIndex = 3;
            this.ButtonGeolocate.Text = "Geolocate";
            this.tipGeolocate.SetToolTip(this.ButtonGeolocate, "Uses Windows API to determine your\r\nlocation. Make sure it\'s turned on!");
            this.ButtonGeolocate.UseVisualStyleBackColor = true;
            this.ButtonGeolocate.Click += new System.EventHandler(this.ButtonGeolocate_ClickAsync);
            // 
            // ButtonRetrieve
            // 
            this.ButtonRetrieve.Location = new System.Drawing.Point(87, 45);
            this.ButtonRetrieve.Name = "ButtonRetrieve";
            this.ButtonRetrieve.Size = new System.Drawing.Size(75, 23);
            this.ButtonRetrieve.TabIndex = 2;
            this.ButtonRetrieve.Text = "Enter";
            this.ButtonRetrieve.UseVisualStyleBackColor = true;
            this.ButtonRetrieve.Click += new System.EventHandler(this.ButtonRetrieve_Click);
            // 
            // tipStation
            // 
            this.tipStation.ToolTipTitle = "Observation station";
            // 
            // BoxForecast
            // 
            this.BoxForecast.Controls.Add(this.tableForecast);
            this.BoxForecast.Location = new System.Drawing.Point(186, 12);
            this.BoxForecast.Name = "BoxForecast";
            this.BoxForecast.Size = new System.Drawing.Size(250, 344);
            this.BoxForecast.TabIndex = 5;
            this.BoxForecast.TabStop = false;
            this.BoxForecast.Text = "Forecast";
            // 
            // tableForecast
            // 
            this.tableForecast.AutoScroll = true;
            this.tableForecast.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableForecast.ColumnCount = 1;
            this.tableForecast.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 244F));
            this.tableForecast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableForecast.Location = new System.Drawing.Point(3, 19);
            this.tableForecast.Name = "tableForecast";
            this.tableForecast.Size = new System.Drawing.Size(244, 322);
            this.tableForecast.TabIndex = 6;
            // 
            // tipGeolocate
            // 
            this.tipGeolocate.ToolTipTitle = "Geolocation";
            // 
            // WxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BoxForecast);
            this.Controls.Add(this.groupBoxInput);
            this.MaximizeBox = false;
            this.Name = "WxForm";
            this.Text = "PowerWX";
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            this.BoxForecast.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textRequest;
        private System.Windows.Forms.Label labelStation;
        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.ToolTip tipStation;
        private System.Windows.Forms.TabControl tabWx;
        private System.Windows.Forms.TabPage tabObservation;
        private System.Windows.Forms.TabPage tabStation;
        private System.Windows.Forms.Button ButtonRetrieve;
        private System.Windows.Forms.GroupBox GroupTemperature;
        private System.Windows.Forms.Label labelTemperature;
        private System.Windows.Forms.Button ButtonGeolocate;
        private System.Windows.Forms.GroupBox BoxForecast;
        private System.Windows.Forms.TableLayoutPanel tableForecast;
        private System.Windows.Forms.ToolTip tipGeolocate;
    }
}

