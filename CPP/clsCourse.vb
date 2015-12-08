Public Class clsCourse
    Private strCourseId As String
    Private strDescription As String
    Private intUnits As Int16

    Public Property courseId As String
        Get
            Return strCourseId
        End Get
        Set(value As String)
            strCourseId = value
        End Set
    End Property

    Public Property description As String
        Get
            Return strDescription
        End Get
        Set(value As String)
            strDescription = value
        End Set
    End Property

    Public Property units As Int16
        Get
            Return intUnits
        End Get
        Set(value As Int16)
            intUnits = value
        End Set
    End Property
End Class
