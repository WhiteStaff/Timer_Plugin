﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Timer_Plugin.Timer_Window.Data;
using Timer_Plugin.Timer_Window;


namespace Timer_Plugin
{
    public partial class Time_Form : Form
    {
        private DateTime current_datetime = new DateTime();
        public static bool IsFormOpen = false;
        public Time_Form()
        {

            InitializeComponent();
            
            IsFormOpen = true;
            if (TimerWindowPackage.IsSolutionOpened)
            {

                label1.Visible = true;
                label2.Text = "Проведено времени в Visual Studio сегодня:";
                int current_time = TimerWindowPackage.time + TimerWindowPackage.s.Elapsed.Seconds;
                current_datetime = Converter.TimeConverterToDate(current_time);
                label1.Text = current_datetime.ToString("HH:mm:ss");
                Timer timer = new Timer();
                timer.Interval = 1000;
                timer.Enabled = true;
                timer.Tick += new EventHandler(write_tick);
            }
            else
            {
                label2.Text = "Для отслеживания времени необходимо войти в проект";
                label1.Visible = false;
            } 
        }

        private void write_tick(object sender, EventArgs e)
        {
            current_datetime = current_datetime.AddSeconds(1);
            label1.Text = current_datetime.ToString("HH:mm:ss");
        }

        private void Exit_button_Click(object sender, EventArgs e)
        {
            IsFormOpen = false;
            Close();
        }

        private void Data_button_Click(object sender, EventArgs e)
        {

        }

        
        private void Time_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsFormOpen = false;
        }
    }
}