using System;
using System.IO;
using System.Windows.Forms;

namespace ImageViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void loadFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            var res = dialog.ShowDialog();

            if (res == DialogResult.OK)
            {
                var path = dialog.SelectedPath;
                string[] files = Directory.GetFiles(path);

                // Clear existing controls
                flowLayoutPanel1.Controls.Clear();

                foreach (var file in files)
                {
                    string extension = Path.GetExtension(file).ToLower();
                    if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                    {
                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Cursor = Cursors.Hand;
                        pictureBox.Width = 100;
                        pictureBox.Height = 100;
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox.Tag = file;
                        pictureBox.Click += PictureBox_Click;

                        try
                        {
                            pictureBox.Load(file);
                            flowLayoutPanel1.Controls.Add(pictureBox);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error loading image {file}: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {
                string imgPath = pictureBox.Tag.ToString();
                try
                {
                    pictureBox1.Image?.Dispose(); // Clean up previous image
                    pictureBox1.Load(imgPath);
                    label1.Text = "File " + imgPath + " is loaded.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading selected image: {ex.Message}");
                }
            }
        }
    }
}