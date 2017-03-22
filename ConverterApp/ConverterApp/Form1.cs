using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConverterApp
    // This program was written by Gail Mosdell and edited by John Robley
    // It forms the base of a converter program for the OS-Assessment Two for Cert IV
    // Date : 22/03/2017
{
    public partial class frm_Main : Form
    {
        // Set true if valid number is entered
        private bool IsNumber;
        // Next possition to store CurrentConversion in array
        private int CurrentConversionLocation = 0;
        // Array to store conversions
        private ConversionStore[] LastFiveConversions = new ConversionStore[5];
        // Class to store type of conversion and UnitOfMeasure
        private class ConversionStore
        {
            public int mode;
            public double value;
        }
        

        public frm_Main()
        {
            InitializeComponent();
        }

        // Checks for valid number in textbox
        private double ValidateUnitOfMeasureTxtBoxInput()
        {
            double value;
            if (!double.TryParse(txt_UnitOfMeasure.Text, out value))
            {
                MessageBox.Show("A numeric must be entered. Please re-enter the value.");
                txt_UnitOfMeasure.Clear();
                txt_UnitOfMeasure.Focus();
                // sets flag
                IsNumber = false;
                return 0;
            }
            else
            {
                // sets flag
                IsNumber = true;
                // returns number from textbox
                return value;
            }
        }

        // Recives a number converts and returns number
        private double ConvertInput(int mode, double value)
        {
            switch (mode)
            {
                case 0:
                    // Convert CM to Inches
                    return value * 0.3937;

                case 1:
                    // Convert Metres to Feet
                    return value * 3.28084;
                case 2:
                    // Convert Celsius to Fahrenheit
                    return (value * 9) / 5 + 32;
                case 3:
                    // Convert Centimetres to Feet
                    return value * 0.0328084;
                case 4:
                    // Convert Kilometres to Miles
                    return value * 0.621371;
                default:
                    return 0;
            }
        }

        // Stores conversion in array and displays current conversion
        private void StoreAndDisplayConversion(int mode)
        {
            // Gets number from textbox
            double value = ValidateUnitOfMeasureTxtBoxInput();
            // Checks for valid number
            if (IsNumber)
            {
                // Adds conversion to array
                LastFiveConversions[CurrentConversionLocation] = new ConversionStore();
                LastFiveConversions[CurrentConversionLocation].mode = mode;
                LastFiveConversions[CurrentConversionLocation].value = value;
                // Displays current conversion
                DisplayCurrentConversion(mode, value, false);
                // Loops through array positions
                if (CurrentConversionLocation < 4)
                {
                    CurrentConversionLocation++;
                }else
                {
                    // Enables Show_Last_Five_Conversions button when array is full
                    btn_Show_Last_Five_Conversions.Enabled = true;
                    CurrentConversionLocation = 0;
                }

            }
            else
            {
                // Hide Current Convertion
                DisplayCurrentConversion(0, 0, true);
            }
            
        }

        // Display current conversion
        private void DisplayCurrentConversion(int mode, double value, bool IsHidden )
        {
            if (!IsHidden)
            {
                // Display current conversion
                txt_Convert.Visible = true;
                txt_Convert.Text = ConvertInput(mode, value).ToString();
                switch (mode)
                {
                    case 0:
                        // Display CM to Inches                        
                        lbl_Display.Text = value + " centimetres is converted to";
                        lbl_Convert.Text = "inches.";
                        break;

                    case 1:
                        // Display Metres to Feet
                        lbl_Display.Text = value + " metres is converted to";
                        lbl_Convert.Text = "feet.";
                        break;
                    case 2:
                        // Display Celsius to Fahrenheit
                        lbl_Display.Text = value + " degrees celsius is converted to";
                        lbl_Convert.Text = "degrees Fahrenheit.";
                        break;
                    case 3:
                        // Display Centimetres to Feet
                        lbl_Display.Text = value + " centimetres is converted to";
                        lbl_Convert.Text = "Feet.";
                        break;
                    case 4:
                        // Display Kilometres to Miles
                        lbl_Display.Text = value + " kilometres is converted to";
                        lbl_Convert.Text = "Miles.";
                        break;
                    default:
                        break;
                }
            }else
            {
                // Clear and hide current conversion                       
                txt_Convert.Clear();
                txt_Convert.Visible = false;
                lbl_Convert.Text = "";
                lbl_Display.Text = "";
            }
        }

        // Displays LastFiveConversions
        private void FillListBox()
        {
            // Clears ListBox
            Conversions_ListBox.Items.Clear();
            // Loops through array
            for (int i = 0; i < 5; i++)
            {
                // Converts and Adds numbers to listbox
                switch (LastFiveConversions[i].mode)
                {
                    case 0:
                        // Display CM to Inches                        
                        Conversions_ListBox.Items.Add( LastFiveConversions[i].value + " centimetres is converted to " + ConvertInput(LastFiveConversions[i].mode, LastFiveConversions[i].value) + " inches.");
                        break;

                    case 1:
                        // Display Metres to Feet
                        Conversions_ListBox.Items.Add(LastFiveConversions[i].value + " metres is converted to " + ConvertInput(LastFiveConversions[i].mode, LastFiveConversions[i].value) + " feet.");
                        break;
                    case 2:
                        // Display Celsius to Fahrenheit
                        Conversions_ListBox.Items.Add(LastFiveConversions[i].value + " degrees celsius is converted to " + ConvertInput(LastFiveConversions[i].mode, LastFiveConversions[i].value) + " degrees Fahrenheit.");
                        break;
                    case 3:
                        // Display Centimetres to Feet
                        Conversions_ListBox.Items.Add(LastFiveConversions[i].value + " centimetres is converted to " + ConvertInput(LastFiveConversions[i].mode, LastFiveConversions[i].value) + " Feet.");
                        break;
                    case 4:
                        // Display Kilometres to Miles
                        Conversions_ListBox.Items.Add(LastFiveConversions[i].value + " kilometres is converted to " + ConvertInput(LastFiveConversions[i].mode, LastFiveConversions[i].value) + " Miles.");
                        break;
                    default:
                        break;
                }
            }
        }



        // Button click events

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_CM_to_Inches_Click(object sender, EventArgs e)
        {
            StoreAndDisplayConversion(0);
        }

        private void btn_M_to_Feet_Click(object sender, EventArgs e)
        {
            StoreAndDisplayConversion(1);
        }

        private void btn_Celsius_to_Fahrenheit_Click(object sender, EventArgs e)
        {
            StoreAndDisplayConversion(2);
        }

        private void btn_CM_to_Feet_Click(object sender, EventArgs e)
        {
            StoreAndDisplayConversion(3);
        }

        private void btn_KM_to_Miles_Click(object sender, EventArgs e)
        {
            StoreAndDisplayConversion(4);
        }

        private void btn_Show_Last_Five_Conversions_Click(object sender, EventArgs e)
        {
            FillListBox();
        }
    }
}
