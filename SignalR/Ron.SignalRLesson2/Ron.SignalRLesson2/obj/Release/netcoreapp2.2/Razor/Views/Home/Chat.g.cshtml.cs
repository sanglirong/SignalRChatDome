#pragma checksum "H:\Demo\2019\06\Examples-master\SignalR\Ron.SignalRLesson2\Ron.SignalRLesson2\Views\Home\Chat.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "04d367af17a4d5d1a0b0c3b60f089c861095496b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Chat), @"mvc.1.0.view", @"/Views/Home/Chat.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Chat.cshtml", typeof(AspNetCore.Views_Home_Chat))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "H:\Demo\2019\06\Examples-master\SignalR\Ron.SignalRLesson2\Ron.SignalRLesson2\Views\_ViewImports.cshtml"
using Ron.SignalRLesson2;

#line default
#line hidden
#line 2 "H:\Demo\2019\06\Examples-master\SignalR\Ron.SignalRLesson2\Ron.SignalRLesson2\Views\_ViewImports.cshtml"
using Ron.SignalRLesson2.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"04d367af17a4d5d1a0b0c3b60f089c861095496b", @"/Views/Home/Chat.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"524b79a6543135940f3a8c6afd66c53ad4f0506c", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Chat : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/signalr/dist/browser/signalr.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/wechat.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "H:\Demo\2019\06\Examples-master\SignalR\Ron.SignalRLesson2\Ron.SignalRLesson2\Views\Home\Chat.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
            BeginContext(45, 1018, true);
            WriteLiteral(@"<div class=""text-center"">
    <h1 class=""display-4"">Welcome</h1>
    <p>Learn about <a href=""https://docs.microsoft.com/aspnet/core"">building Web apps with ASP.NET Core</a>.</p>
</div>
<div>
    <div class=""form-group"">
        <h2>群聊操作</h2>
        房间名称：<input type=""text"" id=""txtRoom"" value=""官方房间"" />
        <input type=""button"" id=""btnJoin"" value=""加入房间"" />
        <input type=""button"" id=""btnLeave"" value=""离开房间"" />
        消息：<input type=""text"" id=""groupContent"" class=""form-control"" />
        <input type=""button"" id=""btnSendToGroup"" class=""form-control bg-success"" value=""发送"" />
    </div>
    <div class=""form-group"">
        <h2>单聊操作</h2>
        用户名：<input type=""text"" id=""txtUserName"" value="""" placeholder=""请输入聊天对象的昵称"" />
        消息：<input type=""text"" id=""txtUserContent"" class=""form-control"" />
        <input type=""button"" id=""btnSendToUser"" class=""form-control bg-success"" value=""发送"" />
    </div>
    <div class=""form-group"">
        <h2>全站消息推送</h2>
        <div>
            用户名：");
            EndContext();
            BeginContext(1064, 26, false);
#line 26 "H:\Demo\2019\06\Examples-master\SignalR\Ron.SignalRLesson2\Ron.SignalRLesson2\Views\Home\Chat.cshtml"
           Write(Context.User.Identity.Name);

#line default
#line hidden
            EndContext();
            BeginContext(1090, 69, true);
            WriteLiteral("\r\n            <input type=\"hidden\" id=\"userName\" class=\"form-control\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 1159, "\"", 1194, 1);
#line 27 "H:\Demo\2019\06\Examples-master\SignalR\Ron.SignalRLesson2\Ron.SignalRLesson2\Views\Home\Chat.cshtml"
WriteAttributeValue("", 1167, Context.User.Identity.Name, 1167, 27, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1195, 311, true);
            WriteLiteral(@" />
        </div>
        <div>
            消息：<input type=""text"" id=""content"" class=""form-control"" />
        </div>
        <div>
            <input type=""button"" id=""btnSend"" class=""form-control bg-success"" value=""发送"" />
        </div>
    </div>
</div>
<ul class=""list-group"" id=""msgList""></ul>
");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(1523, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(1529, 84, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "04d367af17a4d5d1a0b0c3b60f089c861095496b7008", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1613, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(1619, 61, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "04d367af17a4d5d1a0b0c3b60f089c861095496b8350", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1680, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
