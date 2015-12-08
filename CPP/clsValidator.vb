Public Class clsValidator
    Public Shared Sub validateStudent(aStudent As clsStudent)
        Dim util As New RegexUtilities()
        Dim err As String = ""

        If (String.IsNullOrEmpty(aStudent.broncoID)) Then
            err += "Bronco ID is required." & vbCrLf
        End If

        If (String.IsNullOrEmpty(aStudent.firstName)) Then
            err += "First Name is required." & vbCrLf
        End If

        If (String.IsNullOrEmpty(aStudent.lastName)) Then
            err += "Last Name is required." & vbCrLf
        End If

        If (String.IsNullOrEmpty(aStudent.phone)) Then
            err += "Phone Number is required." & vbCrLf
        End If

        If (aStudent.phone.Length <> 10) Then
            err += "Invalid phone number." & vbCrLf
        End If

        If (String.IsNullOrEmpty(aStudent.email)) Then
            err += "Email address is required." & vbCrLf
        Else
            If util.IsValidEmail(aStudent.email) Then
                Console.WriteLine("Valid: " & aStudent.email)
            Else
                err += "Invalid email address."
            End If
        End If

        If err.Length > 0 Then
            Throw New Exception(err)
        End If
    End Sub

    Public Shared Sub validateInstructor(anInstructor As clsInstructor)
        Dim err As String = ""

        If (String.IsNullOrEmpty(anInstructor.profId)) Then
            err += "Professor ID is required." & vbCrLf
        End If

        If (String.IsNullOrEmpty(anInstructor.firstName)) Then
            err += "First Name is required." & vbCrLf
        End If

        If (String.IsNullOrEmpty(anInstructor.lastName)) Then
            err += "Last name is required." & vbCrLf
        End If

        If (String.IsNullOrEmpty(anInstructor.phone)) Then
            err += "Phone number is required." & vbCrLf
        End If

        If (String.IsNullOrEmpty(anInstructor.dept)) Then
            err += "Department is required." & vbCrLf
        End If

        If err.Length > 0 Then
            Throw New Exception(err)
        End If
    End Sub

    Public Shared Sub validateEnrollment(anEnrollment As clsEnrollment)
        Dim err As String = ""

        If (String.IsNullOrEmpty(anEnrollment.broncoId)) Then
            err += "Bronco ID is required." & vbCrLf
        End If

        If (String.IsNullOrEmpty(anEnrollment.catalogId)) Then
            err += "Catalog ID is required"
        End If

        If err.Length > 0 Then
            Throw New Exception(err)
        End If
    End Sub

    Public Shared Sub validateCatalog(aCatalog As clsCatalog)
        Dim err As String = ""

        If (String.IsNullOrEmpty(aCatalog.catalogId)) Then
            err += "Catalog ID is required." & vbCrLf
        End If

        If (aCatalog.year = 0) Then
            err += "Year is required." & vbCrLf
        End If

        If (String.IsNullOrEmpty(aCatalog.quarter)) Then
            err += "Quarter is required." & vbCrLf
        End If

        If (String.IsNullOrEmpty(aCatalog.courseId)) Then
            err += "Course ID is required." & vbCrLf
        End If

        If (String.IsNullOrEmpty(aCatalog.profId)) Then
            err += "Professor ID is required." & vbCrLf
        End If

        If err.Length > 0 Then
            Throw New Exception(err)
        End If
    End Sub

    Public Shared Sub validateCourses(aCourse As clsCourse)
        Dim err As String = ""

        If (String.IsNullOrEmpty(aCourse.courseId)) Then
            err += "Course ID is required." & vbCrLf
        End If

        If (String.IsNullOrEmpty(aCourse.description)) Then
            err += "Desctiption is required." & vbCrLf
        End If

        If (aCourse.units = 0) Then
            err += "Units is required."
        End If

        If err.Length > 0 Then
            Throw New Exception(err)
        End If
    End Sub
End Class
