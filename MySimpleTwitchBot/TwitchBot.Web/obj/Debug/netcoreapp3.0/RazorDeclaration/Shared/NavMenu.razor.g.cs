#pragma checksum "C:\Users\gabri\Documents\Visual Studio 2019\Repos\TwitchBot\MySimpleTwitchBot\TwitchBot.Web\Shared\NavMenu.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fc539aa7c207993a0dc3123958c8422c3374333a"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace TwitchBot.Web.Shared
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
    public class NavMenu : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 28 "C:\Users\gabri\Documents\Visual Studio 2019\Repos\TwitchBot\MySimpleTwitchBot\TwitchBot.Web\Shared\NavMenu.razor"
       
    bool collapseNavMenu = true;

    string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
