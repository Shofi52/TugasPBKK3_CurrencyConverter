using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CurrencyConverter
{
    public partial class Form1 : Form
    {
        private List<Currency> currencies;
        public Form1()
        {
            InitializeComponent();
            InitializeCurrency();
            SetUpComponent();
            Ganti();
        }

        private void CreateNewCurrency(string name, float rate)
        {
            currencies.Add(new Currency(name, rate));
        }
        private void InitializeCurrency()
        {
            // https://www.xe.com/
            currencies = new List<Currency>();
            CreateNewCurrency("IDR", 1.0f);
            CreateNewCurrency("AUD", 0.000090837f);
            CreateNewCurrency("CNY", 0.00045323f);
            CreateNewCurrency("EUR", 0.000058798f);
            CreateNewCurrency("KRW", 0.078216f);
            CreateNewCurrency("MYR", 0.00028620f);
            CreateNewCurrency("PHP", 0.0033467f);
            CreateNewCurrency("USD", 0.000068961f);  
        }

        private void SetUpComponent()
        {
            foreach (Currency currency in currencies)
            {
                comboBox1.Items.Add(currency.currencyCode);
                comboBox2.Items.Add(currency.currencyCode);
            }

            input.Minimum = 0;
            input.Maximum = decimal.MaxValue;
            input.DecimalPlaces = 2;

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;

            input.ValueChanged += input_ValueChanged;
        }

        private Currency GetCurrencyByName(string name)
        {
            for (int i = 0; i < currencies.Count; i++)
            {
                if (currencies[i].currencyCode == name)
                {
                    return currencies[i];
                }
            }
            return null;
        }

        private void Ganti()
        {
            float money = (float)input.Value;
            Currency inCurrency = GetCurrencyByName(comboBox1.Text);
            Currency outCurrency = GetCurrencyByName(comboBox2.Text);
           
            if (outCurrency != null)
           {
                float output = inCurrency.ConvertTo(money, outCurrency);
                string hasil = output.ToString();
                textBox2.Text = hasil;
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ganti();
        }

        private void input_TextChanged(object sender, EventArgs e)
        {
            Ganti();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ganti();
        }

        private void input_ValueChanged(object sender, EventArgs e)
        {

        }
    }
    class Currency
    {
        public string currencyCode { get; }
        private float rate;

        public Currency(string name, float rate)
        {
            this.currencyCode = name;
            this.rate = rate;
        }

        public float ConvertTo(float money, Currency currency)
        {
            return money / rate * currency.rate;
        }
    }

}
