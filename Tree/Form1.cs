using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Tree
{
    public partial class Form1 : Form
    {
        string htmlStartCode;
        string htmlCode;
        string htmlEndCode;
        StreamWriter sw = new StreamWriter("table.html");

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            TreeNode node = new TreeNode(textBox1.Text);
            try
            {
                treeView1.SelectedNode.Nodes.Add(node);
            }
            catch 
            { 
            treeView1.Nodes.Add(node);
            }
            
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
             treeView1.SelectedNode.Text=textBox1.Text ;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            textBox1.Text = treeView1.SelectedNode.Text;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            treeView1.SelectedNode.Remove();
        }

        private void buttonGenateTable_Click(object sender, EventArgs e)
        {
            //обнуляем
            webBrowser1.DocumentText = null;
            htmlCode = null;
            //выводим начало
            htmlStartCode = "<!DOCTYPE HTML><html> <head>  <meta charset='utf-8'>  "
                + "<title>Отчет =) </title> "
                + "<style type='text/css'>"
                + " table { border-collapse: collapse;} "
                + " tr,th {border: 1px solid black;}  </style>"
                + "</head> <body> <table>";
            //вызываем рекурсивный метод
            foreach (TreeNode n in treeView1.Nodes)
            {
                PrintRecursive(n);
            }    
            //вывод конца
            htmlEndCode = "</table> </body> </html>";           
        }
        private void PrintRecursive(TreeNode treeNode)
        {
            //проверка уровня узла
            switch (treeNode.Level)
            {
                case 0: htmlCode += "<tr> <th bgcolor=#CCCCCC>" + treeNode.Text + "</th> <th></th> </tr>"; break;
                case 1: htmlCode += "<tr> <th bgcolor=#CCCCCC>" + treeNode.Text + "</th> <th></th> </tr>"; break;
                case 2: htmlCode += "<tr> <th ></th> <th>" + treeNode.Text + "</th> </tr>"; break;
            }                
            //отображение всех частей html кода
            webBrowser1.DocumentText = htmlStartCode + htmlCode + htmlEndCode;

            // рекурсия для каждого узла
            foreach (TreeNode tn in treeNode.Nodes)
            {   
                PrintRecursive(tn);
            }
            
        }

        private void buttonSaveToFile_Click(object sender, EventArgs e)
        {
            try
            {
                sw.WriteLine(webBrowser1.DocumentText);
                sw.Close();
                MessageBox.Show("Отчет сохранен в файл table.html =)");
            }
            catch 
            {
                MessageBox.Show("Отчет не сохранен в файл =(");
            }
        }
          
    }
}
