namespace WindowsFormsApp
{
    public class RankListItem : UserControl
    {
        private PictureBox pbPlayerImage = null!;
        private Label lblPlayerName = null!;
        private Label lblCount = null!;
        private PlayerImageManager imageManager = null!;

        public override string Text => lblPlayerName.Text;
        public int Count { get; private set; }

        public RankListItem(string playerName, int count)
        {
            InitializeComponent();
            imageManager = new PlayerImageManager();
            SetData(playerName, count);
            LoadPlayerImage(playerName);
        }

        private void InitializeComponent()
        {
            pbPlayerImage = new PictureBox();
            lblPlayerName = new Label();
            lblCount = new Label();
            ((System.ComponentModel.ISupportInitialize)pbPlayerImage).BeginInit();
            SuspendLayout();

            // pbPlayerImage
 
            pbPlayerImage.BorderStyle = BorderStyle.FixedSingle;
            pbPlayerImage.Location = new Point(5, 5);
            pbPlayerImage.Name = "pbPlayerImage";
            pbPlayerImage.Size = new Size(80, 80);
            pbPlayerImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbPlayerImage.TabIndex = 0;
            pbPlayerImage.TabStop = false;

            // lblPlayerName

            lblPlayerName.AutoSize = true;
            lblPlayerName.Font = new Font("Segoe UI", 10F);
            lblPlayerName.Location = new Point(90, 5);
            lblPlayerName.MaximumSize = new Size(200, 0);
            lblPlayerName.Name = "lblPlayerName";
            lblPlayerName.Size = new Size(0, 19);
            lblPlayerName.TabIndex = 1;

            // lblCount

            lblCount.AutoSize = true;
            lblCount.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCount.Location = new Point(90, 30);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(0, 15);
            lblCount.TabIndex = 2;

            // RankListItem

            BackColor = Color.White;
            Controls.Add(pbPlayerImage);
            Controls.Add(lblPlayerName);
            Controls.Add(lblCount);
            Name = "RankListItem";
            Size = new Size(300, 90);
            ((System.ComponentModel.ISupportInitialize)pbPlayerImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        public void SetData(string playerName, int count)
        {
            if (string.IsNullOrEmpty(playerName)) return;

            this.lblPlayerName.Text = playerName;
            this.lblCount.Text = count.ToString("N0");
            this.Count = count;
            LoadPlayerImage(playerName);
        }

        private void LoadPlayerImage(string playerName)
        {
            try
            {
                if (string.IsNullOrEmpty(playerName)) return;

                var image = imageManager.LoadPlayerImage(playerName);
                if (image != null)
                {
                    if (pbPlayerImage.Image != null)
                    {
                        var oldImage = pbPlayerImage.Image;
                        pbPlayerImage.Image = image;
                        oldImage.Dispose();
                    }
                    else
                    {
                        pbPlayerImage.Image = image;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (pbPlayerImage.Image != null)
                {
                    pbPlayerImage.Image.Dispose();
                }
                pbPlayerImage.Dispose();
                lblPlayerName.Dispose();
                lblCount.Dispose();
            }
            base.Dispose(disposing);
        }
    }
} 