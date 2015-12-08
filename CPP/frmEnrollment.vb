Public Class frmEnrollment
    Dim enrollmentList As New List(Of clsEnrollment)



    Public Sub setMode(strMode As String)
        'CONTROL THE DISPLAY OF LIST VS DETAIL OF EnrollmentS

        If strMode = "L" Then
            'MODE IS LIST

            Me.gbEnrollmentDetail.Hide()
            Me.gbEnrollmentList.Left = 0
            Me.gbEnrollmentList.Top = 0
            Me.Width = gbEnrollmentList.Width + 50
            Me.Height = gbEnrollmentList.Height + 50

            Me.gbEnrollmentList.Visible = True
        Else
            'MODE IS DETAIL

            Me.gbEnrollmentList.Hide()
            Me.gbEnrollmentDetail.Left = 0
            Me.gbEnrollmentDetail.Top = 0
            Me.Width = gbEnrollmentDetail.Width + 50
            Me.Height = gbEnrollmentDetail.Height + 50

            Me.gbEnrollmentDetail.Visible = True
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        setMode("D")

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim anEnrollment As New clsEnrollment

        If (Me.BRONCO_IDComboBox.SelectedIndex = -1) Then
            'anEnrollment.broncoId = ""
            Console.WriteLine(Me.BRONCO_IDComboBox.SelectedIndex)
        Else
            anEnrollment.broncoId = Me.BRONCO_IDComboBox.SelectedItem.ToString.Substring(0, 4)
        End If

        If (Me.CATALOG_IDComboBox.SelectedIndex = -1) Then
            'anEnrollment.catalogId = ""
            Console.WriteLine(Me.CATALOG_IDComboBox.SelectedIndex)
        Else
            anEnrollment.catalogId = Me.CATALOG_IDComboBox.SelectedItem.ToString.Substring(0, 1)
        End If

        Try
            clsValidator.validateEnrollment(anEnrollment)

            'CHECK IF WE ARE SAVING OR UPDATING
            If (btnSave.Text = "&Save") Then

                'SAVE STUDENT
                CPP_DB.dbOpen()
                CPP_DB.insertEnrollment(anEnrollment)
                CPP_DB.dbClose()

                'CHECK FOR ERRORS
                If CPP_DB.dbGetError <> "" Then
                    MessageBox.Show(CPP_DB.dbGetError)
                Else
                    enrollmentList.Add(anEnrollment)
                    refreshDataGrid()
                    MessageBox.Show("Enrollment Saved!")
                    Me.setMode("L")
                End If
            Else

                'UPDATE ENROLLMENT
                CPP_DB.dbOpen()
                CPP_DB.updateEnrollment(anEnrollment)
                CPP_DB.dbClose()

                'CHECK FOR ERRORS
                If CPP_DB.dbGetError <> "" Then
                    MessageBox.Show(CPP_DB.dbGetError)
                Else
                    'REMOVE OLD ENROLLMENT FROM LIST
                    For Each enrollment In enrollmentList
                        If enrollment.broncoId = anEnrollment.broncoId And enrollment.catalogId = anEnrollment.catalogId Then
                            enrollmentList.Remove(enrollment)
                            Exit For
                        End If
                    Next
                    enrollmentList.Add(anEnrollment)
                    refreshDataGrid()
                    MessageBox.Show("Enrollment Updated!")
                    Me.setMode("L")
                    Me.btnSave.Text = "&Save"
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("There was an error processing the form" & vbCrLf & vbCrLf & ex.Message.ToString)
        End Try

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim row As DataGridViewRow = Me.CPP_ENROLLMENTDataGridView.CurrentRow

        'CHECK IF ROW IS VALID OTHERWISE STOP
        If IsNothing(row) Then
            MessageBox.Show("Nothing selected!")
            Exit Sub
        End If

        'CONVERT ROW TO STUDENT
        Dim anEnrollment As clsEnrollment = TryCast(row.DataBoundItem, clsEnrollment)

        'DELETE STUDENT FROM DB
        CPP_DB.dbOpen()
        CPP_DB.deleteEnrollment(anEnrollment.broncoId, anEnrollment.catalogId)
        CPP_DB.dbClose()

        'CHECK FOR ERRORS
        If CPP_DB.dbGetError = "" Then
            MessageBox.Show("Enrollment Deleted!")
            'REMOVE STUDENT FROM LIST
            For Each enrollment In enrollmentList
                If enrollment.broncoId = anEnrollment.broncoId And enrollment.catalogId = anEnrollment.catalogId Then
                    enrollmentList.Remove(enrollment)
                    Exit For
                End If
            Next
            'UPDATE GRID
            refreshDataGrid()
        Else
            MessageBox.Show(CPP_DB.dbGetError)
        End If

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        btnSave.Text = "&Save"
        setMode("L")
    End Sub

    Private Sub refreshDataGrid()
        'CREATE A BINDING SOURCE AND 
        Dim enrollmentBindingSource As New BindingSource

        'ASSIGN THE DATASOURCE TO THE ENROLLMENT LIST
        enrollmentBindingSource.DataSource = enrollmentList

        'SET THE GRID DATASOURCE TO THE BINDING SOURCE
        Me.CPP_ENROLLMENTDataGridView.DataSource = enrollmentBindingSource
    End Sub

    Private Sub frmEnrollment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'LOAD FROM DB
        CPP_DB.dbOpen()
        enrollmentList = CPP_DB.loadEnrollment()
        CPP_DB.dbClose()

        'CHECK ERRORS
        If (CPP_DB.dbGetError = "") Then
            refreshDataGrid()
        Else
            MessageBox.Show(CPP_DB.dbGetError)
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'GET CURRENT STUDENT ROW FROM THE GRID
        Dim row As DataGridViewRow = Me.CPP_ENROLLMENTDataGridView.CurrentRow

        'CHECK IF ROW IS VALIID OTHERWISE STOP
        If IsNothing(row) Then
            MessageBox.Show("Nothing Selected!")
            Exit Sub
        End If

        'CONVERT THE ROW TO AN ENROLLMENT OBJECT
        Dim anEnrollment As clsEnrollment = TryCast(row.DataBoundItem, clsEnrollment)

        'GET DATA FROM THE ROW TO THE TEXTBOXES
        Me.BRONCO_IDComboBox.SelectedIndex = BRONCO_IDComboBox.FindString(anEnrollment.broncoId)
        Me.CATALOG_IDComboBox.SelectedIndex = CATALOG_IDComboBox.FindString(anEnrollment.catalogId)

        'SET THE FOCUS ON ID
        Me.BRONCO_IDComboBox.Focus()

        'SWITCH SAVE TO UPDATE
        Me.btnSave.Text = "&Update"

        'DISPLAY DETAIL MODE
        Me.setMode("D")
    End Sub
End Class