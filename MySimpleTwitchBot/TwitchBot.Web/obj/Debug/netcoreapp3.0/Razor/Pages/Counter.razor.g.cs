#pragma checksum "C:\Users\gabri\Documents\Visual Studio 2019\Repos\TwitchBot\MySimpleTwitchBot\TwitchBot.Web\Pages\Counter.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2cb13ecdab24bf3ea8479b8c2d07c9e70a17817e"
// <auto-generated/>
#pragma warning disable 1591
namespace TwitchBot.Web.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\gabri\Documents\Visual Studio 2019\Repos\TwitchBot\MySimpleTwitchBot\TwitchBot.Web\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\gabri\Documents\Visual Studio 2019\Repos\TwitchBot\MySimpleTwitchBot\TwitchBot.Web\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\gabri\Documents\Visual Studio 2019\Repos\TwitchBot\MySimpleTwitchBot\TwitchBot.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\gabri\Documents\Visual Studio 2019\Repos\TwitchBot\MySimpleTwitchBot\TwitchBot.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\gabri\Documents\Visual Studio 2019\Repos\TwitchBot\MySimpleTwitchBot\TwitchBot.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\gabri\Documents\Visual Studio 2019\Repos\TwitchBot\MySimpleTwitchBot\TwitchBot.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\gabri\Documents\Visual Studio 2019\Repos\TwitchBot\MySimpleTwitchBot\TwitchBot.Web\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\gabri\Documents\Visual Studio 2019\Repos\TwitchBot\MySimpleTwitchBot\TwitchBot.Web\_Imports.razor"
using TwitchBot.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\gabri\Documents\Visual Studio 2019\Repos\TwitchBot\MySimpleTwitchBot\TwitchBot.Web\_Imports.razor"
using TwitchBot.Web.Shared;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/counter")]
    public class Counter : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Counter</h1>\r\n\r\n");
            __builder.OpenElement(1, "p");
            __builder.AddContent(2, "Current count: ");
            __builder.AddContent(3, 
#nullable restore
#line 5 "C:\Users\gabri\Documents\Visual Studio 2019\Repos\TwitchBot\MySimpleTwitchBot\TwitchBot.Web\Pages\Counter.razor"
                   currentCount

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(4, "\r\n\r\n");
            __builder.OpenElement(5, "button");
            __builder.AddAttribute(6, "class", "btn btn-primary");
            __builder.AddAttribute(7, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 7 "C:\Users\gabri\Documents\Visual Studio 2019\Repos\TwitchBot\MySimpleTwitchBot\TwitchBot.Web\Pages\Counter.razor"
                                          IncrementCount

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(8, "Click me");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 9 "C:\Users\gabri\Documents\Visual Studio 2019\Repos\TwitchBot\MySimpleTwitchBot\TwitchBot.Web\Pages\Counter.razor"
       
    int currentCount = 0;

    void IncrementCount()
    {
        currentCount++;
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
