using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Product.Учащийся
{
    public partial class Result : Form
    {
        public Result(double allBalls, int numberOfPoints, double studentAssessment, DateTime startDate, DateTime finishedDate, List<Tuple<string, bool, string, string>> result)
        {
            InitializeComponent();

            label1.Text = $"Начат: {startDate}";
            label2.Text = $"Закончен: {finishedDate}";
            label3.Text = $"Баллы: {numberOfPoints}/{allBalls}";
            label4.Text = $"Оценка: {studentAssessment}/10";

            var questions = result.Select(r => r.Item1).ToList();
            var answers = result.Select(r => r.Item2).ToList();

            for (int i = 0; i < result.Count; i++)
            {
                ListViewItem item = new ListViewItem(questions[i]);
                if (answers[i])
                {
                    item.ForeColor = Color.Green;
                }
                else
                {
                    item.ForeColor = Color.Red;
                }

                listView1.Items.Add(item);
            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            StudentResult = result;
        }

        private List<Tuple<string, bool, string, string>> StudentResult { get; }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            var selectedItem = listView1.SelectedItems[0].Text;

            var result = StudentResult.First(s => s.Item1 == selectedItem);

            MessageBox.Show($"Правильный вариант:\n{result.Item3}\n\nВаш вариант:\n{result.Item4}");
        }
    }
}
