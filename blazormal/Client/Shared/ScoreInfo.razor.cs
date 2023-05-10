using Microsoft.AspNetCore.Components;

namespace blazormal.Client.Shared;

public partial class ScoreInfo : ComponentBase
{
    [Parameter]
    public int Score { get; set; }
}
