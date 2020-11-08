﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EduWatch.Presenters;

namespace EduWatch.Views
{
    public partial class LoginForm : Form, ILoginView
    {
        public string Username => userNameTextBox.Text;
        public string Pass => passwordTextBox.Text;

        public LoginPresenter Presenter { private get;  set; }

        public LoginForm()
        {
            InitializeComponent();
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void Message(string msg, string caption, MessageIcon msgIcon, MessageButton msgBtn)
        {
            MessageBox.Show(
                msg, 
                caption, 
                (MessageBoxButtons)Enum.Parse(typeof(MessageBoxButtons), msgBtn.ToString()),
                (MessageBoxIcon)Enum.Parse(typeof(MessageBoxIcon), msgIcon.ToString())
                );
        }


    }
}