using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BlazorModalPlus;
public partial class BsConfirmDialog : BsConfirmDialogBase
{
    /// <summary>
    /// Fragment content (if necessary) for the Header of the Modal Dialog
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; } = null;

    /// <summary>
    /// Fragment content (if necessary) for the Footer of the Modal Dialog
    /// </summary>
    [Parameter]
    public RenderFragment? FooterTemplate { get; set; } = null;

    /// <summary>
    /// Size of the dialog
    /// </summary>
    [Parameter]
    public DialogSize Size { get; set; } = DialogSize.Medium;

    /// <summary>
    /// Custom CSS classes for the dialog
    /// </summary>
    [Parameter]
    public string? CustomCssClass { get; set; }

    [Inject]
    private IStringLocalizer<Language>? Localizer { get; set; }

    private async Task ButtonClick(EventCallback eventCallback)
    {
        try
        {
            await HideDialog();
            
            if (eventCallback.HasDelegate)
            {
                await eventCallback.InvokeAsync();
            }
        }
        catch (Exception)
        {
            // Log error if needed, but don't break the UI
            // Consider adding ILogger injection for proper error logging
        }
    }

    private string GetDialogSizeClass()
    {
        var sizeClass = Size switch
        {
            DialogSize.Small => "modal-sm",
            DialogSize.Large => "modal-lg",
            DialogSize.ExtraLarge => "modal-xl",
            DialogSize.FullScreen => "modal-fullscreen",
            _ => string.Empty
        };

        return string.IsNullOrEmpty(CustomCssClass) ? sizeClass : $"{sizeClass} {CustomCssClass}";
    }

    /// <summary>
    /// Shows the dialog with the specified message, title and buttons.
    /// </summary>
    /// <param name="message">The message for Modal Dialog</param>
    /// <param name="title">Text for the title at the top of the modal dialog. Ignored when HeaderTemplate is defined </param>
    /// <param name="buttons">Array of <see cref="ButtonItem" /></param>
    /// <exception cref="ArgumentException">Thrown when message is null or empty</exception>
    public async Task ShowDialog(string message, string? title = null, IEnumerable<ButtonItem>? buttons = null)
    {
        ValidateMessage(message);

        Message = message;
        Title = title ?? (Localizer?["ConfirmString"] ?? "Confirm");
        Buttons = buttons ?? Enumerable.Empty<ButtonItem>();

        await ShowDialogInternal();
    }
}
