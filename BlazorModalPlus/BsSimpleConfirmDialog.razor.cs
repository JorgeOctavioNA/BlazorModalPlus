namespace BlazorModalPlus;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

public partial class BsSimpleConfirmDialog
{
    /// <summary>
    /// Event callback for the dialog result
    /// </summary>
    [Parameter]
    public EventCallback<DialogButtonResult> ConfirmCallback { get; set; }

    [Parameter]
    public DialogSize Size { get; set; } = DialogSize.Medium;

    [Inject]
    public IServiceProvider serviceProvider { get; set; }

    private IStringLocalizer<Language>? localizer;

    protected override void OnInitialized()
    {
        localizer = (IStringLocalizer<Language>?)serviceProvider.GetService(typeof(IStringLocalizer<Language>));
    }

    private async Task ButtonClick(DialogButtonResult dialogButton)
    {
        Visible = false;
        await ConfirmCallback.InvokeAsync(dialogButton);
    }

    private string GetDialogSizeClass()
    {
        return Size switch
        {
            DialogSize.Small => "modal-sm",
            DialogSize.Large => "modal-lg",
            _ => ""
        };
    }

    /// <summary>
    /// Show the Modal dialog with the specified message and title and buttons configuration
    /// </summary>
    /// <param name="message">Message string</param>
    /// <param name="title">The title on top of the dialog</param>
    /// <param name="butonsConfiguration">Combination of buttons</param>
    /// <returns></returns>
    public async Task ShowDialog(string message, string? title, DialogButtons butonsConfiguration)
    {
        Message = message;
        Title = title ?? (localizer?["ConfirmString"] ?? "Confirm");
        Buttons = GetButtons(butonsConfiguration);

        // Render the dialog
        Visible = true;

        StateHasChanged();
        await Task.CompletedTask;
    }

    /// <summary>
    /// Setup the buttons for the dialog, corresponding to the DialogButtons enum
    /// </summary>
    /// <param name="dialogButtons"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected IEnumerable<ButtonItem> GetButtons(DialogButtons dialogButtons)
    {
        return dialogButtons switch
        {
            DialogButtons.Ok => new List<ButtonItem>
            {
                new ButtonItem(localizer?["ButtonOk"] ?? "OK", BtnRenderStyle.Primary, DialogButtonResult.Ok, null)
            },
            DialogButtons.OkCancel => new List<ButtonItem>
            {
                new ButtonItem(localizer?["ButtonOk"] ?? "OK", BtnRenderStyle.Primary, DialogButtonResult.Ok, null),
                new ButtonItem(localizer?["ButtonCancel"] ?? "Cancel", BtnRenderStyle.Secondary, DialogButtonResult.Cancel, null)
            },
            DialogButtons.YesNo => new List<ButtonItem>
            {
                new ButtonItem(localizer?["ButtonYes"] ?? "Yes", BtnRenderStyle.Primary, DialogButtonResult.Yes, null),
                new ButtonItem(localizer?["ButtonNo"] ?? "No", BtnRenderStyle.Secondary, DialogButtonResult.No, null)
            },
            DialogButtons.YesNoCancel => new List<ButtonItem>
            {
                new ButtonItem(localizer?["ButtonYes"] ?? "Yes", BtnRenderStyle.Primary, DialogButtonResult.Yes, null),
                new ButtonItem(localizer?["ButtonNo"] ?? "No", BtnRenderStyle.Primary, DialogButtonResult.No, null),
                new ButtonItem(localizer?["ButtonCancel"] ?? "Cancel", BtnRenderStyle.Secondary, DialogButtonResult.Cancel, null)
            },
            _ => throw new NotImplementedException()
        };
    }
}
