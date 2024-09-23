namespace Cam.WF;

partial class Form1
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
        pictureBoxVideo = new PictureBox();
        buttonStart = new Button();
        buttonStop = new Button();
        buttonSelect = new Button();
        comboBoxSource = new ComboBox();
        ((System.ComponentModel.ISupportInitialize)pictureBoxVideo).BeginInit();
        SuspendLayout();
        // 
        // pictureBoxVideo
        // 
        pictureBoxVideo.Location = new Point(158, 111);
        pictureBoxVideo.Name = "pictureBoxVideo";
        pictureBoxVideo.Size = new Size(640, 480);
        pictureBoxVideo.TabIndex = 0;
        pictureBoxVideo.TabStop = false;
        // 
        // buttonStart
        // 
        buttonStart.Location = new Point(158, 34);
        buttonStart.Name = "buttonStart";
        buttonStart.Size = new Size(95, 45);
        buttonStart.TabIndex = 1;
        buttonStart.Text = "Start";
        buttonStart.UseVisualStyleBackColor = true;
        buttonStart.Click += buttonStart_Click;
        // 
        // buttonStop
        // 
        buttonStop.Location = new Point(703, 34);
        buttonStop.Name = "buttonStop";
        buttonStop.Size = new Size(95, 45);
        buttonStop.TabIndex = 2;
        buttonStop.Text = "Stop";
        buttonStop.UseVisualStyleBackColor = true;
        buttonStop.Click += buttonStop_Click;
        // 
        // buttonSelect
        // 
        buttonSelect.Location = new Point(602, 34);
        buttonSelect.Name = "buttonSelect";
        buttonSelect.Size = new Size(95, 45);
        buttonSelect.TabIndex = 3;
        buttonSelect.Text = "Select file";
        buttonSelect.UseVisualStyleBackColor = true;
        buttonSelect.Click += buttonSelect_Click;
        // 
        // comboBoxSource
        // 
        comboBoxSource.FormattingEnabled = true;
        comboBoxSource.Location = new Point(259, 41);
        comboBoxSource.Name = "comboBoxSource";
        comboBoxSource.Size = new Size(337, 33);
        comboBoxSource.TabIndex = 4;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1002, 712);
        Controls.Add(comboBoxSource);
        Controls.Add(buttonSelect);
        Controls.Add(buttonStop);
        Controls.Add(buttonStart);
        Controls.Add(pictureBoxVideo);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Camara";
        ((System.ComponentModel.ISupportInitialize)pictureBoxVideo).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private PictureBox pictureBoxVideo;
    private Button buttonStart;
    private Button buttonStop;
    private Button buttonSelect;
    private ComboBox comboBoxSource;
}
