using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;

namespace WhereTheJuiceAt;

public class WhereTheJuiceAtSettings : ISettings
{
    public ToggleNode Enable { get; set; } = new ToggleNode(false);

    //why would you need settings? setting things like minimum compass count is overrated
}