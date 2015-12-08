﻿Public Class frmCourse
    Dim courseList As New List(Of clsCourse)
    Public Sub setMode(strMode As String)
        'CONTROL THE DISPLAY OF LIST VS DETAIL OF CourseS

        If strMode = "L" Then
            'MODE IS LIST

            Me.gbCourseDetail.Hide()
            Me.gbCourseList.Left = 0
            Me.gbCourseList.Top = 0
            Me.Width = gbCourseList.Width + 50
            Me.Height = gbCourseList.Height + 50

            Me.gbCourseList.Visible = True
        Else
            'MODE IS DETAIL

            Me.gbCourseList.Hide()
            Me.gbCourseDetail.Left = 0
            Me.gbCourseDetail.Top = 0
            Me.Width = gbCourseDetail.Width + 50
            Me.Height = gbCourseDetail.Height + 50

            Me.gbCourseDetail.Visible = True
        End If
    End Sub

    Private Sub frmCourse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'LOAD FROM DB
        CPP_DB.dbOpen()
        courseList = CPP_DB.loadCourses()
        CPP_DB.dbClose()

        'CHECK ERRORS
        If (CPP_DB.dbGetError = "") Then
            refreshDataGrid()
        Else
            MessageBox.Show(CPP_DB.dbGetError)
        End If
    End Sub

    Private Sub refreshDataGrid()
        'CREATE A BINDING SOURCE AND 
        Dim CourseBindingSource As New BindingSource

        'ASSIGN THE DATAROUCE TO THE STUDENT LIST
        CourseBindingSource.DataSource = courseList

        'SET THE GRID DATASOURCE TO THE BINDING SOURCE
        Me.CPP_COURSESDataGridView.DataSource = CourseBindingSource
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Me.setMode("D")
        Me.COURSE_IDTextBox.Focus()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'CREATE COURSE OBJECT
        Dim aCourse As New clsCourse

        'POPULATE COURSE OBJECT
        aCourse.courseId = Me.COURSE_IDTextBox.Text
        aCourse.description = Me.DESCRIPTIONTextBox.Text



        Try
            If (Me.UNITSTextBox.Text.Length = 0) Then
                aCourse.units = 0
            Else
                aCourse.units = Me.UNITSTextBox.Text
            End If

            clsValidator.validateCourses(aCourse)

            'CHECK IF WE ARE SAVING OR UPDATING
            If (btnSave.Text = "&Save") Then

                'SAVE COURSE
                CPP_DB.dbOpen()
                CPP_DB.insertCourse(aCourse)
                CPP_DB.dbClose()

                'CHECK FOR ERRORS
                If CPP_DB.dbGetError <> "" Then
                    MessageBox.Show(CPP_DB.dbGetError)
                Else
                    courseList.Add(aCourse)
                    refreshDataGrid()
                    MessageBox.Show("Course Saved!")
                    Me.setMode("L")
                End If
            Else

                'UPDATE COURSE
                CPP_DB.dbOpen()
                CPP_DB.updateCourse(aCourse)
                CPP_DB.dbClose()

                'CHECK FOR ERRORS
                If CPP_DB.dbGetError <> "" Then
                    MessageBox.Show(CPP_DB.dbGetError)
                Else
                    'REMOVE OLD COURSE FROM LIST
                    For Each course In courseList
                        If course.courseId = aCourse.courseId Then
                            courseList.Remove(course)
                            Exit For
                        End If
                    Next
                    courseList.Add(aCourse)
                    refreshDataGrid()
                    MessageBox.Show("Student Updated!")
                    Me.setMode("L")
                    Me.btnSave.Text = "&Save"
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("There was an error processing the form" & vbCrLf & vbCrLf & ex.Message.ToString)
        End Try


    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim row As DataGridViewRow = Me.CPP_COURSESDataGridView.CurrentRow

        'CHECK IF ROW IS VALID OTHERWISE STOP
        If IsNothing(row) Then
            MessageBox.Show("Nothing selected!")
            Exit Sub
        End If

        'CONVERT ROW TO COURSE
        Dim aCourse As clsCourse = TryCast(row.DataBoundItem, clsCourse)

        'DELETE COURSE FROM DB
        CPP_DB.dbOpen()
        CPP_DB.deleteCourse(aCourse.courseId)
        CPP_DB.dbClose()

        'CHECK FOR ERRORS
        If CPP_DB.dbGetError = "" Then
            MessageBox.Show("Course Deleted!")
            'REMOVE COURSE FROM LIST
            For Each course In courseList
                If course.courseId = aCourse.courseId Then
                    courseList.Remove(course)
                    Exit For
                End If
            Next
            'UPDATE GRID
            refreshDataGrid()
        Else
            MessageBox.Show(CPP_DB.dbGetError)
        End If

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'GET CURRENT COURSE ROW FROM THE GRID
        Dim row As DataGridViewRow = Me.CPP_COURSESDataGridView.CurrentRow

        'CHECK IF ROW IS VALIID OTHERWISE STOP
        If IsNothing(row) Then
            MessageBox.Show("Nothing Selected!")
            Exit Sub
        End If

        'CONVERT THE ROW TO A STUDENT OBJECT
        Dim aCourse As clsCourse = TryCast(row.DataBoundItem, clsCourse)

        'GET DATA FROM THE ROW TO THE TEXTBOXES
        Me.COURSE_IDTextBox.Text = aCourse.courseId
        Me.DESCRIPTIONTextBox.Text = aCourse.description
        Me.UNITSTextBox.Text = aCourse.units

        'SET THE FOCUS ON ID
        Me.COURSE_IDTextBox.Focus()

        'SWITCH SAVE TO UPDATE
        Me.btnSave.Text = "&Update"

        'DISPLAY DETAIL MODE
        Me.setMode("D")
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Dim strCourseId As String = InputBox("Enter Course ID")

        For Each row As DataGridViewRow In CPP_COURSESDataGridView.Rows
            If row.Cells(0).Value = strCourseId Then
                row.Selected = True
                MessageBox.Show("Found!")
                Exit Sub
            End If
        Next

        MessageBox.Show("Not found!")
    End Sub
End Class