using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace Fuel_calculator_for_carpool
{
    public partial class Form_add : Form
    {
        public List<Vehicle> carpool_depots = new List<Vehicle>();
        
        public Form_add()
        {
            InitializeComponent();
        }
        private void comboBox_fuel_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_fuel_subtype.Items.Clear();
            comboBox_fuel_subtype.Enabled = true;
            if (comboBox_fuel_type.SelectedItem != null)
            {
                string selectedType = comboBox_fuel_type.SelectedItem.ToString();
                switch (selectedType)
                {
                    case "���":
                        comboBox_fuel_subtype.Items.AddRange(new string[] { "�����", "�������" });
                        break;
                    case "������":
                        comboBox_fuel_subtype.Items.AddRange(new string[] { "������", "������" });
                        break;
                    case "������":
                        comboBox_fuel_subtype.Items.AddRange(new string[] { "��-92", "��-95", "��-98", "��-76", "��-80" });
                        break;
                    default:
                        MessageBox.Show("����������, �������� ��� �������.");
                        break;
                }

            }
        }
        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void button_add_Vehicle_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_fuel_consumption.Text, out int fuelConsumption) &&
    int.TryParse(textBox_AverageSpeed.Text, out int averageSpeed))
            {
                Vehicle vehicle = new Vehicle(textBox_name.Text, comboBox_purpose.SelectedItem.ToString(),
                        comboBox_fuel_type.SelectedItem.ToString(), comboBox_fuel_subtype.SelectedItem.ToString(),
                        fuelConsumption, averageSpeed);
                carpool_depots.Add(vehicle);
                listBox_carpool_depots.Items.Add(vehicle.Name);
                ClearInputs();
            }
            else
            {
                MessageBox.Show("��������� �� ��� ����!!", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show("����������, ������� ���������� �������� �������� ��� ������� � ��������.", 
                    "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ClearInputs()
        {
            textBox_name.Clear();
            textBox_fuel_consumption.Clear();
            textBox_AverageSpeed.Clear();
            comboBox_fuel_type.SelectedIndex = -1;
            comboBox_fuel_subtype.Items.Clear();
            comboBox_purpose.SelectedIndex = -1;
        }

        private void listBox_carpool_depots_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_carpool_depots.SelectedItem != null)
            {
                string selectedVehicleName = listBox_carpool_depots.SelectedItem.ToString();

                Vehicle selectedVehicle = carpool_depots.FirstOrDefault(v => v.Name == selectedVehicleName);

                if (selectedVehicle != null)
                {
                    string message = $"ID: {selectedVehicle.Id}\n" +
                                     $"���: {selectedVehicle.Name}\n" +
                                     $"����������: {selectedVehicle.Purpose}\n" +
                                     $"��� �������: {selectedVehicle.FuelType}\n" +
                                     $"������: {selectedVehicle.FuelSubType}\n" +
                                     $"������: {selectedVehicle.FuelConsumption} �/100 ��\n" +
                                     $"��������: {selectedVehicle.AverageSpeed} ��/�";

                    MessageBox.Show(message, "��������� ������������� ��������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button_calculate_Click(object sender, EventArgs e)
        {
            
            Form_calculate fc = new Form_calculate(this);
            //PrintAllVehicles(carpool_depots);
            fc.Owner = this;
            fc.ShowDialog();
        }
        public void PrintAllVehicles(List<Vehicle> carpool_depots)
        {
            if (carpool_depots.Count == 0)
            {
                MessageBox.Show("��� ��������� ������������ �������.", "����������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            StringBuilder sb = new StringBuilder();
            foreach (var vehicle in carpool_depots)
            {
                sb.AppendLine($"{vehicle.Name} - {vehicle.Purpose} - {vehicle.FuelType}");
            }

            MessageBox.Show(sb.ToString(), "������ ������������ �������", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    public class Vehicle
    {
        public static int NextId = 1;
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Purpose { get; set; }
        public string FuelType { get; set; }
        public string FuelSubType { get; set; }
        public int FuelConsumption { get; set; }
        public int AverageSpeed { get; set; }

        public Vehicle(string name, string purpose, string fuelType, string fuelSubType, int fuelConsumption, int averageSpeed)
        {
            Id = NextId++;
            Name = name;
            Purpose = purpose;
            FuelType = fuelType;
            FuelSubType = fuelSubType;
            FuelConsumption = fuelConsumption;
            AverageSpeed = averageSpeed;
        }
        public string GetFuelType()
        {
            string fuel = "��� �������: " + this.FuelType + "\n������: " +this.FuelSubType;
            return fuel;
        }
    }
}