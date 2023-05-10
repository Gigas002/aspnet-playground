using Microsoft.AspNetCore.Components;

namespace blazormal.Client.Shared;

public partial class DescriptionInfo : ComponentBase
{
    [Parameter]
    public string Descr { get; set; }
}
