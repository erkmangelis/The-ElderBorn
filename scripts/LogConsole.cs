namespace ElderBorn
{
    public class LogConsole : Form
    {
        private TextBox logs;

        public LogConsole()
        {
            InitializeComponent();
            this.Show();
        }

        private void InitializeComponent()
        {
            this.logs = new TextBox();
            this.SuspendLayout();
            
            // Log Textbox
            this.logs.Multiline = true;
            this.logs.ScrollBars = ScrollBars.Vertical;
            this.logs.Dock = DockStyle.Fill;
            this.logs.ReadOnly = true;
            
            // LogConsole Window
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Controls.Add(this.logs);
            this.Name = "LogConsole";
            this.Text = "The ElderBorn - Log Console";
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            this.ResumeLayout(false);
            this.PerformLayout();
        }


        public void logMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => logMessage(message)));
                return;
            }
            logs.AppendText(message + Environment.NewLine);
        }
    }
}