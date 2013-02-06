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
Imports DotNetNuke.Modules.Blog.Components.Common
Imports DotNetNuke.Modules.Blog.Components.Rss

Partial Public Class BlogFeed
 Inherits BlogModuleBase

#Region "Event Handlers"

 Private Sub Page_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
  Response.Clear()
  Response.ClearContent()
  Response.ContentType = "application/xml"

  Dim feed As New BlogRssFeed(ModuleConfiguration, Request, RssView)
  Dim xmlfeed As String = Nothing
  Try
   xmlfeed = CStr(DotNetNuke.Common.Utilities.DataCache.GetCache(feed.CacheKey))
  Catch
  End Try
  If xmlfeed Is Nothing Then
   xmlfeed = feed.WriteRssToString()
   DotNetNuke.Common.Utilities.DataCache.SetCache(feed.CacheKey, xmlfeed, TimeSpan.FromMinutes(Entities.Host.Host.PerformanceSetting * 20))
  End If

  Response.Write(xmlfeed)

  Response.End()
 End Sub

#End Region

End Class