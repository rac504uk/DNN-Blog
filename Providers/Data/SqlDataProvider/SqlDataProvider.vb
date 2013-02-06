'
' DotNetNuke� - http://www.dotnetnuke.com
' Copyright (c) 2002-2012
' by DotNetNuke Corporation
'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'
Imports System
Imports System.Data
Imports Microsoft.ApplicationBlocks.Data
Imports DotNetNuke.Common.Utilities

Namespace Providers.Data

 Public Class SqlDataProvider

  Inherits DataProvider

#Region " Private Members "

  Private Const ProviderType As String = "data"
  Private Const ModuleQualifier As String = "Blog_"
  Private _providerConfiguration As Framework.Providers.ProviderConfiguration = Framework.Providers.ProviderConfiguration.GetProviderConfiguration(ProviderType)
  Private _connectionString As String
  Private _providerPath As String
  Private _objectQualifier As String
  Private _databaseOwner As String

#End Region

#Region " Constructors "
  Public Sub New()

   Dim objProvider As Framework.Providers.Provider = CType(_providerConfiguration.Providers(_providerConfiguration.DefaultProvider), Framework.Providers.Provider)

   'DR - 01/15/2009
   'BLG-9133 Updated to remove reference to appsettings connection string.
   _connectionString = Config.GetConnectionString()

   If _connectionString = "" Then
    ' Use connection string specified in provider
    _connectionString = objProvider.Attributes("connectionString")
   End If

   _providerPath = objProvider.Attributes("providerPath")

   _objectQualifier = objProvider.Attributes("objectQualifier")
   If _objectQualifier <> "" And _objectQualifier.EndsWith("_") = False Then
    _objectQualifier += "_"
   End If

   _databaseOwner = objProvider.Attributes("databaseOwner")
   If _databaseOwner <> "" And _databaseOwner.EndsWith(".") = False Then
    _databaseOwner += "."
   End If

  End Sub
#End Region

#Region " Properties "
  Public ReadOnly Property ConnectionString() As String
   Get
    Return _connectionString
   End Get
  End Property

  Public ReadOnly Property ProviderPath() As String
   Get
    Return _providerPath
   End Get
  End Property

  Public ReadOnly Property ObjectQualifier() As String
   Get
    Return _objectQualifier
   End Get
  End Property

  Public ReadOnly Property DatabaseOwner() As String
   Get
    Return _databaseOwner
   End Get
  End Property
#End Region

#Region " Private Methods "

  Private Function GetNull(ByVal Field As Object) As Object
   Return Null.GetNull(Field, DBNull.Value)
  End Function

  Private Function GetFullyQualifiedName(name As String) As String
   Return DatabaseOwner + ObjectQualifier + ModuleQualifier + name
  End Function

#End Region

#Region " Blogs Methods "

  Public Overrides Function GetBlog(ByVal blogID As Integer) As IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetBlog", blogID), IDataReader)
  End Function

  Public Overrides Function GetBlogsByPortal(ByVal portalId As Integer) As IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetBlogsByPortal", portalId), IDataReader)
  End Function

  Public Overrides Function AddBlog(ByVal PortalID As Integer, ByVal ParentBlogID As Integer, ByVal userID As Integer, ByVal title As String, ByVal description As String, ByVal [Public] As Boolean, ByVal allowComments As Boolean, ByVal allowAnonymous As Boolean, ByVal ShowFullName As Boolean, ByVal syndicated As Boolean, ByVal SyndicateIndependant As Boolean, ByVal SyndicationURL As String, ByVal SyndicationEmail As String, ByVal EmailNotification As Boolean, ByVal AllowTrackbacks As Boolean, ByVal AutoTrackback As Boolean, ByVal MustApproveComments As Boolean, ByVal MustApproveAnonymous As Boolean, ByVal MustApproveTrackbacks As Boolean, ByVal UseCaptcha As Boolean, ByVal EnableGhostWriter As Integer) As Integer
   Return CType(SqlHelper.ExecuteScalar(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "AddBlog", PortalID, ParentBlogID, userID, title, Null.GetNull(description, DBNull.Value), [Public], allowComments, allowAnonymous, ShowFullName, syndicated, SyndicateIndependant, SyndicationURL, SyndicationEmail, EmailNotification, AllowTrackbacks, AutoTrackback, MustApproveComments, MustApproveAnonymous, MustApproveTrackbacks, UseCaptcha, EnableGhostWriter), Integer)
  End Function

  Public Overrides Sub UpdateBlog(ByVal PortalID As Integer, ByVal blogID As Integer, ByVal ParentBlogID As Integer, ByVal userID As Integer, ByVal title As String, ByVal description As String, ByVal [Public] As Boolean, ByVal allowComments As Boolean, ByVal allowAnonymous As Boolean, ByVal ShowFullName As Boolean, ByVal syndicated As Boolean, ByVal SyndicateIndependant As Boolean, ByVal SyndicationURL As String, ByVal SyndicationEmail As String, ByVal EmailNotification As Boolean, ByVal AllowTrackbacks As Boolean, ByVal AutoTrackback As Boolean, ByVal MustApproveComments As Boolean, ByVal MustApproveAnonymous As Boolean, ByVal MustApproveTrackbacks As Boolean, ByVal UseCaptcha As Boolean, ByVal EnableGhostWriter As Integer)
   SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "UpdateBlog", PortalID, blogID, ParentBlogID, userID, title, Null.GetNull(description, DBNull.Value), [Public], allowComments, allowAnonymous, ShowFullName, syndicated, SyndicateIndependant, SyndicationURL, SyndicationEmail, EmailNotification, AllowTrackbacks, AutoTrackback, MustApproveComments, MustApproveAnonymous, MustApproveTrackbacks, UseCaptcha, EnableGhostWriter)
  End Sub

  Public Overrides Sub DeleteBlog(ByVal blogID As Integer, ByVal portalId As Integer)
   SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "DeleteBlog", blogID, portalId)
  End Sub

#End Region

#Region " Entries Methods "
  ' Entries
  Public Overrides Function GetEntry(ByVal EntryID As Integer, ByVal PortalId As Integer) As IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetEntry", EntryID, PortalId), IDataReader)
  End Function

  Public Overrides Function GetEntries(ByVal PortalID As Integer, ByVal BlogID As Integer, ByVal BlogDate As Date, Optional ByVal ShowNonPublic As Boolean = False, Optional ByVal ShowNonPublished As Boolean = False) As IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetEntries", PortalID, BlogID, Null.GetNull(BlogDate, DBNull.Value), ShowNonPublic, ShowNonPublished), IDataReader)
  End Function

  Public Overrides Function GetEntriesByBlog(ByVal BlogID As Integer, ByVal BlogDate As Date, ByVal PageSize As Integer, ByVal CurrentPage As Integer, Optional ByVal ShowNonPublic As Boolean = False, Optional ByVal ShowNonPublished As Boolean = False) As System.Data.IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetEntriesByBlog", BlogID, Null.GetNull(BlogDate, DBNull.Value), PageSize, CurrentPage, ShowNonPublic, ShowNonPublished), IDataReader)
  End Function

        Public Overrides Function GetAllEntriesByBlog(ByVal BlogID As Integer) As IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetAllEntriesByBlog", BlogID), IDataReader)
        End Function

        Public Overrides Function GetEntriesByPortal(ByVal PortalID As Integer, ByVal BlogDate As Date, ByVal BlogDateType As String, ByVal PageSize As Integer, ByVal CurrentPage As Integer, Optional ByVal ShowNonPublic As Boolean = False, Optional ByVal ShowNonPublished As Boolean = False) As IDataReader

            Dim sproc As String = ""
            Select Case BlogDateType
                Case Nothing
                    sproc = "GetEntriesByPortal"
                Case "day"
                    sproc = "GetEntriesByDay"
                Case "month"
                    sproc = "GetEntriesByMonth"
            End Select

            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & sproc, PortalID, Null.GetNull(BlogDate, DBNull.Value), PageSize, CurrentPage, ShowNonPublic, ShowNonPublished), IDataReader)
        End Function


        Public Overrides Function GetAllEntriesByPortal(ByVal PortalID As Integer, Optional ByVal ShowNonPublic As Boolean = False, Optional ByVal ShowNonPublished As Boolean = False) As IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetAllEntriesByPortal", PortalID, ShowNonPublic, ShowNonPublished), IDataReader)
        End Function

  Public Overrides Function GetEntriesByTerm(ByVal portalId As Integer, ByVal BlogDate As Date, ByVal termId As Integer, ByVal pageSize As Integer, ByVal currentPage As Integer, Optional ByVal ShowNonPublic As Boolean = False, Optional ByVal ShowNonPublished As Boolean = False) As System.Data.IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetEntriesByTerm", portalId, BlogDate, termId, pageSize, currentPage, ShowNonPublic, ShowNonPublished), IDataReader)
  End Function

  Public Overrides Function AddEntry(ByVal blogID As Integer, ByVal title As String, ByVal description As String, ByVal Entry As String, ByVal Published As Boolean, ByVal AllowComments As Boolean, ByVal AddedDate As DateTime, ByVal DisplayCopyright As Boolean, ByVal Copyright As String, ByVal PermaLink As String, ByVal CreatedUserId As Integer) As Integer
   Return CType(SqlHelper.ExecuteScalar(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "AddEntry", blogID, title, Null.GetNull(description, DBNull.Value), Null.GetNull(Entry, DBNull.Value), Published, AllowComments, AddedDate, DisplayCopyright, Null.GetNull(Copyright, DBNull.Value), Null.GetNull(PermaLink, DBNull.Value), CreatedUserId), Integer)
  End Function

  Public Overrides Sub UpdateEntry(ByVal BlogID As Integer, ByVal EntryID As Integer, ByVal Title As String, ByVal Description As String, ByVal Entry As String, ByVal Published As Boolean, ByVal AllowComments As Boolean, ByVal AddedDate As DateTime, ByVal DisplayCopyright As Boolean, ByVal Copyright As String, ByVal PermaLink As String, ByVal contentItemId As Integer)
   SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "UpdateEntry", BlogID, EntryID, Title, Null.GetNull(Description, DBNull.Value), Entry, Published, AllowComments, AddedDate, DisplayCopyright, Null.GetNull(Copyright, DBNull.Value), Null.GetNull(PermaLink, DBNull.Value), Null.GetNull(contentItemId, DBNull.Value))
  End Sub

  Public Overrides Sub UpdateEntryViewCount(ByVal EntryId As Integer)
   SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "UpdateEntryViewCount", EntryId)
  End Sub

  Public Overrides Sub DeleteEntry(ByVal EntryID As Integer)
   SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "DeleteEntry", EntryID)
  End Sub

#End Region

#Region " Comments Methods "
  Public Overrides Function GetComment(ByVal commentID As Integer) As IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetComment", commentID), IDataReader)
  End Function

  Public Overrides Function GetCommentsByEntry(ByVal EntryID As Integer, ByVal ShowNonApproved As Boolean) As IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetCommentsByEntry", EntryID, ShowNonApproved), IDataReader)
  End Function

  Public Overrides Function GetCommentsByBlog(ByVal BlogID As Integer, ByVal ShowNonApproved As Boolean, ByVal MaximumComments As Integer) As IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetCommentsByBlog", BlogID, ShowNonApproved, MaximumComments), IDataReader)
  End Function

  Public Overrides Function GetCommentsByPortal(ByVal PortalID As Integer, ByVal ShowNonApproved As Boolean, ByVal MaximumComments As Integer) As IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetCommentsByPortal", PortalID, ShowNonApproved, MaximumComments), IDataReader)
  End Function

  Public Overrides Function AddComment(ByVal EntryID As Integer, ByVal userID As Integer, ByVal Title As String, ByVal comment As String, ByVal Author As String, ByVal Approved As Boolean, ByVal Website As String, ByVal Email As String, ByVal AddedDate As DateTime) As Integer
   Return CType(SqlHelper.ExecuteScalar(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "AddComment", EntryID, Null.GetNull(userID, DBNull.Value), Title, comment, Null.GetNull(Author, DBNull.Value), Approved, Null.GetNull(Website, DBNull.Value), Null.GetNull(Email, DBNull.Value), Null.GetNull(AddedDate, DBNull.Value)), Integer)
  End Function

  Public Overrides Sub UpdateComment(ByVal commentID As Integer, ByVal EntryID As Integer, ByVal userID As Integer, ByVal Title As String, ByVal comment As String, ByVal Author As String, ByVal Approved As Boolean, ByVal Website As String, ByVal Email As String, ByVal AddedDate As DateTime)
   SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "UpdateComment", commentID, EntryID, Null.GetNull(userID, DBNull.Value), Title, comment, Null.GetNull(Author, DBNull.Value), Approved, Null.GetNull(Website, DBNull.Value), Null.GetNull(Email, DBNull.Value), Null.GetNull(AddedDate, DBNull.Value))
  End Sub

  Public Overrides Sub DeleteComment(ByVal commentID As Integer)
   SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "DeleteComment", commentID)
  End Sub

  Public Overrides Sub DeleteAllUnapproved(ByVal EntryID As Integer)
   SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "DelUnAppCommByEntry", EntryID)
  End Sub

#End Region

#Region " Archive Methods "
  Public Overrides Function GetBlogMonths(ByVal PortalID As Integer, ByVal BlogID As Integer) As IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetBlogMonths", PortalID, BlogID), IDataReader)
  End Function

  Public Overrides Function GetBlogDaysForMonth(ByVal PortalID As Integer, ByVal BlogID As Integer, ByVal BlogDate As Date) As IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetBlogDaysForMonth", PortalID, BlogID, BlogDate), IDataReader)
  End Function
#End Region

#Region " Settings Methods "
  Public Overrides Function GetSettings(ByVal PortalID As Integer, ByVal TabID As Integer) As IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetSettings", PortalID, TabID), IDataReader)
  End Function

  Public Overrides Sub UpdateSetting(ByVal PortalID As Integer, ByVal TabID As Integer, ByVal Key As String, ByVal Value As String)
   SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "UpdateSetting", PortalID, TabID, Key, Value)
  End Sub

  Public Overrides Function GetBlogViewEntryModuleID(ByVal TabID As Integer) As Integer
   Return CType(SqlHelper.ExecuteScalar(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "GetBlogViewEntryModuleID", TabID), Integer)
  End Function
#End Region

#Region " Search Methods "
  Public Overrides Function SearchByKeyWordAndPortal(ByVal PortalID As Integer, ByVal SearchString As String, ByVal ShowNonPublic As Boolean, ByVal ShowNonPublished As Boolean) As IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "SearchByKeywordAndPortal", PortalID, SearchString, ShowNonPublic, ShowNonPublished), IDataReader)
  End Function

  Public Overrides Function SearchByKeyWordAndBlog(ByVal BlogID As Integer, ByVal SearchString As String, ByVal ShowNonPublic As Boolean, ByVal ShowNonPublished As Boolean) As IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "SearchByKeywordAndBlog", BlogID, SearchString, ShowNonPublic, ShowNonPublished), IDataReader)
  End Function

  Public Overrides Function SearchByPhraseAndPortal(ByVal PortalID As Integer, ByVal SearchString As String, ByVal ShowNonPublic As Boolean, ByVal ShowNonPublished As Boolean) As IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "SearchByPhraseAndPortal", PortalID, SearchString, ShowNonPublic, ShowNonPublished), IDataReader)
  End Function

  Public Overrides Function SearchByPhraseAndBlog(ByVal BlogID As Integer, ByVal SearchString As String, ByVal ShowNonPublic As Boolean, ByVal ShowNonPublished As Boolean) As IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "SearchByPhraseAndBlog", BlogID, SearchString, ShowNonPublic, ShowNonPublished), IDataReader)
  End Function
#End Region

#Region " Upgrade Methods "

  Public Overrides Function GetCategoriesByEntry(ByVal EntryID As Integer) As System.Data.IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "Upgrade_GetCategoriesByEntry", EntryID), IDataReader)
  End Function

  Public Overrides Function GetTagsByEntry(ByVal EntryID As Integer) As System.Data.IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "Upgrade_GetTagsByEntry", EntryID), IDataReader)
  End Function

  Public Overrides Function GetAllTagsForUpgrade() As System.Data.IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "Upgrade_TagsGet"), IDataReader)
  End Function

  Public Overrides Function GetAllCategoriesForUpgrade() As System.Data.IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "Upgrade_CategoriesGet"), IDataReader)
  End Function

  Public Overrides Function RetrieveTaxonomyRelatedPosts() As System.Data.IDataReader
   Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "Upgrade_RetrieveTaxonomyEntries"), IDataReader)
  End Function

        Public Overrides Function GetAllBlogsForUpgrade() As System.Data.IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & ModuleQualifier & "Upgrade_BlogsGetAll"), IDataReader)
        End Function

#End Region

#Region " Terms "

  Public Overrides Function GetTermsByContentType(portalId As Integer, contentTypeId As Integer, vocabularyId As Integer) As IDataReader
   Return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetTermsByContentType"), portalId, contentTypeId, vocabularyId)
  End Function

  Public Overrides Function GetTermsByContentItem(contentItemId As Integer, vocabularyId As Integer) As IDataReader
   Return SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetTermsByContentItem"), contentItemId, vocabularyId)
  End Function

#End Region

    End Class

End Namespace