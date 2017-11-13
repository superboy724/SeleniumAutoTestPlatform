using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeleniumAutoTestClient
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
        }

        public static LoadingForm GetLoad()
        {
            var loadForm = new LoadingForm();
            return loadForm;
        }
    }
}
