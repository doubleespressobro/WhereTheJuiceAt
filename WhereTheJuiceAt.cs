using ExileCore;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.Elements.InventoryElements;
using ExileCore.Shared.Enums;
using SharpDX;
using System.Linq;
using Vector2 = System.Numerics.Vector2;

namespace WhereTheJuiceAt;

public class WhereTheJuiceAt : BaseSettingsPlugin<WhereTheJuiceAtSettings>
{
    public override bool Initialise()
    {
        Name = "Where The Juice At";
        return true;
    }

    public override void Render()
    {
        var mapDeviceWindow = GameController.Game.IngameState.IngameUi.MapDeviceWindow;
        if (mapDeviceWindow == null)
            return;

        if (!mapDeviceWindow.IsVisible)
            return;

        var items = mapDeviceWindow.Items;
        if (items.FindAll(x => IsScarab(x)).Count == 0)
            return;

        var playerInventory = GameController.Game.IngameState.ServerData.PlayerInventories.First(x => x.Inventory.InventType == (InventoryTypeE)28);
        if(playerInventory == null)
            return;

        if (playerInventory.Inventory.Items.Where(x => x.GetComponent<Mods>()?.ItemMods.Count >= 1).Count() == 4)
            return;

        var activateButtonCenter = mapDeviceWindow.ActivateButton.GetClientRectCache.Center;

        var msg = $"OI DICKHEAD YOU'RE MISSING COMPASS(ES)";
        var size = Graphics.MeasureText(msg);
        Graphics.DrawBox(new RectangleF(activateButtonCenter.X - size.X / 2 - 5, activateButtonCenter.Y - size.Y / 2 - 5, size.X + 10, size.Y + 10), Color.Black);
        Graphics.DrawText(msg, new Vector2(activateButtonCenter.X - size.X / 2, activateButtonCenter.Y - size.Y / 2), Color.Red);  
    }

    private bool IsScarab(NormalInventoryItem item)
    {
        var baseItemType = GameController.Files.BaseItemTypes.Translate(item.Item.Path);
        return baseItemType.ClassName == "MapFragment" && baseItemType.BaseName.EndsWith(" Scarab");
    }
}