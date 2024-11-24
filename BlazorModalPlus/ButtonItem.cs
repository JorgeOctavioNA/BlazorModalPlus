using Microsoft.AspNetCore.Components;

namespace BlazorModalPlus
{
    public class ButtonItem(string caption, BtnRenderStyle btnRenderStyle, DialogButtonResult? dialogResult, EventCallback? click)
    {
        public string Caption { get; init; } = caption;
        public BtnRenderStyle RenderStyle { get; init; } = btnRenderStyle;
        public DialogButtonResult? DialogResult { get; init; } = dialogResult;
        public EventCallback? Click { get; init; } = click;
    }
}
