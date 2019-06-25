namespace PointXY
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonToGrayScale = new System.Windows.Forms.Button();
            this.buttonToRGB24 = new System.Windows.Forms.Button();
            this.buttonToBinary = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonOtsu = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.buttonSobel = new System.Windows.Forms.Button();
            this.buttonRotateClock = new System.Windows.Forms.Button();
            this.buttonMedian = new System.Windows.Forms.Button();
            this.buttonGauss = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.radioButtonBrightness = new System.Windows.Forms.RadioButton();
            this.radioButtonRGB = new System.Windows.Forms.RadioButton();
            this.radioButtonHSV = new System.Windows.Forms.RadioButton();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.buttonGradient = new System.Windows.Forms.Button();
            this.buttonLaplacian = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonS = new System.Windows.Forms.RadioButton();
            this.radioButtonH = new System.Windows.Forms.RadioButton();
            this.radioButtonV = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numericUpDownThreshold = new System.Windows.Forms.NumericUpDown();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonBackup = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonRestore = new System.Windows.Forms.Button();
            this.buttonOperation = new System.Windows.Forms.Button();
            this.radioButtonAdd = new System.Windows.Forms.RadioButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.radioButtonMul = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(10, 37);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(421, 432);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // buttonToGrayScale
            // 
            this.buttonToGrayScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonToGrayScale.Location = new System.Drawing.Point(23, 27);
            this.buttonToGrayScale.Name = "buttonToGrayScale";
            this.buttonToGrayScale.Size = new System.Drawing.Size(95, 23);
            this.buttonToGrayScale.TabIndex = 1;
            this.buttonToGrayScale.Text = "グレースケール";
            this.buttonToGrayScale.UseVisualStyleBackColor = true;
            this.buttonToGrayScale.Click += new System.EventHandler(this.ButtonToGrayScale_Click);
            // 
            // buttonToRGB24
            // 
            this.buttonToRGB24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonToRGB24.Location = new System.Drawing.Point(699, 37);
            this.buttonToRGB24.Name = "buttonToRGB24";
            this.buttonToRGB24.Size = new System.Drawing.Size(95, 23);
            this.buttonToRGB24.TabIndex = 4;
            this.buttonToRGB24.Text = "24bit RGB";
            this.buttonToRGB24.UseVisualStyleBackColor = true;
            this.buttonToRGB24.Click += new System.EventHandler(this.ButtonToRGB24_Click);
            // 
            // buttonToBinary
            // 
            this.buttonToBinary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonToBinary.Location = new System.Drawing.Point(6, 99);
            this.buttonToBinary.Name = "buttonToBinary";
            this.buttonToBinary.Size = new System.Drawing.Size(63, 23);
            this.buttonToBinary.TabIndex = 5;
            this.buttonToBinary.Text = "2値化";
            this.buttonToBinary.UseVisualStyleBackColor = true;
            this.buttonToBinary.Click += new System.EventHandler(this.ButtonToBinary_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReset.Location = new System.Drawing.Point(96, 34);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(95, 23);
            this.buttonReset.TabIndex = 6;
            this.buttonReset.Text = "リセット";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // buttonOtsu
            // 
            this.buttonOtsu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOtsu.Location = new System.Drawing.Point(6, 29);
            this.buttonOtsu.Name = "buttonOtsu";
            this.buttonOtsu.Size = new System.Drawing.Size(95, 23);
            this.buttonOtsu.TabIndex = 7;
            this.buttonOtsu.Text = "大津のしきい値";
            this.buttonOtsu.UseVisualStyleBackColor = true;
            this.buttonOtsu.Click += new System.EventHandler(this.ButtonOtsu_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox2.Location = new System.Drawing.Point(10, 479);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(421, 89);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // buttonSobel
            // 
            this.buttonSobel.Location = new System.Drawing.Point(25, 87);
            this.buttonSobel.Name = "buttonSobel";
            this.buttonSobel.Size = new System.Drawing.Size(95, 23);
            this.buttonSobel.TabIndex = 9;
            this.buttonSobel.Text = "Sobel";
            this.buttonSobel.UseVisualStyleBackColor = true;
            this.buttonSobel.Click += new System.EventHandler(this.ButtonSobel_Click);
            // 
            // buttonRotateClock
            // 
            this.buttonRotateClock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRotateClock.Location = new System.Drawing.Point(96, 63);
            this.buttonRotateClock.Name = "buttonRotateClock";
            this.buttonRotateClock.Size = new System.Drawing.Size(95, 23);
            this.buttonRotateClock.TabIndex = 10;
            this.buttonRotateClock.Text = "時計回り";
            this.buttonRotateClock.UseVisualStyleBackColor = true;
            this.buttonRotateClock.Click += new System.EventHandler(this.ButtonRotateClock_Click);
            // 
            // buttonMedian
            // 
            this.buttonMedian.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMedian.Location = new System.Drawing.Point(699, 105);
            this.buttonMedian.Name = "buttonMedian";
            this.buttonMedian.Size = new System.Drawing.Size(95, 23);
            this.buttonMedian.TabIndex = 11;
            this.buttonMedian.Text = "Median";
            this.buttonMedian.UseVisualStyleBackColor = true;
            this.buttonMedian.Click += new System.EventHandler(this.ButtonMedian_Click);
            // 
            // buttonGauss
            // 
            this.buttonGauss.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGauss.Location = new System.Drawing.Point(699, 76);
            this.buttonGauss.Name = "buttonGauss";
            this.buttonGauss.Size = new System.Drawing.Size(95, 23);
            this.buttonGauss.TabIndex = 12;
            this.buttonGauss.Text = "Gaussian";
            this.buttonGauss.UseVisualStyleBackColor = true;
            this.buttonGauss.Click += new System.EventHandler(this.ButtonGauss_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 571);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(835, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // radioButtonBrightness
            // 
            this.radioButtonBrightness.AutoSize = true;
            this.radioButtonBrightness.Checked = true;
            this.radioButtonBrightness.Location = new System.Drawing.Point(132, 36);
            this.radioButtonBrightness.Name = "radioButtonBrightness";
            this.radioButtonBrightness.Size = new System.Drawing.Size(52, 16);
            this.radioButtonBrightness.TabIndex = 14;
            this.radioButtonBrightness.TabStop = true;
            this.radioButtonBrightness.Text = "明るさ";
            this.radioButtonBrightness.UseVisualStyleBackColor = true;
            // 
            // radioButtonRGB
            // 
            this.radioButtonRGB.AutoSize = true;
            this.radioButtonRGB.Location = new System.Drawing.Point(132, 58);
            this.radioButtonRGB.Name = "radioButtonRGB";
            this.radioButtonRGB.Size = new System.Drawing.Size(47, 16);
            this.radioButtonRGB.TabIndex = 15;
            this.radioButtonRGB.Text = "RGB";
            this.radioButtonRGB.UseVisualStyleBackColor = true;
            // 
            // radioButtonHSV
            // 
            this.radioButtonHSV.AutoSize = true;
            this.radioButtonHSV.Location = new System.Drawing.Point(132, 80);
            this.radioButtonHSV.Name = "radioButtonHSV";
            this.radioButtonHSV.Size = new System.Drawing.Size(46, 16);
            this.radioButtonHSV.TabIndex = 16;
            this.radioButtonHSV.Text = "HSV";
            this.radioButtonHSV.UseVisualStyleBackColor = true;
            // 
            // buttonReplace
            // 
            this.buttonReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReplace.Location = new System.Drawing.Point(23, 34);
            this.buttonReplace.Name = "buttonReplace";
            this.buttonReplace.Size = new System.Drawing.Size(58, 23);
            this.buttonReplace.TabIndex = 17;
            this.buttonReplace.Text = "変更";
            this.buttonReplace.UseVisualStyleBackColor = true;
            this.buttonReplace.Click += new System.EventHandler(this.ButtonReplace_Click);
            // 
            // buttonGradient
            // 
            this.buttonGradient.Location = new System.Drawing.Point(25, 58);
            this.buttonGradient.Name = "buttonGradient";
            this.buttonGradient.Size = new System.Drawing.Size(95, 23);
            this.buttonGradient.TabIndex = 18;
            this.buttonGradient.Text = "Gradient";
            this.buttonGradient.UseVisualStyleBackColor = true;
            this.buttonGradient.Click += new System.EventHandler(this.ButtonGradient_Click);
            // 
            // buttonLaplacian
            // 
            this.buttonLaplacian.Location = new System.Drawing.Point(25, 29);
            this.buttonLaplacian.Name = "buttonLaplacian";
            this.buttonLaplacian.Size = new System.Drawing.Size(95, 23);
            this.buttonLaplacian.TabIndex = 19;
            this.buttonLaplacian.Text = "Laplacian";
            this.buttonLaplacian.UseVisualStyleBackColor = true;
            this.buttonLaplacian.Click += new System.EventHandler(this.ButtonLaplacian_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonLaplacian);
            this.groupBox1.Controls.Add(this.buttonSobel);
            this.groupBox1.Controls.Add(this.radioButtonHSV);
            this.groupBox1.Controls.Add(this.buttonGradient);
            this.groupBox1.Controls.Add(this.radioButtonRGB);
            this.groupBox1.Controls.Add(this.radioButtonBrightness);
            this.groupBox1.Location = new System.Drawing.Point(437, 240);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 134);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "エッジ検出";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.buttonReplace);
            this.groupBox2.Controls.Add(this.buttonReset);
            this.groupBox2.Controls.Add(this.buttonRotateClock);
            this.groupBox2.Location = new System.Drawing.Point(437, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 105);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ソース画像";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.radioButtonS);
            this.groupBox3.Controls.Add(this.radioButtonH);
            this.groupBox3.Controls.Add(this.radioButtonV);
            this.groupBox3.Controls.Add(this.buttonToGrayScale);
            this.groupBox3.Location = new System.Drawing.Point(437, 143);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(212, 91);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "グレースケール変換";
            // 
            // radioButtonS
            // 
            this.radioButtonS.AutoSize = true;
            this.radioButtonS.Location = new System.Drawing.Point(128, 49);
            this.radioButtonS.Name = "radioButtonS";
            this.radioButtonS.Size = new System.Drawing.Size(78, 16);
            this.radioButtonS.TabIndex = 24;
            this.radioButtonS.Text = "S(鮮やかさ)";
            this.radioButtonS.UseVisualStyleBackColor = true;
            // 
            // radioButtonH
            // 
            this.radioButtonH.AutoSize = true;
            this.radioButtonH.Location = new System.Drawing.Point(128, 27);
            this.radioButtonH.Name = "radioButtonH";
            this.radioButtonH.Size = new System.Drawing.Size(63, 16);
            this.radioButtonH.TabIndex = 23;
            this.radioButtonH.Text = "H(色相)";
            this.radioButtonH.UseVisualStyleBackColor = true;
            // 
            // radioButtonV
            // 
            this.radioButtonV.AutoSize = true;
            this.radioButtonV.Checked = true;
            this.radioButtonV.Location = new System.Drawing.Point(128, 68);
            this.radioButtonV.Name = "radioButtonV";
            this.radioButtonV.Size = new System.Drawing.Size(68, 16);
            this.radioButtonV.TabIndex = 23;
            this.radioButtonV.TabStop = true;
            this.radioButtonV.Text = "V(明るさ)";
            this.radioButtonV.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.numericUpDownThreshold);
            this.groupBox4.Controls.Add(this.buttonOtsu);
            this.groupBox4.Controls.Add(this.buttonToBinary);
            this.groupBox4.Location = new System.Drawing.Point(655, 240);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(168, 129);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "二値化";
            // 
            // numericUpDownThreshold
            // 
            this.numericUpDownThreshold.Location = new System.Drawing.Point(85, 103);
            this.numericUpDownThreshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownThreshold.Name = "numericUpDownThreshold";
            this.numericUpDownThreshold.Size = new System.Drawing.Size(54, 19);
            this.numericUpDownThreshold.TabIndex = 8;
            this.numericUpDownThreshold.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // buttonCopy
            // 
            this.buttonCopy.Location = new System.Drawing.Point(10, 8);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(134, 23);
            this.buttonCopy.TabIndex = 24;
            this.buttonCopy.Text = "クリップボードにコピー";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.ButtonCopy_Click);
            // 
            // buttonBackup
            // 
            this.buttonBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBackup.Location = new System.Drawing.Point(596, 429);
            this.buttonBackup.Name = "buttonBackup";
            this.buttonBackup.Size = new System.Drawing.Size(75, 23);
            this.buttonBackup.TabIndex = 27;
            this.buttonBackup.Text = "Backup";
            this.buttonBackup.UseVisualStyleBackColor = true;
            this.buttonBackup.Click += new System.EventHandler(this.ButtonBackup_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Location = new System.Drawing.Point(681, 429);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 28;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // buttonRestore
            // 
            this.buttonRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRestore.Enabled = false;
            this.buttonRestore.Location = new System.Drawing.Point(596, 458);
            this.buttonRestore.Name = "buttonRestore";
            this.buttonRestore.Size = new System.Drawing.Size(75, 23);
            this.buttonRestore.TabIndex = 29;
            this.buttonRestore.Text = "Restore";
            this.buttonRestore.UseVisualStyleBackColor = true;
            this.buttonRestore.Click += new System.EventHandler(this.ButtonRestore_Click);
            // 
            // buttonOperation
            // 
            this.buttonOperation.Enabled = false;
            this.buttonOperation.Location = new System.Drawing.Point(6, 31);
            this.buttonOperation.Name = "buttonOperation";
            this.buttonOperation.Size = new System.Drawing.Size(75, 23);
            this.buttonOperation.TabIndex = 30;
            this.buttonOperation.Text = "Operation";
            this.buttonOperation.UseVisualStyleBackColor = true;
            this.buttonOperation.Click += new System.EventHandler(this.ButtonOperation_Click);
            // 
            // radioButtonAdd
            // 
            this.radioButtonAdd.AutoSize = true;
            this.radioButtonAdd.Checked = true;
            this.radioButtonAdd.Location = new System.Drawing.Point(87, 34);
            this.radioButtonAdd.Name = "radioButtonAdd";
            this.radioButtonAdd.Size = new System.Drawing.Size(43, 16);
            this.radioButtonAdd.TabIndex = 31;
            this.radioButtonAdd.Text = "Add";
            this.radioButtonAdd.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Location = new System.Drawing.Point(437, 429);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(153, 136);
            this.listView1.TabIndex = 32;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.ListView1_SelectedIndexChanged);
            // 
            // radioButtonMul
            // 
            this.radioButtonMul.AutoSize = true;
            this.radioButtonMul.Location = new System.Drawing.Point(138, 34);
            this.radioButtonMul.Name = "radioButtonMul";
            this.radioButtonMul.Size = new System.Drawing.Size(41, 16);
            this.radioButtonMul.TabIndex = 33;
            this.radioButtonMul.Text = "Mul";
            this.radioButtonMul.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.radioButtonAdd);
            this.groupBox5.Controls.Add(this.radioButtonMul);
            this.groupBox5.Controls.Add(this.buttonOperation);
            this.groupBox5.Location = new System.Drawing.Point(596, 487);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(208, 78);
            this.groupBox5.TabIndex = 34;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Operation";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 593);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.buttonRestore);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonBackup);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonToRGB24);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonGauss);
            this.Controls.Add(this.buttonMedian);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonToGrayScale;
        private System.Windows.Forms.Button buttonToRGB24;
        private System.Windows.Forms.Button buttonToBinary;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonOtsu;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button buttonSobel;
        private System.Windows.Forms.Button buttonRotateClock;
        private System.Windows.Forms.Button buttonMedian;
        private System.Windows.Forms.Button buttonGauss;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.RadioButton radioButtonBrightness;
        private System.Windows.Forms.RadioButton radioButtonRGB;
        private System.Windows.Forms.RadioButton radioButtonHSV;
        private System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.Button buttonGradient;
        private System.Windows.Forms.Button buttonLaplacian;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButtonS;
        private System.Windows.Forms.RadioButton radioButtonH;
        private System.Windows.Forms.RadioButton radioButtonV;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown numericUpDownThreshold;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Button buttonBackup;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonRestore;
        private System.Windows.Forms.Button buttonOperation;
        private System.Windows.Forms.RadioButton radioButtonAdd;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.RadioButton radioButtonMul;
        private System.Windows.Forms.GroupBox groupBox5;
    }
}

