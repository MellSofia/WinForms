namespace WinForms_26_09_204
{
    public partial class Form1 : Form
    {
        private string letters = $"�, �, �, �, �, �, �, �, �, �, �, �, �, �, �, �, �, �, �, �, �, �, �, �, �, �, �, �, �, �, �, �, �, A, B, C, D, F, G, H, J, K, L, M, N, P, Q, R, S, T, V, W, X, Z,1,2,3,4,5,6,7,8,9".ToLower(); public Form1()
        {
            InitializeComponent();
            Binding bind = new Binding("Text", textBox1, "Text"); Binding bind2 = new Binding("Text", textBox2, "Text");
            bind.Format += (sender, e) => {
                foreach (char l in e.Value.ToString().ToLower())
                {
                    if (!letters.Contains(l))
                    {
                        label4.Text = "����� �������� ����������� �������!";
                    }
                    else
                    {
                        if (e.Value.ToString().Length < 6)
                        {
                            label4.Text = "����� ������ ���� �� 6 ��������!";
                        }
                        else
                        {
                            label4.Text = "��� �������!";
                        }
                    }
                }
            };
            bind2.Format += (sender, e) => {
                foreach (char l in e.Value.ToString().ToLower())
                {
                    if (!letters.Contains(l))
                    {
                        label3.Text = "����� �������� ����������� �������!";
                    }
                    else
                    {
                        if (e.Value.ToString().Length < 6)
                        {
                            label3.Text = "����� ������ ���� �� 6 ��������!";
                        }
                        else
                        {
                            label3.Text = "��� �������!";
                        }
                    }
                }
            };
            textBox1.DataBindings.Add(bind); textBox2.DataBindings.Add(bind2);
        }
    }
}
