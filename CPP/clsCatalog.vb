Public Class clsCatalog
    Private strCatalogId As String
    Private intYear As Int16
    Private strQuarter As String
    Private strCourseId As String
    Private strProfId As String

    Public Property catalogId As String
        Get
            Return strCatalogId
        End Get
        Set(value As String)
            strCatalogId = value
        End Set
    End Property

    Public Property year As Int16
        Get
            Return intYear
        End Get
        Set(value As Int16)
            intYear = value
        End Set
    End Property

    Public Property quarter As String
        Get
            Return strQuarter
        End Get
        Set(value As String)
            strQuarter = value
        End Set
    End Property

    Public Property courseId As String
        Get
            Return strCourseId
        End Get
        Set(value As String)
            strCourseId = value
        End Set
    End Property

    Public Property profId As String
        Get
            Return strProfId
        End Get
        Set(value As String)
            strProfId = value
        End Set
    End Property
End Class
