Public Class frmCatalog
    Dim catalogList As New List(Of clsCatalog)

    Public Sub setMode(strMode As String)
        'CONTROL THE DISPLAY OF LIST VS DETAIL OF CatalogS

        If strMode = "L" Then
            'MODE IS LIST

            Me.gbCatalogDetail.Hide()
            Me.gbCatalogList.Left = 0
            Me.gbCatalogList.Top = 0
            Me.Width = gbCatalogList.Width + 50
            Me.Height = gbCatalogList.Height + 50

            Me.gbCatalogList.Visible = True
        Else
            'MODE IS DETAIL

            Me.gbCatalogList.Hide()
            Me.gbCatalogDetail.Left = 0
            Me.gbCatalogDetail.Top = 0
            Me.Width = gbCatalogDetail.Width + 50
            Me.Height = gbCatalogDetail.Height + 50

            Me.gbCatalogDetail.Visible = True
        End If
    End Sub

    Private Sub frmCatalog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'LOAD FROM DB
        CPP_DB.dbOpen()
        catalogList = CPP_DB.loadCatalog()
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
        Dim catalogBindingSource As New BindingSource

        'ASSIGN THE DATASOURCE TO THE ENROLLMENT LIST
        catalogBindingSource.DataSource = catalogList

        'SET THE GRID DATASOURCE TO THE BINDING SOURCE
        Me.CPP_CATALOGDataGridView.DataSource = catalogBindingSource
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        setMode("D")
        Me.CATALOG_IDTextBox.Focus()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'CREATE CATALOG OBJECT
        Dim aCatalog As New clsCatalog

        'POPULATE CATALOG OBJECT
        aCatalog.catalogId = Me.CATALOG_IDTextBox.Text

        If (Me.QUARTERComboBox.SelectedIndex = -1) Then
            Console.WriteLine(Me.QUARTERComboBox.SelectedIndex)
        Else
            aCatalog.quarter = Me.QUARTERComboBox.SelectedItem
        End If

        If (Me.COURSE_IDComboBox.SelectedIndex = -1) Then
            Console.WriteLine(Me.COURSE_IDComboBox.SelectedIndex)
        Else
            aCatalog.courseId = Me.COURSE_IDComboBox.SelectedItem.ToString.Substring(0, 7)
        End If

        If (Me.PROF_IDComboBox.SelectedIndex = -1) Then
            Console.WriteLine(Me.PROF_IDComboBox.SelectedIndex)
        Else
            aCatalog.profId = Me.PROF_IDComboBox.SelectedItem.ToString.Substring(0, 4)
        End If

        Try
            If (Me.YEARTextBox.Text.Length = 0) Then
                aCatalog.year = 0
            Else
                aCatalog.year = Me.YEARTextBox.Text
            End If

            clsValidator.validateCatalog(aCatalog)

            'CHECK IF WE ARE SAVING OR UPDATING
            If (btnSave.Text = "&Save") Then

                'SAVE CATALOG
                CPP_DB.dbOpen()
                CPP_DB.insertCatalog(aCatalog)
                CPP_DB.dbClose()

                'CHECK FOR ERRORS
                If CPP_DB.dbGetError <> "" Then
                    MessageBox.Show(CPP_DB.dbGetError)
                Else
                    catalogList.Add(aCatalog)
                    refreshDataGrid()
                    MessageBox.Show("Catalog Saved!")
                    Me.setMode("L")
                End If
            Else

                'UPDATE CATALOG
                CPP_DB.dbOpen()
                CPP_DB.updateCatalog(aCatalog)
                CPP_DB.dbClose()

                'CHECK FOR ERRORS
                If CPP_DB.dbGetError <> "" Then
                    MessageBox.Show(CPP_DB.dbGetError)
                Else
                    'REMOVE OLD CATALOG FROM LIST
                    For Each catalog In catalogList
                        If catalog.catalogId = aCatalog.catalogId Then
                            catalogList.Remove(catalog)
                            Exit For
                        End If
                    Next
                    catalogList.Add(aCatalog)
                    refreshDataGrid()
                    MessageBox.Show("Catalog Updated!")
                    Me.setMode("L")
                    Me.btnSave.Text = "&Save"
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("There was an error processing the form" & vbCrLf & vbCrLf & ex.Message.ToString)
        End Try


    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim row As DataGridViewRow = Me.CPP_CATALOGDataGridView.CurrentRow

        'CHECK IF ROW IS VALID OTHERWISE STOP
        If IsNothing(row) Then
            MessageBox.Show("Nothing selected!")
            Exit Sub
        End If

        'CONVERT ROW TO STUDENT
        Dim aCatalog As clsCatalog = TryCast(row.DataBoundItem, clsCatalog)

        'DELETE CATALOG FROM DB
        CPP_DB.dbOpen()
        CPP_DB.deleteCatalog(aCatalog.catalogId)
        CPP_DB.dbClose()

        'CHECK FOR ERRORS
        If CPP_DB.dbGetError = "" Then
            MessageBox.Show("Catalog Deleted!")
            'REMOVE CATALOG FROM LIST
            For Each catalog In catalogList
                If catalog.catalogId = aCatalog.catalogId Then
                    catalogList.Remove(catalog)
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
        'GET CURRENT CATALOG ROW FROM THE GRID
        Dim row As DataGridViewRow = Me.CPP_CATALOGDataGridView.CurrentRow

        'CHECK IF ROW IS VALIID OTHERWISE STOP
        If IsNothing(row) Then
            MessageBox.Show("Nothing Selected!")
            Exit Sub
        End If

        'CONVERT THE ROW TO A STUDENT OBJECT
        Dim aCatalog As clsCatalog = TryCast(row.DataBoundItem, clsCatalog)

        'GET DATA FROM THE ROW TO THE TEXTBOXES
        Me.CATALOG_IDTextBox.Text = aCatalog.catalogId
        Me.YEARTextBox.Text = aCatalog.year
        Me.QUARTERComboBox.SelectedIndex = QUARTERComboBox.FindString(aCatalog.quarter)
        Me.COURSE_IDComboBox.SelectedIndex = COURSE_IDComboBox.FindString(aCatalog.courseId)
        Me.PROF_IDComboBox.SelectedIndex = PROF_IDComboBox.FindString(aCatalog.profId)

        'SET THE FOCUS ON ID
        Me.CATALOG_IDTextBox.Focus()

        'SWITCH SAVE TO UPDATE
        Me.btnSave.Text = "&Update"

        'DISPLAY DETAIL MODE
        Me.setMode("D")
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Dim strCatalogId As String = InputBox("Enter Catalog ID")

        For Each row As DataGridViewRow In CPP_CATALOGDataGridView.Rows
            If row.Cells(0).Value = strCatalogId Then
                row.Selected = True
                MessageBox.Show("Found!")
                Exit Sub
            End If
        Next

        MessageBox.Show("Not found!")
    End Sub

End Class