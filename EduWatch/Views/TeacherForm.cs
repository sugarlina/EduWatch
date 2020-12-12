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
    public partial class TeacherForm : Form, ITeacherView
    {
        public TeacherForm()
        {
            InitializeComponent();
            HideAll();
        }
        public TeacherPresenter Presenter { private get; set; }
       public string TeacherNameLabelText { get => teachersNameLabel.Text; set => teachersNameLabel.Text = value; }
        public int ComboBoxSubjectSelectedIndex { get => comboBoxSubject.SelectedIndex; set => comboBoxSubject.SelectedIndex = value; }
        public int SelectedSubjectID => (int)comboBoxSubject.SelectedValue;
        public int ComboBoxStudentSelectedIndex { get => comboBoxStudent.SelectedIndex; set => comboBoxStudent.SelectedIndex = value; }
        public int SelectedStudentID => (int)comboBoxStudent.SelectedValue;
        public object GridViewData { get => dataGridView1.DataSource; set => dataGridView1.DataSource = value; }
        public bool SaveButtonEnabled { get => Savebtn.Enabled; set => Savebtn.Enabled = value; }
        public bool AutoGenerateDataColumns { get => dataGridView1.AutoGenerateColumns; set => dataGridView1.AutoGenerateColumns = value; }
        public bool SelectedGrades { get => radioButtonGrade.Checked; set => radioButtonGrade.Checked = value; }
        public bool SelectedNotes { get => radioButtonNote.Checked; set => radioButtonNote.Checked = value; }
        public bool AverageGradeButtonEnabled { get => btnAverage.Enabled; set => btnAverage.Enabled = value; }
        public string AverageGradeTextBox { get =>averageGradeTextBox.Text; set => averageGradeTextBox.Text=value; }
        public bool SelectedGradesView { get => radioButtonGrade.Checked; set => radioButtonGrade.Checked = value; }

        public bool ComboBoxStudentEnabled { get => comboBoxStudent.Enabled; set => comboBoxStudent.Enabled = value; }

        public string ComboBoxGrade { get => gradeComboBox.Text; set => gradeComboBox.Text = value; }

        public string ComboBoxGrade1to12 { get => comboBoxGrade1to12.Text; set => comboBoxGrade1to12.Text = value; }
        public string TextBoxComment { get => commentTextBox.Text; set =>commentTextBox.Text=value; }
        public bool IsDataGridViewVisible => dataGridView1.Visible;
        public bool IsTextBoxCommentVisible => commentTextBox.Visible;
        public bool IsCommentLabelVisible => CommentLabel.Visible;
        public bool IsGradeLabelVisible => GradeLabel.Visible;
        public bool IsGradeComboBoxVisible => gradeComboBox.Visible;
        public bool IsSaveBTNVisible => Savebtn.Visible;

        public bool ComboBoxSubjectEnabled { get => comboBoxSubject.Enabled; set => comboBoxSubject.Enabled=value; }
        public bool gradeRadioBtnEnabled { get => radioButtonGrade.Enabled; set => radioButtonGrade.Enabled=value; }
        public bool noteRadioBtnEnabled { get => radioButtonNote.Enabled; set => radioButtonNote.Enabled=value; }
        public void FIllInCorrespondingSubjects(List<Tuple<int, string>> subjects)
        {
            comboBoxSubject.DisplayMember = "Item2";
            comboBoxSubject.ValueMember = "Item1";
            comboBoxSubject.DataSource = subjects;
        }

        public void FillInCorrespondingStudents(List<Tuple<int, string>> students)
        {
            comboBoxSubject.DisplayMember = "Item2";
            comboBoxSubject.ValueMember = "Item1";
            comboBoxSubject.DataSource = students;
        }

        public MessageResult Message(string msg, string caption = "Съобщение", MessageIcon msgIcon = MessageIcon.None, MessageButton msgBtn = MessageButton.OK)
        {
            var result = MessageBox.Show(
                    msg,
                    caption,
                    (MessageBoxButtons)Enum.Parse(typeof(MessageBoxButtons), msgBtn.ToString()),
                    (MessageBoxIcon)Enum.Parse(typeof(MessageBoxIcon), msgIcon.ToString())
                    );

            return (MessageResult)Enum.Parse(typeof(MessageResult), result.ToString());
        }

        private void comboBoxSubject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Presenter.OnSubjectSelection();
            ComboBoxStudentSelectedIndex = -1;
        }

        private void comboBoxStudent_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Presenter.OnStudentSelection();
        }

        public void ClearData()
        {
            dataGridView1.Columns.Clear();
        }

      

        public void FillInGrades(string[] grades)
        {
            gradeComboBox.Items.AddRange(grades);
        }

        private void SeeDatabtn_Click(object sender, EventArgs e)
        {
            HideAll();        
            dataGridView1.Visible=true;
        
        }

        private void AddRecordbtn_Click(object sender, EventArgs e)
        {
            if (radioButtonGrade.Checked)
            {
                HideAll();
                CommentLabel.Text = "Коментар:";
                CommentLabel.Visible = true; ;
                commentTextBox.Visible = true ;
                GradeLabel.Visible=true;
                gradeComboBox.Visible=true;
                Savebtn.Visible=true;
            }
            else if (radioButtonNote.Checked)
            {
                HideAll();
                CommentLabel.Text = "Забележка:";
                CommentLabel.Visible=true;
                commentTextBox.Visible=true;
                Savebtn.Visible=true;
            }
            else
            {
                Message("Моля, изберете какво искате да добавите.", "Внимание", Views.MessageIcon.Warning);
            }
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            Presenter.OnExitButtonClick();
        }

        public void FillInWhichGrade(string[] grade)
        {
            comboBoxGrade1to12.Items.AddRange(grade);
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            if(radioButtonGrade.Checked)
            {
                Presenter.OnSaveGradeButtonClick();
            }
            else
            {
                Presenter.OnSaveNoteButtonClick();
            }
        }

        private void btnAverage_Click(object sender, EventArgs e)
        {
            Presenter.OnAverageGradeButtonClick();
        }
        public void FormatDataDisplay()
        {
            var checkboxCol = dataGridView1.Columns.OfType<DataGridViewCheckBoxColumn>().Single();
            checkboxCol.SortMode = DataGridViewColumnSortMode.Automatic;

            var bigCols = dataGridView1.Columns.OfType<DataGridViewTextBoxColumn>().Where(x => new string[] { "Note", "Comment" }.Contains(x.Name));
            foreach (var col in bigCols)
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var gradeColumn = dataGridView1.Columns.OfType<DataGridViewTextBoxColumn>().Where(x => x.Name == "Grade").SingleOrDefault();
            if (gradeColumn != null)
                gradeColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
         
           
        }

        private void radioButtonGrade_CheckedChanged(object sender, EventArgs e)
        {
            Presenter.OnGradesButtonClick();
        }

        private void optionsBtn_Click(object sender, EventArgs e)
        {
            Presenter.OnSettingsButtonClick();

        }
        void HideAll()
        {
            CommentLabel.Visible = false;
            commentTextBox.Visible=false;
            GradeLabel.Visible = false; ;
            gradeComboBox.Visible=false;
            dataGridView1.Visible = false;
            Savebtn.Visible = false;
        }

        private void comboBoxGrade1to12_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBoxStudentSelectedIndex = -1;

            ComboBoxSubjectSelectedIndex = -1;
        }
    }
}
