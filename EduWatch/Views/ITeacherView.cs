﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWatch.Views
{
    public interface ITeacherView : IView
    {
        Presenters.TeacherPresenter Presenter { set; }
        string TeacherNameLabelText { get; set; }
        int ComboBoxSubjectSelectedIndex { get; set; }
        int ComboBoxStudentSelectedIndex { get; set; }
        int SelectedSubjectID { get;}
        int SelectedStudentID { get;}
        bool ComboBoxStudentEnabled { get; set; }
        bool ComboBoxSubjectEnabled { get; set;}
        bool gradeRadioBtnEnabled { get; set; }
        bool noteRadioBtnEnabled { get; set; }
        string ComboBoxGrade { get; }
        string TextBoxComment { get; set; }
        string ComboBoxGrade1to12 { get; }
        object GridViewData { get; set; }
        bool SaveButtonEnabled { get; set; }
        bool AutoGenerateDataColumns { get; set; }
        bool SelectedGradesView { get; set; }
        bool AverageGradeButtonEnabled { get; set; }
        string AverageGradeTextBox { get; set; }
        bool IsDataGridViewVisible { get;  }
        bool IsTextBoxCommentVisible { get;  }
        bool IsCommentLabelVisible { get;  }
        bool IsGradeLabelVisible { get; }
        bool IsGradeComboBoxVisible { get; } 
        bool IsSaveBTNVisible { get; }

        void FIllInCorrespondingSubjects(List<Tuple<int, string>> subjects);
        void FillInCorrespondingStudents(List<Tuple<int, string>> students);
        void FillInGrades(string[] grades);
        void FillInWhichGrade(string[] grade);


        void FormatDataDisplay();
        void ClearData();
       
    }
}
