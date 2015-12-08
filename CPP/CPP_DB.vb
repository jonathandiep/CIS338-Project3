Imports System.Data.SqlClient
Public Class CPP_DB
    Private Shared cn As SqlConnection
    Private Shared strError As String

    Public Shared Function loadStudents() As List(Of clsStudent)
        'List of students that will be returned
        Dim studentList As New List(Of clsStudent)

        'DB variables
        Dim strSQL As String
        Dim cmd As SqlCommand
        Dim rdr As SqlDataReader

        'clear the errors
        strError = ""

        Try
            strSQL = "Select * From CPP_STUDENTS"

            dbConnect()
            cmd = New SqlCommand(strSQL, cn)
            cmd.CommandType = CommandType.Text
            rdr = cmd.ExecuteReader()

            Do While rdr.Read()
                'Add basic student information
                Dim aStudent As New clsStudent
                aStudent.broncoID = rdr("BRONCO_ID")
                aStudent.firstName = rdr("FIRST_NAME")
                aStudent.lastName = rdr("LAST_NAME")
                aStudent.email = rdr("EMAIL")
                aStudent.phone = rdr("PHONE")

                studentList.Add(aStudent)
            Loop
        Catch ex As SqlException
            dbAddError(ex.Message)
        Catch ex As Exception
            dbAddError(ex.Message)
        End Try
        Return studentList
    End Function

    Public Shared Function insertStudent(aStudent As clsStudent) As Integer
        'Result that will be returned
        Dim intResult As Integer = 0

        'DB variables
        Dim cmd As New SqlCommand
        Dim strStudentSQL As String

        'clear the errors
        strError = ""

        'insert into database
        Try
            dbConnect()
            strStudentSQL = "INSERT INTO CPP_STUDENTS (BRONCO_ID, FIRST_NAME, LAST_NAME, PHONE, EMAIL) " &
                            "values('" & aStudent.broncoID & "','" & aStudent.firstName & "','" & aStudent.lastName & "','" & aStudent.phone & "', '" &
                            aStudent.email & "')"

            cmd.Connection = cn
            cmd.CommandText = strStudentSQL
            cmd.ExecuteNonQuery()

            dbClose()
        Catch ex As Exception
            dbAddError("Insert Failed " & vbCrLf & ex.Message)
        Finally
            cmd.Dispose()
        End Try

        Return intResult
    End Function

    Public Shared Function deleteStudent(strStudentID As String) As Integer
        'Result that will be returned
        Dim intResult As Integer = 0

        'DB variables
        Dim cmd As New SqlCommand
        Dim strSQL As String

        'Clear errors
        strError = ""

        'Delete from database
        Try
            strSQL = "Delete from CPP_STUDENTS where BRONCO_ID = '" & strStudentID & "'"

            dbConnect()
            cmd.Connection = cn
            cmd.CommandText = strSQL

            intResult = cmd.ExecuteNonQuery()

            If (intResult < 1) Then
                dbAddError("DELETE Failed, Student id " & strStudentID & " was not found!")
            End If

            dbClose()
        Catch ex As Exception
            dbAddError("DELETE Failed " & vbCrLf & ex.Message)
        Finally
            cmd.Dispose()
        End Try

        Return intResult
    End Function

    Public Shared Sub updateStudent(aStudent As clsStudent)
        strError = ""

        'To update we remove old student and add new student
        'there are other ways to do this as well using the update statement
        deleteStudent(aStudent.broncoID)
        insertStudent(aStudent)

        If strError <> "" Then
            strError = "Could not Update student " & aStudent.broncoID & vbCrLf & vbCrLf & strError
        End If
    End Sub

    Public Shared Function findStudent(strStudentID As String) As clsStudent
        'student that will be returned
        Dim aStudent As clsStudent = New clsStudent

        'db variables
        Dim cmd As SqlCommand
        Dim rdr As SqlDataReader
        Dim strSQL As String

        'clear error
        strError = ""

        Try
            Dim MyData As New ArrayList

            strSQL = "Select * From CPP_STUDENT Where ID = '" & strStudentID & "'"
            cmd = New SqlCommand(strSQL, cn)
            cmd.CommandType = CommandType.Text

            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                aStudent.broncoID = rdr("BRONCO_ID")
                aStudent.firstName = rdr("FIRST_NAME")
                aStudent.lastName = rdr("LAST_NAME")
                aStudent.email = rdr("EMAIL")
                aStudent.phone = rdr("PHONE")
            Else
                dbAddError("Student not found")
            End If

        Catch ex As SqlException
            dbAddError(ex.Message)
        Catch ex As Exception
            dbAddError(ex.Message)
        End Try

        Return aStudent
    End Function


    Public Shared Function loadInstructors() As List(Of clsInstructor)
        'List of instructors that will be returned
        Dim instructorList As New List(Of clsInstructor)

        'DB variables
        Dim strSQL As String
        Dim cmd As SqlCommand
        Dim rdr As SqlDataReader

        'clear the errors
        strError = ""

        Try
            strSQL = "Select * From CPP_INSTRUCTORS"

            dbConnect()
            cmd = New SqlCommand(strSQL, cn)
            cmd.CommandType = CommandType.Text
            rdr = cmd.ExecuteReader()

            Do While rdr.Read()
                'Add basic instructor information
                Dim anInstructor As New clsInstructor
                anInstructor.profId = rdr("PROF_ID")
                anInstructor.firstName = rdr("FIRST_NAME")
                anInstructor.lastName = rdr("LAST_NAME")
                anInstructor.phone = rdr("PHONE")
                anInstructor.dept = rdr("DEPARTMENT")

                instructorList.Add(anInstructor)
            Loop
        Catch ex As SqlException
            dbAddError(ex.Message)
        Catch ex As Exception
            dbAddError(ex.Message)
        End Try
        Return instructorList
    End Function

    Public Shared Function insertInstructor(anInstructor As clsInstructor) As Integer
        'Result that will be returned
        Dim intResult As Integer = 0

        'DB variables
        Dim cmd As New SqlCommand
        Dim strInstructorSQL As String

        'clear the errors
        strError = ""

        'insert into database
        Try
            dbConnect()
            strInstructorSQL = "INSERT INTO CPP_INSTRUCTORS (PROF_ID, FIRST_NAME, LAST_NAME, PHONE, DEPARTMENT) " &
                            "values('" & anInstructor.profId & "','" & anInstructor.firstName & "','" & anInstructor.lastName & "','" & anInstructor.phone & "', '" &
                            anInstructor.dept & "')"

            cmd.Connection = cn
            cmd.CommandText = strInstructorSQL
            cmd.ExecuteNonQuery()

            dbClose()
        Catch ex As Exception
            dbAddError("Insert Failed " & vbCrLf & ex.Message)
        Finally
            cmd.Dispose()
        End Try

        Return intResult
    End Function

    Public Shared Function deleteInstructor(strProfID As String) As Integer
        'Result that will be returned
        Dim intResult As Integer = 0

        'DB variables
        Dim cmd As New SqlCommand
        Dim strSQL As String

        'Clear errors
        strError = ""

        'Delete from database
        Try
            strSQL = "Delete from CPP_INSTRUCTORS where PROF_ID = '" & strProfID & "'"

            dbConnect()
            cmd.Connection = cn
            cmd.CommandText = strSQL

            intResult = cmd.ExecuteNonQuery()

            If (intResult < 1) Then
                dbAddError("DELETE Failed, Prof id " & strProfID & " was not found!")
            End If

            dbClose()
        Catch ex As Exception
            dbAddError("DELETE Failed " & vbCrLf & ex.Message)
        Finally
            cmd.Dispose()
        End Try

        Return intResult
    End Function

    Public Shared Sub updateInstructor(anInstructor As clsInstructor)
        strError = ""

        'To update we remove old instructor and add new instructor 
        'there are other ways to do this as well using the update statement
        deleteInstructor(anInstructor.profId)
        insertInstructor(anInstructor)

        If strError <> "" Then
            strError = "Could not update instructor " & anInstructor.profId & vbCrLf & vbCrLf & strError
        End If
    End Sub

    Public Shared Function findInstructor(strInstructorID As String) As clsInstructor
        'instructor that will be returned
        Dim anInstructor As clsInstructor = New clsInstructor

        'db variables
        Dim cmd As SqlCommand
        Dim rdr As SqlDataReader
        Dim strSQL As String

        'clear error
        strError = ""

        Try
            Dim MyData As New ArrayList

            strSQL = "Select * From CPP_INSTRUCTORS Where ID = '" & strInstructorID & "'"
            cmd = New SqlCommand(strSQL, cn)
            cmd.CommandType = CommandType.Text

            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                anInstructor.profId = rdr("PROF_ID")
                anInstructor.firstName = rdr("FIRST_NAME")
                anInstructor.lastName = rdr("LAST_NAME")
                anInstructor.phone = rdr("PHONE")
                anInstructor.dept = rdr("DEPARTMENT")
            Else
                dbAddError("Instructor not found")
            End If

        Catch ex As SqlException
            dbAddError(ex.Message)
        Catch ex As Exception
            dbAddError(ex.Message)
        End Try

        Return anInstructor
    End Function


    Public Shared Function loadEnrollment() As List(Of clsEnrollment)
        'List of students that will be returned
        Dim enrollmentList As New List(Of clsEnrollment)

        'DB variables
        Dim strSQL As String
        Dim cmd As SqlCommand
        Dim rdr As SqlDataReader

        'clear the errors
        strError = ""

        Try
            strSQL = "Select * From CPP_ENROLLMENT"

            dbConnect()
            cmd = New SqlCommand(strSQL, cn)
            cmd.CommandType = CommandType.Text
            rdr = cmd.ExecuteReader()

            Do While rdr.Read()
                'Add basic student information
                Dim anEnrollment As New clsEnrollment
                anEnrollment.broncoId = rdr("BRONCO_ID")
                anEnrollment.catalogId = rdr("CATALOG_ID")

                enrollmentList.Add(anEnrollment)
            Loop
        Catch ex As SqlException
            dbAddError(ex.Message)
        Catch ex As Exception
            dbAddError(ex.Message)
        End Try
        Return enrollmentList
    End Function

    Public Shared Function insertEnrollment(anEnrollment As clsEnrollment) As Integer
        'Result that will be returned
        Dim intResult As Integer = 0

        'DB variables
        Dim cmd As New SqlCommand
        Dim strEnrollmentSQL As String

        'clear the errors
        strError = ""

        'insert into database
        Try
            dbConnect()
            strEnrollmentSQL = "INSERT INTO CPP_ENROLLMENT (BRONCO_ID, CATALOG_ID) " &
                            "values('" & anEnrollment.broncoId & "','" & anEnrollment.catalogId & "')"

            cmd.Connection = cn
            cmd.CommandText = strEnrollmentSQL
            cmd.ExecuteNonQuery()

            dbClose()
        Catch ex As Exception
            dbAddError("Insert Failed " & vbCrLf & ex.Message)
        Finally
            cmd.Dispose()
        End Try

        Return intResult
    End Function

    Public Shared Function deleteEnrollment(strBroncoID As String, strCatalogID As String) As Integer
        'Result that will be returned
        Dim intResult As Integer = 0

        'DB variables
        Dim cmd As New SqlCommand
        Dim strSQL As String

        'Clear errors
        strError = ""

        'Delete from database
        Try
            strSQL = "Delete from CPP_ENROLLMENT where BRONCO_ID = '" & strBroncoID & "' AND CATALOG_ID = '" & strCatalogID & "'"

            dbConnect()
            cmd.Connection = cn
            cmd.CommandText = strSQL

            intResult = cmd.ExecuteNonQuery()

            If (intResult < 1) Then
                dbAddError("DELETE Failed")
            End If

            dbClose()
        Catch ex As Exception
            dbAddError("DELETE Failed " & vbCrLf & ex.Message)
        Finally
            cmd.Dispose()
        End Try

        Return intResult
    End Function

    Public Shared Sub updateEnrollment(anEnrollment As clsEnrollment)
        strError = ""

        deleteEnrollment(anEnrollment.broncoId, anEnrollment.catalogId)
        insertEnrollment(anEnrollment)

        If strError <> "" Then
            strError = "Could not update Enrollment" & vbCrLf & vbCrLf & strError
        End If
    End Sub


    Public Shared Function loadCatalog() As List(Of clsCatalog)
        'List of students that will be returned
        Dim catalogList As New List(Of clsCatalog)

        'DB variables
        Dim strSQL As String
        Dim cmd As SqlCommand
        Dim rdr As SqlDataReader

        'clear the errors
        strError = ""

        Try
            strSQL = "Select * From CPP_CATALOG"

            dbConnect()
            cmd = New SqlCommand(strSQL, cn)
            cmd.CommandType = CommandType.Text
            rdr = cmd.ExecuteReader()

            Do While rdr.Read()
                'Add basic student information
                Dim aCatalog As New clsCatalog
                aCatalog.catalogId = rdr("CATALOG_ID")
                aCatalog.year = rdr("YEAR")
                aCatalog.quarter = rdr("QUARTER")
                aCatalog.courseId = rdr("COURSE_ID")
                aCatalog.profId = rdr("PROF_ID")

                catalogList.Add(aCatalog)
            Loop
        Catch ex As SqlException
            dbAddError(ex.Message)
        Catch ex As Exception
            dbAddError(ex.Message)
        End Try
        Return catalogList
    End Function

    Public Shared Function insertCatalog(aCatalog As clsCatalog) As Integer
        'Result that will be returned
        Dim intResult As Integer = 0

        'DB variables
        Dim cmd As New SqlCommand
        Dim strCatalogSQL As String

        'clear the errors
        strError = ""

        'insert into database
        Try
            dbConnect()
            strCatalogSQL = "INSERT INTO CPP_CATALOG (CATALOG_ID, YEAR, QUARTER, COURSE_ID, PROF_ID) " &
                            "values('" & aCatalog.catalogId & "','" & aCatalog.year & "','" & aCatalog.quarter & "','" & aCatalog.courseId & "', '" &
                            aCatalog.profId & "')"

            cmd.Connection = cn
            cmd.CommandText = strCatalogSQL
            cmd.ExecuteNonQuery()

            dbClose()
        Catch ex As Exception
            dbAddError("Insert Failed " & vbCrLf & ex.Message)
        Finally
            cmd.Dispose()
        End Try

        Return intResult
    End Function

    Public Shared Function deleteCatalog(strCatalogID As String) As Integer
        'Result that will be returned
        Dim intResult As Integer = 0

        'DB variables
        Dim cmd As New SqlCommand
        Dim strSQL As String

        'Clear errors
        strError = ""

        'Delete from database
        Try
            strSQL = "Delete from CPP_CATALOG where CATALOG_ID = '" & strCatalogID & "'"

            dbConnect()
            cmd.Connection = cn
            cmd.CommandText = strSQL

            intResult = cmd.ExecuteNonQuery()

            If (intResult < 1) Then
                dbAddError("DELETE Failed, Catalog id " & strCatalogID & " was not found!")
            End If

            dbClose()
        Catch ex As Exception
            dbAddError("DELETE Failed " & vbCrLf & ex.Message)
        Finally
            cmd.Dispose()
        End Try

        Return intResult
    End Function

    Public Shared Sub updateCatalog(aCatalog As clsCatalog)
        strError = ""

        deleteCatalog(aCatalog.catalogId)
        insertCatalog(aCatalog)

        If strError <> "" Then
            strError = "Could not Update catalog" & aCatalog.catalogId & vbCrLf & vbCrLf & strError
        End If
    End Sub

    Public Shared Function findCatalog(strCatalogID As String) As clsCatalog
        'catalog that will be returned
        Dim aCatalog As clsCatalog = New clsCatalog

        'db variables
        Dim cmd As SqlCommand
        Dim rdr As SqlDataReader
        Dim strSQL As String

        'clear error
        strError = ""

        Try
            Dim MyData As New ArrayList

            strSQL = "Select * From CPP_CATALOG Where CATALOG_ID = '" & strCatalogID & "'"
            cmd = New SqlCommand(strSQL, cn)
            cmd.CommandType = CommandType.Text

            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                aCatalog.catalogId = rdr("CATALOG_ID")
                aCatalog.year = rdr("YEAR")
                aCatalog.quarter = rdr("QUARTER")
                aCatalog.courseId = rdr("COURSE_ID")
                aCatalog.profId = rdr("PROF_ID")

            Else
                dbAddError("Catalog not found")
            End If

        Catch ex As SqlException
            dbAddError(ex.Message)
        Catch ex As Exception
            dbAddError(ex.Message)
        End Try

        Return aCatalog
    End Function


    Public Shared Function loadCourses() As List(Of clsCourse)
        'List of courses that will be returned
        Dim courseList As New List(Of clsCourse)

        'DB variables
        Dim strSQL As String
        Dim cmd As SqlCommand
        Dim rdr As SqlDataReader

        'clear the errors
        strError = ""

        Try
            strSQL = "SELECT * FROM CPP_COURSES"

            dbConnect()
            cmd = New SqlCommand(strSQL, cn)
            cmd.CommandType = CommandType.Text
            rdr = cmd.ExecuteReader()

            Do While rdr.Read()
                'Add basic student information
                Dim aCourse As New clsCourse
                aCourse.courseId = rdr("COURSE_ID")
                aCourse.description = rdr("DESCRIPTION")
                aCourse.units = rdr("UNITS")

                courseList.Add(aCourse)
            Loop
        Catch ex As SqlException
            dbAddError(ex.Message)
        Catch ex As Exception
            dbAddError(ex.Message)
        End Try
        Return courseList
    End Function

    Public Shared Function insertCourse(aCourse As clsCourse) As Integer
        'Result that will be returned
        Dim intResult As Integer = 0

        'DB variables
        Dim cmd As New SqlCommand
        Dim strCourseSQL As String

        'clear the errors
        strError = ""

        'insert into database
        Try
            dbConnect()
            strCourseSQL = "INSERT INTO CPP_COURSES (COURSE_ID, DESCRIPTION, UNITS) " &
                            "values('" & aCourse.courseId & "','" & aCourse.description & "','" & aCourse.units & "')"

            cmd.Connection = cn
            cmd.CommandText = strCourseSQL
            cmd.ExecuteNonQuery()

            dbClose()
        Catch ex As Exception
            dbAddError("Insert Failed " & vbCrLf & ex.Message)
        Finally
            cmd.Dispose()
        End Try

        Return intResult
    End Function

    Public Shared Function deleteCourse(strCourseID As String) As Integer
        'Result that will be returned
        Dim intResult As Integer = 0

        'DB variables
        Dim cmd As New SqlCommand
        Dim strSQL As String

        'Clear errors
        strError = ""

        'Delete from database
        Try
            strSQL = "Delete from CPP_COURSES where COURSE_ID = '" & strCourseID & "'"

            dbConnect()
            cmd.Connection = cn
            cmd.CommandText = strSQL

            intResult = cmd.ExecuteNonQuery()

            If (intResult < 1) Then
                dbAddError("DELETE Failed, Course id " & strCourseID & " was not found!")
            End If

            dbClose()
        Catch ex As Exception
            dbAddError("DELETE Failed " & vbCrLf & ex.Message)
        Finally
            cmd.Dispose()
        End Try

        Return intResult
    End Function

    Public Shared Sub updateCourse(aCourse As clsCourse)
        strError = ""

        deleteCourse(aCourse.courseId)
        insertCourse(aCourse)

        If strError <> "" Then
            strError = "Could not Update course " & aCourse.courseId & vbCrLf & vbCrLf & strError
        End If
    End Sub

    Public Shared Function findCourse(strCourseID As String) As clsCourse
        'catalog that will be returned
        Dim aCourse As clsCourse = New clsCourse
        'db variables
        Dim cmd As SqlCommand
        Dim rdr As SqlDataReader
        Dim strSQL As String

        'clear error
        strError = ""

        Try
            Dim MyData As New ArrayList

            strSQL = "Select * From CPP_COURSES Where COURSE_ID = '" & strCourseID & "'"
            cmd = New SqlCommand(strSQL, cn)
            cmd.CommandType = CommandType.Text

            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                aCourse.courseId = rdr("COURSE_ID")
                aCourse.description = rdr("DESCRIPTION")
                aCourse.units = rdr("UNITS")

            Else
                dbAddError("Catalog not found")
            End If

        Catch ex As SqlException
            dbAddError(ex.Message)
        Catch ex As Exception
            dbAddError(ex.Message)
        End Try

        Return aCourse
    End Function


    Public Shared Sub dbOpen()
        'Only assign one reference to the connection
        If IsNothing(cn) Then
            cn = New SqlConnection
            cn.ConnectionString = "Data Source=DESKTOP-O996Q45\SQLEXPRESS;Initial Catalog=CPPDATA;Integrated Security=True"
        End If
    End Sub

    Public Shared Sub dbConnect()
        'Only open if connection is closed
        If cn.State = ConnectionState.Closed Then
        End If
        cn.Open()
    End Sub

    Public Shared Sub dbClose()
        'Only close if open
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If
    End Sub

    Private Shared Sub dbAddError(ByVal s As String)
        'build error
        If strError = "" Then
            strError = s
        Else
            strError += vbCrLf & s
        End If
    End Sub

    Public Shared Function dbGetError() As String
        'return error
        Return strError
    End Function
End Class
