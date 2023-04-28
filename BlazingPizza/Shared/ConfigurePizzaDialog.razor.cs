using Microsoft.AspNetCore.Components;

namespace BlazingPizza.Shared;

public partial class ConfigurePizzaDialog
{
    [Parameter] public Pizza Pizza { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnConfirm { get; set; }
}
