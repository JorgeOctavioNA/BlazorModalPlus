using Microsoft.AspNetCore.Components;

namespace BlazorModalPlus
{
    public class ButtonItem
    {
        public string Caption { get; init; }
        public BtnRenderStyle RenderStyle { get; init; }
        public DialogButtonResult? DialogResult { get; init; }
        public EventCallback? Click { get; init; }
        public string? IconClass { get; init; } // Nueva propiedad para iconos

        public ButtonItem(string caption, BtnRenderStyle renderStyle, DialogButtonResult? dialogResult, EventCallback? click, string? iconClass = null)
        {
            Caption = caption;
            RenderStyle = renderStyle;
            DialogResult = dialogResult;
            Click = click;
            IconClass = iconClass;
        }
    }
}
