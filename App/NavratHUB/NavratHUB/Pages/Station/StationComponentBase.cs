using Microsoft.AspNetCore.Components;

namespace NavratHUB.Pages.Station
{
    public abstract class StationComponentBase : ComponentBase
    {
        [Parameter]
        public float Value { get; set; }
    }
}