#pragma checksum "D:\CLASS NOTES\CSIT 6th SEM\By myself\dotnetcore--quiz-portal-master\QuizPortal\Views\Shared\Error.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3c6d2ecdb88e20eecc9a4717bfe26b047ecdbafc0d64b7114bc58b436aaf1e42"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCoreGeneratedDocument.Views_Shared_Error), @"mvc.1.0.view", @"/Views/Shared/Error.cshtml")]
namespace AspNetCoreGeneratedDocument
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\CLASS NOTES\CSIT 6th SEM\By myself\dotnetcore--quiz-portal-master\QuizPortal\Views\_ViewImports.cshtml"
using QuizPortal

#nullable disable
    ;
#nullable restore
#line 2 "D:\CLASS NOTES\CSIT 6th SEM\By myself\dotnetcore--quiz-portal-master\QuizPortal\Views\_ViewImports.cshtml"
using QuizPortal.Models

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"3c6d2ecdb88e20eecc9a4717bfe26b047ecdbafc0d64b7114bc58b436aaf1e42", @"/Views/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"9323c03f07962c32313aeb5fe16c7b2d507980be1770c5c390c4afc9497fe398", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    internal sealed class Views_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<
#nullable restore
#line 1 "D:\CLASS NOTES\CSIT 6th SEM\By myself\dotnetcore--quiz-portal-master\QuizPortal\Views\Shared\Error.cshtml"
       ErrorViewModel

#line default
#line hidden
#nullable disable
    >
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\CLASS NOTES\CSIT 6th SEM\By myself\dotnetcore--quiz-portal-master\QuizPortal\Views\Shared\Error.cshtml"
  
    ViewData["Title"] = "Error";

#line default
#line hidden
#nullable disable

            WriteLiteral("\n<h1 class=\"text-danger\">Error.</h1>\n<h2 class=\"text-danger\">An error occurred while processing your request.</h2>\n\n");
#nullable restore
#line 9 "D:\CLASS NOTES\CSIT 6th SEM\By myself\dotnetcore--quiz-portal-master\QuizPortal\Views\Shared\Error.cshtml"
 if (Model.ShowRequestId)
{

#line default
#line hidden
#nullable disable

            WriteLiteral("    <p>\n        <strong>Request ID:</strong> <code>");
            Write(
#nullable restore
#line 12 "D:\CLASS NOTES\CSIT 6th SEM\By myself\dotnetcore--quiz-portal-master\QuizPortal\Views\Shared\Error.cshtml"
                                            Model.RequestId

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</code>\n    </p>\n");
#nullable restore
#line 14 "D:\CLASS NOTES\CSIT 6th SEM\By myself\dotnetcore--quiz-portal-master\QuizPortal\Views\Shared\Error.cshtml"
}

#line default
#line hidden
#nullable disable

            WriteLiteral(@"
<h3>Development Mode</h3>
<p>
    Swapping to <strong>Development</strong> environment will display more detailed information about the error that occurred.
</p>
<p>
    <strong>The Development environment shouldn't be enabled for deployed applications.</strong>
    It can result in displaying sensitive information from exceptions to end users.
    For local debugging, enable the <strong>Development</strong> environment by setting the <strong>ASPNETCORE_ENVIRONMENT</strong> environment variable to <strong>Development</strong>
    and restarting the app.
</p>
");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ErrorViewModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
