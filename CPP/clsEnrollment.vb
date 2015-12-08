Public Class clsEnrollment
    Private strBroncoId As String
    Private strCatalogId As String

    Public Property broncoId As String
        Get
            Return strBroncoId
        End Get
        Set(value As String)
            strBroncoId = value
        End Set
    End Property

    Public Property catalogId As String
        Get
            Return strCatalogId
        End Get
        Set(value As String)
            strCatalogId = value
        End Set
    End Property
End Class
