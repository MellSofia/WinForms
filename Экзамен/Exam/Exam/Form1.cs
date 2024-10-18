using System;
using System.Globalization;

namespace Exam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.AcceptButton = button_culc;

        }
        public void Validate_textBox(TextBox textBox)
        {
            string num = textBox.Text.Replace(" ", "");
            num = num.Replace('.', ',');
            bool ex = true;
            if (string.IsNullOrEmpty(num) || num == "," || num == "-," || num == "." || num == "-.")
            {
                MessageBox.Show("������ �����: ������ ������ �������� �� ���� � ����� ���� �������������.");
                textBox.Text = string.Empty; // ������� ���� ������ ����� ��������������
                return; // ������� �� ������
            }

            // ���������, �������� �� ������ �������� ������
            if (!double.TryParse(num, out _) && num != "-")
            {
                MessageBox.Show("������ �����: ������ ������ �������� �� ���� � ����� ���� �������������.");
                textBox.Text = string.Empty; // ������� ���� ������ ����� ��������������
            }
        }
        private void button_culc_Click(object sender, EventArgs e)
        {
            label_result.Text = "=";
            string input1 = textBox_part_1.Text.Replace('.', ',');
            string input2 = textBox_part_2.Text.Replace('.', ',');
            string input3 = textBox_part_3.Text.Replace('.', ',');

            if (!double.TryParse(input1, out double num1) ||
                !double.TryParse(input2, out double num2) ||
                !double.TryParse(input3, out double num3))
            {
                MessageBox.Show("������ �����: ���������, ��� ��� ����� ������� ���������.");
                return;
            }

            string operation1 = comboBox_operat_1.SelectedItem?.ToString();
            string operation2 = comboBox_operat_2.SelectedItem?.ToString();

            if (operation1 == null || operation2 == null)
            {
                MessageBox.Show("����������, �������� �������� ��� ����������.");
                return;
            }

            double result;
            if (operation1 == "*" || operation1 == "/")
            {
                result = PerformOperation(num1, num2, operation1);
                result = PerformOperation(result, num3, operation2);
            }
            else
            {
                result = PerformOperation(num2, num3, operation2);
                result = PerformOperation(num1, result, operation1);
            }
            result = Math.Round(result, 2);

            label_result.Text = $"���������: {result}";
        }

        private double PerformOperation(double num1, double num2, string operation)
        {
            switch (operation)
            {
                case "+":
                    return num1 + num2;
                case "-":
                    return num1 - num2;
                case "*":
                    return num1 * num2;
                case "/":
                    if (num2 == 0)
                    {
                        MessageBox.Show("������� �� ���� ����������.");
                        return 0;
                    }
                    if (num1 == 0)
                    {
                        MessageBox.Show("��������� ���� ����������.");
                        return 0;
                    }
                    else
                    {
                        return num1 / num2;
                    }
                default:
                    MessageBox.Show("����������� ��������.");
                    return 0;
            }
        }


        private void textBox_part_1_TextChanged(object sender, EventArgs e)
        {
            Validate_textBox(textBox_part_1);
        }

        private void textBox_part_2_TextChanged(object sender, EventArgs e)
        {
            Validate_textBox(textBox_part_2);
        }

        private void textBox_part_3_TextChanged(object sender, EventArgs e)
        {
            Validate_textBox(textBox_part_3);
        }

        private void button_instr_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\n ����������� ��� ���� ��� ����� ����� � ��� ���� ��� ������ ��������. " +
                            "\n ������� '���������', ����� �������� ���������.");
        }
    }
}
