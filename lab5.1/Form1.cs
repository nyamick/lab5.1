namespace lab5._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);
            g.DrawRectangle(new Pen(Color.Red,2), 200, 100, 50, 30);
            g.FillRectangle(new SolidBrush(Color.Yellow), 200, 100, 50, 30);
        }
    }
}