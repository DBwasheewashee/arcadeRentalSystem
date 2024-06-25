namespace rentalSystemMain
{
    partial class invoiceTransaction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(invoiceTransaction));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panelPrint = new System.Windows.Forms.Panel();
            this.rentDays = new System.Windows.Forms.Label();
            this.rentalEnd = new System.Windows.Forms.Label();
            this.total = new System.Windows.Forms.Label();
            this.customerPayment = new System.Windows.Forms.Label();
            this.discount = new System.Windows.Forms.Label();
            this.subtotal = new System.Windows.Forms.Label();
            this.depositAmount = new System.Windows.Forms.Label();
            this.employeeName = new System.Windows.Forms.Label();
            this.customerEmail = new System.Windows.Forms.Label();
            this.customerPhone = new System.Windows.Forms.Label();
            this.customerAddress = new System.Windows.Forms.Label();
            this.customerName = new System.Windows.Forms.Label();
            this.rentalStart = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.listOfGamesToRent = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelPrint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(30, 559);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(134, 559);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Submit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panelPrint
            // 
            this.panelPrint.Controls.Add(this.pictureBox1);
            this.panelPrint.Controls.Add(this.rentDays);
            this.panelPrint.Controls.Add(this.rentalEnd);
            this.panelPrint.Controls.Add(this.total);
            this.panelPrint.Controls.Add(this.customerPayment);
            this.panelPrint.Controls.Add(this.discount);
            this.panelPrint.Controls.Add(this.subtotal);
            this.panelPrint.Controls.Add(this.depositAmount);
            this.panelPrint.Controls.Add(this.employeeName);
            this.panelPrint.Controls.Add(this.customerEmail);
            this.panelPrint.Controls.Add(this.customerPhone);
            this.panelPrint.Controls.Add(this.customerAddress);
            this.panelPrint.Controls.Add(this.customerName);
            this.panelPrint.Controls.Add(this.rentalStart);
            this.panelPrint.Controls.Add(this.label17);
            this.panelPrint.Controls.Add(this.label12);
            this.panelPrint.Controls.Add(this.label10);
            this.panelPrint.Controls.Add(this.label9);
            this.panelPrint.Controls.Add(this.label6);
            this.panelPrint.Controls.Add(this.label8);
            this.panelPrint.Controls.Add(this.listOfGamesToRent);
            this.panelPrint.Controls.Add(this.label7);
            this.panelPrint.Controls.Add(this.label16);
            this.panelPrint.Controls.Add(this.label13);
            this.panelPrint.Controls.Add(this.label2);
            this.panelPrint.Controls.Add(this.panel1);
            this.panelPrint.Controls.Add(this.label15);
            this.panelPrint.Controls.Add(this.label1);
            this.panelPrint.Controls.Add(this.label14);
            this.panelPrint.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPrint.Location = new System.Drawing.Point(0, 0);
            this.panelPrint.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelPrint.Name = "panelPrint";
            this.panelPrint.Size = new System.Drawing.Size(784, 553);
            this.panelPrint.TabIndex = 19;
            // 
            // rentDays
            // 
            this.rentDays.AutoSize = true;
            this.rentDays.Location = new System.Drawing.Point(112, 92);
            this.rentDays.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.rentDays.Name = "rentDays";
            this.rentDays.Size = new System.Drawing.Size(13, 13);
            this.rentDays.TabIndex = 31;
            this.rentDays.Text = "?";
            // 
            // rentalEnd
            // 
            this.rentalEnd.AutoSize = true;
            this.rentalEnd.Location = new System.Drawing.Point(112, 67);
            this.rentalEnd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.rentalEnd.Name = "rentalEnd";
            this.rentalEnd.Size = new System.Drawing.Size(13, 13);
            this.rentalEnd.TabIndex = 31;
            this.rentalEnd.Text = "?";
            // 
            // total
            // 
            this.total.AutoSize = true;
            this.total.Location = new System.Drawing.Point(663, 521);
            this.total.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.total.Name = "total";
            this.total.Size = new System.Drawing.Size(13, 13);
            this.total.TabIndex = 31;
            this.total.Text = "?";
            // 
            // customerPayment
            // 
            this.customerPayment.AutoSize = true;
            this.customerPayment.Location = new System.Drawing.Point(663, 470);
            this.customerPayment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.customerPayment.Name = "customerPayment";
            this.customerPayment.Size = new System.Drawing.Size(13, 13);
            this.customerPayment.TabIndex = 31;
            this.customerPayment.Text = "?";
            // 
            // discount
            // 
            this.discount.AutoSize = true;
            this.discount.Location = new System.Drawing.Point(663, 443);
            this.discount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.discount.Name = "discount";
            this.discount.Size = new System.Drawing.Size(13, 13);
            this.discount.TabIndex = 31;
            this.discount.Text = "?";
            // 
            // subtotal
            // 
            this.subtotal.AutoSize = true;
            this.subtotal.Location = new System.Drawing.Point(900, 454);
            this.subtotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.subtotal.Name = "subtotal";
            this.subtotal.Size = new System.Drawing.Size(13, 13);
            this.subtotal.TabIndex = 31;
            this.subtotal.Text = "?";
            // 
            // depositAmount
            // 
            this.depositAmount.AutoSize = true;
            this.depositAmount.Location = new System.Drawing.Point(424, 443);
            this.depositAmount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.depositAmount.Name = "depositAmount";
            this.depositAmount.Size = new System.Drawing.Size(13, 13);
            this.depositAmount.TabIndex = 31;
            this.depositAmount.Text = "?";
            // 
            // employeeName
            // 
            this.employeeName.AutoSize = true;
            this.employeeName.Location = new System.Drawing.Point(145, 443);
            this.employeeName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.employeeName.Name = "employeeName";
            this.employeeName.Size = new System.Drawing.Size(13, 13);
            this.employeeName.TabIndex = 31;
            this.employeeName.Text = "?";
            // 
            // customerEmail
            // 
            this.customerEmail.AutoSize = true;
            this.customerEmail.Location = new System.Drawing.Point(336, 92);
            this.customerEmail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.customerEmail.Name = "customerEmail";
            this.customerEmail.Size = new System.Drawing.Size(13, 13);
            this.customerEmail.TabIndex = 31;
            this.customerEmail.Text = "?";
            // 
            // customerPhone
            // 
            this.customerPhone.AutoSize = true;
            this.customerPhone.Location = new System.Drawing.Point(336, 67);
            this.customerPhone.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.customerPhone.Name = "customerPhone";
            this.customerPhone.Size = new System.Drawing.Size(13, 13);
            this.customerPhone.TabIndex = 31;
            this.customerPhone.Text = "?";
            // 
            // customerAddress
            // 
            this.customerAddress.Location = new System.Drawing.Point(502, 39);
            this.customerAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.customerAddress.Name = "customerAddress";
            this.customerAddress.Size = new System.Drawing.Size(128, 69);
            this.customerAddress.TabIndex = 31;
            this.customerAddress.Text = "?";
            // 
            // customerName
            // 
            this.customerName.AutoSize = true;
            this.customerName.Location = new System.Drawing.Point(336, 36);
            this.customerName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.customerName.Name = "customerName";
            this.customerName.Size = new System.Drawing.Size(13, 13);
            this.customerName.TabIndex = 31;
            this.customerName.Text = "?";
            // 
            // rentalStart
            // 
            this.rentalStart.AutoSize = true;
            this.rentalStart.Location = new System.Drawing.Point(112, 36);
            this.rentalStart.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.rentalStart.Name = "rentalStart";
            this.rentalStart.Size = new System.Drawing.Size(13, 13);
            this.rentalStart.TabIndex = 31;
            this.rentalStart.Text = "?";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(584, 521);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(46, 13);
            this.label17.TabIndex = 30;
            this.label17.Text = "Balance";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(584, 470);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(42, 13);
            this.label12.TabIndex = 29;
            this.label12.Text = "TOTAL";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(584, 443);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "Discounts";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(820, 454);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Subtotal";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(372, 443);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Deposit";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(60, 443);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Transact by: ";
            // 
            // listOfGamesToRent
            // 
            this.listOfGamesToRent.FormattingEnabled = true;
            this.listOfGamesToRent.Location = new System.Drawing.Point(30, 142);
            this.listOfGamesToRent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listOfGamesToRent.Name = "listOfGamesToRent";
            this.listOfGamesToRent.Size = new System.Drawing.Size(725, 277);
            this.listOfGamesToRent.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(502, 26);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Address";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(268, 92);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(32, 13);
            this.label16.TabIndex = 22;
            this.label16.Text = "Email";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(268, 67);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "Phone";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(268, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Name";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Location = new System.Drawing.Point(30, 119);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(724, 18);
            this.panel1.TabIndex = 19;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(28, 92);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 13);
            this.label15.TabIndex = 3;
            this.label15.Text = "Days";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Rental End";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(27, 36);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Invoice Date";
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::rentalSystemMain.Properties.Resources.logo21;
            this.pictureBox1.Location = new System.Drawing.Point(654, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 93);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 32;
            this.pictureBox1.TabStop = false;
            // 
            // invoiceTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 592);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panelPrint);
            this.Name = "invoiceTransaction";
            this.Text = "invoiceTransaction";
            this.panelPrint.ResumeLayout(false);
            this.panelPrint.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panelPrint;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listOfGamesToRent;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label rentDays;
        private System.Windows.Forms.Label rentalEnd;
        private System.Windows.Forms.Label rentalStart;
        private System.Windows.Forms.Label customerEmail;
        private System.Windows.Forms.Label customerPhone;
        private System.Windows.Forms.Label customerAddress;
        private System.Windows.Forms.Label customerName;
        private System.Windows.Forms.Label total;
        private System.Windows.Forms.Label customerPayment;
        private System.Windows.Forms.Label discount;
        private System.Windows.Forms.Label depositAmount;
        private System.Windows.Forms.Label employeeName;
        private System.Windows.Forms.Label subtotal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}