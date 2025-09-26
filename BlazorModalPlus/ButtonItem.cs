using Microsoft.AspNetCore.Components;

namespace BlazorModalPlus
{
    /// <summary>
    /// Represents a button item for modal dialogs
    /// </summary>
    public class ButtonItem
    {
        /// <summary>
        /// The text to display on the button
        /// </summary>
        public string Caption { get; init; }
        
        /// <summary>
        /// The visual style of the button
        /// </summary>
        public BtnRenderStyle RenderStyle { get; init; }
        
        /// <summary>
        /// The result value associated with this button (for simple dialogs)
        /// </summary>
        public DialogButtonResult? DialogResult { get; init; }
        
        /// <summary>
        /// The click event handler for this button
        /// </summary>
        public EventCallback? Click { get; init; }
        
        /// <summary>
        /// Optional CSS class for an icon to display in the button
        /// </summary>
        public string? IconClass { get; init; }
        
        /// <summary>
        /// Optional CSS classes to apply to the button
        /// </summary>
        public string? CssClass { get; init; }
        
        /// <summary>
        /// Whether the button is disabled
        /// </summary>
        public bool IsDisabled { get; init; }

        /// <summary>
        /// Creates a new ButtonItem instance
        /// </summary>
        /// <param name="caption">The text to display on the button</param>
        /// <param name="renderStyle">The visual style of the button</param>
        /// <param name="dialogResult">The result value associated with this button</param>
        /// <param name="click">The click event handler</param>
        /// <param name="iconClass">Optional CSS class for an icon</param>
        /// <param name="cssClass">Optional additional CSS classes</param>
        /// <param name="isDisabled">Whether the button should be disabled</param>
        /// <exception cref="ArgumentException">Thrown when caption is null or empty</exception>
        public ButtonItem(string caption, BtnRenderStyle renderStyle, DialogButtonResult? dialogResult = null, 
                         EventCallback? click = null, string? iconClass = null, string? cssClass = null, bool isDisabled = false)
        {
            if (string.IsNullOrWhiteSpace(caption))
                throw new ArgumentException("Button caption cannot be null or empty.", nameof(caption));

            Caption = caption;
            RenderStyle = renderStyle;
            DialogResult = dialogResult;
            Click = click;
            IconClass = iconClass;
            CssClass = cssClass;
            IsDisabled = isDisabled;
        }
    }
}
