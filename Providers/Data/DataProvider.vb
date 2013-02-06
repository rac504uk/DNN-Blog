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

Namespace Providers.Data

 Public MustInherit Class DataProvider

#Region "Shared/Static Methods"

  ' singleton reference to the instantiated object 
  Private Shared objProvider As DataProvider = Nothing

  ' constructor
  Shared Sub New()
   CreateProvider()
  End Sub

  ' dynamically create provider
  Private Shared Sub CreateProvider()
   objProvider = CType(Framework.Reflection.CreateObject("data", "DotNetNuke.Modules.Blog.Providers.Data", ""), DataProvider)
  End Sub

  ' return the provider
  Public Shared Shadows Function Instance() As DataProvider
   Return objProvider
  End Function

#End Region

#Region "Abstract Methods"

#Region " Blogs Methods "

  Public MustOverride Function GetBlog(ByVal blogID As Integer) As IDataReader
  Public MustOverride Function GetBlogsByPortal(ByVal portalId As Integer) As IDataReader

  Public MustOverride Function AddBlog(ByVal PortalID As Integer, ByVal ParentBlogID As Integer, ByVal userID As Integer, ByVal title As String, ByVal description As String, ByVal [Public] As Boolean, ByVal allowComments As Boolean, ByVal allowAnonymous As Boolean, ByVal ShowFullName As Boolean, ByVal syndicated As Boolean, ByVal SyndicateIndependant As Boolean, ByVal SyndicationURL As String, ByVal SyndicationEmail As String, ByVal EmailNotification As Boolean, ByVal AllowTrackbacks As Boolean, ByVal AutoTrackback As Boolean, ByVal MustApproveComments As Boolean, ByVal MustApproveAnonymous As Boolean, ByVal MustApproveTrackbacks As Boolean, ByVal UseCaptcha As Boolean, ByVal EnableGhostWriter As Integer) As Integer

  Public MustOverride Sub UpdateBlog(ByVal PortalID As Integer, ByVal blogID As Integer, ByVal ParentBlogID As Integer, ByVal userID As Integer, ByVal title As String, ByVal description As String, ByVal [Public] As Boolean, ByVal allowComments As Boolean, ByVal allowAnonymous As Boolean, ByVal ShowFullName As Boolean, ByVal syndicated As Boolean, ByVal SyndicateIndependant As Boolean, ByVal SyndicationURL As String, ByVal SyndicationEmail As String, ByVal EmailNotification As Boolean, ByVal AllowTrackbacks As Boolean, ByVal AutoTrackback As Boolean, ByVal MustApproveComments As Boolean, ByVal MustApproveAnonymous As Boolean, ByVal MustApproveTrackbacks As Boolean, ByVal UseCaptcha As Boolean, ByVal EnableGhostWriter As Integer)
  Public MustOverride Sub DeleteBlog(ByVal blogID As Integer, ByVal portalId As Integer)

#End Region

#Region " Entries Methods "

  Public MustOverride Function GetEntry(ByVal EntryID As Integer, ByVal PortalId As Integer) As IDataReader
  Public MustOverride Function GetEntries(ByVal PortalID As Integer, ByVal BlogID As Integer, ByVal BlogDate As Date, Optional ByVal ShowNonPublic As Boolean = False, Optional ByVal ShowNonPublished As Boolean = False) As IDataReader
  Public MustOverride Function AddEntry(ByVal blogID As Integer, ByVal title As String, ByVal description As String, ByVal Entry As String, ByVal Published As Boolean, ByVal AllowComments As Boolean, ByVal AddedDate As DateTime, ByVal DisplayCopyright As Boolean, ByVal Copyright As String, ByVal PermaLink As String, ByVal CreatedUserId As Integer) As Integer
  Public MustOverride Sub UpdateEntry(ByVal EntryID As Integer, ByVal blogID As Integer, ByVal title As String, ByVal description As String, ByVal Entry As String, ByVal Published As Boolean, ByVal AllowComments As Boolean, ByVal AddedDate As DateTime, ByVal DisplayCopyright As Boolean, ByVal Copyright As String, ByVal PermaLink As String, ByVal contentItemId As Integer)
  Public MustOverride Sub UpdateEntryViewCount(ByVal EntryID As Integer)
  Public MustOverride Sub DeleteEntry(ByVal EntryID As Integer)
  Public MustOverride Function GetEntriesByBlog(ByVal BlogID As Integer, ByVal BlogDate As Date, ByVal PageSize As Integer, ByVal CurrentPage As Integer, Optional ByVal ShowNonPublic As Boolean = False, Optional ByVal ShowNonPublished As Boolean = False) As IDataReader
  Public MustOverride Function GetAllEntriesByBlog(ByVal BlogID As Integer) As IDataReader
  Public MustOverride Function GetEntriesByPortal(ByVal PortalID As Integer, ByVal BlogDate As Date, ByVal BlogDateType As String, ByVal PageSize As Integer, ByVal CurrentPage As Integer, Optional ByVal ShowNonPublic As Boolean = False, Optional ByVal ShowNonPublished As Boolean = False) As IDataReader
  Public MustOverride Function GetAllEntriesByPortal(ByVal PortalID As Integer, Optional ByVal ShowNonPublic As Boolean = False, Optional ByVal ShowNonPublished As Boolean = False) As IDataReader
  Public MustOverride Function GetEntriesByTerm(ByVal portalId As Integer, ByVal BlogDate As Date, ByVal termId As Integer, ByVal pageSize As Integer, ByVal currentPage As Integer, Optional ByVal ShowNonPublic As Boolean = False, Optional ByVal ShowNonPublished As Boolean = False) As IDataReader

#End Region

#Region " Blog Comments Methods "

  Public MustOverride Function GetComment(ByVal commentID As Integer) As IDataReader
  Public MustOverride Function GetCommentsByEntry(ByVal EntryID As Integer, ByVal ShowNonApproved As Boolean) As IDataReader
  Public MustOverride Function GetCommentsByBlog(ByVal BlogID As Integer, ByVal ShowNonApproved As Boolean, ByVal MaximumComments As Integer) As IDataReader
  Public MustOverride Function GetCommentsByPortal(ByVal PortalID As Integer, ByVal ShowNonApproved As Boolean, ByVal MaximumComments As Integer) As IDataReader
  Public MustOverride Function AddComment(ByVal EntryID As Integer, ByVal UserID As Integer, ByVal Title As String, ByVal comment As String, ByVal Author As String, ByVal Approved As Boolean, ByVal Website As String, ByVal Email As String, ByVal AddedDate As DateTime) As Integer
  Public MustOverride Sub UpdateComment(ByVal commentID As Integer, ByVal EntryID As Integer, ByVal userID As Integer, ByVal Title As String, ByVal comment As String, ByVal Author As String, ByVal Approved As Boolean, ByVal Website As String, ByVal Email As String, ByVal AddedDate As DateTime)
  Public MustOverride Sub DeleteComment(ByVal commentID As Integer)
  Public MustOverride Sub DeleteAllUnapproved(ByVal EntryID As Integer)

#End Region

#Region " Archive Methods "

  Public MustOverride Function GetBlogMonths(ByVal PortalID As Integer, ByVal BlogID As Integer) As IDataReader
  Public MustOverride Function GetBlogDaysForMonth(ByVal PortalID As Integer, ByVal BlogID As Integer, ByVal BlogDate As Date) As IDataReader

#End Region

#Region " Search Methods "

  Public MustOverride Function SearchByKeyWordAndPortal(ByVal PortalID As Integer, ByVal SearchString As String, ByVal ShowNonPublic As Boolean, ByVal ShowNonPublished As Boolean) As IDataReader
  Public MustOverride Function SearchByKeyWordAndBlog(ByVal BlogID As Integer, ByVal SearchString As String, ByVal ShowNonPublic As Boolean, ByVal ShowNonPublished As Boolean) As IDataReader
  Public MustOverride Function SearchByPhraseAndPortal(ByVal PortalID As Integer, ByVal SearchString As String, ByVal ShowNonPublic As Boolean, ByVal ShowNonPublished As Boolean) As IDataReader
  Public MustOverride Function SearchByPhraseAndBlog(ByVal BlogID As Integer, ByVal SearchString As String, ByVal ShowNonPublic As Boolean, ByVal ShowNonPublished As Boolean) As IDataReader

#End Region

#Region " Settings Methods "

  Public MustOverride Function GetSettings(ByVal PortalID As Integer, ByVal TabID As Integer) As IDataReader
  Public MustOverride Sub UpdateSetting(ByVal PortalID As Integer, ByVal TabID As Integer, ByVal Key As String, ByVal Value As String)
  Public MustOverride Function GetBlogViewEntryModuleID(ByVal tabID As Integer) As Integer

#End Region

#Region " Upgrade Methods "

  Public MustOverride Function GetCategoriesByEntry(ByVal EntryID As Integer) As IDataReader
  Public MustOverride Function GetTagsByEntry(ByVal EntryID As Integer) As IDataReader
  Public MustOverride Function GetAllTagsForUpgrade() As IDataReader
  Public MustOverride Function GetAllCategoriesForUpgrade() As IDataReader
  Public MustOverride Function RetrieveTaxonomyRelatedPosts() As IDataReader
        Public MustOverride Function GetAllBlogsForUpgrade() As IDataReader

#End Region

#Region "Terms"

  Public MustOverride Function GetTermsByContentType(ByVal portalId As Integer, ByVal contentTypeId As Integer, ByVal vocabularyId As Integer) As IDataReader

  Public MustOverride Function GetTermsByContentItem(ByVal contentItemId As Integer, ByVal vocabularyId As Integer) As IDataReader

#End Region

#End Region

 End Class
End Namespace