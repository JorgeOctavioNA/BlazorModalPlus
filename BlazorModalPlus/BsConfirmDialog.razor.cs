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

    [Parameter]
    public DialogSize Size { get; set; } = DialogSize.Medium;

    [Inject]
    public IServiceProvider serviceProvider { get; set; }

    private IStringLocalizer<Language>? localizer;

    protected override void OnInitialized()
    {
        localizer = (IStringLocalizer<Language>?)serviceProvider.GetService(typeof(IStringLocalizer<Language>));
    }

    private async Task ButtonClick(EventCallback eventCallback)
    {
        Visible = false;
        await eventCallback.InvokeAsync();
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
    /// Shows the dialog with the specified message, title and buttons.
    /// </summary>
    /// <param name="message">The message for Modal Dialog</param>
    /// <param name="title">Text for the title at the top of the modal dialog. Ignored when HeaderTemplate is defined </param>
    /// <param name="buttons">Array of <see cref="ButtonItem" /></param>
    public async Task ShowDialog(string message, string? title, IEnumerable<ButtonItem>? buttons)
    {
        Message = message;
        Title = title ?? (localizer?["ConfirmString"] ?? "Confirm");
        Buttons = buttons;

        // Render the dialog
        Visible = true;

        StateHasChanged();
        await Task.CompletedTask;
    }
}
