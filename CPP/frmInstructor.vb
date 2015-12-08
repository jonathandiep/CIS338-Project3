Public Class frmInstructor
    Dim instructorList As New List(Of clsInstructor)

    Private Sub frmInstructor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'LOAD FROM DB
        CPP_DB.dbOpen()
        instructorList = CPP_DB.loadInstructors()
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
        Dim InstructorBindingSource As New BindingSource

        'ASSIGN THE DATAROUCE TO THE STUDENT LIST
        InstructorBindingSource.DataSource = instructorList

        'SET THE GRID DATASOURCE TO THE BINDING SOURCE
        Me.CPP_INSTRUCTORSDataGridView.DataSource = InstructorBindingSource
    End Sub

    Public Sub setMode(strMode As String)
        'CONTROL THE DISPLAY OF LIST VS DETAIL OF Instructors

        If strMode = "L" Then
            'MODE IS LIST

            Me.gbInstructorDetail.Hide()
            Me.gbInstructorList.Left = 0
            Me.gbInstructorList.Top = 0
            Me.Width = gbInstructorList.Width + 50
            Me.Height = gbInstructorList.Height + 50

            Me.gbInstructorList.Visible = True
        Else
            'MODE IS DETAIL

            Me.gbInstructorList.Hide()
            Me.gbInstructorDetail.Left = 0
            Me.gbInstructorDetail.Top = 0
            Me.Width = gbInstructorDetail.Width + 50
            Me.Height = gbInstructorDetail.Height + 50

            Me.gbInstructorDetail.Visible = True
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Me.setMode("D")
        Me.PROF_IDTextBox.Focus()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'CREATE INSTRUCTOR OBJECT
        Dim anInstructor As New clsInstructor

        'POPULATE INSTRUCTOR OBJECT
        anInstructor.profId = Me.PROF_IDTextBox.Text
        anInstructor.firstName = Me.FIRST_NAMETextBox.Text
        anInstructor.lastName = Me.LAST_NAMETextBox.Text
        anInstructor.phone = Me.PHONETextBox.Text
        anInstructor.dept = Me.DEPARTMENTTextBox.Text
        Try
            clsValidator.validateInstructor(anInstructor)
            'CHECK IF WE ARE SAVING OR UPDATING
            If (btnSave.Text = "&Save") Then

                'SAVE STUDENT
                CPP_DB.dbOpen()
                CPP_DB.insertInstructor(anInstructor)
                CPP_DB.dbClose()

                'CHECK FOR ERRORS
                If CPP_DB.dbGetError <> "" Then
                    MessageBox.Show(CPP_DB.dbGetError)
                Else
                    instructorList.Add(anInstructor)
                    refreshDataGrid()
                    MessageBox.Show("Instructor Saved!")
                    Me.setMode("L")
                End If
            Else

                'UPDATE INSTRUCTOR
                CPP_DB.dbOpen()
                CPP_DB.updateInstructor(anInstructor)
                CPP_DB.dbClose()

                'CHECK FOR ERRORS
                If CPP_DB.dbGetError <> "" Then
                    MessageBox.Show(CPP_DB.dbGetError)
                Else
                    'REMOVE OLD INSTRUCTOR FROM LIST
                    For Each instructor In instructorList
                        If instructor.profId = anInstructor.profId Then
                            instructorList.Remove(instructor)
                            Exit For
                        End If
                    Next
                    instructorList.Add(anInstructor)
                    refreshDataGrid()
                    MessageBox.Show("Instructor Updated!")
                    Me.setMode("L")
                    refreshDataGrid()
                    Me.btnSave.Text = "&Save"
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("There was an error processing the form" & vbCrLf & vbCrLf & ex.Message.ToString)
        End Try

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'GET CURRENT INSTRUCTOR ROW FROM THE GRID
        Dim row As DataGridViewRow = Me.CPP_INSTRUCTORSDataGridView.CurrentRow

        'CHECK IF ROW IS VALIID OTHERWISE STOP
        If IsNothing(row) Then
            MessageBox.Show("Nothing Selected!")
            Exit Sub
        End If

        'CONVERT THE ROW TO A INSTRUCTOR OBJECT
        Dim anInstructor As clsInstructor = TryCast(row.DataBoundItem, clsInstructor)

        'GET DATA FROM THE ROW TO THE TEXTBOXES
        Me.PROF_IDTextBox.Text = anInstructor.profId
        Me.FIRST_NAMETextBox.Text = anInstructor.firstName
        Me.LAST_NAMETextBox.Text = anInstructor.lastName
        Me.PHONETextBox.Text = anInstructor.phone
        Me.DEPARTMENTTextBox.Text = anInstructor.dept

        'SET THE FOCUS ON ID
        Me.PROF_IDTextBox.Focus()

        'SWITCH SAVE TO UPDATE
        Me.btnSave.Text = "&Update"

        'DISPLAY DETAIL MODE
        Me.setMode("D")
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim row As DataGridViewRow = Me.CPP_INSTRUCTORSDataGridView.CurrentRow

        'CHECK IF ROW IS VALID OTHERWISE STOP
        If IsNothing(row) Then
            MessageBox.Show("Nothing selected!")
            Exit Sub
        End If

        'CONVERT ROW TO STUDENT
        Dim anInstructor As clsInstructor = TryCast(row.DataBoundItem, clsInstructor)

        'DELETE STUDENT FROM DB
        CPP_DB.dbOpen()
        CPP_DB.deleteInstructor(anInstructor.profId)
        CPP_DB.dbClose()

        'CHECK FOR ERRORS
        If CPP_DB.dbGetError = "" Then
            MessageBox.Show("Instructor Deleted!")
            'REMOVE STUDENT FROM LIST
            For Each instructor In instructorList
                If instructor.profId = anInstructor.profId Then
                    instructorList.Remove(instructor)
                    Exit For
                End If
            Next
            'UPDATE GRID
            refreshDataGrid()
        Else
            MessageBox.Show(CPP_DB.dbGetError)
        End If
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Dim strProfId As String = InputBox("Enter Prof ID")

        For Each row As DataGridViewRow In CPP_INSTRUCTORSDataGridView.Rows
            If row.Cells(0).Value = strProfId Then
                row.Selected = True 'CPP_INSTRUCTORSDataGridView.CurrentRow.
                MessageBox.Show("Found!")
                Exit Sub
            End If
        Next

        MessageBox.Show("Not found!")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'CLEAR ALL CONTROLS
        For Each ctrl In gbInstructorDetail.Controls
            If TypeOf (ctrl) Is TextBox Then
                TryCast(ctrl, TextBox).Clear()
            End If
        Next

        'SET SAVE BUTTON TO DEFAULT 
        btnSave.Text = "&Save"

        'SWITCH TO LIST MODE
        setMode("L")
    End Sub


End Class