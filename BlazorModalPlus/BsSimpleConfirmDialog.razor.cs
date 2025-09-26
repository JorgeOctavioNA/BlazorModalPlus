namespace BlazorModalPlus;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

public partial class BsSimpleConfirmDialog
{
    /// <summary>
    /// Event callback for the dialog result
    /// </summary>
    [Parameter]
    public EventCallback<DialogButtonResult> ConfirmCallback { get; set; }

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

    private async Task ButtonClick(DialogButtonResult dialogButton)
    {
        try
        {
            await HideDialog();
            
            if (ConfirmCallback.HasDelegate)
            {
                await ConfirmCallback.InvokeAsync(dialogButton);
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
    /// Show the Modal dialog with the specified message and title and buttons configuration
    /// </summary>
    /// <param name="message">Message string</param>
    /// <param name="title">The title on top of the dialog</param>
    /// <param name="buttonsConfiguration">Combination of buttons</param>
    /// <exception cref="ArgumentException">Thrown when message is null or empty</exception>
    /// <exception cref="NotImplementedException">Thrown when an unsupported button configuration is used</exception>
    public async Task ShowDialog(string message, string? title = null, DialogButtons buttonsConfiguration = DialogButtons.Ok)
    {
        ValidateMessage(message);

        Message = message;
        Title = title ?? (Localizer?["ConfirmString"] ?? "Confirm");
        Buttons = GetButtons(buttonsConfiguration);

        await ShowDialogInternal();
    }

    /// <summary>
    /// Setup the buttons for the dialog, corresponding to the DialogButtons enum
    /// </summary>
    /// <param name="dialogButtons">The button configuration to use</param>
    /// <returns>Collection of ButtonItem objects</returns>
    /// <exception cref="NotImplementedException">Thrown when an unsupported button configuration is used</exception>
    protected IEnumerable<ButtonItem> GetButtons(DialogButtons dialogButtons)
    {
        return dialogButtons switch
        {
            DialogButtons.Ok => new List<ButtonItem>
            {
                new ButtonItem(Localizer?["ButtonOk"] ?? "OK", BtnRenderStyle.Primary, DialogButtonResult.Ok, null)
            },
            DialogButtons.OkCancel => new List<ButtonItem>
            {
                new ButtonItem(Localizer?["ButtonOk"] ?? "OK", BtnRenderStyle.Primary, DialogButtonResult.Ok, null),
                new ButtonItem(Localizer?["ButtonCancel"] ?? "Cancel", BtnRenderStyle.Secondary, DialogButtonResult.Cancel, null)
            },
            DialogButtons.YesNo => new List<ButtonItem>
            {
                new ButtonItem(Localizer?["ButtonYes"] ?? "Yes", BtnRenderStyle.Primary, DialogButtonResult.Yes, null),
                new ButtonItem(Localizer?["ButtonNo"] ?? "No", BtnRenderStyle.Secondary, DialogButtonResult.No, null)
            },
            DialogButtons.YesNoCancel => new List<ButtonItem>
            {
                new ButtonItem(Localizer?["ButtonYes"] ?? "Yes", BtnRenderStyle.Primary, DialogButtonResult.Yes, null),
                new ButtonItem(Localizer?["ButtonNo"] ?? "No", BtnRenderStyle.Primary, DialogButtonResult.No, null),
                new ButtonItem(Localizer?["ButtonCancel"] ?? "Cancel", BtnRenderStyle.Secondary, DialogButtonResult.Cancel, null)
            },
            _ => throw new NotImplementedException($"DialogButtons configuration '{dialogButtons}' is not supported.")
        };
    }
}
