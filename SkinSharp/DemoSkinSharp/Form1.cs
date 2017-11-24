using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using SkinSharp;

namespace DemoSkinSharp
{
    public partial class Form1 : Form
    {
        public SkinH_Net skin;
        public Form1()
        {
            skin = new SkinH_Net();
            skin.Attach();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "www.Test404.com";
            Console.WriteLine("www.Test404.com");
        }

        private void label2_Click(object sender, EventArgs e)
        {
           
        }

        private void textBoxPass_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void textBoxPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)//触发回车
            {
                ListViewItem item = new ListViewItem();
                //加入第一列的文本
                item.Text = listView1.Items.Count.ToString();//第一列
                //加入第二列的文本
                item.SubItems.Add(textBoxUser.Text);//第二列
                //加入第三列的问题
                item.SubItems.Add(textBoxPass.Text);//第三列
                //加入一整行
                listView1.Items.Add(item);
            } 
        }
    }
}
